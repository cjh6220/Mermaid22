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
    Table_Gift Table;

    public void SetItem(Table_Gift table)
    {
        Table = table;
        Product_Name.text = table.name;
        Option_Name.text = table.option;
        Count.text = table.box_per_count.ToString();
        Count_Per_Time.text = table.person_per_count.ToString();
        Price.text = table.price_per_person.ToString();
    }

    protected override void OnClickImpl()
    {
        base.OnClickImpl();

        SendMessage(MessageID.OnClick_Remove, Table);
    }
}
