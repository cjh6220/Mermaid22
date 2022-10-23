using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Calender_Popup : MessageListener
{
    public Text Year;
    public Text Month;
    public GameObject Item;
    public Transform Calender;
    public Button Left;
    public Button Right;
    public Button Mail;
    DateTime CurrentDate;
    List<Product> Gift_List = new List<Product>();

    protected override void AddMessageListener()
    {
        base.AddMessageListener();

        Left.onClick.AddListener(BeforeMonth);
        Right.onClick.AddListener(AfterMonth);
        Mail.onClick.AddListener(SendMail);
    }

    protected override void AwakeImpl()
    {
        base.AwakeImpl();

        DateTime now = DateTime.Now;
        SetCalender(now.Year, now.Month, now.Day);
    }

    void SetCalender(int year, int month, int day)
    {
        RemoveAllItem();
        Year.text = year + "년";
        Month.text = month + "월";
        var today = new DateTime(year, month, day);
        CurrentDate = today;
        var firstDay = today.AddDays(1 - today.Day);
        var lastDay = today.AddMonths(1).AddDays(0 - today.Day);

        var firstDow = firstDay.DayOfWeek;
        int date = 1;
        int dow = (int)firstDow;
        for (int i = 0; i < 42; i++)
        {
            var newItem = Instantiate(Item, Calender);
            if (i < (int)firstDow)
            {
                newItem.GetComponent<Calender_Popup_Item>().SetDate(0);
            }
            else
            {
                if (date > lastDay.Day)
                {
                    newItem.GetComponent<Calender_Popup_Item>().SetDate(0);
                    if(dow == 6) return;
                }
                else
                {
                    newItem.GetComponent<Calender_Popup_Item>().SetDate(date, dow);
                }
                date++;
                dow++;
                if (dow >= 7)
                {
                    dow = 0;
                }
            }
        }
    }

    void BeforeMonth()
    {
        var before = CurrentDate.AddMonths(-1);
        SetCalender(before.Year, before.Month, before.Day);
    }

    void AfterMonth()
    {
        var After = CurrentDate.AddMonths(1);
        SetCalender(After.Year, After.Month, After.Day);
    }

    void RemoveAllItem()
    {
        while (Calender.childCount > 0)
        {
            DestroyImmediate(Calender.GetChild(0).gameObject);
        }
    }

    void SendMail()
    {
        string mailto = "mermaidjy95@naver.com";

        string subject = EscapeURL("덤 제공 보고서 / " + CurrentDate.ToString("yyyy/MM"));

        string body = EscapeURL
        (
            GetMonthlyGift()
        );

        Application.OpenURL("mailto:" + mailto + "?subject=" + subject + "&body=" + body);
    }

    string EscapeURL(string url)
    {
        return UnityWebRequest.EscapeURL(url).Replace("+", "%20");
    }

    string GetMonthlyGift()
    {
        var str = "";
        Gift_List.Clear();
        SendMessage<Data_User>(MessageID.Delegate_User_Info, (userdata) =>
        {
            var GuestList = userdata.ClientList.FindAll(t => t.Time.Year == CurrentDate.Year && t.Time.Month == CurrentDate.Month);
            if (GuestList.Count > 0)
            {
                for (int i = 0; i < GuestList.Count; i++)
                {
                    for (int a = 0; a < GuestList[i].Products.Count; a++)
                    {                        
                        AddGiftList(GuestList[i].Products[a]);
                    }
                }

                float totalCash = 0;
                Sort();
                for (int i = 0; i < Gift_List.Count; i++)
                {
                    str += "품번 : " + Gift_List[i].Idx + " / 품명 : " + Gift_List[i].Product_Name + " / 옵션 : " + Gift_List[i].Product_Option + " / 수량 : " + Gift_List[i].Count + "\n";
                    totalCash += Gift_List[i].Price_Per_Person * Gift_List[i].Count;
                }

                str += "총 지급 금액 : " + totalCash.ToString("C") + "원";
            }
            else
            {
                str = " 이번달은 손님이 없어요 ㅠㅠ ";
            }
        });

        return str;
    }

    void AddGiftList(Product data)
    {
        var target = Gift_List.Find(t => t.Idx == data.Idx);
        if (target != null)
        {
            target.Count += data.Count;
        }
        else
        {
            Gift_List.Add((Product)data.Clone());
        }
    }

    void Sort()
    {
        Gift_List.Sort((char1, char2) =>
        {
            var check1 = char1;
            var check2 = char2;

            int value = 0;

            value = check1.Idx.CompareTo(check2.Idx);
            return value;
        });
    }
}
