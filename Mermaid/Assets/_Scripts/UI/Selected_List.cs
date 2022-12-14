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
    List<List<Table_Gift>> Persons = new List<List<Table_Gift>>();
    List<Table_Gift> Gift_List = new List<Table_Gift>();
    List<GiftData> Total_Gift = new List<GiftData>();

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
                    var info = data as Table_Gift;

                    CreateItem(info);
                }
                break;
            case MessageID.OnClick_Remove:
                {
                    var info = data as Table_Gift;

                    RemoveItem(info);
                }
                break;
            case MessageID.OnClick_Confirm:
                {
                    if (Gift_List.Count < 1) return;

                    SaveProducts();
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
                totalCash += Mathf.FloorToInt(Gift_List[i].price_per_person);
            }
        }
        SendMessage(MessageID.Event_Set_TotalCash, totalCash);
    }

    void CreateItem(Table_Gift item)
    {
        var newItem = Instantiate(Item, Content);
        newItem.GetComponent<Selected_Item>().SetItem(item);
        Gift_List.Add(item);
        CalTotalCash();
    }

    void RemoveItem(Table_Gift item)
    {
        for (int i = 0; i < Gift_List.Count; i++)
        {
            if (Gift_List[i].idx == item.idx)
            {
                Gift_List.RemoveAt(i);
                Destroy(Content.GetChild(i).gameObject);
                break;
            }
        }
        CalTotalCash();
    }

    void SaveProducts()
    {
        Persons.Add(new List<Table_Gift>(Gift_List));
        for (int i = 0; i < Gift_List.Count; i++)
        {
            var item = Total_Gift.Find(t => t.Idx == Gift_List[i].idx);
            if (item == null)
            {
                var newItem = new GiftData();
                newItem.Idx = Gift_List[i].idx;
                newItem.Name = Gift_List[i].name;
                newItem.Option = Gift_List[i].option;
                newItem.Person_Per_Count = Gift_List[i].person_per_count;
                newItem.Price = Mathf.FloorToInt(Gift_List[i].price_per_person);
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
            str += (a + 1).ToString() + "?? \n";
            for (int i = 0; i < Persons[a].Count; i++)
            {
                str += "???? : " + Persons[a][i].name + "\n";
                str += "???? : " + Persons[a][i].option + "\n";
                str += "???? ???? : " + Persons[a][i].price_per_person.ToString("C") + "\n\n";
                totalCash += Mathf.FloorToInt(Persons[a][i].price_per_person);

            }
            str += "?? ???? : " + totalCash.ToString("C") + "\n";
            str += "---------------------------------\n";
        }

        str += "?? ???? ??\n";
        for (int i = 0; i < Total_Gift.Count; i++)
        {
            str += (i + 1) + ". ???? : " + Total_Gift[i].Idx + " / ???? : " + Total_Gift[i].Name + " / ?????? : " + Total_Gift[i].Option + " / ???? : " + Total_Gift[i].Count + "\n";
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
