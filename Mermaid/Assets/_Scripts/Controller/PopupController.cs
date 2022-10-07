using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupController : MessageListener
{
    public ObjectSpawnPool Pool;
    public Transform ToastParent;
    protected override void AddMessageListener()
    {
        base.AddMessageListener();

        AddListener(MessageID.Call_UI_Toast);
        AddListener(MessageID.Call_UI_Toast_Close);
    }

    protected override void OnMessage(MessageID msgID, object sender, object data)
    {
        base.OnMessage(msgID, sender, data);

        switch (msgID)
        {
            case MessageID.Call_UI_Toast:
                {
                    var info = data as string;
                    var popup = Pool.Spawn("Default_Popup");
                    popup.transform.SetParent(ToastParent);
                    popup.transform.localPosition = Vector3.zero;
                    popup.transform.localScale = Vector3.one;

                    popup.GetComponent<UIPopup_Toast>().SetPopup(info, this);
                }
                break;
            case MessageID.Call_UI_Toast_Close:
                {
                    var info = data as GameObject;
                    DespawnPopup(info);
                }
                break;
        }
    }

    protected override void AwakeImpl()
    {
        base.AwakeImpl();

        Pool = GetComponent<ObjectSpawnPool>();
    }

    public void DespawnPopup(GameObject go)
    {
        Pool.Despawn(go);
    }
}
