using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 背包类型的父类
/// </summary>
public class Inventory : MonoBehaviour {

    public List<Grid> gridsList;//能存储物品的的格子列表


    void Start()
    {
        if (gridsList == null)
            gridsList = new List<Grid>();
     gridsList.AddRange( GetComponentsInChildren<Grid>());
    }
    /// <summary>
    /// 向物品槽添加物品
    /// </summary>
    /// <param name="itemId"></param>
    public bool AddItemToGrid(int itemId)
    {
        Grid grid;
        if (FindSameIdGrid(itemId, out grid))
        {
            Item item = InventoryManger.Instance.GetItemObject(itemId);
            grid.AddItem(item);
            return true;
        }else if (FindEmptyGrid(out grid))
        {
            Item item = InventoryManger.Instance.GetItemObject(itemId);
            grid.AddItem(item);
            return true;
        }
        else
        {
            Debug.Log("无格子可放");
        }

        return false;
    }
    /// <summary>
    /// 向物品槽添加物品
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool AddItemToGrid(Item item)
    {
        Grid grid;
        if (FindSameIdGrid(item.id, out grid))
        { 
            grid.AddItem(item);
            return true;
        }
        else if (FindEmptyGrid(out grid))
        {
            grid.AddItem(item);
            return true;
        }
        else
        {

            Debug.Log("无格子可放");
        }

        return false;
    }


    /// <summary>
    /// 整理 按照id的顺序刷新列表
    /// </summary>
    public void RefreshKnapsack()
    {
        //将格子物品按id排序存储
        if (gridsList.Count == 0) return;
        List<Grid> grids = new List<Grid>();
        for (int i = 0; i < gridsList.Count; i++)
        {
            if (gridsList[i].isEmpty) continue;
            if (grids.Count==0)
            {
                grids.Add(gridsList[i]);
                continue;
            }
            for (int j = 0; j < grids.Count; j++)
            {
                if (gridsList[i].ItemStoredId < grids[0].ItemStoredId)
                {
                    grids.Insert(0, gridsList[i]);
                    break;
                }
                else if (gridsList[i].ItemStoredId >= grids[grids.Count - 1].ItemStoredId)
                {
                    grids.Add(gridsList[i]);
                    break;
                }
                else if (j < grids.Count - 1 && gridsList[i].ItemStoredId >= grids[j].ItemStoredId && gridsList[i].ItemStoredId < grids[j + 1].ItemStoredId)
                {
                    grids.Insert(j + 1, gridsList[i]);
                    break;
                }
            }

        }
        //将格式按照存放的物品id调换位置
        for (int i = 0; i < grids.Count; i++)
        {
            Debug.Log(grids[i].ItemStoredId+"  nums:"+grids[i].CurrentStorageNum+"  "  + grids[i].isEmpty);
            grids[i].transform.SetSiblingIndex(i);
        }
        grids.Clear();
        //防止由于界面的格子顺序与列表中不一致而在添加时显示不是从前往后的顺序添加
        gridsList.Clear();
        gridsList.AddRange(GetComponentsInChildren<Grid>());
    }


    /// <summary>
    /// 找出一个已经存有相同物品的格子 且格子的存储数没达到上限
    /// </summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="grid"></param>
    /// <returns></returns>
    private bool FindSameIdGrid(int id,out Grid grid)
    {
        for (int i = 0; i < gridsList.Count; i++)
        {
            if (gridsList[i].IsEmpty == false && gridsList[i].CurrentStorageNum < gridsList[i].StorageSpaceMax && id == gridsList[i].ItemList[0].id)
            {
                grid= gridsList[i];
                return true;
            }
        }
        grid = null;
        return false;
    }
    /// <summary>
    /// 找出一个空格子
    /// </summary>
    /// <param name="grid">out出找到的格子</param>
    /// <returns>是否找到</returns>
    private bool  FindEmptyGrid(out Grid grid)
    {
        for (int i = 0; i < gridsList.Count; i++)
        {
            if (gridsList[i].IsEmpty)
            {
                grid = gridsList[i];
                return true;
            }
        }
        grid = null;
        return false;
    }




}
