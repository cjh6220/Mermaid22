using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBaseList : UIBase
{
    public Transform ContentWindow;
    public Object SlotObject;
    public int ScrollableMaxItemCount = 0;

    protected const int ShowAddValue = 6;

    protected float _slotStart_X = 0.0f;
    protected float _slotStart_Y = 0.0f;
    protected float _slotSpace_X = 0.0f;
    protected float _slotSpace_Y = 0.0f;
    protected float _addContentSizeX = 0.0f;
    protected float _addContentSizeY = 0.0f;
    protected int _showAddValue = 0;

    protected int _currentSlotIndex = 0;
    protected int _baseShowSlotCount = 0;
    protected int _line = 1;

    protected List<UIBaseListItem> _slotList = new List<UIBaseListItem>();
    protected List<object> _slots = null;

    protected bool _set = false;

    protected Vector2 _lastContentPosition;

    protected void Set(float slotStartX, float slotStartY, float slotSpaceX, float slotSpaceY, int line, List<object> dataList)
    {
        _slotStart_X = slotStartX;
        _slotStart_Y = slotStartY;
        _slotSpace_X = slotSpaceX;
        _slotSpace_Y = slotSpaceY;
        _slots = dataList;
        _line = line;

        InitSlot();

        _set = true;

        ContentWindow.localPosition = _lastContentPosition;
    }

    protected void SetSlots(List<object> datas)
    {
        _slots = datas;

        UpdateSize();

        int count = _baseShowSlotCount;
        if (_slots.Count < count)
        {
            count = _slots.Count;
        }

        for (int i = 0; i < count; i++)
        {
            var slotObj = GetSlot(i);

            UpdatePos(slotObj.transform, i);

            var slot = slotObj.GetComponent<UIBaseListItem>();
            slot.Set(i, _slots[i]);
            slot.name = "Slot_" + i;
        }

        if (count < _slotList.Count)
        {
            for (int i = count; i < _slotList.Count; i++)
            {
                Destroy(_slotList[i].gameObject);
            }

            _slotList.RemoveRange(count, _slotList.Count - count);
        }

        _set = true;
        _currentSlotIndex = 0;
    }

    protected void Reset()
    {
        _currentSlotIndex = 0;
        _baseShowSlotCount = 0;
        _set = false;

        for (int i = 0; i < _slotList.Count; i++)
        {
            Destroy(_slotList[i].gameObject);
        }

        _slotList.Clear();
        _slots = null;

        var scrollRect = GetComponentInChildren<ScrollRect>(true);
        if (null != scrollRect)
        {
            GetComponentInChildren<ScrollRect>().StopMovement();
        }

        ContentWindow.localPosition = Vector3.zero;
    }

    public void ReadyShow()
    {
        for (int i = 0; i < _slotList.Count; i++)
        {
            _slotList[i].ReadyShow();
        }
    }

    public void Show()
    {
        for (int i = 0; i < _slotList.Count; i++)
        {
            _slotList[i].Show();
        }
    }

    public void Hide()
    {
        for (int i = 0; i < _slotList.Count; i++)
        {
            _slotList[i].Hide();
        }
    }

    protected void UpdateInfos()
    {
        for (int i = 0; i < _slotList.Count; i++)
        {
            _slotList[i].UpdateInfo();
        }
    }

    protected GameObject CreateSlot()
    {
        var slotObj = Instantiate(SlotObject) as GameObject;
        slotObj.transform.SetParent(ContentWindow, false);

        return slotObj;
    }

    protected GameObject GetSlot(int index)
    {
        GameObject slotObj = null;

        if (_slotList.Count <= index)
        {
            slotObj = CreateSlot();
            _slotList.Add(slotObj.GetComponent<UIBaseListItem>());
        }
        else
        {
            slotObj = _slotList[index].gameObject;
        }

        return slotObj;
    }

    protected virtual void UpdateSize()
    {

    }

    protected virtual void MoveToItem(int index)
    {
        var scrollRect = GetComponentInChildren<ScrollRect>();
        if (null != scrollRect)
        {
            var length = (_slotList.Count / _line) * _slotSpace_X;
            var pos = (_slotSpace_X * index) - _slotSpace_X;

            var normalizedPosition = scrollRect.normalizedPosition;
            normalizedPosition.x = pos / length;
            scrollRect.normalizedPosition = normalizedPosition;
        }
    }

    protected virtual void InitSlot()
    {

    }

    protected virtual void UpdatePos(Transform updateTransform, int index)
    {

    }


}
