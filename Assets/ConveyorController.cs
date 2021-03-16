using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorController : MonoBehaviour, ObjectScript
{
    public float Speed = 1.0f;
    private WorldController worldController;

    public bool Activated { get; set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        this.worldController = GameObject.FindObjectOfType<WorldController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.Activated)
        {
            var resources = this.worldController.GetResources(this.gameObject).ToArray();
            foreach (var resource in resources)
            {
                this.worldController.MoveResourceTo(resource, resource.transform.position + this.gameObject.transform.forward * this.Speed * Time.deltaTime);
            }
        }
    }
}
