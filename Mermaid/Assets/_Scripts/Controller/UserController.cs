using System.Xml.Schema;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;
using Newtonsoft.Json;
//using Photon.Pun;

public class UserController : MessageListener
{
    Data_User _userData = new Data_User();    

    protected override void AwakeImpl()
    {
        base.AwakeImpl();

        LoadJson();
    }

    protected override void AddMessageListener()
    {
        base.AddMessageListener();

        AddListener(MessageID.Delegate_User_Info);
        AddListener(MessageID.OnClick_Table_To_Json);
    }

    protected override void OnMessage(MessageID msgID, object sender, object data)
    {
        base.OnMessage(msgID, sender, data);
        
        switch(msgID)
        {
            case MessageID.Delegate_User_Info:
                {
                    var requestAction = data as System.Action<Data_User>;

                    requestAction(_userData);
                }
                break;
            case MessageID.OnClick_Table_To_Json:
                {
                    TableToJson();
                }
                break;
        }
    }

    void TableToJson()
    {
        var table = Table_Manager.Instance.GetTables<Table_Gift>();
        List<Product> productList = new List<Product>();
        for (int i = 0; i < table.Count; i++)
        {
            var item = new Product();
            item.Idx = table[i].idx;
            item.Product_Idx = table[i].product_idx;
            item.Product_Name = table[i].name;
            item.Product_Option = table[i].option;
            item.Box_Count = table[i].box_count;
            item.Box_Per_Count = table[i].box_per_count;
            item.Person_Per_Count = table[i].person_per_count;
            item.Total_Person = table[i].total_person;
            item.Price_Per_Person = table[i].price_per_person;
            
            productList.Add(item);
        }

        var str = JsonConvert.SerializeObject(productList);        
        File.WriteAllText(Application.dataPath + "/ProductDB.json", str);
        LoadJson();
        SendMessage(MessageID.Event_Set_Log, "Table To Json Success");
    }

    void LoadJson()
    {   
        var jsonStrRead = File.ReadAllText(Application.dataPath + "/ProductDB.json");
        var deserializedBarList = JsonConvert.DeserializeObject<List<Product>>(jsonStrRead);

        _userData.ProductList = deserializedBarList;
        SendMessage(MessageID.Event_Set_Log, "DB Load Success");
    }
}
