using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product_List : MessageListener
{
    public GameObject Item;
    public Transform Content;
    public class Product
    {
        public int Product_Id;
        public List<Table_Gift> Table = new List<Table_Gift>();
    }

    List<Product> Products = new List<Product>();
    protected override void AddMessageListener()
    {
        base.AddMessageListener();
    }

    protected override void OnMessage(MessageID msgID, object sender, object data)
    {
        base.OnMessage(msgID, sender, data);
    }

    protected override void AwakeImpl()
    {
        base.AwakeImpl();

        var table = Table_Manager.Instance.GetTables<Table_Gift>();
        for (int i = 0; i < table.Count; i++)
        {   
            var item = Products.Find(t => t.Product_Id == table[i].product_idx);
            if (item != null)
            {   
                item.Table.Add(table[i]);
            }
            else
            {
                var newItem = new Product();
                newItem.Product_Id = table[i].product_idx;
                newItem.Table.Add(table[i]);
                Products.Add(newItem);
            }
        }

        MakeItemList();
    }

    void MakeItemList()
    {
        for (int i = 0; i < Products.Count; i++)
        {   
            var item = Instantiate(Item, Content);
            item.GetComponent<Product_Item>().SetItem(Products[i]);
        }
    }
}
