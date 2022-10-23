using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calender_Popup_Item : UIBaseButton
{
   public Text Date;
   public Color Red;
   public Color Blue;

   public void SetDate(int day, int dow = 0)
   {
        if (day == 0)
        {
            Date.text = "";
        }
        else
        {
            Date.text = day.ToString();    
            if (dow == 0)
            {
                Date.color = Red;
            }
            else if(dow == 6)
            {
                Date.color = Blue;
            }
            else
            {
                Date.color = Color.black;
            }
        }
   }
}
