using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option_Item : UIBaseButton
{
    public Text Option_Name;
    public Image BG;
    Product Table;
    bool isEmpty = false;
    protected override void AddMessageListener()
    {
        base.AddMessageListener();

        AddListener(MessageID.OnClick_Option);
        AddListener(MessageID.OnClick_Select);
    }

    protected override void OnMessage(MessageID msgID, object sender, object data)
    {
        base.OnMessage(msgID, sender, data);

        switch (msgID)
        {
            case MessageID.OnClick_Option:
                {
                    var info = data as Table_Gift;

                    if (isEmpty) return;
                    if (info.idx == Table.Idx)
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

                    var info = data as Table_Gift;
                    if(info.idx == Table.Idx)
                    {
                        ChangeProductCount(-1);
                    }
                    BG.color = Color.white;
                }
                break;
        }
    }

    public void SetItem(Product table)
    {
        Table = table;
        Option_Name.text = table.Product_Option + "(" + (table.Remain_Count / table.Person_Per_Count) + ")";
        if (table.Remain_Count < table.Person_Per_Count)
        {
            BG.color = Color.red;
            isEmpty = true;
        }
    }

    void ChangeProductCount(int count)
    {
        Table.Remain_Count += count * Table.Person_Per_Count;
        Option_Name.text = Table.Product_Option + "(" + (Table.Remain_Count / Table.Person_Per_Count) + ")";
        if (Table.Remain_Count < Table.Person_Per_Count)
        {
            BG.color = Color.red;
            isEmpty = true;
        }
    }

    protected override void OnClickImpl()
    {
        base.OnClickImpl();

        if (isEmpty) return;

        SendMessage(MessageID.OnClick_Option, Table);
    }
}
