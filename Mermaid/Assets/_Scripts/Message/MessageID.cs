public enum MessageID
{
    None,

    OnClick_Product,
    OnClick_Option,
    OnClick_Select,
    OnClick_Select_Success,    
    OnClick_Remove,
    Event_Set_TotalCash,
    OnClick_Confirm,
    OnClick_Reset,
    Event_Set_Log,
    OnClick_Update_Table,
    OnClick_Start,
    Event_Update_Remain_Product,
    OnClick_Table_To_Json,
    OnClick_Update_Selected_Product_Count,
    OnClick_Add_Selected_Product_Count,
    OnClick_Reduce_Selected_Product_Count,
    Event_Save_Product_List,

    Delegate_User_Info,
    Event_InfoUpdate_UserData,




    Call_Scene_Load,
    Call_Scene_MesssageAfterLoad,
    Event_SceneChange_Start,
    Event_SceneChange_End,

    Call_User_Save,

    Call_UI_Set,
    Call_UI_Push,
    Call_UI_Push_Popup,
    Call_UI_Push_Tutorial,
    Call_UI_Pop,
    Call_UI_Pop_Tutorial,
    Call_UITutorial_Push,
    Call_UI_Push_Direction,
    Call_UI_Toast,
    Call_UI_Toast_Close,

    Call_Audio_PlayFX,
    Call_Audio_PlayBGM,
    Call_Audio_PlaySkinSpeech,
    Call_Audio_PlaySkinSpeech_Enemy,

    Event_UI_Show,
    Event_UI_Hide,
    Event_UI_Showed,
    Event_UI_Hidden,
    Event_UI_Created,

    Event_TextManager_ChangedLanguage,

    Call_BackKey_Register,
    Call_BackKey_Unregister,
    Call_BackKey_AllUnregister,
    Call_BackKey_Enable,
    Call_BackKey_Disable,
    Call_BackKey_PopBack,
    Call_BackKey_Back,
    Call_BackKey_SetManual,
    Event_BackKey_Back,

    Call_TouchFx_Enable,
    Call_TouchFx_Disable,
    Call_InputEvent_Lock,
    Call_InputEvent_Unlock,

    Event_Info_UserData,
    
        
    Call_Battle_CreatePlayer,
    Call_Battle_CreatePlayer_Immediately,
    Call_Battle_CreatePlayerWithInfo,
    Call_Battle_AutoAddHexa,

    Event_Battle_Info,
    //Event_Battle_SPInfo,
    Event_Battle_RemoveEnemy,
    Event_Battle_DestroyEnemy,
    Event_Battle_DestroyEnemy_Immediately,
    Event_Battle_HexaLinkUpdate,

    Call_Battle_HexaLinkUpdate,
    

    Event_Battle_Start,
    Event_Battle_StartReady,
    Event_Battle_StartActionBegin,
    Event_Battle_StartActionEnd,
    Event_Battle_End,
    Event_Battle_LeftEmptySlotCount,

    Event_Battle_BossCreation,
    Event_Battle_BossCreationActionBegin,
    Event_Battle_BossCreationActionEnd,
    Event_Battle_BossClear,

    //Event_Battle_StartPSValue,
    //Event_Battle_StartUsePSValue,

    Event_Battle_CreatePlayer,

    Event_Battle_WaveUpdate,
    // Event_Battle_WaveActionBegin,
    // Event_Battle_WaveActionEnd,
    Event_Battle_LifeUpdate,

    Event_Battle_Win,
    //Event_Battle_WinBegin,
    //Event_Battle_WinEnd,
    Event_Battle_Failure,

    Event_Battle_ResultInfo,

    Event_battle_SelectedHexa,
    Event_battle_UnselectedHexa,

    Delegate_Battle_EnemyInfo,
    Delegate_Battle_AreaEnemyInfo,

    Call_Battle_CreateEnemy,
    Call_Battle_DespawnHexa,
    Call_Battle_DamageShow,
    Call_Battle_ShotProjectile,

    Delegate_Battle_GetPvPPlayController,

    Call_Battle_Fx,
    Call_Battle_DespawnFx,
    Call_Battle_DespawnFx_Immediately,

    Event_Mydeck_DeckSlotClick,
    Event_Mydeck_SelectionHexa,
    Event_Mydeck_HexaInfo,
    Event_Mydeck_HexaMenuOn,

    Call_Mydeck_SetHexa,
    Event_Box_RewadInfo,

    Call_Loading_Start,
    Event_Loading_StartEnd,

    Call_Loading_End,
    Event_Loading_EndEnd,

    Call_ConnectServer,
    Request_Find_Room,
    Request_ReFind_Room,
    Response_Create_Room,
    Response_Join_Room,
    Request_MasterInfo,
    Response_MasterInfo,
    Event_OnClick_EndButton,
    Event_OnPlayer_LeftRoom,
    Event_SetUserInfo_Ingame,
    Event_SetTileController,
    Event_InitPlayer,
    Call_Shot_Destroy,
    Call_Shot_DestroyDelay,
    Call_Set_EnemyDeck,
    Call_Set_EnemyInfo,
    //Request_SP_Info,
    Call_Ownershop_Transfer,
///////////////////////////////////////////////
    
    Response_Set_Players_Deck,
    Call_Create_Players,
    Request_Giveup_Game,
    Response_Giveup_Game,
    Request_Attack_Enemy,
    Response_Attack_Enemy,
    Call_OtherPlayer_Attack_Enemy,
    Request_Change_Hexa,
    Response_Change_Hexa,
    Call_OtherPlayer_Change_Hexa,
    Event_Packet_ServerSetting,
    Call_RewardMsg_Info,
    Call_IsLogin_State,
    //Call_GiveUp_Game_ClickEvent,

    #region RPC
    Request_Destroy_Hexa,
    Response_Destroy_Hexa,
    Request_OtherPlayer_Name,
    Response_OtherPlayer_Name,
    Request_OtherPlayer_UserID,
    Response_OtherPlayer_UserID,
    Request_Destroy_Enemy,
    Request_Destroy_Enemy_Immediately,
    Response_Destroy_Enemy,
    Response_Destroy_Enemy_Immediately,
    Request_Random_Boss,
    Response_Random_Boss,
    Request_Skill_MoveSpeed,
    Response_Skill_MoveSpeed,
    Request_Create_Fx,
    Response_Create_Fx,
    Request_Destroy_Fx,
    Response_Destroy_Fx,
    Request_Guest_Change_Hexa,
    Response_Guest_Change_Hexa,
    Request_ReJoin_PlayersDeck,
    Response_ReJoin_PlayersDeck,
    Event_ReJoin,
    Request_Wave_Update,
    Response_Wave_Update,
    Request_ReJoin_PlayersName,
    Response_ReJoin_PlayersName,
    Request_Battle_Change_HexaTile,
    Response_Battle_Change_HexaTile,
    Response_Enemy_SpeechText,
    Request_Enemy_SpeechText,

    #endregion

    Call_Wave_GenStart,
    Call_Wave_GenEnd,
    Request_Skip_Wave,
    Response_Skip_Wave,
    ////////////////////////////////////////////UI

    #region Lobby
    Call_Lobby_DeckChange,
    Call_Set_UserName,
    Call_Change_UserName,
    Event_Cancel_Matching,
    Event_Cancel_FindPvPRoom,
    Call_Btn_MenuArea,
    Call_Same_NickName,
    Event_Send_HexaUnit_Info_Pvp,
    Event_PvP_Searching_Ready,
    #endregion

    #region Battle
    Call_Set_Player,
    Request_Player_Datas,
    Response_Player_Datas,
    Event_Create_HexaPlayer,
    Event_Create_Enemy,
    Call_Battle_HexaChange,
    Call_Battle_SetHexaChange,
    Event_Battle_Change_HexaTile,
    Event_Remove_HexaTile,
    Event_OtherPlayer_Disconnect,
    Event_ReJoinRoom,
    Event_Reset_HexaPlayer,
    Event_Reset_HexaEnemy,
    Request_Get_UserNames,
    Response_Get_UserNames,
    Request_Start_Wait,
    Response_Start_Wait,
    Event_Start_Wait,
    Event_Start_Wave,
    Call_Start_Wave,
    Event_End_Wave,
    Event_Update_Remain_Life,
    Call_Update_Remain_Life,
    Event_Update_Action_Count,
    Call_Update_Action_Count,
    Event_Update_Remain_Enemy,
    Event_SetLink,
    Event_Update_LinkedHexa,
    Request_My_Hexas,
    Response_My_Hexas,

    Event_GetPoint_HexaPassInfo,

    #endregion

    #region ItemSkill
    Event_OnClick_SkillButton,
    Request_OnClick_SkillButton,
    Response_OnClick_SkillButton,
    Request_ItemSkill_Effect,
    Response_ItemSkill_Effect,
    Event_ItemSkill_RPC_Attach,
    Event_Packet_UpdateItem,
    Call_Hexa_ClearDebuff,
    Request_UnlockLinkedTiles,
    Response_UnlockLinkedTiles,
    
    Get_Battle_EnemyPathRandomPoint,    
    Get_Battle_HexaPlayers,    
    Get_Battle_HexaTiles,    
    #endregion
    
    #region InGame
    Event_OtherPlayer_SetTileController,
    Event_OnClick_Battle_Hexa_Info,
    Event_Show_Battle_Hexa_Info,
    Request_Enemy_List,
    Response_Enemy_List,
    Get_Enemy_List,
    Get_Hexa_List,
    Get_Datachip_List,
    Request_Enemy_Data,
    Response_Enemy_Data,
    Event_PointerDown_Ingame,
    Event_PointerUp_Ingame, 
    Event_OnClick_SkipButton,
    Request_OnClick_SkipButton,
    Request_Remain_Time,
    Response_Remain_Time,
    Event_Boss_Create_Production,
    Event_Set_HP,
    Event_Set_Shield,
    #endregion 

    #region InGameUI
    Event_OnClick_DeckInfo_Hexa,
    Request_Deck_Info_Popup,
    Response_Deck_Info_Popup,
    Event_OnClick_DeckInfo_DataChip,
    Event_Battle_Cancel_EmojiPopup,
    Event_Battle_CallSpeech,
    Call_Battle_SpeechText,
    Call_Battle_EmojiReset,
    Event_GetSkinData_InGame_Source,
    Get_PlayerSkin_BgSprite,
    Event_Wave_Clear,
    Event_Wave_ClearActionBegin,
    Event_Wave_ClearActionEnd,
    
    Request_Enemy_SpeechAudioSource,
    Request_Speech_AudioResource,
    Event_Show_Item_Info,
    Event_Close_Item_Info,
    #endregion

    #region Skill
    Event_Battle_LinkSkillUpdate,
    Event_CheckAttack_LinkSkill,
    Event_CheckAfterAttack_LinkSkill,
    //Event_Skill_SplashDamage,
    #endregion

    #region Server
    Request_Packet,
    Response_Packet,
    Event_NetworkError,
    Event_Packet_GetUserData,
    Event_Packet_SetUserName,
    Event_Packet_SuccessFindRoom,
    Event_Packet_PvPReady,
    Event_Packet_PvPRoomClose,
    #endregion

    #region bag
    Call_Bag_HalfPopup,
    Event_Bag_AddButton,
    Event_Bag_InfoButton,
    Event_Send_HexaUnit_Info,
    Call_Bag_Unit_HexaInfo,
    Event_Hexa_ReplaceEnd,
    Event_UIBag_MenuSelect,
    Call_UIBag_TeamIndex,
    Call_Inventory_ScreenPoint,
    Call_UIBag_ListenerOnOff,
    Call_UIBag_MenuType,
    Event_UIBag_Click_Sorting,
    Call_HexaInfo_SkillInfo,
    Event_Packet_UpdateTeamInfo,
    Call_Bag_HalfPopup_DragStart,
    Call_Bag_HalfPopup_DragEnd,
    Event_Call_HexaInfo_Rankview,
    Event_Bag_RemoveButton,
    Event_Packet_HexaEnhance,
    Call_Hexa_ReplaceClose,
    Event_Bag_ChangeBtn,
    Event_Bag_ChangeState,
    Event_Btn_LevelPreView,
    Event_Btn_ClassPreView,
    Event_UIBag_DeckChange,
    Event_Click_ListTapArea,
    Call_UIBag_TypeIndex,
    #endregion

    #region DebugTool
    Event_DebugHexaInfo,
    Event_DebugLinkSkill,
    #endregion

    Call_UI_CanvasCamera,
    Call_UI_UnitScroll,
    Call_UI_ChipScroll,
    Call_UI_UnitListScript,
    Call_UI_ChipListScript,
    Dev_Click_ItemID,
    Event_Packet_GetRewards,

    #region Mega_Menu

    Call_MegaUI_Click_AccountSet,
    Call_MegaUI_Click_Menu,
    Event_MegaUI_AccountSetting_EnterSkin,
    Event_Account_Skin_BtnInfo,
	Event_Get_Account_SkinInfo,
    Event_SkinSelectID,
    Event_Packet_UseItem,
    Event_Response_UseItemPacket,
    Event_SkinList_DetailInfo,
    Event_SkinInfo_SkinChange,
    Event_SkinInfo_Btn_Left,
    Event_SkinInfo_Btn_Right,
    Event_Account_TopMenu_Click,
    #endregion

    #region IngameItem_Ui

    Event_Input_PvpIngameItem,
    Call_GetIsEmptySlot_IngameItem,
    Call_Lobby_IngameItem_Refresh,
    Call_PvpItem_Change_TargetID,
    Call_Replace_PvpIngameItem,
    Call_PvpItem_ChangeArea_SelectID,
    Call_Delete_PvpIngameItemSlot,
    Event_Packet_SaveSlot_IngameItem,
    Call_PvpItem_InfoPopup,
    Get_Profile_UI,
    Get_PlayGround_UI,
    #endregion

    #region Shop
    Call_GachaPopup_TypeLoad,
    Call_Gacha_RateTable,
    Event_Packet_ShopPurchased,
    #endregion


    #region TestTool
    Call_TestTool_NetworkInit,
    Call_TestTool_Battle_Info,
    Call_TestTool_Battle_Ready,
    #endregion


    #region TestTool
    Event_TestTool_CreatedCheatCommandPhoton,
    #endregion


    
}