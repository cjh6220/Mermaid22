using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MessageListener
{
    protected Data_User _userData;

    bool _init = false;
    bool _setUserData = false;

    protected override void AwakeImpl()
    {
        base.AwakeImpl();
    }

    protected override void AddMessageListener()
    {
        base.AddMessageListener();

        //AddListener(MessageID.Event_Info_UserData);
        AddListener(MessageID.Event_InfoUpdate_UserData);
    }

    protected override void OnMessage(MessageID msgID, object sender, object data)
    {
        base.OnMessage(msgID, sender, data);

        switch (msgID)
        {
            // case MessageID.Event_Info_UserData:
            //     {
            //         if (false == _setUserData)
            //         {
            //             _userData = data as Data_User;

            //             SetUserData(_userData);

            //             _setUserData = true;
            //         }
            //     }
            //     break;
            case MessageID.Event_InfoUpdate_UserData:
                {
                    _userData = data as Data_User;

                    UpdateUserData(_userData);
                }
                break;
        }
    }

    protected virtual void Init()
    {

    }

    protected virtual void SetUserData(Data_User userData)
    {

    }

    protected virtual void UpdateUserData(Data_User userData)
    {

    }

    protected override void OnEnableImpl()
    {
       
        base.OnEnableImpl();

        if (false == _setUserData)
        {
            SendMessage<Data_User>(MessageID.Delegate_User_Info,
            (userData) =>
            {
            _userData = userData;

            SetUserData(_userData);
            });

            _setUserData = true;
        }
    }
}
