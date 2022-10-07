using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Log : MessageListener
{
    public Text log;
    protected override void AddMessageListener()
    {
        base.AddMessageListener();

        AddListener(MessageID.Event_Set_Log);
    }

    protected override void OnMessage(MessageID msgID, object sender, object data)
    {
        base.OnMessage(msgID, sender, data);

        switch(msgID)
        {
            case MessageID.Event_Set_Log:
                {
                    var info = data as string;

                    log.text = info;
                }
                break;
        }
    }
}
