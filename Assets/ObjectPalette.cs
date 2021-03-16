using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ObjectPalette : MonoBehaviour
{
    public GameObject[] Prefabs;

    public GameObject CurrentObject { get; private set; }

    public void Start()
    {
        this.CurrentObject = this.Prefabs[0];
    }

    public void SelectByIndex(int index)
    {
        var number = index == 0 ? 10 : index;
        if (this.Prefabs.Length >= number)
        {
            this.CurrentObject = this.Prefabs[number - 1];
        }
    }
}
