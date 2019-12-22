using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knapsack : Inventory {
    private static Knapsack _instance;
    public static Knapsack Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("KnapsackPanel").GetComponent<Knapsack>();
            }
            return _instance;
        }
    }
}
