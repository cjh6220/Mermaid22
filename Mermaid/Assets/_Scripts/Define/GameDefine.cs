using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using CodeStage.AntiCheat.ObscuredTypes;

public class GameDefine
{
    public static GameDefine Instance = new GameDefine();

    //public const bool Test_Invincibility = false;
    public const bool Test_AIStop = false;

    //public Hexa_Server_Setting ServerSetting = new Hexa_Server_Setting();

    public const int MaxHexaLink = 6;
    public const int MaxHexaDeck = 5;
    public const float ProjectileSpeed = 8.0f;
    public const float NextFirstHexaDuration = 0.1f;
    public const float DestoryHexaDuration = 0.1f;
    public const int BaseLife = 20;
    public const float MoveBackDuration = 1.0f;
    public const float BossHPAddDuration = 0.5f;
    public const float BossHPRate = 0.5f;
    public const int MaxHexaRank = 6;
    public const int BoxOpenHexaCount = 3;
    public const int InGame_UseSPAdd = 10;
    public const int Max_Lv_Battle = 4;

    // public const int BaseHexa_KRF_id = 5000;
    // public const int BaseHexa_HPMC_id = 5001;
    // public const int BaseHexa_SGA_id = 5002;
    public const float StatRateFormat = 0.01f;
    public const float Enemy_Gen_DelayTime = 0.8f;

    public const int Hexa_Max_Lobby_Lv = 10;

    public const int Item_Max_Count = 99999;

    #region Wave
    public ObscuredInt Normal_Wave_Time = 30;
    public ObscuredInt Boss_Wave_Time = 40;
    public ObscuredInt Next_Wave_Waiting_Time = 30;
    public const int Left_Point = 0;
    public const int Right_Point = 3;
    public const int Max_Point_Count = 6;    
    public const int Normal_Monster_Decrease_LifePoint = 1;
    public const int Named_Monster_Decrease_LifePoint = 5;
    public const int Boss_Monster_Decrease_LifePoint = 10;
    public const float Monster_HP_Wave_Variable = 1.0f;
    #endregion

    #region Battle
    public const int First_Create_Hexa_Count = 7;
    public const int Default_Create_Hexa_Count = 2;
    public const int Max_SetHexa_Count = 19;
    public const int Action_Count = 3;
    public const int Max_Shield_Count = 5;

    public const float Speech_PopUp_liftTime = 2.0f;
    #endregion
}