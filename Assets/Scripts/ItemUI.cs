using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemUI : MonoBehaviour
{
    private Image itemImage;
    public Text itemNum;
    public int currenNum=0;
    private Vector3 off;
    public Item currentItem;
    public int CurrenNum
    {
        get
        {
            return currenNum;
        }
    }


    void Start()
    {
    }
    void Update()
    {
    }
    public void SetItemShow(Item item,int num=1)
    {
        if (itemImage==null)
        {
            itemImage = GetComponent<Image>();
        }
        itemImage.sprite= Resources.Load<Sprite>(item.spritePath);
        currentItem = item;
        if (itemNum == null)
        {
            itemNum = Tools.FindObjectInChildren<Text>(transform, "numText");
        }
        currenNum = num;
        if (num == 1 || num == 0)
        {
            itemNum.text = "";
        }
        else
        {
            itemNum.text = num.ToString(); ;
        }
    }
	
    public void SetTextADD()
    {
        if (itemNum==null)
        {
            itemNum = Tools.FindObjectInChildren<Text>(transform, "numText");
        }
        currenNum +=1;
        if (currenNum == 1|| currenNum == 0)
        {
            itemNum.text = "";
        }
        else
        {
            itemNum.text = currenNum.ToString(); ;
        }
    }

    public void SetTextReduce()
    {
        if (itemNum == null)
        {
            itemNum = Tools.FindObjectInChildren<Text>(transform, "numText");
        }
        currenNum -= 1;
        if (currenNum == 1 || currenNum == 0)
        {
            itemNum.text = "";
        }
        else
        {
            itemNum.text = currenNum.ToString(); ;
        }
    }
    public void DestorySelf()
    {
        Destroy(gameObject);
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
