using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Product_Item : UIBaseButton
{
    public Text Product_Name;
    public Image BG;
    Product_List.Product Product;
    bool isEmpty = false;

    protected override void AddMessageListener()
    {
        base.AddMessageListener();

        AddListener(MessageID.OnClick_Product);
        AddListener(MessageID.OnClick_Select);
        AddListener(MessageID.OnClick_Confirm);
        AddListener(MessageID.OnClick_Confirm);
        AddListener(MessageID.Event_Update_Remain_Product);
    }

    protected override void OnMessage(MessageID msgID, object sender, object data)
    {
        base.OnMessage(msgID, sender, data);

        switch (msgID)
        {
            case MessageID.OnClick_Product:
                {
                    var info = data as Product_List.Product;
                    if (info.Product_Id == Product.Product_Id)
                    {
                        BG.color = new Color(0.5322179f, 0.5322179f, 0.9811321f);
                    }
                    else
                    {
                        if (isEmpty) return;
                        BG.color = Color.white;
                    }
                }
                break;
            case MessageID.OnClick_Select:
            case MessageID.OnClick_Confirm:
                {
                    if (isEmpty) return;
                    BG.color = Color.white;
                }
                break;
            case MessageID.Event_Update_Remain_Product:
                {
                    UpdateItem();
                    break;
                }
        }
    }

    public void SetItem(Product_List.Product products)
    {
        Product = products;
        int productCount = 0;
        for (int i = 0; i < products.Table.Count; i++)
        {
            productCount += Mathf.FloorToInt(products.Table[i].remain_count) / products.Table[i].person_per_count;
        }
        Product_Name.text = products.Table[0].name;

        if (productCount <= 0)
        {
            isEmpty = true;
            BG.color = Color.red;
        }
    }

    public void UpdateItem()
    {
        var table = Table_Manager.Instance.GetTables<Table_Gift>().FindAll(t => t.product_idx == Product.Product_Id);
        int totalCount = 0;
        for (int i = 0; i < table.Count; i++)
        {
            var count = Mathf.FloorToInt(table[i].remain_count) / table[i].person_per_count;
            totalCount += count;
        }

        if (totalCount <= 0)
        {
            isEmpty = true;
            BG.color = Color.red;
        }
    }

    protected override void OnClickImpl()
    {
        base.OnClickImpl();

        if (isEmpty) return;
        SendMessage(MessageID.OnClick_Product, Product);
    }
}
