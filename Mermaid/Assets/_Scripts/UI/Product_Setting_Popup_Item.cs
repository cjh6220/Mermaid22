using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Product_Setting_Popup_Item : MessageListener
{
    public Image NameBG;
    public Text Product_Name;
    public Text Total_Count;
    public Text Inside_Count;
    public Text Per_Count;
    public Text Present_Count;
    public Text AS_Count;
    public Text Remain_Count;
    public Text Price_Per_Count;
    public Button Btn;
    public Button RemoveBtn;
    Product_Setting_Popup.ProductGroup Group;

    protected override void AddMessageListener()
    {
        base.AddMessageListener();
        Btn.onClick.AddListener(OnClickBtn);
        RemoveBtn.onClick.AddListener(OnClickRemove);
        AddListener(MessageID.OnClick_Product_Edit_Button);
    }

    protected override void OnMessage(MessageID msgID, object sender, object data)
    {
        base.OnMessage(msgID, sender, data);

        switch (msgID)
        {
            case MessageID.OnClick_Product_Edit_Button:
                {
                    var info = (int)data;

                    if (info == Group.Product_Idx)
                    {
                        NameBG.color = new Color(0.8f, 0.8f, 0.8f);
                    }
                    else
                    {
                        if (GetRemainCount(Group) > 0)
                        {
                            NameBG.color = Color.white;
                        }
                        else
                        {
                            NameBG.color = new Color(0.9528302f, 0.2800408f, 0.2471965f);
                        }
                    }
                }
                break;
        }
    }

    public void SetItem(Product_Setting_Popup.ProductGroup data)
    {
        Group = data;
        NameBG.color = Color.white;
        Product_Name.text = data.Products[0].Product_Name;
        Total_Count.text = GetTotalCount(data).ToString();
        Inside_Count.text = data.Products[0].Box_Per_Count.ToString();
        Per_Count.text = data.Products[0].Person_Per_Count.ToString();
        Present_Count.text = GetPresentCount(data).ToString();
        AS_Count.text = GetErrorCount(data).ToString();
        Remain_Count.text = GetRemainCount(data).ToString();
        Price_Per_Count.text = (data.Products[0].Price_Per_Person).ToString("C");
    }

    int GetTotalCount(Product_Setting_Popup.ProductGroup data)
    {
        int count = 0;
        for (int i = 0; i < data.Products.Count; i++)
        {
            count += data.Products[i].Box_Count;
        }
        return count;
    }

    int GetPresentCount(Product_Setting_Popup.ProductGroup data)
    {
        int count = 0;
        for (int i = 0; i < data.Products.Count; i++)
        {
            count += data.Products[i].Total_Person;
        }
        return count;
    }

    int GetErrorCount(Product_Setting_Popup.ProductGroup data)
    {
        int count = 0;
        for (int i = 0; i < data.Products.Count; i++)
        {
            count += data.Products[i].Error_Count;
        }
        return count;
    }

    int GetRemainCount(Product_Setting_Popup.ProductGroup data)
    {
        float count = 0;
        for (int i = 0; i < data.Products.Count; i++)
        {
            count += data.Products[i].Remain_Count;
        }
        if (Mathf.FloorToInt(count) <= 0)
        {
            ChangeColor();
        }
        return Mathf.FloorToInt(count);
    }

    void ChangeColor()
    {
        NameBG.color = new Color(0.9528302f, 0.2800408f, 0.2471965f);
    }

    void OnClickBtn()
    {
        SendMessage(MessageID.OnClick_Product_Edit_Button, Group.Product_Idx);
    }

    void OnClickRemove()
    {
        SendMessage(MessageID.Call_UI_Push_Popup, String_UIName.Popup_Really_Remove);
        SendMessage(MessageID.Event_Open_Remove_Product_Popup, Group.Product_Idx);
    }
}
