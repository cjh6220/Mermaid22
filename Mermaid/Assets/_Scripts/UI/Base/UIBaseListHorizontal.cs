using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBaseListHorizontal : UIBaseList
{
    protected override void InitSlot()
    {
        UpdateSize();

        int count = _baseShowSlotCount;
        if (_slots.Count < count)
        {
            count = _slots.Count;
        }

        for (int i = 0; i < count; i++)
        {
            var slotObj = CreateSlot();

            UpdatePos(slotObj.transform, i);

            var slot = slotObj.GetComponent<UIBaseListItem>();
            slot.Set(i, _slots[i]);
            slot.name = "Slot_" + i;
            _slotList.Add(slot);
        }

        ScrollLock();
    }

    protected override void UpdateSize()
    {
        var size = ContentWindow.GetComponent<RectTransform>().sizeDelta;
        if (0 < _slots.Count)
        {
            int offset = Mathf.CeilToInt(_slots.Count / (float)_line);

            size.x = Mathf.Abs(_slotStart_X) + (offset * _slotSpace_X) - (_slotSpace_X / 2.0f) + _addContentSizeX;
        }

        ContentWindow.GetComponent<RectTransform>().sizeDelta = size;

        var width = GetComponent<RectTransform>().rect.size.x;

        _baseShowSlotCount = (int)((width / _slotSpace_X)) * _line;
        _baseShowSlotCount += ShowAddValue + _showAddValue;
    }

    protected override void UpdatePos(Transform updateTransform, int index)
    {
        var pos = updateTransform.localPosition;
        pos.x = _slotStart_X + ((index / _line) * _slotSpace_X);
        pos.y = _slotStart_Y - (_slotSpace_Y * (index % _line));

        updateTransform.localPosition = pos;
    }

    protected override void MoveToItem(int index)
    {
        var scrollRect = GetComponentInChildren<ScrollRect>();

        var pos = 0.0f;
        if (0 < index)
        {
            if (null != scrollRect)
            {
                pos = ((float)index / (float)_line) / ((float)_slots.Count - 1);
            }
        }

        if (_slots.Count - 1 == index)
        {
            pos = 1.0f;
        }

        scrollRect.horizontalNormalizedPosition = pos;
    }

    protected virtual void UpdateImpl()
    {

    }

    void Update()
    {
        UpdateImpl();

        if (false == _set)
        {
            return;
        }

        if (0 >= ContentWindow.localPosition.x)
        {
            var checkIndex = (int)Mathf.Abs((ContentWindow.localPosition.x + _slotStart_X) / _slotSpace_X) - 1;

            if (_currentSlotIndex != checkIndex)
            {
                var minIndex = (checkIndex * _line);
                if (0 > minIndex)
                {
                    minIndex = 0;
                }

                var maxIndex = (minIndex + _baseShowSlotCount);

                if (_slots.Count < maxIndex)
                {
                    maxIndex = _slots.Count;
                }

                for (int i = 0; i < _slotList.Count; i++)
                {
                    if (minIndex >= _slotList[i].Index || (maxIndex - 1) < _slotList[i].Index)
                    {
                        _slotList[i].Reset();
                    }
                }

                for (int i = minIndex; i < maxIndex; i++)
                {
                    if (0 > _slotList.FindIndex((checkSlot) => checkSlot.Index.Equals(i)))
                    {
                        for (int slotIndex = 0; slotIndex < _slotList.Count; slotIndex++)
                        {
                            if (0 > _slotList[slotIndex].Index)
                            {
                                _slotList[slotIndex].Set(i, _slots[i]);
                                UpdatePos(_slotList[slotIndex].transform, i);
                                break;
                            }
                        }
                    }
                }

                _currentSlotIndex = checkIndex;

                //ConsoleProDebug.Watch("Min Max", minIndex + "/" + maxIndex);
            }

            //ConsoleProDebug.Watch("Check", _currentSlotIndex.ToString());
        }

        _lastContentPosition = ContentWindow.localPosition;
    }

    void ScrollLock()
    {
        if (_line * ScrollableMaxItemCount < _slotList.Count)
        {
            GetComponent<ScrollRect>().horizontal = true;
        }
        else
        {
            GetComponent<ScrollRect>().horizontal = false;
        }
    }
}
