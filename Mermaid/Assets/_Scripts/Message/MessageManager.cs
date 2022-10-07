using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MessageManager
{
    public static MessageManager Instance = new MessageManager();

    public delegate void MessageDelegate(MessageID msgID, object sender, object data);

    MessageController _controller;
    Devcat.EnumDictionary<MessageID, MessageDelegate> _eventDic = new Devcat.EnumDictionary<MessageID, MessageDelegate>();

    public void Init(MessageController controller)
    {
        _controller = controller;
    }

    public static void SendMessage(MessageID msgID, object data = null)
    {
        MessageManager.Instance.SendImmediate(msgID, null, data);
    }

    public static void SendMessageDelay(MessageID msgID, object data, float delay)
    {
        MessageManager.Instance.SendDelay(msgID, null, data, delay);
    }

    public static void SendMessageDelayRealtime(MessageID msgID, object data, float delay)
    {
        MessageManager.Instance.SendDelayRealtime(msgID, null, data, delay);
    }

    public void AddListener(MessageID msgId, MessageDelegate regEventFunc)
    {
        if (true == _eventDic.ContainsKey(msgId))
        {
            _eventDic[msgId] += regEventFunc;
        }
        else
        {
            _eventDic.Add(msgId, regEventFunc);
        }
    }

    public void RemoveListener(MessageID msgId, MessageDelegate regEventFunc)
    {
        if (true == _eventDic.ContainsKey(msgId))
        {
            _eventDic[msgId] -= regEventFunc;
        }
    }

    public void SendDelay(MessageID msgID, object sender, object args)
    {
        if (null == _controller)
        {
            return;
        }

        _controller.StartCoroutine(SendCoroutine(msgID, sender, args));
    }

    public void SendDelay(MessageID msgID, object sender, object args, float delay)
    {
        if (null == _controller)
        {
            return;
        }

        _controller.StartCoroutine(SendCoroutineDealy(msgID, sender, args, delay));
    }

    public void SendDelayRealtime(MessageID msgID, object sender, object args, float delay)
    {
        if (null == _controller)
        {
            return;
        }

        _controller.StartCoroutine(SendCoroutineDealyRealtime(msgID, sender, args, delay));
    }

    IEnumerator SendCoroutine(MessageID msgID, object sender, object args)
    {
        yield return null;

        SendImmediate(msgID, sender, args);
    }

    IEnumerator SendCoroutineDealy(MessageID msgID, object sender, object args, float delay)
    {
        yield return new WaitForSeconds(delay);

        SendImmediate(msgID, sender, args);
    }

    IEnumerator SendCoroutineDealyRealtime(MessageID msgID, object sender, object args, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        SendImmediate(msgID, sender, args);
    }

    public void Send(MessageID msgID, object sender, object args)
    {
        SendImmediate(msgID, sender, args);
    }

    public void SendImmediate(MessageID msgID, object sender, object args)
    {
        MessageDelegate eventFunc = null;

        if (true == _eventDic.TryGetValue(msgID, out eventFunc))
        {
            if (null != eventFunc)
            {
                eventFunc(msgID, sender, args);
            }
        }
    }

    public void Clear()
    {
        _eventDic.Clear();
    }
}
