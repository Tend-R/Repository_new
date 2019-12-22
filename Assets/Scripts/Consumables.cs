using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 消耗品类
/// </summary>
public class Consumables: Item
{
    public int hp;//血量
    public int mp;//蓝量

    public Consumables(int _id, string _name, ItemType _itemType, ItemQuality _itemQuality, ItemCareer _itemCareer, string _description, int _buyParice, int _sellPrice, string _sprite,
         int _maxNum,int _hp,int _mp) :
        base(_id, _name, _itemType, _itemQuality, _itemCareer, _description, _buyParice, _sellPrice, _sprite, _maxNum)
    {
        this.hp = _hp;
        this.mp = _mp;
    }
}
