using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPopup_Toast : MessageListener
{
    public TextMeshProUGUI Message;
    public Animation Ani;
    public PopupController _popupController;
    public void SetPopup(string message, PopupController popupController)
    {
        _popupController = popupController;
        Message.SetText(message);
        //Ani.Play();
        SendMessage(MessageID.Call_InputEvent_Lock);
    }

    public void ClosePopup()
    {
        _popupController.DespawnPopup(this.gameObject);
        SendMessage(MessageID.Call_InputEvent_Unlock);
        SendMessage(MessageID.Call_Same_NickName);
    }
}
