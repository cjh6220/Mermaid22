using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Edit_Product_Popup : MessageListener
{
    public GameObject Item;
    public GameObject NewItem;
    public Transform Content;
    public Text Product_Name;
    public Text Product_Name_Placeholder;
    public Text Total_Count;
    public Text Total_Count_Placeholder;
    public Text Person_Per_Count;
    public Text Person_Per_Count_Placeholder;
    public Button Add_Button;
    public Button Save_Button;
    int groupIdx = 0;
    List<int> removedIdx = new List<int>();
    protected override void AddMessageListener()
    {
        base.AddMessageListener();

        AddListener(MessageID.OnClick_Product_Edit_Button);
        AddListener(MessageID.Event_Remove_Option);
        Add_Button.onClick.AddListener(Add_New_Item);
        Save_Button.onClick.AddListener(Save_Items);
    }

    protected override void OnMessage(MessageID msgID, object sender, object data)
    {
        base.OnMessage(msgID, sender, data);

        switch (msgID)
        {
            case MessageID.OnClick_Product_Edit_Button:
                {
                    var info = data as Product_Setting_Popup.ProductGroup;

                    SetItems(info);
                }
                break;
            case MessageID.Event_Remove_Option:
                {
                    var info = (int)data;
                    if (info == 0) return;

                    removedIdx.Add(info);
                }
                break;
        }
    }

    void SetItems(Product_Setting_Popup.ProductGroup data)
    {
        groupIdx = data.Product_Idx;
        Product_Name_Placeholder.text = data.Products[0].Product_Name;
        Total_Count_Placeholder.text = data.Products[0].Box_Per_Count.ToString();
        Person_Per_Count_Placeholder.text = data.Products[0].Person_Per_Count.ToString();

        for (int i = 0; i < data.Products.Count; i++)
        {
            var item = Instantiate(Item, Content);
            item.GetComponent<Edit_Product_Popup_Item>().SetItem(data.Products[i]);
        }
    }

    void Add_New_Item()
    {
        Instantiate(Item, Content);
    }

    void Save_Items()
    {
        var targets = Content.GetComponentsInChildren<Edit_Product_Popup_Item>();
        string _name = Product_Name.text == "" ? (Product_Name_Placeholder.text == "") ? "이름없는 제품" : Product_Name_Placeholder.text : Product_Name.text;
        var itemList = new List<Product>();
        for (int i = 0; i < targets.Length; i++)
        {
            var itemData = targets[i].GetItemData();
            itemData.Product_Idx = groupIdx;
            itemData.Product_Name = _name;
            itemData.Box_Per_Count = int.Parse((Total_Count.text == "") ? Total_Count_Placeholder.text == "" ? "0" : Total_Count_Placeholder.text : Total_Count.text);
            itemData.Person_Per_Count = int.Parse((Person_Per_Count.text == "") ? Person_Per_Count_Placeholder.text == "" ? "0" : Person_Per_Count_Placeholder.text : Person_Per_Count.text);
            itemList.Add(itemData);
        }
        SendMessage(MessageID.Event_Edit_New_Products, itemList);
        SendMessage(MessageID.Event_Edit_Remove_Products, removedIdx);
        SendMessage(MessageID.Call_UI_Pop);
    }
}
