using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option_List : MessageListener
{
    public GameObject Item;
    public Transform Content;
    protected override void AddMessageListener()
    {
        base.AddMessageListener();

        AddListener(MessageID.OnClick_Product);
        AddListener(MessageID.OnClick_Confirm);
        AddListener(MessageID.OnClick_Reset);
    }

    protected override void OnMessage(MessageID msgID, object sender, object data)
    {
        base.OnMessage(msgID, sender, data);

        switch (msgID)
        {
            case MessageID.OnClick_Product:
                {
                    var item = data as Product_List.Temp_Product;

                    CreateItems(item);
                }
                break;
            case MessageID.OnClick_Confirm:
            case MessageID.OnClick_Reset:
                {
                    RemoveAllItems();
                }
                break;
        }
    }

    void CreateItems(Product_List.Temp_Product products)
    {
        RemoveAllItems();

        for (int i = 0; i < products.Products.Count; i++)
        {
            var item = Instantiate(Item, Content);
            item.GetComponent<Option_Item>().SetItem(products.Products[i]);
        }
    }

    void RemoveAllItems()
    {
        while (Content.childCount > 0)
        {
            DestroyImmediate(Content.GetChild(0).gameObject);
        }
    }
}
