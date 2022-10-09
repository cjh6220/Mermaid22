using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table_Update_Button : UIBaseButton
{
    protected override void OnClickImpl()
    {
        base.OnClickImpl();

#if UNITY_EDITOR
        SendMessage(MessageID.OnClick_Table_To_Json);
#endif
    }
}
