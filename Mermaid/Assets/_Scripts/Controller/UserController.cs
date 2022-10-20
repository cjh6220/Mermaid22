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
        AddListener(MessageID.Event_Add_New_Products);
        AddListener(MessageID.Event_Edit_New_Products);
        AddListener(MessageID.Event_Edit_Remove_Products);
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
            case MessageID.Event_Add_New_Products:
                {
                    var info = data as List<Product>;

                    AddNewProduct(info);
                }
                break;
            case MessageID.Event_Edit_New_Products:
                {
                    var info = data as List<Product>;

                    EditProduct(info);
                }
                break;
            case MessageID.Event_Edit_Remove_Products:
                {
                    var info = data as List<int>;

                    RemoveProduct(info);
                }
                break;
        }
    }

    void TableToJson()
    {
        var table = Table_Manager.Instance.GetTables<Table_Gift>();
        List<Product> productList = new List<Product>();
        int lastIdx = 0;
        int lastGroupIdx = 0;
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

            if(item.Idx > lastIdx) lastIdx = item.Idx;
            if(item.Product_Idx > lastGroupIdx) lastGroupIdx = item.Product_Idx;

            productList.Add(item);
        }
        Debug.Log("LastIdx = " + lastIdx + " / LastProductIdx = " + lastGroupIdx);
        PlayerPrefs.SetInt("LastIdx", lastIdx);
        PlayerPrefs.SetInt("LastProductIdx", lastGroupIdx);

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

            _userData.LastIdx = PlayerPrefs.GetInt("LastIdx");
            _userData.LastProductIdx = PlayerPrefs.GetInt("LastProductIdx");
        }
        else
        {
            File.WriteAllText(Application.dataPath + "/ProductDB.json", "");
            PlayerPrefs.SetInt("LastIdx", 0);
            PlayerPrefs.SetInt("LastProductIdx", 0);
        }

        if (File.Exists(Application.dataPath + "/ClientDB.json"))
        {
            var jsonStrRead_Client = File.ReadAllText(Application.dataPath + "/ClientDB.json");
            var deserializedBarList_Client = JsonConvert.DeserializeObject<List<Data_Client>>(jsonStrRead_Client);
            _userData.ClientList = deserializedBarList_Client;
            if (_userData.ClientList == null)
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

        PlayerPrefs.SetInt("LastIdx", _userData.LastIdx);
        PlayerPrefs.SetInt("LastProductIdx", _userData.LastProductIdx);
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

    void AddNewProduct(List<Product> data)
    {
        var targets = data;
        int idx = _userData.LastIdx;
        int productIdx = _userData.LastProductIdx + 1;
        for (int i = 0; i < targets.Count; i++)
        {
            targets[i].Idx = ++idx;
            targets[i].Product_Idx = productIdx;
            targets[i].Remain_Count = (targets[i].Box_Count * targets[i].Box_Per_Count) - (targets[i].Total_Person * targets[i].Person_Per_Count) - targets[i].Error_Count;
            _userData.ProductList.Add(targets[i]);
        }

        if (targets.Count > 0)
        {
            _userData.LastIdx = idx;
            _userData.LastProductIdx = productIdx;
        }

        SaveJson();
        SendMessage(MessageID.Event_Update_Edit_Page);
    }

    void EditProduct(List<Product> data)
    {
        var targets = data;
        int idx = _userData.LastIdx;
        int productIdx = _userData.LastProductIdx;
        for (int i = 0; i < targets.Count; i++)
        {
            var item = _userData.ProductList.Find(t => t.Idx == targets[i].Idx);
            if (item != null)
            {
                int existIdx = _userData.ProductList.IndexOf(item);
                _userData.ProductList[existIdx] = targets[i];
                _userData.ProductList[existIdx].Remain_Count = (targets[i].Box_Count * targets[i].Box_Per_Count) - (targets[i].Total_Person * targets[i].Person_Per_Count) - targets[i].Error_Count;
            }
            else
            {
                targets[i].Idx = ++idx;
                targets[i].Remain_Count = (targets[i].Box_Count * targets[i].Box_Per_Count) - (targets[i].Total_Person * targets[i].Person_Per_Count) - targets[i].Error_Count;
                _userData.ProductList.Add(targets[i]);
            }
        }
        _userData.LastIdx = idx;
        _userData.LastProductIdx = productIdx;
    }

    void RemoveProduct(List<int> data)
    {
        for (int i = 0; i < data.Count; i++)
        {
            var target = _userData.ProductList.Find(t => t.Idx == data[i]);
            if (target != null)
            {
                _userData.ProductList.Remove(target);
            }
        }
        SaveJson();
        SendMessage(MessageID.Event_Update_Edit_Page);
    }
}
