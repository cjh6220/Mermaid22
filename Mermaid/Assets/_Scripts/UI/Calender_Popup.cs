using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Calender_Popup : MessageListener
{
    public GameObject Item;
    public Transform Calender;

    protected override void AwakeImpl()
    {
        base.AwakeImpl();

    }

    void SetCalender(int year, int month, int day)
    {
        var test = new DateTime(year, month, day);
        var firstDay = test.DayOfWeek;
        while (true)
        {
            var newItem = Instantiate(Item, Calender);
        }
    }
}
