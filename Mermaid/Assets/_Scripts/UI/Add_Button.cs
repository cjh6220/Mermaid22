using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Add_Button : UIBaseButton
{
    Table_Gift table = null;
    protected override void AddMessageListener()
    {
        base.AddMessageListener();

        AddListener(MessageID.OnClick_Option);
        AddListener(MessageID.OnClick_Product);
    }

    protected override void OnMessage(MessageID msgID, object sender, object data)
    {
        base.OnMessage(msgID, sender, data);

        switch (msgID)
        {
            case MessageID.OnClick_Option:
                {
                    var info = data as Table_Gift;

                    table = info;
                }
                break;
            case MessageID.OnClick_Product:
                {
                    table = null;
                }
                break;
        }
    }

    protected override void OnClickImpl()
    {
        base.OnClickImpl();

        if (table != null)
        {
            SendMessage(MessageID.OnClick_Select, table);
            table = null;
        }
    }
}
