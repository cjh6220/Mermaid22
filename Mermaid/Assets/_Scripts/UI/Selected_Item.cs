using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selected_Item : MessageListener
{
    public Text Product_Name;
    public Text Option_Name;
    public Text Count;
    public Text Count_Per_Time;
    public Text Total_Count;
    public Text Price;
    public Button Reduce_Btn;
    public Button Add_Btn;
    public Button Remove_Btn;
    int Product_Count = 1;
    Product Table = new Product();

    protected override void AddMessageListener()
    {
        base.AddMessageListener();

        Reduce_Btn.onClick.AddListener(Reduce_Count);
        Add_Btn.onClick.AddListener(Add_Count);
        Remove_Btn.onClick.AddListener(Remove_Product);
    }

    public void SetItem(Product table)
    {
        Table = (Product)table.Clone();
        Product_Name.text = table.Product_Name;
        Option_Name.text = table.Product_Option;
        Count.text = table.Box_Per_Count.ToString();
        Count_Per_Time.text = table.Person_Per_Count.ToString();
        Total_Count.text = "1";
        Price.text = table.Price_Per_Person.ToString();
    }

    void UpdateProductCount()
    {
        Total_Count.text = Product_Count.ToString();
        Price.text = Mathf.FloorToInt(Product_Count * Table.Price_Per_Person).ToString();

        //var msg = new Data_Selected_Product();
        //msg.Product = Table;
        //msg.Product_Count = Product_Count;
        //SendMessage(MessageID.OnClick_Update_Selected_Product_Count, msg);
    }

    void Reduce_Count()
    {
        Product_Count--;
        if(Product_Count <= 0)
        {
            Product_Count = 1;
        }
        else
        {
            SendMessage(MessageID.OnClick_Reduce_Selected_Product_Count, Table);
        }
        UpdateProductCount();
    }

    void Add_Count()
    {
        Product_Count++;
        int remainPerson = Mathf.FloorToInt(Table.Remain_Count / Table.Person_Per_Count);
        if (Product_Count > remainPerson)
        {
            Product_Count = remainPerson;
        }
        else
        {
            SendMessage(MessageID.OnClick_Add_Selected_Product_Count, Table);
        }
        UpdateProductCount();
    }

    void Remove_Product()
    {
        SendMessage(MessageID.OnClick_Remove, Table);
    }
}
