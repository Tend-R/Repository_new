using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 物品类（基类）
/// </summary>
public class Item {

    public int id;//id
    public string name;//name
    public ItemType itemType;//装备类型
    public ItemQuality itemQuality;//装备质量
    public ItemCareer itemCareer;//装备职业
    public string description;//装备描述
    public int buyPrice;//购买价格
    public int sellPrice;//出售价格
    public string spritePath;//精灵地址
    public int maxNum;//在同一个格子中能存储几个这个看了类型的物品

    public Item(int _id,string _name,ItemType _itemType,ItemQuality _itemQuality,ItemCareer _itemCareer, string _description,int _buyParice,int _sellPrice,string _sprite,int _maxNum)
    {
        this.id = _id;
        this.name = _name;
        this.itemType = _itemType;
        this.itemQuality = _itemQuality;
        this.itemCareer = _itemCareer;
        this.description = _description;
        this.buyPrice = _buyParice;
        this.sellPrice = _sellPrice;
        this.spritePath = _sprite;
        this.maxNum = _maxNum;
    }

}
