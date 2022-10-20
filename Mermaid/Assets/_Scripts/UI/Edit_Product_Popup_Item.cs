using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Edit_Product_Popup_Item : MessageListener
{
    public Button Remove_Button;
    public Text Product_Name;
    public Text Product_Name_Placeholder;
    public Text Total_Product;
    public Text Total_Product_Placeholder;
    public Text Gift_Count;
    public Text Gift_Count_Placeholder;
    public Text Error_Count;
    public Text Error_Count_Placeholder;
    public Text Cash;
    public Text Cash_Placeholder;
    int idx = 0;
    int groupIdx = 0;

    protected override void AddMessageListener()
    {
        base.AddMessageListener();

        Remove_Button.onClick.AddListener(RemoveItem);
    }

    public void SetItem(Product data)
    {
        idx = data.Idx;
        groupIdx = data.Product_Idx;
        Product_Name_Placeholder.text = data.Product_Option;
        Total_Product_Placeholder.text = data.Box_Count.ToString();
        Gift_Count_Placeholder.text = data.Total_Person.ToString();
        Error_Count_Placeholder.text = data.Error_Count.ToString();
        Cash_Placeholder.text = data.Price_Per_Person.ToString();
    }

    public Product GetItemData()
    {
        var item = new Product();
        item.Idx = idx;
        item.Product_Idx = groupIdx;
        item.Product_Option = (Product_Name.text == "") ? (Product_Name_Placeholder.text == "") ? "이름없는 옵션" : Product_Name_Placeholder.text : Product_Name.text;
        item.Box_Count = int.Parse((Total_Product.text == "") ? (Total_Product_Placeholder.text == "") ? "0" : Total_Product_Placeholder.text : Total_Product.text);
        item.Total_Person = int.Parse((Gift_Count.text == "") ? (Gift_Count_Placeholder.text == "") ? "0" : Gift_Count_Placeholder.text : Gift_Count.text);
        item.Error_Count = int.Parse((Error_Count.text == "") ? (Error_Count_Placeholder.text == "") ? "0" : Error_Count_Placeholder.text : Error_Count.text);
        item.Price_Per_Person = float.Parse((Cash.text == "") ? (Cash_Placeholder.text == "") ? "0" : Cash_Placeholder.text : Cash.text);

        return item;
    }

    void RemoveItem()
    {
        SendMessage(MessageID.Event_Remove_Option, idx);
        DestroyImmediate(this.gameObject);
    }
}
