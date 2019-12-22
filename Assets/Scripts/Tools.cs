using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tools {
    /// <summary>
    /// 给button添加事件
    /// </summary>
    /// <param name="button"></param>
    /// <param name="action"></param>
    public  static void AddButEvent(Button button,UnityEngine.Events.UnityAction action)
    {
        if (button == null) return;
        button.onClick.AddListener(action);
    }
	
    /// <summary>
    /// 在子物体寻找对应名字的物体上的组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="parent"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static T FindObjectInChildren<T>(Transform parent,string name) where T: Component
    {
        T t = null;
        T[] ts = parent.GetComponentsInChildren<T>();
        foreach (T item in ts)
        {
            if (item.name == name)
            {
                t = item;
                break;
            }
        }
        return t;
    }



}
public  enum UIType
{
    MainPanel,
    KnapsackPanel,
}
/// <summary>
/// 装备类型
/// </summary>
public enum EquipmentType
{

    Weapon = 0,//武器
    Helmet = 1,//头盔
    Corselet = 2,//盔甲
    //Ring=3,//戒指
    //  Barcer=4,//护臂
    //   Glove=5,//手套
    Cloak = 6,//披风
    // Legguard=7,//护腿
    Trousers = 8,//裤子
    Shose = 9,//鞋子
}
/// <summary>
/// 物品的职业
/// </summary>
public enum ItemCareer
{
    Universal,//通用
    Soldier,//战士
    Master,//法师
}
/// <summary>
/// 物品的种类
/// </summary>
public enum ItemType
{
    Consumables,//消耗品
    Equipment,//装备
    Weapon,//武器
}
/// <summary>
/// 物品的质量
/// </summary>
public enum ItemQuality
{
    Common,//普通
    Advanced,//高级
    Legend,//传说
    Epic,//史诗

}

