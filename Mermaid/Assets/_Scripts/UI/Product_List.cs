using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product_List : MessageListener
{
    public GameObject Item;
    public Transform Content;
    public class Temp_Product
    {
        public int Product_Id;
        public List<Product> Products = new List<Product>();

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    List<Temp_Product> Products = new List<Temp_Product>();
    protected override void AddMessageListener()
    {
        base.AddMessageListener();

    }

    protected override void OnMessage(MessageID msgID, object sender, object data)
    {
        base.OnMessage(msgID, sender, data);

        switch(msgID)
        {
            
        }
    }

    protected override void AwakeImpl()
    {
        base.AwakeImpl();

        //var table = Table_Manager.Instance.GetTables<Table_Gift>();
        //for (int i = 0; i < table.Count; i++)
        //{   
        //    var item = Products.Find(t => t.Product_Id == table[i].product_idx);
        //    if (item != null)
        //    {   
        //        item.Table.Add(table[i]);
        //    }
        //    else
        //    {
        //        var newItem = new Product();
        //        newItem.Product_Id = table[i].product_idx;
        //        newItem.Table.Add(table[i]);
        //        Products.Add(newItem);
        //    }
        //}
        
        SendMessage<Data_User>(MessageID.Delegate_User_Info, (userdata) =>
        {   
            for(int i = 0; i < userdata.ProductList.Count; i++)
            {
                var item = Products.Find(t => t.Product_Id == userdata.ProductList[i].Product_Idx);
                if (item != null)
                {
                    item.Products.Add(userdata.ProductList[i]);
                }
                else
                {
                    var newItem = new Temp_Product();
                    newItem.Product_Id = userdata.ProductList[i].Product_Idx;
                    newItem.Products.Add(userdata.ProductList[i]);
                    Products.Add(newItem);
                }
            }
        });

        
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
