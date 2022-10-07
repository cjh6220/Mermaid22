using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option_Item : UIBaseButton
{
    public Text Option_Name;
    public Image BG;
    Table_Gift Table;
    bool isEmpty = false;
    protected override void AddMessageListener()
    {
        base.AddMessageListener();

        AddListener(MessageID.OnClick_Option);
        AddListener(MessageID.OnClick_Select);
    }

    protected override void OnMessage(MessageID msgID, object sender, object data)
    {
        base.OnMessage(msgID, sender, data);

        switch (msgID)
        {
            case MessageID.OnClick_Option:
                {
                    var info = data as Table_Gift;

                    if (isEmpty) return;
                    if (info.idx == Table.idx)
                    {
                        BG.color = Color.blue;
                    }
                    else
                    {
                        BG.color = Color.white;
                    }
                }
                break;
            case MessageID.OnClick_Select:
                {
                    if (isEmpty) return;
                    BG.color = Color.white;
                }
                break;
        }
    }

    public void SetItem(Table_Gift table)
    {
        Table = table;
        Option_Name.text = table.option + "(" + (table.remain_count / table.person_per_count) + ")";
        if (table.remain_count < table.person_per_count)
        {
            BG.color = Color.red;
            isEmpty = true;
        }
    }

    protected override void OnClickImpl()
    {
        base.OnClickImpl();

        if (isEmpty) return;

        SendMessage(MessageID.OnClick_Option, Table);
    }
}
