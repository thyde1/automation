using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHandler : MonoBehaviour
{
    public GameObject CubePrefab;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.HandleClick();
        }
    }

    private void HandleClick()
    {
        var mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        var ground = GameObject.FindGameObjectWithTag(Tags.Ground);
        var groundPlane = new Plane(ground.GetComponent<MeshFilter>().mesh.normals[0], -ground.transform.position.y);
        if (groundPlane.Raycast(mouseRay, out var rayCollisionDistance)) {
            // Clicked on plane
            var clickedPosition = mouseRay.GetPoint(rayCollisionDistance);
            GameObject.Instantiate(this.CubePrefab, clickedPosition + new Vector3(0, this.CubePrefab.GetComponent<MeshFilter>().sharedMesh.bounds.extents.y, 0), Quaternion.identity);
        }
    }
}
