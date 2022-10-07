using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputEventController : MessageListener
{
    public GameObject UnityEventSystem;

    protected override void AwakeImpl()
    {
        base.AwakeImpl();
    }

    protected override void AddMessageListener()
    {
        base.AddMessageListener();

        AddListener(MessageID.Call_InputEvent_Lock);
        AddListener(MessageID.Call_InputEvent_Unlock);
    }

    protected override void OnMessage(MessageID msgID, object sender, object data)
    {
        base.OnMessage(msgID, sender, data);

        switch (msgID)
        {
            case MessageID.Call_InputEvent_Lock:
                UnityEventSystem.SetActive(false);
                break;
            case MessageID.Call_InputEvent_Unlock:
                UnityEventSystem.SetActive(true);
                break;
        }
    }
}

