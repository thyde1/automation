using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class NumberKeyReader
{
    public static int ReadKey()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            return 1;
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            return 2;
        }

        if (Input.GetKey(KeyCode.Alpha3))
        {
            return 3;
        }

        if (Input.GetKey(KeyCode.Alpha4))
        {
            return 4;
        }

        if (Input.GetKey(KeyCode.Alpha5))
        {
            return 5;
        }

        if (Input.GetKey(KeyCode.Alpha6))
        {
            return 6;
        }

        if (Input.GetKey(KeyCode.Alpha7))
        {
            return 7;
        }

        if (Input.GetKey(KeyCode.Alpha8))
        {
            return 8;
        }

        if (Input.GetKey(KeyCode.Alpha9))
        {
            return 9;
        }

        if (Input.GetKey(KeyCode.Alpha0))
        {
            return 0;
        }

        return -1;
    }
}
