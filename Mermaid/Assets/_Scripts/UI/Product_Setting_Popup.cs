using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class Product_Setting_Popup : MessageListener
{
    public GameObject Item;
    public Transform Content;    
    List<ProductGroup> ProductGroups = new List<ProductGroup>();    
    List<GameObject> GroupItems = new List<GameObject>();

    public class ProductGroup
    {
        public int Product_Idx;
        public List<Product> Products = new List<Product>();
    }

    protected override void AddMessageListener()
    {
        base.AddMessageListener();

        AddListener(MessageID.OnClick_Remove_Edit_Button);
        AddListener(MessageID.Event_Update_Edit_Page);
    }

    protected override void OnMessage(MessageID msgID, object sender, object data)
    {
        base.OnMessage(msgID, sender, data);

        switch (msgID)
        {
            case MessageID.OnClick_Remove_Edit_Button:
                {
                    var info = (int)data;

                    var target = ProductGroups.Find(t => t.Product_Idx == info);
                    if (target != null)
                    {
                        var idx = ProductGroups.IndexOf(target);
                        var item = GroupItems[idx];
                        GroupItems.RemoveAt(idx);
                        DestroyImmediate(item);
                        ProductGroups.RemoveAt(idx);
                    }
                }
                break;
                case MessageID.Event_Update_Edit_Page:
                {
                    RemoveAllItems();
                    SetProducts();
                }
                break;
        }
    }
    protected override void AwakeImpl()
    {
        base.AwakeImpl();

        SetProducts();
    }

    void SetProducts()
    {
        SendMessage<Data_User>(MessageID.Delegate_User_Info, (userdata) =>
        {
            ProductGroups.Clear();
            for (int i = 0; i < userdata.ProductList.Count; i++)
            {
                var target = ProductGroups.Find(t => t.Product_Idx == userdata.ProductList[i].Product_Idx);
                if (target != null)
                {
                    target.Products.Add((Product)userdata.ProductList[i].Clone());
                }
                else
                {
                    var newGroup = new ProductGroup();
                    newGroup.Product_Idx = userdata.ProductList[i].Product_Idx;
                    newGroup.Products.Add((Product)userdata.ProductList[i].Clone());
                    ProductGroups.Add(newGroup);
                }
            }

            CreateItems();
        });
    }

    void CreateItems()
    {
        for (int i = 0; i < ProductGroups.Count; i++)
        {
            var newItem = Instantiate(Item, Content);
            newItem.gameObject.GetComponent<Product_Setting_Popup_Item>().SetItem(ProductGroups[i]);
            GroupItems.Add(newItem);
        }
    }

    void RemoveAllItems()
    {
        while (Content.childCount > 0)
        {
            DestroyImmediate(Content.GetChild(0).gameObject);
        }
        ProductGroups.Clear();
        GroupItems.Clear();
    }
}
