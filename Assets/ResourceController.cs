using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    public float Speed = 1.0f;
    private WorldController worldController;

    // Start is called before the first frame update
    void Start()
    {
        this.worldController = GameObject.FindObjectOfType<WorldController>();
    }

    // Update is called once per frame
    void Update()
    {
        var gameObjectOnTile = this.worldController.GetGameObject(this.gameObject);
        var (x, z) = WorldController.GetIntPosition(this.gameObject);
        var tileCenter = new Vector3(x, this.gameObject.transform.position.y, z);
        if (gameObjectOnTile == null)
        {
            this.worldController.MoveResourceTo(this.gameObject, Vector3.MoveTowards(this.transform.position, tileCenter, this.Speed * Time.deltaTime));
        }
    }
}
