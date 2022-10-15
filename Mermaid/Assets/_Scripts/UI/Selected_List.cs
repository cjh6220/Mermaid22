using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class Selected_List : MessageListener
{
    public GameObject Item;
    public Transform Content;
    List<List<Product>> Persons = new List<List<Product>>();
    List<Product> Gift_List = new List<Product>();
    List<GiftData> Total_Gift = new List<GiftData>();

    List<Data_Client> Client_Data = new List<Data_Client>();

    public class GiftData
    {
        public int Idx;
        public string Name;
        public string Option;
        public int Price;
        public int Person_Per_Count;
        public int Count = 1;
    }

    protected override void AddMessageListener()
    {
        base.AddMessageListener();

        AddListener(MessageID.OnClick_Select);
        AddListener(MessageID.OnClick_Remove);
        AddListener(MessageID.OnClick_Confirm);
        AddListener(MessageID.OnClick_Reset);
    }

    protected override void OnMessage(MessageID msgID, object sender, object data)
    {
        base.OnMessage(msgID, sender, data);

        switch (msgID)
        {
            case MessageID.OnClick_Select:
                {
                    var info = data as Product;

                    CreateItem(info);
                }
                break;
            case MessageID.OnClick_Remove:
                {
                    var info = data as Product;

                    RemoveItem(info);
                }
                break;
            case MessageID.OnClick_Confirm:
                {
                    if (Gift_List.Count < 1) return;

                    var info = data as string;

                    SaveProducts(info);
                }
                break;
            case MessageID.OnClick_Reset:
                {
                    RemoveAllItems();
                }
                break;
        }
    }

    void CalTotalCash()
    {
        int totalCash = 0;
        if (Gift_List.Count > 0)
        {
            for (int i = 0; i < Gift_List.Count; i++)
            {
                totalCash += Mathf.FloorToInt(Gift_List[i].Price_Per_Person);
            }
        }
        SendMessage(MessageID.Event_Set_TotalCash, totalCash);
    }

    void CreateItem(Product item)
    {
        var newItem = Instantiate(Item, Content);
        newItem.GetComponent<Selected_Item>().SetItem(item);
        Gift_List.Add(item);
        CalTotalCash();
    }

    void RemoveItem(Product item)
    {
        for (int i = 0; i < Gift_List.Count; i++)
        {
            if (Gift_List[i].Idx == item.Idx)
            {
                Gift_List.RemoveAt(i);
                Destroy(Content.GetChild(i).gameObject);
                break;
            }
        }
        CalTotalCash();
    }

    void SaveProducts(string Name)
    {
        var newTarget = new Data_Client();
        newTarget.Client_Name = name;
        //newTarget.Products = Gift_List;
        Persons.Add(new List<Product>(Gift_List));
        for (int i = 0; i < Gift_List.Count; i++)
        {
            var item = Total_Gift.Find(t => t.Idx == Gift_List[i].Idx);
            if (item == null)
            {
                var newItem = new GiftData();
                newItem.Idx = Gift_List[i].Idx;
                newItem.Name = Gift_List[i].Product_Name;
                newItem.Option = Gift_List[i].Product_Option;
                newItem.Person_Per_Count = Gift_List[i].Person_Per_Count;
                newItem.Price = Mathf.FloorToInt(Gift_List[i].Price_Per_Person);
                Total_Gift.Add(newItem);

                var target = Table_Manager.Instance.GetTables<Table_Gift>().Find(t => t.idx == newItem.Idx);
                if (target != null)
                {
                    target.remain_count -= newItem.Person_Per_Count;
                }
                else
                {
                    Debug.Log("Target is Null");
                }
            }
            else
            {
                item.Count++;
                var target = Table_Manager.Instance.GetTables<Table_Gift>().Find(t => t.idx == item.Idx);
                if (target != null)
                {
                    target.remain_count -= item.Person_Per_Count;
                }
                else
                {
                    Debug.Log("Target is Null");
                }
                //Table_Manager.Instance.GetTable<Table_Gift>(item.Idx).remain_count -= item.Person_Per_Count;
            }
        }

        string str = "";
        for (int a = 0; a < Persons.Count; a++)
        {
            int totalCash = 0;
            str += (a + 1).ToString() + "번 \n";
            for (int i = 0; i < Persons[a].Count; i++)
            {
                str += "품명 : " + Persons[a][i].Product_Name + "\n";
                str += "옵션 : " + Persons[a][i].Product_Option + "\n";
                str += "상품 가격 : " + Persons[a][i].Price_Per_Person.ToString("C") + "\n\n";
                totalCash += Mathf.FloorToInt(Persons[a][i].Price_Per_Person);

            }
            str += "총 금액 : " + totalCash.ToString("C") + "\n";
            str += "---------------------------------\n";
        }

        str += "총 제공 덤\n";
        for (int i = 0; i < Total_Gift.Count; i++)
        {
            str += (i + 1) + ". 품번 : " + Total_Gift[i].Idx + " / 품명 : " + Total_Gift[i].Name + " / 옵션명 : " + Total_Gift[i].Option + " / 개수 : " + Total_Gift[i].Count + "\n";
        }

        var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Gift.txt");

        DirectoryInfo directoryInfo = new DirectoryInfo(Path.GetDirectoryName(filePath));
        if (!directoryInfo.Exists)
        {
            directoryInfo.Create();
        }
        FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
        StreamWriter writer = new StreamWriter(fileStream, System.Text.Encoding.Unicode);
        writer.WriteLine(str);
        writer.Close();


        //BinaryFormatter bf = new BinaryFormatter();
        //FileStream newFile = File.Create(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Gift.txt");
        //bf.Serialize(newFile, str);
        //newFile.Close();

        RemoveAllItems();

        SendMessage(MessageID.Event_Update_Remain_Product);
    }

    void RemoveAllItems()
    {
        while (Content.childCount > 0)
        {
            DestroyImmediate(Content.GetChild(0).gameObject);
        }
        Gift_List.Clear();
    }
}
