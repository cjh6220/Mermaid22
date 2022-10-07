using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageListener : MonoBehaviour
{
    protected List<MessageID> _messageList = new List<MessageID>();

    void Awake()
    {
        AwakeImpl();

        AddMessageListener();
    }

    void OnDestroy()
    {
        RemoveMessageListener();

        OnDestroyImpl();
    }

    void OnEnable()
    {

        OnEnableImpl();
    }

    protected virtual void AwakeImpl()
    {

    }

    protected virtual void OnDestroyImpl()
    {

    }

    protected virtual void OnEnableImpl()
    {

    }

    protected virtual void AddMessageListener()
    {

    }

    void RemoveMessageListener()
    {
        foreach (var msg in _messageList)
        {
            MessageManager.Instance.RemoveListener(msg, OnMessage);
        }
        _messageList.Clear();
    }

    protected void AddListener(MessageID msgID)
    {
        if (0 > _messageList.FindIndex((MessageID msgid) => { return msgid.Equals(msgID); }))
        {
            _messageList.Add(msgID);
            MessageManager.Instance.AddListener(msgID, OnMessage);
        }
    }

    protected void SendMessage(MessageID msgID, object data = null)
    {
        // switch (msgID)
        // {
        //     case MessageID.Delegate_User_Info:

        //         break;
        //     case MessageID.Request_Packet:
        //         Debug.Log($"<color=red>SendMessage:{msgID} Data:{data.GetType()}</color>");
        //         break;
        //     default:
        //         Debug.Log($"<color=red>SendMessage:{msgID} Data:{data}</color>");
        //         break;
        // }

        MessageManager.Instance.SendImmediate(msgID, this, data);
    }

    protected void SendMessage<T>(MessageID msgID, System.Action<T> data = null)
    {
        //ConsoleProDebug.LogToFilter(msgID.ToString() + "/" + data, "MessageID");

        // switch (msgID)
        // {
        //     case MessageID.Delegate_User_Info:
        //         break;
        //     default:
        //         Debug.Log($"<color=red>SendMessage:{msgID} Data:{data}</color>");
        //         break;
        // }


        MessageManager.Instance.SendImmediate(msgID, this, data);
    }

    protected void SendMessageDelay(MessageID msgID, object data = null)
    {
        //Debug.Log($"<color=red>SendMessageDelay:{msgID} Data:{data}</color>");

        MessageManager.Instance.SendDelay(msgID, this, data);
    }

    protected void SendMessageDelayBySeconds(MessageID msgID, object data = null, float delay = 0.0f)
    {
        //ConsoleProDebug.LogToFilter(msgID.ToString() + " Delay:" + delay, "MessageID");

        //Debug.Log($"<color=red>SendMessageDelayBySeconds:{msgID} Data:{data}</color>");

        MessageManager.Instance.SendDelay(msgID, this, data, delay);
    }

    protected void SendMessageDelayRealtime(MessageID msgID, object data, float delay)
    {
        //ConsoleProDebug.LogToFilter(msgID.ToString() + " DelayRealtime:" + delay, "MessageID");

        //Debug.Log($"<color=red>SendMessageDelayRealtime:{msgID} Data:{data}</color>");

        MessageManager.Instance.SendDelayRealtime(msgID, this, data, delay);
    }

    protected virtual void OnMessage(MessageID msgID, object sender, object data)
    {

    }

    public void DirectMessage(MessageID msgID, object sender, object data)
    {
        OnMessage(msgID, sender, data);
    }
}