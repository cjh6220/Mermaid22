using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_Button : UIBaseButton
{
    protected override void OnClickImpl()
    {
        base.OnClickImpl();

        SendMessage(MessageID.OnClick_Start);
    }
}
