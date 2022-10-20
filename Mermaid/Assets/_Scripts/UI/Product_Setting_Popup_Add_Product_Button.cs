using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product_Setting_Popup_Add_Product_Button : UIBaseButton
{
    protected override void OnClickImpl()
    {
        base.OnClickImpl();

        SendMessage(MessageID.Call_UI_Push_Popup, String_UIName.Popup_Add_Product);
    }
}
