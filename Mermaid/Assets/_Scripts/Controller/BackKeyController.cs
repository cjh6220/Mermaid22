using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BackKeyController : MessageListener
{
    // bool _inputLock = false;
    // bool _inputForceLock = false;
    // bool _playing = false;

    // Stack<string> _windowStack = new Stack<string>();
    // string _currentSceneName = "";
    // string _currentWindowUIName = "";
    // bool _showExitPopup = false;
    // bool _tutorial = false;
    // bool _megaMenuShowed = false;

    // Dictionary<string, bool> _manualWindow = new Dictionary<string, bool>();

    // protected override void AwakeImpl()
    // {
    //     base.AwakeImpl();

    //     DontDestroyOnLoad(gameObject);
    // }

    // protected override void AddMessageListener()
    // {
    //     base.AddMessageListener();

    //     AddListener(MessageID.Event_UI_Show);
    //     AddListener(MessageID.Event_UI_Hide);
    //     AddListener(MessageID.Call_MegaMenu_Show);
    //     AddListener(MessageID.Call_MegaMenu_Hide);

    //     AddListener(MessageID.Call_Scene_Load);

    //     AddListener(MessageID.Call_BackKey_SetManual);
    //     AddListener(MessageID.Call_BackKey_Register);
    //     AddListener(MessageID.Call_BackKey_Unregister);
    //     AddListener(MessageID.Call_BackKey_AllUnregister);
    //     AddListener(MessageID.Call_BackKey_Enable);
    //     AddListener(MessageID.Call_BackKey_Disable);
    //     AddListener(MessageID.Call_BackKey_PopBack);
    //     AddListener(MessageID.Call_PopupDialog_Exit);

    //     AddListener(MessageID.Call_InputEvent_Lock);
    //     AddListener(MessageID.Call_InputEvent_Unlock);
    //     AddListener(MessageID.Call_InputEvent_BackKey_Lock);
    //     AddListener(MessageID.Call_InputEvent_BackKey_Unlock);
    //     AddListener(MessageID.Call_InputEvent_BackKey_ForceLock);
    //     AddListener(MessageID.Call_InputEvent_BackKey_ForceUnlock);

    //     AddListener(MessageID.Event_Detection_Cheet);

    //     AddListener(MessageID.Event_SceneChange_Start);
    //     AddListener(MessageID.Event_SceneChange_End);

    //     AddListener(MessageID.Request_Net_Packet);
    //     AddListener(MessageID.Request_Net_Guild);
    //     AddListener(MessageID.Request_Net_Lobby);

    //     AddListener(MessageID.Response_Net_Packet);
    //     AddListener(MessageID.Response_Net_Error);
    //     AddListener(MessageID.Response_Net_Guild);
    //     AddListener(MessageID.Response_Net_GuildError);
    //     AddListener(MessageID.Response_Net_Lobby);
    //     AddListener(MessageID.Response_Net_LobbyError);

    //     AddListener(MessageID.Event_Tutorial_Begin);
    //     AddListener(MessageID.Event_Tutorial_End);
    // }

    // protected override void OnMessage(MessageID msgID, object sender, object data)
    // {
    //     base.OnMessage(msgID, sender, data);

    //     switch (msgID)
    //     {
    //         case MessageID.Event_UI_Show:
    //             _currentWindowUIName = data.ToString();
    //             _inputLock = true;
    //             StopAllCoroutines();
    //             break;
    //         case MessageID.Event_UI_Hide:
    //             {
    //                 if (WindowUIName.InGamePause.Equals(data) ||
    //                     WindowUIName.BattleDetailInfo.Equals(data) ||
    //                     WindowUIName.BattleMaze_SurvivalMaiden.Equals(data) ||
    //                     WindowUIName.ShopDiaLackOkPopup.Equals(data) ||
    //                     WindowUIName.ShopCometLackOkPopup.Equals(data))
    //                 {
    //                     _currentWindowUIName = "";
    //                 }
    //             }
    //             break;
    //         case MessageID.Call_MegaMenu_Show:
    //             _megaMenuShowed = true;
    //             break;
    //         case MessageID.Call_MegaMenu_Hide:
    //             _megaMenuShowed = false;
    //             break;
    //         case MessageID.Call_Scene_Load:
    //             _currentSceneName = data.ToString();
    //             _currentWindowUIName = "";

    //             _inputLock = true;
    //             StopAllCoroutines();

    //             while (0 < _windowStack.Count)
    //             {
    //                 var popup = _windowStack.Pop();
    //                 if (popup.Contains(PopupController.Popup))
    //                 {
    //                     SendMessageImmediate(MessageID.Call_BackKey_Back, popup);
    //                 }
    //             }
    //             _windowStack.Clear();
    //             break;
    //         case MessageID.Call_BackKey_SetManual:
    //             if (false == _manualWindow.ContainsKey(data.ToString()))
    //             {
    //                 _manualWindow.Add(data.ToString(), true);
    //             }
    //             break;
    //         case MessageID.Call_BackKey_Register:
    //             _windowStack.Push(data.ToString());
    //             break;

    //         case MessageID.Call_BackKey_Unregister:
    //             if (0 < _windowStack.Count)
    //             {
    //                 _windowStack.Pop();
    //             }

    //             _manualWindow.Remove(data.ToString());
    //             //Debug.Log("Call_BackKey_Unregister:" + data.ToString() + "/" + _manualWindow.Count);
    //             break;
    //         case MessageID.Call_BackKey_AllUnregister:
    //             _windowStack.Clear();
    //             break;
    //         case MessageID.Call_BackKey_Enable:
    //             break;
    //         case MessageID.Call_BackKey_Disable:
    //             break;
    //         case MessageID.Call_BackKey_PopBack:
    //             {
    //                 PopBack();
    //             }
    //             break;
    //         case MessageID.Call_PopupDialog_Exit:
    //             {
    //                 ShowExitPopup();
    //             }
    //             break;
    //         case MessageID.Call_InputEvent_Lock:
    //             _inputLock = true;
    //             break;
    //         case MessageID.Call_InputEvent_Unlock:
    //             _inputLock = false;
    //             break;
    //         case MessageID.Call_InputEvent_BackKey_Lock:
    //             _inputLock = true;
    //             break;
    //         case MessageID.Call_InputEvent_BackKey_Unlock:
    //             _inputLock = false;
    //             break;
    //         case MessageID.Call_InputEvent_BackKey_ForceLock:
    //             _inputForceLock = true;
    //             break;
    //         case MessageID.Call_InputEvent_BackKey_ForceUnlock:
    //             _inputForceLock = false;
    //             break;
    //         case MessageID.Event_Detection_Cheet:
    //             _inputLock = true;
    //             break;
    //         case MessageID.Event_SceneChange_Start:
    //             _inputLock = true;
    //             break;
    //         case MessageID.Event_SceneChange_End:
    //             {
    //                 if (SceneName.BattleStageScene.ToString().Equals(data) ||
    //                     SceneName.BattlePvPScene.ToString().Equals(data) ||
    //                     SceneName.BattleMazeScene.ToString().Equals(data))
    //                 {
    //                     _currentWindowUIName = "";
    //                 }
    //                 _inputLock = false;
    //             }
    //             break;
    //         case MessageID.Request_Net_Packet:
    //         case MessageID.Request_Net_Guild:
    //         case MessageID.Request_Net_Lobby:
    //             _inputLock = true;
    //             break;
    //         case MessageID.Response_Net_Packet:
    //         case MessageID.Response_Net_Error:
    //         case MessageID.Response_Net_Guild:
    //         case MessageID.Response_Net_GuildError:
    //         case MessageID.Response_Net_Lobby:
    //         case MessageID.Response_Net_LobbyError:
    //             _inputLock = false;
    //             break;
    //         case MessageID.Event_Tutorial_Begin:
    //             _tutorial = true;
    //             break;
    //         case MessageID.Event_Tutorial_End:
    //             _tutorial = false;
    //             break;
    //     }
    // }

    // private void Update()
    // {
    //     if (true == _inputForceLock)
    //     {
    //         return;
    //     }

    //     if (true == _inputLock)
    //     {
    //         return;
    //     }

    //     if (true == TutorialDataManager.Instance.IsTutorialPlay)
    //     {
    //         return;
    //     }

    //     if (Input.GetKeyDown(KeyCode.Escape))
    //     {
    //         StopAllCoroutines();
    //         StartCoroutine(CheckPopBack());
    //     }
    // }

    // IEnumerator CheckPopBack()
    // {
    //     yield return new WaitForSecondsRealtime(0.1f);

    //     if (false == _inputLock)
    //     {
    //         PopBack();
    //     }
    // }

    // void PopBack()
    // {
    //     if (true == _megaMenuShowed)
    //     {
    //         SendMessage(MessageID.Call_MegaMenu_Hide);
    //         return;
    //     }

    //     if (true == _tutorial)
    //     {
    //         if (true == _showExitPopup)
    //         {
    //             if (0 < _windowStack.Count)
    //             {
    //                 SendMessage(MessageID.Call_Audio_PlayFX, Audio.ui_button_back);

    //                 var key = _windowStack.Pop();
    //                 SendMessageImmediate(MessageID.Call_BackKey_Back, key);
    //             }
    //         }
    //         else
    //         {
    //             SendMessage(MessageID.Call_Audio_PlayFX, Audio.ui_button_back);
    //             ShowExitPopup();
    //         }
    //     }
    //     else
    //     {
    //         if (_currentWindowUIName.Equals(WindowUIName.StoryViewer))
    //         {
    //             if (0 < _windowStack.Count)
    //             {
    //                 SendMessage(MessageID.Call_Audio_PlayFX, Audio.ui_button_back);

    //                 var key = _windowStack.Pop();

    //                 if (true == _manualWindow.ContainsKey(key))
    //                 {
    //                     _windowStack.Push(key);
    //                     SendMessageImmediate(MessageID.Event_BackKey_Back, key);
    //                 }
    //                 else
    //                 {
    //                     SendMessageImmediate(MessageID.Call_BackKey_Back, key);
    //                 }
    //             }
    //             else
    //             {
    //                 ShowExitPopup();
    //             }
    //         }
    //         else
    //         {
    //             if (_currentWindowUIName.Equals(WindowUIName.Battle_Result_Failure) ||
    //                 _currentWindowUIName.Equals(WindowUIName.Battle_Result_Success) ||
    //                 _currentWindowUIName.Equals(WindowUIName.PvPBattle_Result_Failure) ||
    //                 _currentWindowUIName.Equals(WindowUIName.PvPBattle_Result_Success) ||
    //                 _currentWindowUIName.Equals(WindowUIName.BattleMaze_Result_Success) ||
    //                 _currentWindowUIName.Equals(WindowUIName.BattleMaze_Result_Failure))
    //             {
    //                 if (_currentWindowUIName.Equals(WindowUIName.PvPBattle_Result_Success) &&
    //                     UIStateInfoManager.Instance.ContainsKey(UIStateInfoManager.Key.PvP_Quick))
    //                 {
    //                     UIStateInfoManager.Instance.Remove(UIStateInfoManager.Key.PvP_Quick);
    //                     UIStateInfoManager.Instance.SetInfo(UIStateInfoManager.Key.PvP_Result_PrevGrade, 0);
    //                     UIStateInfoManager.Instance.SetInfo(UIStateInfoManager.Key.PvP_Result_CurrentGrade, 0);

    //                     SendMessage(MessageID.Call_UI_Pop);
    //                 }
    //                 else
    //                 {
    //                     if (true == _showExitPopup)
    //                     {
    //                         if (0 < _windowStack.Count)
    //                         {
    //                             SendMessage(MessageID.Call_Audio_PlayFX, Audio.ui_button_back);

    //                             var key = _windowStack.Pop();
    //                             SendMessageImmediate(MessageID.Call_BackKey_Back, key);
    //                         }
    //                     }
    //                     else
    //                     {
    //                         SendMessage(MessageID.Call_Audio_PlayFX, Audio.ui_button_back);
    //                         ShowExitPopup();
    //                     }
    //                 }
    //             }
    //             else
    //             {
    //                 if (0 < _windowStack.Count)
    //                 {
    //                     SendMessage(MessageID.Call_Audio_PlayFX, Audio.ui_button_back);

    //                     var key = _windowStack.Pop();

    //                     if (true == _manualWindow.ContainsKey(key))
    //                     {
    //                         _windowStack.Push(key);
    //                         SendMessageImmediate(MessageID.Event_BackKey_Back, key);
    //                     }
    //                     else
    //                     {
    //                         SendMessageImmediate(MessageID.Call_BackKey_Back, key);
    //                     }
    //                 }
    //                 else
    //                 {
    //                     if (_currentWindowUIName.Equals(WindowUIName.Lobby) ||
    //                         _currentWindowUIName.Equals(WindowUIName.Intro) ||
    //                         _currentWindowUIName.Equals(WindowUIName.ContentsDownload))
    //                     {
    //                         ShowExitPopup();
    //                     }
    //                     else if (string.IsNullOrEmpty(_currentWindowUIName))
    //                     {
    //                         SendMessageImmediate(MessageID.Show_Battle_Pause);
    //                     }
    //                     else
    //                     {
    //                         SendMessageImmediate(MessageID.Call_UI_Pop);
    //                     }
    //                 }
    //             }
    //         }


    //     }




    // }

    // void ShowPausePopup()
    // {
    //     //SendMessage(MessageID.Show_PlayPause);
    // }

    // void ShowExitPopup()
    // {
    //     //SendMessage(MessageID.Call_Audio_PlayFX, Audio.ui_effect_pop_open);

    //     _showExitPopup = true;

    //     TimeManager.Pause();

    //     var msgData = new Msg_Popup()
    //     {
    //         Text = TextManager.Instance.GetText("ui_quit"),
    //         YesText = TextManager.Instance.GetText("ui_ok"),
    //         NoText = TextManager.Instance.GetText("ui_cancel"),
    //         CallBack = (yes, index) =>
    //         {
    //             if (yes)
    //             {
    //                 SendMessageImmediate(MessageID.Request_Net_Packet, new Network.Packet_UserLogout());

    //                 GameUtil.ApplicationQuit();
    //             }
    //             else
    //             {
    //                 TimeManager.Resume();
    //             }

    //             _showExitPopup = false;
    //         }
    //     };

    //     SendMessageImmediate(MessageID.Call_Popup_ShowDialog_YesNo, msgData);
    // }

    // private void OnApplicationPause(bool pause)
    // {

    // }
}
