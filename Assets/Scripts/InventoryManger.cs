using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryManger : MonoBehaviour {
    private static InventoryManger instance;
    public ItemMessageInfo showInfo;
    public ItemUI showItemUI;
    public Dictionary<int, JsonData> jsonDatasDic = new Dictionary<int, JsonData>();//存储读取到的物品信息 按id存储
    private bool isPickedItem = false;
    private bool isShowItmInfo = false;
    private Grid bePickGrid;

    public static InventoryManger Instance
    {
        get
        {
            if (instance==null)
            {
                instance = GameObject.FindObjectOfType(typeof(InventoryManger)) as InventoryManger;
            }
            return instance;
        }
    }

    public bool IsPickedItem
    {
        get
        {
            return isPickedItem;
        }

        set
        {
            isPickedItem = value;
        }
    }

    public bool IsShowItmInfo
    {
        get
        {
            return isShowItmInfo;
        }

        set
        {
            isShowItmInfo = value;
        }
    }

    public Grid BePickGrid
    {
        get
        {
            return bePickGrid;
        }

        set
        {
            bePickGrid = value;
        }
    }

    void Awake()
    {
        instance = this;
        InitJsonMessage();
    }
    void  Start()
    {
        Debug.Log("aaa");
  
    }
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            GetOverUI(GameObject.Find("Canvas"));
        }
#endif
        if (IsPickedItem)
        {
            if (Input.GetMouseButton(0))
            {
                showItemUI.Show();
                showInfo.Hide();
                showItemUI.transform.position = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(0))
            {
                List<GameObject> objects = GetOverUI(GameObject.Find("Canvas"));
                for (int i = 0; i < objects.Count; i++)
                {
                    if (objects[i].name.Contains("Grid"))
                    {
                        //1,是玩家的装备格子
                        if (objects[i].name.Contains("EquipmentGrid"))
                        {
                            EquipmentGrid equipmentGrid = objects[i].GetComponent<EquipmentGrid>();
                            if (showItemUI.currentItem.itemType == ItemType.Weapon && equipmentGrid.equipmentType == EquipmentType.Weapon)
                            {
                                if (equipmentGrid.CurrentItem == null)
                                {
                                    equipmentGrid.AddItem(showItemUI.currentItem);
                                    equipmentGrid.CurrentItem = showItemUI.currentItem;
                                }
                                else//替换掉 替换掉,原来的装备放回背包
                                {
                                    Knapsack.Instance.AddItemToGrid(equipmentGrid.CurrentItem);
                                    equipmentGrid.RemoveItem(equipmentGrid.CurrentItem);
                                    equipmentGrid.AddItem(showItemUI.currentItem);
                                    equipmentGrid.CurrentItem = showItemUI.currentItem;
                                }
                              
                            }
                            else if (showItemUI.currentItem.itemType == ItemType.Equipment && equipmentGrid.equipmentType == (showItemUI.currentItem as Equipment).equipmentType)
                            {
                                if (equipmentGrid.CurrentItem==null)
                                {
                                    equipmentGrid.AddItem(showItemUI.currentItem);
                                    equipmentGrid.CurrentItem = showItemUI.currentItem;
                                }
                                else //替换掉,原来的装备放回背包
                                {
                                    Knapsack.Instance.AddItemToGrid(equipmentGrid.CurrentItem);
                                    equipmentGrid.RemoveItem(equipmentGrid.CurrentItem);
                                    equipmentGrid.AddItem(showItemUI.currentItem);
                                    equipmentGrid.CurrentItem = showItemUI.currentItem;
                                }
                              
                            }
                            else //放回原处
                            {
                              bePickGrid.AddItem(showItemUI.currentItem);
                            }
                           
                        }
                        else
                        {
                            //2,不是玩家装备格子
                            Grid grid = objects[i].GetComponent<Grid>();
                            if (grid.IsEmpty)//格子为空时
                            {
                                grid.AddItem(showItemUI.currentItem);
                            }
                            else if (showItemUI.currentItem.id == grid.ItemStoredId && grid.CurrentStorageNum < grid.StorageSpaceMax)//格子中有物品但未达到该类物品的存储上限
                            {
                                grid.AddItem(showItemUI.currentItem);
                            }
                            else//格子中有物品且达到该类物品的存储上限
                            {
                                //回到背包TODO
                                bePickGrid.AddItem(showItemUI.currentItem);

                            }
                        }

                        break;
                    }
                    else
                    {
                        //出售掉装备 跟新玩家的金币数量
                        //TODO

                    }
                }

                IsPickedItem = false;
            }
           
        }
        else
        {
            showItemUI.Hide();
            BePickGrid = null;
        }
        if (IsPickedItem==false&&IsShowItmInfo)
        {
            showInfo.SetPosition(Input.mousePosition);
        }
        else
        {
            showInfo.Hide();
        }
    }
    /// <summary>
    /// 解析json 存储数据
    /// </summary>
    public void InitJsonMessage()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("ItemJosn");
        if (textAsset==null)
        {
            Debug.Log("null");
            return;
        }
        string jsonString = textAsset.text;
        Debug.Log(jsonString);
        JsonData data = JsonMapper.ToObject(jsonString);
        JsonData data1 = data["ItemJsonList"];
        for (int i = 0; i < data1.Count; i++)
        {
            jsonDatasDic.Add(i+1, data1[i]);
        }
    }

   /// <summary>
   /// 获取对应id物品的精灵图片路径
   /// </summary>
   /// <param name="id"></param>
   /// <returns></returns>
    public string GetSpritePath(int id)
    {
        if (!jsonDatasDic.ContainsKey(id)) return null;
        JsonData data = jsonDatasDic[id];
        return data["spritePath"].ToString();
    }
    /// <summary>
    /// 获取相同装备在同一个物品槽的存放数量
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public int GetSameNum(int id)
    {
        if (!jsonDatasDic.ContainsKey(id)) return 1;
        JsonData data = jsonDatasDic[id];
        return int.Parse( data["maxNum"].ToString());
    }
   

    /// <summary>
    /// 根据id 和json的数据实例化item
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Item GetItemObject(int id)
    {
        Item item = null;
        if (jsonDatasDic.Count==0)
        {
            InitJsonMessage();
        }
        if (!jsonDatasDic.ContainsKey(id)) return null;
        JsonData data = jsonDatasDic[id];
        string name = data["name"].ToString();
        ItemType itemType = (ItemType)System.Enum.Parse(typeof(ItemType), data["itemType"].ToString());
        ItemQuality itemQuality = (ItemQuality)System.Enum.Parse(typeof(ItemQuality), data["itemQuality"].ToString());
        ItemCareer itemCareer = (ItemCareer)System.Enum.Parse(typeof(ItemCareer), data["itemCareer"].ToString());
        string description = data["description"].ToString();
        int buyPrice = int.Parse(data["buyPrice"].ToString());
        int sellPrice = int.Parse(data["sellPrice"].ToString());
        string spritePath = data["spritePath"].ToString();
        int maxNum = int.Parse(data["maxNum"].ToString());
        switch (itemType)
        {
            case ItemType.Consumables:
                int hp = int.Parse(data["hp"].ToString());
                int mp = int.Parse(data["mp"].ToString());
                item = new Consumables(id,name,itemType,itemQuality,itemCareer,description,buyPrice,sellPrice,spritePath,maxNum,hp,mp);
                break;
            case ItemType.Equipment:
                int defensivePower = int.Parse(data["defensivePower"].ToString());
                int agility = int.Parse(data["agility"].ToString());
                int stamina = int.Parse(data["stamina"].ToString());
                EquipmentType equipmentType = (EquipmentType)System.Enum.Parse(typeof(EquipmentType), data["equipmentType"].ToString());
                item = new Equipment(id, name, itemType, itemQuality, itemCareer, description, buyPrice, sellPrice, spritePath, maxNum, defensivePower, agility, stamina, equipmentType);
                break;
            case ItemType.Weapon:
                int damage = int.Parse(data["damage"].ToString());
                item = new Weapon(id, name, itemType, itemQuality, itemCareer, description, buyPrice, sellPrice, spritePath, maxNum, damage);
                break;
            default:
                break;
        }
        return item; 
    }

   
    
/// <summary>
/// 整理背包
/// </summary>
    public void ClearUpKnapsack()
    {
       Knapsack.Instance.RefreshKnapsack();
    }
    /// <summary>
    /// 整理仓库
    /// </summary>
    public void ClearUpWareHouse()
    {
        Warehouse.Instance.RefreshKnapsack(); 
    }

    /// <summary>
    /// 获取鼠标停留处UI
    /// </summary>
    /// <param name="canvas"></param>
    /// <returns></returns>
    public List<GameObject> GetOverUI(GameObject canvas)
    {
        List<GameObject> list = new List<GameObject>();
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        GraphicRaycaster gr = canvas.GetComponent<GraphicRaycaster>();
        List<RaycastResult> results = new List<RaycastResult>();
        gr.Raycast(pointerEventData, results);
        foreach (var item in results)
        {
            list.Add(item.gameObject);
        }
        return list;
    }

    public void Test()//only to test
    {
        int a = Random.Range(1, 10);
        Debug.Log("randon:" + a);
        if (Knapsack.Instance.AddItemToGrid(a))
        {
            Debug.Log("添加成功");
        }

      
    }
    public void Test2()
    {
      //  RefreshKnapsack(gridsList);
    }


}
