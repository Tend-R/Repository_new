using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 装备类
/// </summary>
public class Equipment : Item {
    public int defensivePower;//防御力
    public int agility;//敏捷度
    public int stamina;//体力
    public EquipmentType equipmentType;//装备的类型

    public Equipment(int _id, string _name, ItemType _itemType, ItemQuality _itemQuality, ItemCareer _itemCareer, string _description, int _buyParice, int _sellPrice, string _sprite,int _maxNum,
        int _defensivePower,int _agility,int _stamina,EquipmentType _equipmentType) :
        base( _id,  _name,  _itemType,  _itemQuality,  _itemCareer,  _description,  _buyParice,  _sellPrice,  _sprite, _maxNum)
    {
        this.defensivePower = _defensivePower;
        this.agility = _agility;
        this.stamina = _stamina;
        this.equipmentType = _equipmentType;
    }
}
