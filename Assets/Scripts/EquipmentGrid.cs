using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentGrid : Grid {

    public EquipmentType equipmentType;//格子可以存储的装备类型
    private Item currentItem;//当前存储的装备

    public Item CurrentItem
    {
        get
        {
            return currentItem;
        }

        set
        {
            currentItem = value;
        }
    }


 
}
