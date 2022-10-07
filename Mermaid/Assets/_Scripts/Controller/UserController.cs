using System.Xml.Schema;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Photon.Pun;

public class UserController : MessageListener
{
    Data_User _userData = new Data_User();

    // public string SavedSelectItem 
    // {
    //     get  { return SimpleSaveManager.GetString(SimpleSaveManager.Save_SelectItem ,""); }
    //     set  { SimpleSaveManager.SetString(SimpleSaveManager.Save_SelectItem , value);}       
    // }

    // public string SavedLastDeck
    // {
    //     get { return SimpleSaveManager.GetString(SimpleSaveManager.Save_LastHexaDeck, ""); }
    //     set { SimpleSaveManager.SetString(SimpleSaveManager.Save_LastHexaDeck , value); }
    // }

    protected override void AwakeImpl()
    {
        base.AwakeImpl();
    }

    protected override void AddMessageListener()
    {
        base.AddMessageListener();

        AddListener(MessageID.Delegate_User_Info);
        AddListener(MessageID.Event_UI_Show);

        AddListener(MessageID.Event_Mydeck_DeckSlotClick);
        AddListener(MessageID.Call_Mydeck_SetHexa);
        AddListener(MessageID.Call_User_Save);

        AddListener(MessageID.Event_Packet_GetUserData);
        AddListener(MessageID.Event_Packet_SetUserName);
        AddListener(MessageID.Event_Packet_PvPRoomClose);
        AddListener(MessageID.Event_Packet_UpdateTeamInfo);
        AddListener(MessageID.Event_Packet_HexaEnhance);
        AddListener(MessageID.Event_Packet_GetRewards);
        AddListener(MessageID.Event_Packet_UseItem);
        AddListener(MessageID.Event_Packet_SaveSlot_IngameItem);
        AddListener(MessageID.Event_Packet_ShopPurchased);
        AddListener(MessageID.Event_Packet_ServerSetting);

    }

    protected override void OnMessage(MessageID msgID, object sender, object data)
    {
        base.OnMessage(msgID, sender, data);

        // switch (msgID)
        // {
        //     case MessageID.Delegate_User_Info:
        //         {
        //             var requsetAction = data as System.Action<Data_User>;

        //             requsetAction(_userData);
        //         }
        //         break;
        //     case MessageID.Event_UI_Show:
        //         SendMessage(MessageID.Event_Info_UserData, _userData);
        //         break;

        //     case MessageID.Event_Mydeck_DeckSlotClick:
        //         {
        //             var deckSlotIndex = (int)data;

        //             _userData.HexaDeck.Hexas[deckSlotIndex] = null;

        //             SendMessage(MessageID.Event_InfoUpdate_UserData, _userData);
        //         }
        //         break;
        //     case MessageID.Call_Mydeck_SetHexa:
        //         {
        //             var msgData = data as Msg_MydeckSetHexa;

        //             _userData.HexaDeck.Hexas[msgData.Slot] = msgData.HexaData;
        //             _userData.Save();

        //             SendMessage(MessageID.Event_InfoUpdate_UserData, _userData);
        //         }
        //         break;
        //     case MessageID.Call_User_Save:
        //         _userData.Save();

        //         SendMessage(MessageID.Event_InfoUpdate_UserData, _userData);
        //         break;
        //     case MessageID.Event_Packet_GetUserData:
        //         {
        //             var info = data as Packet_GetUserData.UserDataPacket;
        //             _userData.UpdateInfo(info);
        //             _userData.SetUserNickName(info.UserName);
        //             if (PhotonNetwork.IsConnected)
        //             {
        //                 //PhotonNetwork.NickName = UserName;
        //                 PhotonNetwork.NickName = info.UserID;
        //             }

        //             if(!string.IsNullOrEmpty(SavedLastDeck))
        //             {
        //                 var jsonReceiveData = SimpleSaveManager.GetStringToJson<PlayerDecksObject>
        //                 (SimpleSaveManager.Save_LastHexaDeck, SavedLastDeck);

        //                 LogManager.LogHexa("저장된 Deck정보 불러옴"+jsonReceiveData.LastTeamIndex);

        //                 _userData.SetTeamInfo(jsonReceiveData.LastTeamIndex, jsonReceiveData.LastHexaIDs, jsonReceiveData.LastChipIDs);
        //                 SimpleSaveManager.SetString(SimpleSaveManager.Save_LastHexaDeck, "");
        //             }

        //             if(!string.IsNullOrEmpty(SavedSelectItem))
        //             {
        //                 var jsonReceiveData = SimpleSaveManager.GetStringToJson<PlayerSavedItemSlot>
        //                 (SimpleSaveManager.Save_SelectItem ,SavedSelectItem);

        //                 _userData.SetSelectsItem(jsonReceiveData.LastItemSlots);
        //                 SimpleSaveManager.SetString(SimpleSaveManager.Save_SelectItem, "");
        //             }

        //             SendMessage(MessageID.Event_InfoUpdate_UserData, _userData);
        //         }
        //         break;
        //     case MessageID.Event_Packet_SetUserName:
        //         {
        //             var name = data as string;
        //             _userData.SetUserNickName(name);
        //         }
        //         break;
        //     case MessageID.Event_Packet_PvPRoomClose:
        //         {
        //             var info = data as Packet_PvPEnd.PvPEndPacket;
        //             //_userData.UpdatePvPData(info);
        //         }
        //     break;
        //     case MessageID.Event_Packet_UpdateTeamInfo:
        //     {
        //         var teaminfo = data as UpdateTeamInfoPacket.TeamInfo_Packet;

        //         var hexaList = teaminfo.Team.HexaIds;
        //         var hexaList_id = teaminfo.Team.LinkHexaIds;
        //         _userData.SetTeamInfo(teaminfo.TeamIndex, hexaList, hexaList_id);

        //         SimpleSaveManager.SetString(SimpleSaveManager.Save_LastHexaDeck, "");
        //     }
        //     break;
        //     case MessageID.Event_Packet_HexaEnhance:
        //     {
        //         var enhanceinfo = data as Packet_HexaEnhance.HexaEnhance_Packet;

                
        //         var hexa_id = enhanceinfo.hexaInfo.Hexa_id;
        //         var level = enhanceinfo.hexaInfo.Level;
        //         var part = enhanceinfo.hexaInfo.Part;

        //         _userData.SetGold(enhanceinfo.gold);
        //         _userData.SetHexaInfo(hexa_id, level, part);

                
        //     }
        //     break;    
        //     case MessageID.Event_Packet_GetRewards:
        //     {
        //          var rewardinfo = data as Packet_RewardsGet.RewardsGetResponse_Packet;

        //          var hexas = rewardinfo.Hexas;

        //          _userData.SetGold(rewardinfo.Gold);
        //          _userData.SetCash(rewardinfo.Dia);
        //          _userData.SetFreeCash(rewardinfo.FreeDia);

        //          //Debug.Log("GetRewards"+hexas.Count);
        //             for (int i = 0; i < hexas.Count; i++)
        //             { 
        //                 if (_userData.IsUserHexas_Contains(hexas[i].ID) == -1)
        //                 {
        //                     _userData.AddHexa(hexas[i].ID, hexas[i].Piece);
        //                     _userData.SetHexaInfo(hexas[i].ID, hexas[i].Lv_Lobby, hexas[i].Piece);
        //                 }
        //                 else
        //                 {
        //                     _userData.SetHexaInfo(hexas[i].ID, hexas[i].Lv_Lobby, hexas[i].Piece);
        //                 }
        //             }

        //           SendMessage(MessageID.Event_InfoUpdate_UserData, _userData);
        //     }
        //     break;
        //     case MessageID.Event_Packet_UseItem:
        //     {
        //           var msgData = data as Packet_UseItem.UseItem_UserPacket;

        //           _userData.Get_UseItems(msgData);

        //           SendMessage(MessageID.Event_Response_UseItemPacket, msgData);
        //     }
        //     break;
        //     case MessageID.Event_Packet_SaveSlot_IngameItem:
        //     {
        //         var iteminfo = data as Packet_Select_ItemSlot.SaveSlot_InGameItem_Packet;

        //         _userData.SetSelectsItem(iteminfo.select_items);

        //          SimpleSaveManager.SetString(SimpleSaveManager.Save_SelectItem, "");
        //     }
        //     break;

        //     case MessageID.Event_Packet_ServerSetting:
        //     {
        //          var serversetting = data as Packet_ServerSetting.Server_Setting_Packet;

        //          GameDefine.Instance.ServerSetting.Pvp_Play_TotalCount = serversetting.Pvp_Daily_playable_count;
        //          GameDefine.Instance.ServerSetting.Pvp_Retry_Count = serversetting.Pvp_Ready_retry_count;
        //     }
        //     break;
        //     case MessageID.Event_Packet_ShopPurchased:
        //     {
        //         var rewarddata = data as Packet_ShopPurchase.ShopPurchasePacket;

        //         var RewardList = rewarddata.Rewards;

                
        //         var msg_reward = new Msg_Popup_Reward();
        //         msg_reward.Title = BeforeTextManager.Instance.GetText("ui_account_history");
        //         msg_reward.Rewards = RewardList;

        //         SendMessage(MessageID.Call_UI_Push_Popup, String_UIName.Popup_RewardPopup);
        //         SendMessage(MessageID.Call_RewardMsg_Info, msg_reward);
        //         // for(int i=0; i<RewardList.Count; i++)
        //         // {
        //         //     switch(RewardList[i].Type)
        //         //     {
        //         //         case RewardType.Item:
        //         //         {
                            
        //         //         }
        //         //         break;

        //         //         case RewardType.Hexa:
        //         //         {
        //         //              _userData.AddHexa(RewardList[i].id, RewardList[i].count);


        //         //                     // if (_userData.IsUserHexas_Contains(RewardList[i].id) == -1)
        //         //                     // {
        //         //                     //     _userData.AddHexa(RewardList[i].id, RewardList[i].count);
        //         //                     //    // _userData.SetHexaInfo(hexas[i].ID, hexas[i].Lv_Lobby, hexas[i].Piece);
        //         //                     // }
        //         //                     // else
        //         //                     // {
        //         //                     //     _userData.SetHexaInfo(hexas[i].ID, hexas[i].Lv_Lobby, hexas[i].Piece);
        //         //                     // }
        //         //         }
        //         //         break;

        //         //     }

        //         // }

        //           SendMessage(MessageID.Event_InfoUpdate_UserData, _userData);
        //     }
        //     break;
        // }
    }
}
