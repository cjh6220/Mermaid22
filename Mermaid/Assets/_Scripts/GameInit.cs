using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using AM97Soft;

using OfficeOpenXml;
using Excel;
//using GameAnalyticsSDK;

public class GameInit : MessageListener
{
    protected override void AwakeImpl()
    {
        base.AwakeImpl();

        LogManager.Init();                 

#if CHEAT
        gameObject.AddComponent<CheatCommandController>();
#endif

#if UNITY_EDITOR
        Application.runInBackground = true;
#else
        Application.runInBackground = false;
#endif

        Input.multiTouchEnabled = false;

        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        Table_Manager.Instance.Init();
        //TextManager.Instance.Init();
        //TextManager.Instance.ChangeLanguage(TextManager.Language.EN, false);

        BeforeTextManager.Instance.Init();
        //BeforeTextManager.Instance.ChangeLanguage(BeforeTextManager.Language.KO, false);

        //GameAnalytics.Initialize();
    }

    protected override void AddMessageListener()
    {
        base.AddMessageListener();
    }

    protected override void OnMessage(MessageID msgID, object sender, object data)
    {
        base.OnMessage(msgID, sender, data);
    }

    private IEnumerator Start()
    {
        yield return null;

        StartGame();        
    }

    void StartGame()
    {
        //SendMessage(MessageID.Call_Audio_Ready);

        // RootChecker.Init();
        // if (true == RootChecker.CheckIfRooted())
        // {
        //     var msg = new Msg_Popup();
        //     msg.Text = TextManager.Instance.GetText("ui_androidrooting_check");
        //     msg.CallBack = (yes, index) =>
        //         {
        //             GameUtil.ApplicationQuit();
        //         };

        //     MessageManager.SendMessageImmediate(MessageID.Call_Popup_ShowDialog_OK, msg);
        // }
        // else
        // {
        //     MessageManager.Instance.Send(MessageID.Call_Scene_Load, null, SceneName.LogoScene);
        // }

        SendMessageDelay(MessageID.Call_Scene_Load, Type_SceneName.TableScene);
    }

    void Update()
    {
      //  LogManager.Update();
    }
}