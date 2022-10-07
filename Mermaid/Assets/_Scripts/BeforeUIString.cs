using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BeforeUIString : MessageListener
{
    TextMeshProUGUI _text;

    protected override void AwakeImpl()
    {
        base.AwakeImpl();

        UpdateText();
    }

    protected override void AddMessageListener()
    {
        base.AddMessageListener();

        AddListener(MessageID.Event_TextManager_ChangedLanguage);
    }

    protected override void OnMessage(MessageID msgID, object sender, object data)
    {
        base.OnMessage(msgID, sender, data);

        switch (msgID)
        {
            case MessageID.Event_TextManager_ChangedLanguage:
                UpdateText();
                break;
        }
    }

    public void UpdateText()
    {
        //TextManager.Instance.ChangeLanguage(TextManager.Language.None);
        //SetText(TextManager.Instance.GetText(name));
        SetText(BeforeTextManager.Instance.GetText(name));
    }

    public void UpdateText(BeforeTextManager.Language language)
    {
        // TextManager.Instance.ChangeLanguage(TextManager.Language.None);
        // TextManager.Instance.ChangeLanguage(language);
        //SetText(TextManager.Instance.GetText(name));
        BeforeTextManager.Instance.ChangeLanguage(BeforeTextManager.Language.None);
        BeforeTextManager.Instance.ChangeLanguage(language);
        SetText(BeforeTextManager.Instance.GetText(name));
    }

    public void SetName(string changeName)
    {
        name = changeName;
        UpdateText();
    }

    void SetText(string text)
    {
        if (null == _text)
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        _text.SetText(text);
    }
}
