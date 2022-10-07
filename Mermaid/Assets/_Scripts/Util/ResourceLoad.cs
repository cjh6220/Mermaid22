using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using TMPro;

public class ResourceLoad
{
    public static Object GetObject(string resourceName)
    {
        if (true == string.IsNullOrEmpty(resourceName))
        {
            return null;
        }

        return Resources.Load(resourceName);
    }

    public static AudioClip GetAudio(string resourceName)
    {
        if (true == string.IsNullOrEmpty(resourceName))
        {
            return null;
        }

        AudioClip asset = null;

        // #if ASSETBUNDLE_LOCAL || ASSETBUNDLE_WEB
        //         asset = AssetBundleManager.Instance.Load<AudioClip>(Name_AssetBundle.bgm, resourceName);
        // #else
        asset = Resources.Load<AudioClip>("Audio/" + resourceName);
        //#endif

        NULLCheck(asset, "Audio/" + resourceName);

        return asset;
    }

    public static string GetTableData(string resourceName)
    {
        string text = null;

        TextAsset asset = null;

        asset = Resources.Load<TextAsset>("Data/TableData/" + resourceName);

        NULLCheck(asset, "Data/TableData/" + resourceName);

        if (null != asset)
        {
            text = asset.text;
        }

        return text;
    }

    public static TMP_FontAsset GetTMPFontAsset(string resourceName, bool forceResources = false)
    {
        if (true == string.IsNullOrEmpty(resourceName))
        {
            return null;
        }

        TMP_FontAsset asset = null;

        asset = Resources.Load<TMP_FontAsset>("TextMesh/" + resourceName);

        NULLCheck(asset, "TextMesh/" + resourceName);

        return asset;
    }

    public static Sprite GetHexaSprite(string resourceName)
    {
        if (true == string.IsNullOrEmpty(resourceName))
        {
            return null;
        }

        Sprite asset = null;

        asset = Resources.Load<Sprite>("HexaTextures/" + resourceName);

        NULLCheck(asset, "HexaTextures/" + resourceName);

        return asset;
    }

    // public static Sprite GetHexaGradeSprite(Type_HexaGrade grade)
    // {
    //     string textureName = "";
    //     switch (grade)
    //     {
    //         case Type_HexaGrade.Rare:
    //             textureName = "Hexagon_Block_Rank_rare";
    //             break;
    //         case Type_HexaGrade.Hero:
    //             textureName = "Hexagon_Block_Rank_hero";
    //             break;
    //         case Type_HexaGrade.Legend:
    //             textureName = "Hexagon_Block_Rank_legend";
    //             break;
    //         default:
    //             textureName = "Hexagon_Block_Rank_normal";
    //             break;
    //     }

    //     return GetHexaSprite(textureName);
    // }

    public static Object GetUI(string resourceName)
    {
        if (true == string.IsNullOrEmpty(resourceName))
        {
            return null;
        }

        Object asset = null;

        asset = Resources.Load("UI/" + resourceName);

        NULLCheck(asset, "UI/" + resourceName);

        return asset;
    }

    // public static Sprite GetSkin_Resource(string resouceName, Data_Skin_Operator.ResourceType type)
    // {
    //     if (true == string.IsNullOrEmpty(resouceName)) return null;


    //     string Path = null;

    //     Sprite asset = null;

    //     if (type == Data_Skin_Operator.ResourceType.Oper_FullIllust)
    //     {
    //         SpriteAtlas spriteAtlas = null;

    //         Path = "Skin/" + resouceName + "/operator_illust_" + resouceName;
    //         spriteAtlas = Resources.Load<SpriteAtlas>(Path);

    //         if (null != spriteAtlas)
    //         {
    //             asset = spriteAtlas.GetSprite("img_operator_" + resouceName + "_body");
    //         }
    //     }
    //     else
    //     {
    //         switch (type)
    //         {
    //             case Data_Skin_Operator.ResourceType.HexaImg:
    //                 Path = "Skin/" + resouceName + "/img_hexa_slot_" + resouceName;
    //                 asset = Resources.Load<Sprite>(Path);
    //                 break;
    //             case Data_Skin_Operator.ResourceType.BgskinImg:
    //                 Path = "Skin/" + resouceName + "/img_bg_skin_" + resouceName;
    //                 asset = Resources.Load<Sprite>(Path);
    //                 break;
    //             case Data_Skin_Operator.ResourceType.MonsterIn:
    //                 {
    //                     Path = "Skin/" + resouceName + "/img_monster_in_" + resouceName;

    //                     asset = Resources.Load<Sprite>(Path);

    //                     if (asset == null)
    //                     {
    //                         Path = "HexaTextures/img_monster_in";
    //                         asset = Resources.Load<Sprite>(Path);
    //                     }
    //                     break;
    //                 }
    //             case Data_Skin_Operator.ResourceType.MonsterOut:
    //                 {
    //                     Path = "Skin/" + resouceName + "/img_monster_out_" + resouceName;

    //                     asset = Resources.Load<Sprite>(Path);

    //                     if (asset == null)
    //                     {
    //                         Path = "HexaTextures/img_monster_out";
    //                         asset = Resources.Load<Sprite>(Path);
    //                     }
    //                     break;
    //                 }
    //             case Data_Skin_Operator.ResourceType.Oper_Speech:
    //                 Path = "Skin/" + resouceName + "/img_operator_speech_" + resouceName;
    //                 asset = Resources.Load<Sprite>(Path);
    //                 break;
    //             case Data_Skin_Operator.ResourceType.Oper_Face:
    //                 Path = "Skin/" + resouceName + "/img_operator_face_" + resouceName;
    //                 asset = Resources.Load<Sprite>(Path);
    //                 break;
    //         }
    //     }

    //     NULLCheck(asset, Path);

    //     return asset;
    // }

    // public static Sprite GetSkin_FaceEmotion(string resouceName, Data_Skin_Operator.Face type)
    // {
    //     if (true == string.IsNullOrEmpty(resouceName))
    //     {
    //         return null;
    //     }

    //     SpriteAtlas spriteAtlas = null;
    //     Sprite obj = null;

    //     spriteAtlas = Resources.Load<SpriteAtlas>("Skin/" + resouceName + "/operator_illust_" + resouceName);

    //     if (null != spriteAtlas)
    //     {
    //         switch (type)
    //         {
    //             case Data_Skin_Operator.Face.Base:
    //                 {
    //                     obj = spriteAtlas.GetSprite("img_operator_" + resouceName + "_base");
    //                 }
    //                 break;
    //             case Data_Skin_Operator.Face.Damage:
    //                 {
    //                     obj = spriteAtlas.GetSprite("img_operator_" + resouceName + "_damage");
    //                 }
    //                 break;
    //             case Data_Skin_Operator.Face.Win:
    //                 {
    //                     obj = spriteAtlas.GetSprite("img_operator_" + resouceName + "_win");
    //                 }
    //                 break;
    //             case Data_Skin_Operator.Face.Lose:
    //                 {
    //                     obj = spriteAtlas.GetSprite("img_operator_" + resouceName + "_lose");
    //                 }
    //                 break;
    //             case Data_Skin_Operator.Face.Celebrate:
    //                 {
    //                     obj = spriteAtlas.GetSprite("img_operator_" + resouceName + "_celebrate");
    //                 }
    //                 break;

    //         }
    //     }

    //     NULLCheck(obj, "Skin/" + resouceName + "/operator_illust_" + resouceName);

    //     return obj;
    // }

    public static Sprite GetSkin_Emoji(int speech_type)
    {
        string speechType = "";
        switch (speech_type)
        {
            case 1:
                {
                    speechType = "greeting";
                }
                break;
            case 2:
                {
                    speechType = "thanks";
                }
                break;
            case 3:
                {

                    speechType = "sorry";
                }
                break;
            case 4:
                {
                    speechType = "angry";
                }
                break;
            case 5:
                {
                    speechType = "admire";
                }
                break;
            case 6:
                {
                    speechType = "surrender";
                }
                break;

        }
        string Path = "Skin/img_skin_speech_" + speechType;

        Sprite asset = null;

        asset = Resources.Load<Sprite>(Path);

        NULLCheck(asset, Path);

        return asset;
    }

    // public static Sprite GetItemSkill_Icon(string resourceKey)
    // {
    //     string Path = "HexaTextures/img_" + resourceKey;

    //     Sprite asset = null;

    //     asset = Resources.Load<Sprite>(Path);

    //     if (asset == null)
    //     {
    //         LogManager.LogError(Path + " is Null");
    //         return null;
    //     }

    //     return asset;
    // }

    public static AudioClip GetSkinSpeech_Audio(string resourceName)
    {
        if (true == string.IsNullOrEmpty(resourceName))
        {
            return null;
        }

        AudioClip asset = null;

        // #if ASSETBUNDLE_LOCAL || ASSETBUNDLE_WEB
        //         asset = AssetBundleManager.Instance.Load<AudioClip>(Name_AssetBundle.bgm, resourceName);
        // #else
        asset = Resources.Load<AudioClip>("Skin/Sounds/voice_" + resourceName);
        //#endif

        NULLCheck(asset, "Skin/Sounds/" + resourceName);

        return asset;
    }

    public static Object GetMacro(string resourceName)
    {
        if (true == string.IsNullOrEmpty(resourceName))
        {
            return null;
        }

        Object asset = Resources.Load("BoltMacro/" + resourceName);

        NULLCheck(asset, "BoltMacro/" + resourceName);

        return asset;
    }



    public static void NULLCheck(Object obj, string objName)
    {
        if (null == obj)
        {
            //LogManager.LogError(objName);
        }
    }


}