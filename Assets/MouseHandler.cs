using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHandler : MonoBehaviour
{
    public GameObject CubePrefab;
    public GameObject ConveyorPrefab;
    public float TileSize = 1;
    private GameObject paintPrefab;
    private GameObject paintGhost;
    private float currentRotation = 0;
    private WorldController worldController;

    // Start is called before the first frame update
    void Start()
    {
        this.worldController = GameObject.FindObjectOfType<WorldController>();
        this.paintPrefab = this.ConveyorPrefab;
        this.paintGhost = GameObject.Instantiate(this.paintPrefab, new Vector3(0, this.CubePrefab.GetComponent<MeshFilter>().sharedMesh.bounds.extents.y, 0), Quaternion.identity);
        var ghostRenderer = this.paintGhost.GetComponent<MeshRenderer>();
        var paintObjectColor = ghostRenderer.material.color;
        ghostRenderer.material.color = new Color(paintObjectColor.r, paintObjectColor.g, paintObjectColor.b, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            this.HandleRotate();
        }

        var rawMousePositionOnPlane = GetMousePositionOnPlane();
        var snappedMousePositionOnPlane = new Vector3(Mathf.Round(rawMousePositionOnPlane.x), rawMousePositionOnPlane.y, Mathf.Round(rawMousePositionOnPlane.z));
        this.paintGhost.transform.position = snappedMousePositionOnPlane + new Vector3(0, this.paintGhost.GetComponent<MeshFilter>().sharedMesh.bounds.extents.y, 0);
        this.paintGhost.transform.rotation = Quaternion.AngleAxis(currentRotation, Vector3.up);

        if (Input.GetMouseButtonDown(0))
        {
            this.HandleClick();
        }

        if (Input.GetMouseButtonDown(1))
        {
            this.HandleRightClick();
        }
    }

    private void HandleRotate()
    {
        this.currentRotation = currentRotation >= 270 ? 0 : currentRotation + 90;
    }

    private void HandleClick()
    {
        var obj = GameObject.Instantiate(this.paintPrefab, this.paintGhost.transform.position, this.paintGhost.transform.rotation);
        this.worldController.AddObject(obj);
        var objectScripts = obj.GetComponents<ObjectScript>();
        foreach (var script in objectScripts)
        {
            script.Activated = true;
        }
    }

    private void HandleRightClick()
    {
        var resourcePosition = this.paintGhost.transform.position + new Vector3(0, this.paintGhost.GetComponent<MeshFilter>().sharedMesh.bounds.size.y, 0);
        var resource = GameObject.Instantiate(this.CubePrefab, resourcePosition, this.paintGhost.transform.rotation);
        this.worldController.AddResource(resource);
    }

    private static Vector3 GetMousePositionOnPlane()
    {
        var mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        var ground = GameObject.FindGameObjectWithTag(Tags.Ground);
        var groundPlane = new Plane(ground.GetComponent<MeshFilter>().mesh.normals[0], -ground.transform.position.y);
        if (groundPlane.Raycast(mouseRay, out var rayCollisionDistance))
        {
            return mouseRay.GetPoint(rayCollisionDistance);
        } else
        {
            return new Vector3();
        }
    }
}
