using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Msg_DeliveryData 
{
    public MessageID SendMessageID;
    public object SendMessageData;  

    public Msg_DeliveryData(MessageID sendMessageID, object sendMessageData = null)  
    {
        SendMessageID = sendMessageID;
        SendMessageData = sendMessageData;
    }
}
