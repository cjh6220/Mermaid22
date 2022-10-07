using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIBaseListVertical : UIBaseList
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
            slot.name = "Slot_" + i;
            slot.Set(i, _slots[i]);
            _slotList.Add(slot);
        }

        ScrollLock();
    }

    protected override void UpdateSize()
    {
        ContentWindow.GetComponent<RectTransform>().ForceUpdateRectTransforms();

        var size = ContentWindow.GetComponent<RectTransform>().sizeDelta;

        if (0 < _slots.Count)
        {
            int offset = Mathf.CeilToInt(_slots.Count / (float)_line);
            size.y = Mathf.Abs(_slotStart_Y) + (offset * _slotSpace_Y) - (_slotSpace_Y / 2.0f) + _addContentSizeY;
        }

        ContentWindow.GetComponent<RectTransform>().sizeDelta = size;

        var height = GetComponent<RectTransform>().rect.size.y;

        _baseShowSlotCount = (int)((height / _slotSpace_Y) + ShowAddValue + _showAddValue) * _line;
    }

    protected override void UpdatePos(Transform updateTransform, int index)
    {
        var pos = updateTransform.localPosition;
        pos.x = _slotStart_X + (_slotSpace_X * (index % _line));
        pos.y = _slotStart_Y - ((index / _line) * _slotSpace_Y);

        updateTransform.localPosition = pos;
    }

    protected override void MoveToItem(int index)
    {
        var scrollRect = GetComponentInChildren<ScrollRect>();

        var pos = 0.0f;
        if (null != scrollRect)
        {
            var count = _slots.Count;

            if (1 < _line)
            {
                count = (_slots.Count / _line);
                if (1 <= _slots.Count % _line)
                {
                    count++;
                }

                index = index % _line;
            }

            index = count - index;
            pos = (float)index / ((float)count);
        }

        scrollRect.verticalNormalizedPosition = pos;
    }

    void Update()
    {
        if (false == _set)
        {
            return;
        }

        if (0 <= ContentWindow.localPosition.y)
        {
            var checkY = ContentWindow.localPosition.y + _slotStart_Y;
            if (0 > checkY)
            {
                checkY = 0.0f;
            }

            var checkIndex = (int)(checkY / (_slotSpace_Y));

            if (_currentSlotIndex != checkIndex)
            {
                var minIndex = checkIndex * _line;
                var maxIndex = minIndex + _baseShowSlotCount;

                if (_slots.Count < maxIndex)
                {
                    maxIndex = _slots.Count;
                }

                for (int i = 0; i < _slotList.Count; i++)
                {
                    if (minIndex > _slotList[i].Index || (maxIndex - 1) < _slotList[i].Index)
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
            GetComponent<ScrollRect>().vertical = true;
        }
        else
        {
            GetComponent<ScrollRect>().vertical = false;
        }
    }

    void OnEnable()
    {
        var scrollRect = GetComponent<ScrollRect>();
        if (null != scrollRect)
        {
            scrollRect.movementType = ScrollRect.MovementType.Elastic;
        }
    }
}
