using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageController : MessageListener
{
    protected override void AwakeImpl()
    {
        base.AwakeImpl();

        MessageManager.Instance.Init(this);
    }

    protected override void AddMessageListener()
    {
        base.AddMessageListener();
    }

    protected override void OnMessage(MessageID msgID, object sender, object data)
    {
        base.OnMessage(msgID, sender, data);
    }
}
