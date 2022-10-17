using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Option_Item : UIBaseButton
{
    public Text Option_Name;
    public Image BG;
    Product Table = new Product();
    int originRemainCount = 0;
    bool isEmpty = false;
    protected override void AddMessageListener()
    {
        base.AddMessageListener();

        AddListener(MessageID.OnClick_Option);
        AddListener(MessageID.OnClick_Select);
        AddListener(MessageID.OnClick_Select_Success);
        AddListener(MessageID.OnClick_Add_Selected_Product_Count);
        AddListener(MessageID.OnClick_Reduce_Selected_Product_Count);
        AddListener(MessageID.OnClick_Remove);
    }

    protected override void OnMessage(MessageID msgID, object sender, object data)
    {
        base.OnMessage(msgID, sender, data);

        switch (msgID)
        {
            case MessageID.OnClick_Option:
                {
                    var info = data as Product;

                    if (isEmpty) return;
                    if (info.Idx == Table.Idx)
                    {
                        BG.color = new Color(0.5322179f, 0.5322179f, 0.9811321f);
                    }
                    else
                    {
                        BG.color = Color.white;
                    }
                }
                break;
            case MessageID.OnClick_Select:
                {
                    if (isEmpty) return;
                    
                    BG.color = Color.white;
                }
                break;
            case MessageID.OnClick_Select_Success:
            case MessageID.OnClick_Add_Selected_Product_Count:
                {
                    var info = data as Product;
                    if (info.Idx == Table.Idx)
                    {
                        ChangeProductCount(-1);
                    }
                }
                break;            
            case MessageID.OnClick_Reduce_Selected_Product_Count:
                {
                    var info = data as Product;
                    if (info.Idx == Table.Idx)
                    {
                        ChangeProductCount(1);
                    }
                }
                break;
            case MessageID.OnClick_Remove:
                {
                    var info = data as Product;

                    if (info.Idx == Table.Idx)
                    {
                        Table.Remain_Count = originRemainCount;
                        ChangeProductCount(0);
                    }
                }
                break;
        }
    }

    public void SetItem(Product table)
    {
        Table = (Product)table.Clone();
        originRemainCount = Mathf.FloorToInt(table.Remain_Count);
        int Remain_Person = Mathf.FloorToInt(table.Remain_Count / table.Person_Per_Count);
        Option_Name.text = table.Product_Option + "(" + (Remain_Person) + ")";
        if (table.Remain_Count < table.Person_Per_Count)
        {
            BG.color = Color.red;
            isEmpty = true;
        }
    }

    void ChangeProductCount(int count)
    {
        //Remain_Person = Mathf.FloorToInt(Table.Remain_Count / Table.Person_Per_Count) - count;
        Table.Remain_Count += count * Table.Person_Per_Count;
        Option_Name.text = Table.Product_Option + "(" + (Table.Remain_Count / Table.Person_Per_Count) + ")";
        if (Table.Remain_Count / Table.Person_Per_Count <= 0)
        {
            BG.color = Color.red;
            isEmpty = true;
        }
        else
        {
            BG.color = Color.white;
            isEmpty = false;
        }
    }

    protected override void OnClickImpl()
    {
        base.OnClickImpl();

        if (isEmpty) return;

        SendMessage(MessageID.OnClick_Option, Table);
    }
}
