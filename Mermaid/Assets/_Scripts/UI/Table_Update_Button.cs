using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table_Update_Button : UIBaseButton
{
    protected override void OnClickImpl()
    {
        base.OnClickImpl();

        SendMessage(MessageID.OnClick_Update_Table);
    }
}
