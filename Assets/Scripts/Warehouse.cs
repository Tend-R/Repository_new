using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warehouse : Inventory {

    private static Warehouse _instance;
    public static Warehouse Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("Warehouse").GetComponent<Warehouse>();
            }
            return _instance;
        }
    }
}
