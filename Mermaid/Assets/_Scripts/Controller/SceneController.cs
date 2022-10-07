using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class SceneController : MessageListener
{
    GameObject _currentScene;
    Queue<Msg_DeliveryData> _sceneDeliveryQueue = new Queue<Msg_DeliveryData>();

    Type_SceneName _loadReadyName;

    protected override void AddMessageListener()
    {
        base.AddMessageListener();

        AddListener(MessageID.Call_Scene_Load);
        AddListener(MessageID.Call_Scene_MesssageAfterLoad);

        AddListener(MessageID.Event_Loading_StartEnd);
    }

    protected override void OnMessage(MessageID msgID, object sender, object data)
    {
        base.OnMessage(msgID, sender, data);

        switch (msgID)
        {
            case MessageID.Call_Scene_Load:
                SendMessage(MessageID.Call_InputEvent_Lock);

                _loadReadyName = (Type_SceneName)data;

                switch (_loadReadyName)
                {
                    case Type_SceneName.LogoScene:
                        StartCoroutine(Load(_loadReadyName.ToString()));
                        break;
                    default:
                        StartCoroutine(Load(_loadReadyName.ToString()));
                        //SendMessage(MessageID.Call_Loading_Start);
                        break;
                }
                break;
            case MessageID.Call_Scene_MesssageAfterLoad:
                _sceneDeliveryQueue.Enqueue(data as Msg_DeliveryData);
                break;
            case MessageID.Event_Loading_StartEnd:
                {
                    StartCoroutine(Load(_loadReadyName.ToString()));
                }
                break;
        }
    }

    IEnumerator Load(string sceneName)
    {
        SendMessage(MessageID.Event_SceneChange_Start);

        yield return null;

        var async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        async.allowSceneActivation = true;

        bool isLogoToLobby = (sceneName == "LobbyScene" && _currentScene.name == "LogoScene");

        while (false == async.isDone)
        {
            yield return async;
        }

        if (null != _currentScene)
        {
            _currentScene.SetActive(false);
            async = SceneManager.UnloadSceneAsync(_currentScene.name);

            while (false == async.isDone)
            {
                yield return async;
            }
        }

        yield return null;

        _currentScene = GameObject.Find(sceneName);

        if (null == _currentScene)
        {
            LogManager.LogError("Load Scene : NULL !!! [" + sceneName + "]");
        }
        else
        {
            SendSceneDeliveryMessage();

            yield return null;

            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));

            MessageManager.Instance.Send(MessageID.Event_SceneChange_End, this, sceneName);
        }

        yield return null;

        SendMessage(MessageID.Call_InputEvent_Unlock);

        switch (_loadReadyName)
        {
            case Type_SceneName.LogoScene:
                break;
            case Type_SceneName.BattleScene:
                break;
            case Type_SceneName.CoopScene:
                break;
            case Type_SceneName.TutorialScene:
                break;
            default:
                SendMessage(MessageID.Call_Loading_End);
                break;
        }

        switch ((Type_SceneName)_loadReadyName)
        {
            case Type_SceneName.TitleScene:
                SendMessage(MessageID.Call_Audio_PlayBGM, String_Audio.bgm_lobby);
                break;
            case Type_SceneName.LobbyScene:
                SendMessage(MessageID.Call_IsLogin_State, isLogoToLobby);
                SendMessage(MessageID.Call_Audio_PlayBGM, String_Audio.bgm_lobby);
                break;
            case Type_SceneName.BattleScene:
            case Type_SceneName.TutorialScene:
                SendMessage(MessageID.Call_Audio_PlayBGM, String_Audio.bgm_ingame);
                break;
            //case Type_SceneName.CoopScene:
            //    SendMessage(MessageID.Call_Audio_PlayBGM, String_Audio.bgm_coop);
                break;
            default:
                SendMessage(MessageID.Call_Audio_PlayBGM, String_Audio.bgm_lobby);
                break;
        }
    }

    void SendSceneDeliveryMessage()
    {
        while (0 < _sceneDeliveryQueue.Count)
        {
            var data = _sceneDeliveryQueue.Dequeue();

            MessageManager.Instance.Send(data.SendMessageID, this, data.SendMessageData);
        }
    }
}
