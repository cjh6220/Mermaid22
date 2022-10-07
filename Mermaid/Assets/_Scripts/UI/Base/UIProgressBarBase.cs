using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIProgressBarBase : MessageListener
{
    const float GageFillAnimationDuration = 0.2f;

    public Scrollbar Bar;
    public float Value { get => _value; }

    private float _value;
    private bool _isSet;

    protected override void AwakeImpl()
    {
        base.AwakeImpl();

        if (null == Bar)
        {
            Bar = GetComponent<Scrollbar>();
            if (null == Bar)
            {
                LogManager.LogWarning("UIScrollBarBase:" + name);
                return;
            }
        }

        if (_isSet)
        {
            Bar.size = _value;
        }
    }

    protected override void AddMessageListener()
    {
        base.AddMessageListener();
    }

    protected override void OnMessage(MessageID msgID, object sender, object data)
    {
        base.OnMessage(msgID, sender, data);
    }

    public void SetValue(float value)
    {
        if (0.0f > value)
        {
            value = 0.0f;
        }

        if (Bar == null)
        {
            _isSet = true;
            _value = value;
            return;
        }

        Bar.size = value;
    }

    public void SetValue_Animation(float value, float time, TweenCallback callBack = null)
    {
        if (0.0f > value)
        {
            value = 0.0f;
        }

        if (Bar == null)
        {
            _isSet = true;
            _value = value;
            return;
        }

        DOTween.To(() => Bar.size, (size) => Bar.size = size, value, time).
            OnComplete(callBack).SetUpdate(true);
    }
}
