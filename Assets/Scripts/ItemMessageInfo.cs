using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMessageInfo : MonoBehaviour {

	private Text text;
	void Start () {
        text = GetComponent<Text>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

	private void SetShowText(string str)
	{
		text.text = str;
	}
	public void Show()
	{
		gameObject.SetActive(true);
	}
	public void Hide()
	{
		gameObject.SetActive(false);
	}
    public void SetPosition(Vector3 v)
    {
        transform.position = v;
    }

    /// <summary>
    /// 根据id 将对应物品介绍显示
    /// </summary>
    /// <param name="id"></param>
    public void SetShowInfo(Item item)
    {
        switch (item.itemType)
        {
            case ItemType.Consumables:
                Consumables consumables = item as Consumables;
                SetShowText(string.Format(
@"名称:{0}

种类:{1}

质量:{2}

职业:{3}

描述:{4}

回蓝量:{5}

购买价格:{6}  出售价格:{7}
", consumables.name, consumables.itemType, consumables.itemQuality, consumables.itemCareer, consumables.description, consumables.mp, consumables.buyPrice, consumables.sellPrice)
                    );
                break;
            case ItemType.Equipment:
                Equipment equipment = item as Equipment;
                SetShowText(string.Format(
 @"名称:{0}

种类:{1}

质量:{2}

职业:{3}

描述:{4}

防御力:{5}

敏捷度:{6}

体力:{7}

装备种类:{8}

购买价格:{9}  出售价格:{10}
", equipment.name, equipment.itemType, equipment.itemQuality, equipment.itemCareer, equipment.description, equipment.defensivePower, equipment.agility, equipment.stamina, equipment.equipmentType, equipment.buyPrice, equipment.sellPrice)
                    );
                break;
            case ItemType.Weapon:
                Weapon weapon = item as Weapon;
                SetShowText(string.Format(
@"名称:{0}

种类:{1}

质量:{2}

职业:{3}

描述:{4}

伤害：{5}

购买价格:{6}  出售价格:{7}
", weapon.name, weapon.itemType, weapon.itemQuality, weapon.itemCareer, weapon.description, weapon.damage, weapon.buyPrice, weapon.sellPrice)
                    );
                break;
            default:
                break;
        }

    }
}
