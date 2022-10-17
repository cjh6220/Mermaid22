using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class SendMail : UIBaseButton
{
    List<Product> Gift_List = new List<Product>();
    protected override void OnClickImpl()
    {
        base.OnClickImpl();

        string mailto = "mermaidjy95@naver.com";

        string subject = EscapeURL("덤 제공 보고서 / " + DateTime.Now.ToString("yyyy/MM/dd"));

        string body = EscapeURL
        (
            GetTodayGift()
        );

        Application.OpenURL("mailto:" + mailto + "?subject=" + subject + "&body=" + body);
    }

    string EscapeURL(string url)
    {
        return UnityWebRequest.EscapeURL(url).Replace("+", "%20");
    }

    string GetTodayGift()
    {
        var str = "";
        Gift_List.Clear();
        SendMessage<Data_User>(MessageID.Delegate_User_Info, (userdata) =>
        {
            var todayGuestList = userdata.ClientList.FindAll(t => t.Time.Year == DateTime.Now.Year && t.Time.Month == DateTime.Now.Month && t.Time.Date == DateTime.Now.Date);
            if (todayGuestList.Count > 0)
            {
                for (int i = 0; i < todayGuestList.Count; i++)
                {
                    int Cash = 0;
                    str += (i + 1).ToString() + "번. " + todayGuestList[i].Client_Name + " \n";
                    for (int a = 0; a < todayGuestList[i].Products.Count; a++)
                    {
                        str += "품명 : " + todayGuestList[i].Products[a].Product_Name + "\n";
                        str += "옵션 : " + todayGuestList[i].Products[a].Product_Option + "\n";
                        str += "제공 횟수 : " + todayGuestList[i].Products[a].Count + "\n";
                        str += "금액 : " + (todayGuestList[i].Products[a].Count * todayGuestList[i].Products[a].Price_Per_Person).ToString("C") + "\n\n";
                        Cash += Mathf.FloorToInt(todayGuestList[i].Products[a].Price_Per_Person);
                        AddGiftList(todayGuestList[i].Products[a]);
                    }
                    str += "총 금액 : " + Cash.ToString("C") + "\n";
                    str += "---------------------------------\n";
                }

                float totalCash = 0;
                for (int i = 0; i < Gift_List.Count; i++)
                {
                    str += "품번 : " + Gift_List[i].Idx + " / 품명 : " + Gift_List[i].Product_Name + " / 옵션 : " + Gift_List[i].Product_Option + " / 수량 : " + Gift_List[i].Count + "\n";
                    totalCash += Gift_List[i].Price_Per_Person * Gift_List[i].Count;
                }

                str += "총 지급 금액 : " + totalCash.ToString("C") + "원";
            }
            else
            {
                str = " 오늘은 손님이 없어요 ㅠㅠ ";
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
}
