using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confirm_Button : UIBaseButton
{
    protected override void OnClickImpl()
    {
        base.OnClickImpl();

        SendMessage(MessageID.OnClick_Confirm); 
    }
}
