using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIListItemBase : UIBaseButton
{
    const float FadeInOutDuration = 0.2f;
    const float FadeInDelay = 0.1f;

    public int Index { get; private set; } = -1;
    protected object _data;

    List<Image> _fadeImages = new List<Image>();
    List<TextMeshProUGUI> _fadeTextMeshes = new List<TextMeshProUGUI>();

    public void Set(int index, object data)
    {
        gameObject.SetActive(true);

        Index = index;
        _data = data;

        UpdateInfo();
    }

    public void Reset()
    {
        gameObject.SetActive(false);

        Index = -1;

        ResetImpl();
    }

    public virtual void ReadyShow()
    {
        _fadeImages.Clear();
        _fadeTextMeshes.Clear();

        var images = GetComponentsInChildren<Image>();
        for (int i = 0; i < images.Length; i++)
        {
            var color = images[i].color;

            if (0.0f < color.a)
            {
                color.a = 0.0f;
                images[i].color = color;

                _fadeImages.Add(images[i]);
            }
        }

        var textMeshes = GetComponentsInChildren<TextMeshProUGUI>();
        for (int i = 0; i < textMeshes.Length; i++)
        {
            var color = textMeshes[i].color;

            if (0.0f < color.a)
            {
                color.a = 0.0f;
                textMeshes[i].color = color;

                _fadeTextMeshes.Add(textMeshes[i]);
            }
        }
    }

    public virtual void Show()
    {
        for (int i = 0; i < _fadeImages.Count; i++)
        {
            _fadeImages[i].DOFade(1.0f, FadeInOutDuration);
        }

        for (int i = 0; i < _fadeTextMeshes.Count; i++)
        {
            _fadeTextMeshes[i].DOFade(1.0f, FadeInOutDuration);
        }
    }

    public virtual void Hide()
    {
        for (int i = 0; i < _fadeImages.Count; i++)
        {
            _fadeImages[i].DOKill();
            _fadeImages[i].DOFade(0.0f, FadeInOutDuration);
        }

        for (int i = 0; i < _fadeTextMeshes.Count; i++)
        {
            _fadeTextMeshes[i].DOKill();
            _fadeTextMeshes[i].DOFade(0.0f, FadeInOutDuration);
        }
    }

    public virtual void UpdateInfo()
    {

    }

    protected virtual void ResetImpl()
    {

    }
}

