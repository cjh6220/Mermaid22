using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Product_Item : UIBaseButton
{
    public Text Product_Name;
    public Image BG;
    Product_List.Temp_Product Product;
    bool isEmpty = false;

    protected override void AddMessageListener()
    {
        base.AddMessageListener();

        AddListener(MessageID.Event_InfoUpdate_UserData);
        AddListener(MessageID.OnClick_Product);
        AddListener(MessageID.OnClick_Select);
        AddListener(MessageID.OnClick_Confirm);
        AddListener(MessageID.OnClick_Confirm);
        AddListener(MessageID.Event_Update_Remain_Product);
        AddListener(MessageID.OnClick_Update_Selected_Product_Count);
        AddListener(MessageID.OnClick_Add_Selected_Product_Count);
        AddListener(MessageID.OnClick_Reduce_Selected_Product_Count);
    }

    protected override void OnMessage(MessageID msgID, object sender, object data)
    {
        base.OnMessage(msgID, sender, data);

        switch (msgID)
        {
            case MessageID.Event_InfoUpdate_UserData:
                {
                    var info = data as Data_User;

                    var item = new Product_List.Temp_Product();
                    item.Product_Id = Product.Product_Id;

                    var updateItem = info.ProductList.FindAll(t => t.Product_Idx == Product.Product_Id);
                    if(updateItem.Count > 0)
                    {
                        item.Products = updateItem;
                        SetItem(item);
                    }                    
                }
                break;
            case MessageID.OnClick_Product:
                {
                    var info = data as Product_List.Temp_Product;
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
            case MessageID.OnClick_Select:               
            case MessageID.OnClick_Add_Selected_Product_Count:
                {
                    var info = data as Product;

                    TempUpdateItem(info, -1);
                }
                break;
            case MessageID.OnClick_Reduce_Selected_Product_Count:
                {
                    var info = data as Product;

                    TempUpdateItem(info, 1);
                }
                break;
        }
    }

    public void SetItem(Product_List.Temp_Product products)
    {
        Product = (Product_List.Temp_Product)products.Clone();
        int productCount = 0;
        for (int i = 0; i < products.Products.Count; i++)
        {
            productCount += Mathf.FloorToInt(products.Products[i].Remain_Count) / products.Products[i].Person_Per_Count;
        }
        Product_Name.text = products.Products[0].Product_Name;

        if (productCount <= 0)
        {
            isEmpty = true;
            BG.color = Color.red;
        }
    }    

    void TempUpdateItem(Product data, int count)
    {
        var target = Product.Products.Find(t => t.Idx == data.Idx);
        if (target != null)
        {
            target.Remain_Count += count * target.Person_Per_Count;
        }
        UpdateTextColor();
    }

    void UpdateItem()
    {
        SendMessage<Data_User>(MessageID.Delegate_User_Info, (userdata) =>
        {
            Product.Products = new List<Product>(userdata.ProductList.FindAll(t => t.Product_Idx == Product.Product_Id));
        });

        UpdateTextColor();
    }

    void UpdateTextColor()
    {
        int totalCount = 0;
        for (int i = 0; i < Product.Products.Count; i++)
        {
            var count = Mathf.FloorToInt(Product.Products[i].Remain_Count) / Product.Products[i].Person_Per_Count;
            totalCount += count;
        }

        if (totalCount <= 0)
        {
            isEmpty = true;
            BG.color = Color.red;
        }
        else
        {
            isEmpty = false;
            BG.color = Color.white;
        }
    }

    protected override void OnClickImpl()
    {
        base.OnClickImpl();

        if (isEmpty) return;
        SendMessage(MessageID.OnClick_Product, Product);
    }
}
