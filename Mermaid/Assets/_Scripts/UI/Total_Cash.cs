using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Total_Cash : MessageListener
{
    public Text TotalCash;
    protected override void AddMessageListener()
    {
        base.AddMessageListener();

        AddListener(MessageID.Event_Set_TotalCash);
        AddListener(MessageID.OnClick_Reset);
        AddListener(MessageID.OnClick_Confirm);
    }

    protected override void OnMessage(MessageID msgID, object sender, object data)
    {
        base.OnMessage(msgID, sender, data);

        switch (msgID)
        {
            case MessageID.Event_Set_TotalCash:
                {
                    var info = (int)data;

                    TotalCash.text = info.ToString("C");
                }
                break;
            case MessageID.OnClick_Reset:
            case MessageID.OnClick_Confirm:
            {
                    int info = 0;

                    TotalCash.text = info.ToString("C");
                }
                break;
        }
    }
}
