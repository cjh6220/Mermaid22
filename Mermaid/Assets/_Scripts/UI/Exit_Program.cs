using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit_Program : UIBaseButton
{
    protected override void OnClickImpl()
    {
        base.OnClickImpl();

        Application.Quit();
    }
}
