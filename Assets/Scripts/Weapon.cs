using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 武器类
/// </summary>
public class Weapon : Item
{
    public int damage;//增加的伤害
    public Weapon(int _id, string _name, ItemType _itemType, ItemQuality _itemQuality, ItemCareer _itemCareer, string _description, int _buyParice, int _sellPrice, string _sprite,
        int _maxNum, int _damage) : 
        base(_id, _name, _itemType, _itemQuality, _itemCareer, _description, _buyParice, _sellPrice, _sprite, _maxNum)
    {
        this.damage = _damage;
    }
}
