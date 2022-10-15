using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Confirm_Button : UIBaseButton
{
    public Text Name;
    protected override void OnClickImpl()
    {
        base.OnClickImpl();

        SendMessage(MessageID.OnClick_Confirm, Name.text); 
    }
}
