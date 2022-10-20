using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Add_Product_Popup : MessageListener
{
    public GameObject Item;
    public Transform Content;
    public Text Product_Name;
    public Text Total_Count;
    public Text Person_Per_Count;
    public Button Add_Button;
    public Button Save_Button;

    protected override void AddMessageListener()
    {
        base.AddMessageListener();

        Add_Button.onClick.AddListener(Add_New_Item);
        Save_Button.onClick.AddListener(Save_Items);
    }

    protected override void OnMessage(MessageID msgID, object sender, object data)
    {
        base.OnMessage(msgID, sender, data);
    }

    void Add_New_Item()
    {
        Instantiate(Item, Content);
    }

    void Save_Items()
    {
        var targets = Content.GetComponentsInChildren<Add_Product_Popup_Item>();
        string _name = Product_Name.text == "" ? "이름없는 제품" : Product_Name.text;
        var itemList = new List<Product>();
        for (int i = 0; i < targets.Length; i++)
        {
            var itemData = targets[i].GetItemData();
            itemData.Product_Name = _name;
            itemData.Box_Per_Count = int.Parse((Total_Count.text == "") ? "0" : Total_Count.text);
            itemData.Person_Per_Count = int.Parse((Person_Per_Count.text == "") ? "0" : Person_Per_Count.text);
            itemList.Add(itemData);
        }
        SendMessage(MessageID.Event_Add_New_Products, itemList);
        SendMessage(MessageID.Call_UI_Pop);
    }
}
