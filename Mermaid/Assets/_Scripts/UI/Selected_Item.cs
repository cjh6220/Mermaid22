using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selected_Item : UIBaseButton
{
    public Text Product_Name;
    public Text Option_Name;
    public Text Count;
    public Text Count_Per_Time;
    public Text Price;
    Product Table;

    public void SetItem(Product table)
    {
        Table = table;
        Product_Name.text = table.Product_Name;
        Option_Name.text = table.Product_Option;
        Count.text = table.Box_Per_Count.ToString();
        Count_Per_Time.text = table.Person_Per_Count.ToString();
        Price.text = table.Price_Per_Person.ToString();
    }

    protected override void OnClickImpl()
    {
        base.OnClickImpl();

        SendMessage(MessageID.OnClick_Remove, Table);
    }
}
