using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UILoader : MessageListener
{
    List<GameObject> _uis = new List<GameObject>();
    GameObject _currentWindow = null;
    Dictionary<string, bool> _windowPushTypes = new Dictionary<string, bool>();
    Queue<Msg_DeliveryData> _deliveryQueue = new Queue<Msg_DeliveryData>();

    int index = 0;
    string _currentScene = "";

    GameObject _canvasObj;
    protected override void AwakeImpl()
    {
        base.AwakeImpl();

        _canvasObj = transform.parent.gameObject;
    }

    protected override void AddMessageListener()
    {
        base.AddMessageListener();

        AddListener(MessageID.Event_SceneChange_End);
        AddListener(MessageID.Call_UI_Set);
        AddListener(MessageID.Call_UI_Push);
        AddListener(MessageID.Call_UI_Push_Popup);
        AddListener(MessageID.Call_UI_Pop);
        AddListener(MessageID.Call_UI_Push_Tutorial);
        AddListener(MessageID.Call_UI_Pop_Tutorial);
        AddListener(MessageID.Call_BackKey_Back);
        AddListener(MessageID.Call_UI_CanvasCamera);
    }

    protected override void OnMessage(MessageID msgID, object sender, object data)
    {
        switch (msgID)
        {
            case MessageID.Event_SceneChange_End:
                _currentScene = data.ToString();
                break;
            case MessageID.Call_UI_Set:
                {
                    for (int i = 0; i < _uis.Count; i++)
                    {
                        Destroy(_uis[i]);
                    }

                    _uis.Clear();
                    _currentWindow = null;

                    Push(false, data.ToString());

#if UNITY_EDITOR
                    Debug.Log("<color=#FE9A2E>Call_UI_Set</color>/" + data.ToString() + "/" + sender);
#endif
                }
                break;
            case MessageID.Call_UI_Push:
                {
                    // bool currentDestory = false;
                    // if (currentDestory)
                    // {
                    //     var removeWindow = _currentWindow;

                    //     Destroy(removeWindow);

                    //     _windowList.RemoveAt(_windowList.Count - 1);

                    //     _currentWindow = null;
                    //     if (0 < _windowList.Count)
                    //     {
                    //         _currentWindow = _windowList[_windowList.Count - 1];
                    //     }
                    // }

                    Push(false, data.ToString());

#if UNITY_EDITOR
                    Debug.Log("<color=#FE9A2E>Call_UI_Push</color>/" + data.ToString() + "/" + sender);
#endif
                }
                break;
            case MessageID.Call_UI_Push_Popup:
                {
                    Push(true, data.ToString());

#if UNITY_EDITOR
                    Debug.Log("<color=#FE9A2E>Call_UI_Push_Popup</color>/" + data.ToString() + "/" + sender);
#endif
                }
                break;

            case MessageID.Call_UI_Pop:
                {
                    Pop();
                }
                break;
            case MessageID.Call_BackKey_Back:
                {

                }
                break;
            case MessageID.Call_UI_CanvasCamera:
            {
                var callback = data as System.Action<Camera>;

                callback(_canvasObj.GetComponent<Canvas>().worldCamera);
            }
            break;
        }
    }

    GameObject CreateWindow(bool popup, string windowName)
    {
        index++;

        GameObject newWindow = null;
        newWindow = Instantiate(ResourceLoad.GetUI(windowName)) as GameObject;
        newWindow.name = windowName + "_" + index;
        newWindow.transform.SetParent(transform, false);
        newWindow.transform.SetAsLastSibling();

        _windowPushTypes.Add(newWindow.name, popup);

        _uis.Add(newWindow);

        return newWindow;
    }

    void Push(bool popup, string uiName)
    {
        if (null != _currentWindow)
        {
            _currentWindow.SetActive(popup);
        }

        _currentWindow = CreateWindow(popup, uiName);

        SendMessage(MessageID.Event_UI_Show, uiName);
    }

    void Pop()
    {
        //LogManager.LogHexa("UI 수 = " + _uis.Count);
        // if (1 == _uis.Count)
        // {
        //     return;
        // }

        if (null != _currentWindow)
        {
            if (0 < _uis.Count)
            {
                _uis.RemoveAt(_uis.Count - 1);
            }

            Destroy(_currentWindow);
        }

        if (0 < _uis.Count)
        {
            _currentWindow = _uis[_uis.Count - 1];
            _currentWindow.SetActive(true);

            SendMessage(MessageID.Event_UI_Show, GetUIName(_currentWindow.name));
        }
    }

    // IEnumerator PushWindowCoroutine(bool popup, string windowName, bool changeAction)
    // {
    //     yield return null;


    //     bool backeyRegist = false;
    //     if (null != _currentWindow)
    //     {
    //         backeyRegist = true;

    //         var hideWindow = _currentWindow;

    //         hideWindow.SetActive(popup);

    //         if (false == popup)
    //         {
    //             SendMessageImmediate(MessageID.Event_UI_Hide, GetWindowName(hideWindow.name));

    //             SendMessageImmediate(MessageID.Event_UI_Hidden, GetWindowName(hideWindow.name));
    //         }
    //     }

    //     _currentWindow = CreateWindow(popup, windowName);

    //     if (true == backeyRegist)
    //     {
    //         SendMessageImmediate(MessageID.Call_BackKey_Register, _currentWindow.name);
    //     }

    //     var showWindow = _currentWindow;

    //     yield return null;

    //     ShowWindow(GetWindowName(showWindow.name), showWindow, false, false);

    //     if (true == changeAction)
    //     {
    //         SendMessage(MessageID.Call_ChangeAction_Hide);
    //     }
    // }

    // void PushWindow(bool popup, string windowName, bool changeAction = false)
    // {
    //     _pushWindow = true;

    //     SendMessageImmediate(MessageID.Call_InputEvent_Lock);

    //     StartCoroutine(PushWindowCoroutine(popup, windowName, changeAction));
    // }

    // void PopWindow(string checkName)
    // {
    //     _pushWindow = false;

    //     SendMessageImmediate(MessageID.Call_InputEvent_Lock);

    //     if (null != _currentWindow)
    //     {
    //         if (_currentWindow.name.Equals(checkName))
    //         {
    //             if (0 < _windowList.Count)
    //             {
    //                 _windowList.RemoveAt(_windowList.Count - 1);
    //             }

    //             HideAndDestory_CurrentWindow();
    //         }
    //         else
    //         {
    //             SendMessageImmediate(MessageID.Call_InputEvent_Unlock);
    //         }
    //     }
    //     else
    //     {
    //         SendMessageImmediate(MessageID.Call_InputEvent_Unlock);
    //     }
    // }

    // void HideAndDestory_CurrentWindow()
    // {
    //     if (false == _windowPushTypes.ContainsKey(_currentWindow.name))
    //     {
    //         return;
    //     }

    //     if (true == _windowPushTypes[_currentWindow.name])
    //     {
    //         HideAndDestory(_currentWindow, () =>
    //         {
    //             if (0 < _windowList.Count)
    //             {
    //                 _currentWindow = _windowList[_windowList.Count - 1];

    //                 ShowWindow(GetWindowName(_currentWindow.name), _currentWindow, _windowPushTypes[_currentWindow.name], false);
    //             }
    //             else
    //             {
    //                 if (_currentScene == SceneName.LobbyScene.ToString())
    //                 {
    //                     switch (GetWindowName(_currentWindow.name))
    //                     {
    //                         case WindowUIName.AstroneDungeon:
    //                         case WindowUIName.PhantomTower:
    //                         case WindowUIName.Maze:
    //                         case WindowUIName.FastMission:
    //                             _currentWindow = null;
    //                             PushWindow(false, WindowUIName.World);
    //                             break;
    //                         case WindowUIName.CharacterInfo:
    //                             _currentWindow = null;
    //                             PushWindow(false, WindowUIName.CharacterManagement);
    //                             break;
    //                         default:
    //                             _currentWindow = null;
    //                             PushWindow(false, WindowUIName.Lobby);
    //                             break;
    //                     }
    //                 }
    //                 else
    //                 {
    //                     SendMessageImmediate(MessageID.Call_InputEvent_Unlock);
    //                 }
    //             }
    //         }, false);
    //     }
    //     else
    //     {
    //         if (0 < _windowList.Count)
    //         {
    //             HideAndDestory(_currentWindow, null, false);
    //             _currentWindow = _windowList[_windowList.Count - 1];

    //             ShowWindow(GetWindowName(_currentWindow.name), _currentWindow, _windowPushTypes[_currentWindow.name], true);
    //         }
    //         else
    //         {


    //             if (_currentScene == SceneName.LobbyScene.ToString())
    //             {
    //                 switch (GetWindowName(_currentWindow.name))
    //                 {
    //                     case WindowUIName.AstroneDungeon:
    //                     case WindowUIName.PhantomTower:
    //                     case WindowUIName.FastMission:
    //                     case WindowUIName.Maze:
    //                     case WindowUIName.Stage:
    //                         HideAndDestory(_currentWindow, null, true);

    //                         _currentWindow = null;
    //                         PushWindow(false, WindowUIName.World);
    //                         break;
    //                     case WindowUIName.SearchRewardLobby:
    //                         HideAndDestory(_currentWindow, null, true);

    //                         _currentWindow = null;
    //                         PushWindow(false, WindowUIName.World);
    //                         SendMessage(MessageID.Event_World_Tab_Change, false);
    //                         break;
    //                     default:
    //                         _uiPopToLobby = true;
    //                         SendMessage(MessageID.Call_ChangeAction_Show);
    //                         break;
    //                 }
    //             }
    //             else
    //             {
    //                 HideAndDestory(_currentWindow, null, true);

    //                 _currentWindow = null;

    //                 SendMessageImmediate(MessageID.Call_InputEvent_Unlock);
    //             }
    //         }
    //     }
    // }

    // void ShowWindow(string windowName, GameObject window, bool popWindow, bool showForce)
    // {
    //     if (false == popWindow)
    //     {
    //         window.SetActive(true);

    //         SendMessageImmediate(MessageID.Event_UI_Show, windowName);

    //         SendMessage(MessageID.Event_UI_Showed, windowName);

    //         SendMessage(MessageID.Call_InputEvent_Unlock);
    //     }
    //     else
    //     {
    //         if (false == window.activeSelf)
    //         {
    //             window.SetActive(true);

    //             SendMessageImmediate(MessageID.Event_UI_Show, windowName);

    //             SendMessage(MessageID.Event_UI_Showed, windowName);

    //             SendMessage(MessageID.Call_InputEvent_Unlock);
    //         }
    //         else
    //         {
    //             SendMessageImmediate(MessageID.Event_UI_Show, windowName);

    //             SendMessage(MessageID.Event_UI_Showed, windowName);

    //             SendMessage(MessageID.Call_InputEvent_Unlock);
    //         }
    //     }
    // }

    // void HideAndDestory(GameObject window, System.Action callBack, bool immediate)
    // {
    //     if (null == window)
    //     {
    //         return;
    //     }

    //     _windowPushTypes.Remove(window.name);

    //     Destroy(window);


    //     SendMessageImmediate(MessageID.Event_UI_Hide, GetWindowName(window.name));

    //     SendMessageImmediate(MessageID.Event_UI_Hidden, GetWindowName(window.name));

    //     if (null != callBack)
    //     {
    //         callBack();
    //     }
    // }

    string GetUIName(string checkName)
    {
        var index = checkName.ToString().LastIndexOf("_");
        var uiName = checkName.ToString().Substring(0, index);

        return uiName;
    }
}

