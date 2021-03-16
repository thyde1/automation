using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    private Dictionary<(int, int), GameObject> objects = new Dictionary<(int, int), GameObject>();
    private Dictionary<(int, int), List<GameObject>> resources = new Dictionary<(int, int), List<GameObject>>();

    public void AddObject(GameObject obj)
    {
        var intPosition = GetIntPosition(obj);
        if (this.objects.ContainsKey(intPosition))
        {
            this.objects[intPosition] = obj;
        }
        else
        {
            this.objects.Add(intPosition, obj);
        }
    }

    public void AddResource(GameObject obj)
    {
        var intPosition = GetIntPosition(obj);
        if (!this.resources.ContainsKey(intPosition))
        {
            this.resources.Add(intPosition, new List<GameObject>());
        }

        this.resources[intPosition].Add(obj);
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
