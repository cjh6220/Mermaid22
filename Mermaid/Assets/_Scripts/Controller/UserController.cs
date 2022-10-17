using System.Xml.Schema;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
//using Photon.Pun;

public class UserController : MessageListener
{
    [ShowInInspector]
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
        AddListener(MessageID.Event_Save_Product_List);
        AddListener(MessageID.OnClick_Remove_Edit_Button);
    }

    protected override void OnMessage(MessageID msgID, object sender, object data)
    {
        base.OnMessage(msgID, sender, data);

        switch (msgID)
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
            case MessageID.Event_Save_Product_List:
                {
                    var info = data as Data_Client;

                    UpdateRemainProducts(info);
                }
                break;
            case MessageID.OnClick_Remove_Edit_Button:
                {
                    var info = (int)data;

                    var targets = new List<Product>(_userData.ProductList.FindAll(t => t.Product_Idx == info));
                    if (targets.Count > 0)
                    {
                        for (int i = 0; i < targets.Count; i++)
                        {
                            _userData.ProductList.Remove(targets[i]);
                        }
                    }
                    SaveJson();
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
            item.Remain_Count = table[i].remain_count;
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

        if (File.Exists(Application.dataPath + "/ProductDB.json"))
        {
            var jsonStrRead = File.ReadAllText(Application.dataPath + "/ProductDB.json");
            var deserializedBarList = JsonConvert.DeserializeObject<List<Product>>(jsonStrRead);
            _userData.ProductList = deserializedBarList;
            if (_userData.ProductList == null)
            {
                _userData.ProductList = new List<Product>();
            }
        }
        else
        {
            File.WriteAllText(Application.dataPath + "/ProductDB.json", "");
        }

        if (File.Exists(Application.dataPath + "/ClientDB.json"))
        {
            var jsonStrRead_Client = File.ReadAllText(Application.dataPath + "/ClientDB.json");
            var deserializedBarList_Client = JsonConvert.DeserializeObject<List<Data_Client>>(jsonStrRead_Client);            
            _userData.ClientList = deserializedBarList_Client;
            if(_userData.ClientList == null)
            {
                _userData.ClientList = new List<Data_Client>();
            }
        }
        else
        {
            File.WriteAllText(Application.dataPath + "/ClientDB.json", "");
        }

        SendMessage(MessageID.Event_Set_Log, "DB Load Success");
    }

    void SaveJson()
    {
        var str = JsonConvert.SerializeObject(_userData.ProductList);
        File.WriteAllText(Application.dataPath + "/ProductDB.json", str);

        var clientStr = JsonConvert.SerializeObject(_userData.ClientList);
        File.WriteAllText(Application.dataPath + "/ClientDB.json", clientStr);
    }

    void UpdateRemainProducts(Data_Client data)
    {
        for (int i = 0; i < data.Products.Count; i++)
        {
            var item = _userData.ProductList.Find(t => t.Idx == data.Products[i].Idx);
            if (item != null)
            {
                item.Total_Person += data.Products[i].Count;
                item.Remain_Count -= data.Products[i].Count * data.Products[i].Person_Per_Count;
            }
        }

        var newClient = new Data_Client();
        newClient.Client_Name = data.Client_Name;
        newClient.Products = data.Products;
        newClient.Time = DateTime.Now;
        for (int i = 0; i < data.Products.Count; i++)
        {
            newClient.Total_Cash += Mathf.FloorToInt(data.Products[i].Price_Per_Person * data.Products[i].Count);
        }
        _userData.ClientList.Add(newClient);
        SaveJson();

        SendMessage(MessageID.Event_InfoUpdate_UserData, _userData);
    }
}
