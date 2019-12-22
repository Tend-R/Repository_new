using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// 物品槽
/// </summary>
public class Grid : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler,IPointerUpHandler {
    private int itemStoredId;
    private int storageSpaceMax;//物品槽能放相同物品的最大数量
    private int currentStorageNum;//当前物品槽中的物品数量
    public  bool isEmpty=true;//物品槽是否为空
    private List<Item> itemList;//存放的物品

    public int StorageSpaceMax
    {
        get
        {
            return storageSpaceMax;
        }
    }

    public int CurrentStorageNum
    {
        get
        {
            return currentStorageNum;
        }
    }

    public bool IsEmpty
    {
        get
        {
            return isEmpty;
        }
    }

    public List<Item> ItemList
    {
        get
        {
            return itemList;
        }
    }

    public int ItemStoredId
    {
        get
        {
            return itemStoredId;
        }
    }

    /// <summary>
    /// 向格子里添加物品
    /// </summary>
    /// <param name="_item"></param>
    public void AddItem(Item _item)
    {
        if (itemList == null)
        {
            itemList = new List<Item>();
        }
        if (isEmpty)
        {
            Debug.Log("aaaaa");
            isEmpty = false;
            storageSpaceMax = _item.maxNum;
            GameObject itemUIGO = Instantiate(Resources.Load<GameObject>("ItemUI"));
            itemUIGO.transform.SetParent(transform, false);
            itemUIGO.transform.localScale = Vector3.one;
            ItemUI itemUI = itemUIGO.GetComponent<ItemUI>();
            itemUI.SetItemShow(_item,1);
            itemList.Add(_item);
            itemStoredId = _item.id;
            currentStorageNum++;
        }else
        if (currentStorageNum<storageSpaceMax)
        {
            ItemUI itemUI = transform.GetComponentInChildren<ItemUI>();
            itemUI.SetTextADD();
            itemList.Add(_item);
            currentStorageNum++;
        }
        else
        {
            Debug.Log("格子类存储数量到达上限");
        }
    }

    /// <summary>
    /// 移除格子里的物品
    /// </summary>
    /// <param name="_item"></param>
    /// <returns></returns>
    public Item RemoveItem(Item _item)
    {
        if (itemList == null||itemList.Count==0)
        {
            Debug.Log("要移除的物品不存在");
            return null;
        }
        if (itemList.Contains(_item))
        {
            currentStorageNum--;
            itemList.Remove(_item);
            transform.GetComponentInChildren<ItemUI>().SetTextReduce();
            if (currentStorageNum==0)
            {
                isEmpty = true;
                itemStoredId = -1;
                storageSpaceMax = 0;
                transform.GetComponentInChildren<ItemUI>().DestorySelf() ;
            }

            return _item;
        }
        else
        {
            Debug.Log("要移除的物品不存在");
            return null;
        }
       
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (gameObject.name.Contains("EquipmentGrid")) return;
        if (!IsEmpty)
        {
            InventoryManger.Instance.BePickGrid = this;
            InventoryManger.Instance.showItemUI.SetItemShow(itemList[0]);
            InventoryManger.Instance.IsPickedItem = true;
            RemoveItem(itemList[0]);
        }
          
    }
    public void OnPointerUp(PointerEventData eventData)
    {
       // throw new System.NotImplementedException();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!IsEmpty)
        {
            InventoryManger.Instance.showInfo.Show();
            InventoryManger.Instance.showInfo.SetShowInfo(itemList[0]);
            InventoryManger.Instance.IsShowItmInfo = true;

        }
       
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!IsEmpty)
        {
            InventoryManger.Instance.showInfo.Hide();
        }
        InventoryManger.Instance.IsShowItmInfo = false;
    }
}
