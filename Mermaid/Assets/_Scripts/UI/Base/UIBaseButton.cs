using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBaseButton : UIBase
{
    const float ButtonDelay = 0.3f;

    public Button Button;

    protected bool _buttonDelayOn = true;

    bool _firstClick = false;
    float _buttonDelayCheck = 0.0f;



    protected override void AwakeImpl()
    {
        base.AwakeImpl();

        if (null == Button)
        {
            Button = GetComponentInChildren<Button>(true);
            if (null == Button)
            {
                return;
            }       
        }
         Button.onClick.AddListener(OnClick);
    }

    protected override void AddMessageListener()
    {
        base.AddMessageListener();
    }

    protected override void OnMessage(MessageID msgID, object sender, object data)
    {
        base.OnMessage(msgID, sender, data);
    }

    public void OnClick()
    {
        if (0.0f >= _buttonDelayCheck)
        {
            if (true == _buttonDelayOn)
            {
                _buttonDelayCheck = ButtonDelay;
            }

            OnClickAudio();

            OnClickImpl();

            if (false == _firstClick)
            {
                _firstClick = true;
                OnFirstClickImpl();
            }
        }

    }

    public void ResetFirstClick()
    {
        _firstClick = false;
    }

    protected virtual void OnClickAudio()
    {
        SendMessage(MessageID.Call_Audio_PlayFX, String_Audio.ui_button);
    }

    protected virtual void OnClickImpl()
    {

    }

    protected virtual void OnFirstClickImpl()
    {

    }

    protected virtual void UpdateImpl()
    {

    }

    void Update()
    {
        if (true == _buttonDelayOn)
        {
            if (0 < _buttonDelayCheck)
            {
                _buttonDelayCheck -= Time.unscaledDeltaTime;
            }
        }

        UpdateImpl();
    }

    public void SetButtonEnable(bool enable)
    {
        if (null != Button)
        {
            Button.interactable = enable;
        }
    }
}
