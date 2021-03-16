using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    private Dictionary<(int, int), GameObject> objects = new Dictionary<(int, int), GameObject>();
    private Dictionary<(int, int), List<GameObject>> resources = new Dictionary<(int, int), List<GameObject>>();

    public GameObject AddObject(GameObject template, Vector3 position, Quaternion rotation)
    {
        var intPosition = GetIntPosition(position);
        if (this.objects.TryGetValue(intPosition, out var existing) && existing != null)
        {
            return null;
        }

        var obj = GameObject.Instantiate(template, position, rotation);
        if (this.objects.ContainsKey(intPosition))
        {
            this.objects[intPosition] = obj;
        }
        else
        {
            this.objects.Add(intPosition, obj);
        }

        return obj;
    }

    public void AddResource(GameObject obj, Vector3 position)
    {
        var intPosition = GetIntPosition(position);
        if (!this.resources.ContainsKey(intPosition))
        {
            this.resources.Add(intPosition, new List<GameObject>());
        }

        if (this.resources[intPosition].Count > 0)
        {
            return;
        }

        var newResource = GameObject.Instantiate(obj, position, Quaternion.identity);
        this.resources[intPosition].Add(newResource);
    }

    public GameObject GetGameObject((int, int) position)
    {
        if (this.objects.TryGetValue(position, out var obj))
        {
            return obj;
        }

        return null;
    }

    public List<GameObject> GetResources((int, int) position)
    {
        if (this.resources.TryGetValue(position, out var resources))
        {
            return resources;
        }

        return new List<GameObject>();
    }

    public List<GameObject> GetResources(GameObject obj)
    {
        return this.GetResources(GetIntPosition(obj));
    }

    public void MoveResourceTo(GameObject resource, Vector3 newPosition)
    {
        var oldIntPosition = GetIntPosition(resource);
        var newIntPosition = GetIntPosition(newPosition);

        if (oldIntPosition != newIntPosition && this.GetResources(newIntPosition).Count > 0)
        {
            return;
        }

        if (oldIntPosition != newIntPosition)
        {
            var oldDictionaryList = this.resources[oldIntPosition];
            oldDictionaryList.Remove(resource);

            if (!this.resources.ContainsKey(newIntPosition))
            {
                this.resources.Add(newIntPosition, new List<GameObject>());
            }

            this.resources[newIntPosition].Add(resource);
        }

        resource.transform.position = newPosition;
    }

    private static (int, int) GetIntPosition(Vector3 position)
    {
        return (Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.z));
    }

    private static (int, int) GetIntPosition(GameObject obj)
    {
        return GetIntPosition(obj.transform.position);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
