using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Add_Product_Popup_Item : MessageListener
{
    public Button Remove_Button;
    public Text Product_Name;
    public Text Total_Product;
    public Text Gift_Count;
    public Text Error_Count;
    public Text Cash;

    protected override void AddMessageListener()
    {
        base.AddMessageListener();

        Remove_Button.onClick.AddListener(RemoveItem);
    }

    public Product GetItemData()
    {
        var item = new Product();
        item.Product_Option = (Product_Name.text == "") ? "이름 없는 옵션" : Product_Name.text;
        item.Box_Count = int.Parse((Total_Product.text == "") ? "0" : Total_Product.text);
        item.Total_Person = int.Parse((Gift_Count.text == "") ? "0" : Gift_Count.text);
        item.Error_Count = int.Parse((Error_Count.text == "") ? "0" : Error_Count.text);
        item.Price_Per_Person = float.Parse((Cash.text == "") ? "0" : Cash.text);

        return item;
    }

    void RemoveItem()
    {
        DestroyImmediate(this.gameObject);
    }
}
