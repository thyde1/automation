using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryController : MonoBehaviour, ObjectScript
{
    public float ProductionSeconds = 1.0f;
    public GameObject producedResource;
    private WorldController worldController;
    private float productionCountdown;

    public bool Activated { get; set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        this.worldController = GameObject.FindObjectOfType<WorldController>();
        this.productionCountdown = this.ProductionSeconds;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.Activated)
        {
            this.productionCountdown -= Time.deltaTime;
            if (this.productionCountdown <= 0)
            {
                this.worldController.AddResource(this.producedResource, this.transform.position + this.transform.forward);
                this.productionCountdown = this.ProductionSeconds;
            }
        }
    }
}
