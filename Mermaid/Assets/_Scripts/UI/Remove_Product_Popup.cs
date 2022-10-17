using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remove_Product_Popup : UIBaseButton
{
    int Idx;
    protected override void AddMessageListener()
    {
        base.AddMessageListener();

        AddListener(MessageID.Event_Open_Remove_Product_Popup);
    }

    protected override void OnMessage(MessageID msgID, object sender, object data)
    {
        base.OnMessage(msgID, sender, data);

        switch (msgID)
        {
            case MessageID.Event_Open_Remove_Product_Popup:
            {
                var info = (int)data;

                Idx = info;
            }
            break;
        }
    }
    protected override void OnClickImpl()
    {
        base.OnClickImpl();

        SendMessage(MessageID.OnClick_Remove_Edit_Button, Idx);
        SendMessage(MessageID.Call_UI_Pop);
    }
}
