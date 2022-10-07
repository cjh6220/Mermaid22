using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BeforeTextManager
{
    public static BeforeTextManager Instance = new BeforeTextManager();

    public const Language DafaultLanguage = Language.KO;

    const string TableName = "String_";
    const string ErrorTableName = "String_ErrorCode_";
    const string OutlineMaterial = "Font_ko Material_Outline_ingame_Black";

    public const string Key1 = "@@";
    const int Key1_Length = 2;

    public const string Key2 = "@";
    public const string Key2_1 = "/";
    const int Key2_Length = 1;

    public const char Key4 = '%';

    public const string Tag1 = "#C";
    public const string Tag2 = "#O";
    public const string Tag3 = "#S";

    enum TextTag
    {
        None,
        Color,
        Outline,
        Size
    }

    public enum Language
    {
        None = 0,
        KO = 1,
        EN,
        JA,
        CH,

        Max
    }

    public enum Text
    {
        None = 0,
        Title,

    }

    Dictionary<string, string> _defaultText = new Dictionary<string, string>();
    Dictionary<string, string> _enText = new Dictionary<string, string>();
    Dictionary<string, string> _text = new Dictionary<string, string>();
    Dictionary<string, string> _text_error = new Dictionary<string, string>();
    Dictionary<string, bool> _prohibitDic = new Dictionary<string, bool>();

    Language _currentLanguage = Language.None;

    bool _init = false;
    bool _assetbundleMode = false;

    public BeforeTextManager()
    {
        Init();
    }

    public Language CurrentLanguage
    {
        get { return _currentLanguage; }
    }

    public void Init()
    {
#if UNITY_EDITOR
        _init = false;
#endif

        if (true == _init)
        {
            return;
        }

        _init = true;

        SetDefaultText();
    }

    public void Init_AssetBundle()
    {
        _assetbundleMode = true;
        SetDefaultText();
        UpdateFontAsset();
    }

    void SetDefaultText()
    {
        _defaultText.Clear();

        var text = GetTableData(TableName + DafaultLanguage.ToString());
        if (false == string.IsNullOrEmpty(text))
        {
            text = text.Replace("\r", "");

            var lines = text.Split('\n');

            string[] col = null;
            for (int i = 0; i < lines.Length; i++)
            {
                if (string.IsNullOrEmpty(lines[i]))
                {
                    continue;
                }

                col = lines[i].Split('\t');

                if (1 < col.Length)
                {
                    _defaultText.Add(col[0], col[1].Replace("\\n", "\n"));
                }
            }
        }

        _enText.Clear();

        text = GetTableData(TableName + Language.EN.ToString());

        if (false == string.IsNullOrEmpty(text))
        {
            text = text.Replace("\r", "");

            var lines = text.Split('\n');

            string[] col = null;
            for (int i = 0; i < lines.Length; i++)
            {
                if (string.IsNullOrEmpty(lines[i]))
                {
                    continue;
                }

                col = lines[i].Split('\t');

                if (1 < col.Length)
                {
                    _enText.Add(col[0], col[1].Replace("\\n", "\n"));
                }
            }
        }

        UpdateBaseText();
    }

    void UpdateBaseText()
    {
        _text.Clear();
        _text_error.Clear();

        var text = GetTableData(TableName + _currentLanguage.ToString());




        if (false == string.IsNullOrEmpty(text))
        {
            text = text.Replace("\r", "");

            var lines = text.Split('\n');

            string[] col = null;
            for (int i = 0; i < lines.Length; i++)
            {
                if (string.IsNullOrEmpty(lines[i]))
                {
                    continue;
                }

                col = lines[i].Split('\t');

                if (1 < col.Length)
                {
                    _text.Add(col[0], col[1].Replace("\\n", "\n"));
                }
            }
        }

        var text_error = GetTableData(ErrorTableName + DafaultLanguage.ToString());
        if (true == string.IsNullOrEmpty(text_error))
        {
            text_error = GetTableData(ErrorTableName + DafaultLanguage.ToString());
        }

        if (false == string.IsNullOrEmpty(text_error))
        {
            text_error = text_error.Replace("\r", "");

            var lines = text_error.Split('\n');

            string[] col = null;
            for (int i = 0; i < lines.Length; i++)
            {
                if (string.IsNullOrEmpty(lines[i]))
                {
                    continue;
                }

                col = lines[i].Split('\t');

                if (1 < col.Length)
                {
                    _text_error.Add(col[0], col[1].Replace("\\n", "\n"));
                }
            }
        }
    }

    public void ChangeLanguage(Language language, bool sendMsg = false)
    {
        if (_currentLanguage == language)
        {
            return;
        }

        if (language == Language.None)
        {
            _currentLanguage = language;
            return;
        }

        _currentLanguage = language;

        UpdateBaseText();

        UpdateFontAsset();

        if (true == sendMsg)
        {
            MessageManager.SendMessage(MessageID.Event_TextManager_ChangedLanguage, language);
        }
    }

    public string GetErrorText(int code)
    {
        string value = null;
        if (false == _text_error.TryGetValue(code.ToString(), out value))
        {
            LogNotFound("Error" + code.ToString());
        }

        return value;
    }

    public string GetText_EN(string key)
    {
        string value = null;
        _enText.TryGetValue(key, out value);

        if (string.IsNullOrEmpty(value))
        {
            return "";
        }

        return value;
    }

    public string GetText(string key)
    {

        if (string.IsNullOrEmpty(key))
        {
            return GetTextFromDic(key);
        }

        string valueText = null;
        if (0 <= key.IndexOf(Key4))
        {
            var values = key.Split(Key4);

            key = values[0];
            valueText = values[1];
        }

        var text = GetTextFromDic(key);

        if (false == string.IsNullOrEmpty(text))
        {
            bool includeKey1 = 0 <= text.IndexOf(Key1);

            if (true == includeKey1)
            {
                List<string> keyList = new List<string>();

                int startIndex = 0;
                while (true)
                {
                    startIndex = text.IndexOf(Key1, startIndex);

                    if (0 > startIndex)
                    {
                        break;
                    }

                    int endIndex = text.IndexOf(Key1, startIndex + Key1_Length);

                    keyList.Add(text.Substring(startIndex + Key1_Length, endIndex - (startIndex + Key1_Length)));

                    startIndex = endIndex + Key1_Length;
                }

                for (int i = 0; i < keyList.Count; i++)
                {
                    text = text.Replace(Key1 + keyList[i] + Key1, GetText(keyList[i]));
                }
            }

            TextTag textTag = TextTag.None;
            int textIndex = 0;

            int checkCount = 100;
            while (FindTag(text, out textTag, out textIndex))
            {
                checkCount--;

                switch (textTag)
                {
                    case TextTag.Color:
                        {
                            var colorCode = "#" + text.Substring(textIndex + 2, 6);

                            text = text.Remove(textIndex, 9);
                            text = text.Insert(textIndex, "<color=" + colorCode + ">");

                            textIndex = text.IndexOf("]");
                            text = text.Remove(textIndex, 1);
                            text = text.Insert(textIndex, "</color>");
                        }
                        break;
                    case TextTag.Outline:
                        {
                            text = text.Remove(textIndex, 3);
                            text = text.Insert(textIndex, "<material=" + OutlineMaterial + ">");

                            textIndex = text.IndexOf("]");
                            text = text.Remove(textIndex, 1);
                            text = text.Insert(textIndex, "</material>");
                        }
                        break;
                    case TextTag.Size:
                        {
                            var size = text.Substring(textIndex + 2, 3);

                            text = text.Remove(textIndex, 6);
                            text = text.Insert(textIndex, "<size=" + size + ">");

                            textIndex = text.IndexOf("]");
                            text = text.Remove(textIndex, 1);
                            text = text.Insert(textIndex, "</size>");
                        }
                        break;
                }

                if (0 > checkCount)
                {
                    break;
                }
            }
        }

        if (false == string.IsNullOrEmpty(valueText))
        {
            text = string.Format(text, valueText);
        }

        if (string.IsNullOrEmpty(text))
        {
            if (false == string.IsNullOrEmpty(key))
            {
                LogNotFound(key);
            }
        }

        return text;
    }

    bool FindTag(string text, out TextTag tagType, out int startIndex)
    {
        tagType = TextTag.None;
        startIndex = -1;

        List<int> indexes = new List<int>();
        indexes.Add(text.IndexOf(Tag1));
        indexes.Add(text.IndexOf(Tag2));
        indexes.Add(text.IndexOf(Tag3));

        int min = 10000;
        int minIndex = -1;
        for (int i = 0; i < indexes.Count; i++)
        {
            if (0 <= indexes[i] && min > indexes[i])
            {
                minIndex = i;
                min = indexes[i];
            }
        }

        if (0 <= minIndex)
        {
            switch (minIndex)
            {
                case 0:
                    tagType = TextTag.Color;
                    break;
                case 1:
                    tagType = TextTag.Outline;
                    break;
                case 2:
                    tagType = TextTag.Size;
                    break;
            }

            startIndex = indexes[minIndex];
        }

        if (0 <= startIndex)
        {
            return true;
        }

        return false;
    }

    public List<string> GetTextKey2Split(string key, out string originText)
    {
        List<string> split = new List<string>();

        var text = GetText(key);
        originText = text;

        if (false == string.IsNullOrEmpty(text))
        {
            while (0 < text.Length)
            {
                var startIndex = text.IndexOf(Key2);

                if (0 > startIndex)
                {
                    split.Add(text);
                    break;
                }

                int endIndex = text.IndexOf(Key2, startIndex + Key2_Length);

                split.Add(text.Substring(0, startIndex));
                split.Add(text.Substring(startIndex, (endIndex + Key2_Length) - startIndex));

                text = text.Substring(endIndex + Key2_Length, text.Length - (endIndex + Key2_Length));
            }
        }

        return split;
    }

    public string GetTextFromDic(string key)
    {
        string value = null;
        _text.TryGetValue(key, out value);

        if (string.IsNullOrEmpty(value))
        {
            LogNotFound(key);
        }

        return value;
    }

    string GetTableData(string resource)
    {
        var text = "";

        if (true == _assetbundleMode)
        {
            text = ResourceLoad.GetTableData(resource);

            System.IO.File.WriteAllText(Application.persistentDataPath + "/" + resource + ".txt", text);
        }
        else
        {
            var asset = Resources.Load<TextAsset>("Data/TableData/" + resource);
            if (null != asset)
            {
                text = asset.text;
            }
        }

        return text;
    }

    public void UpdateFontAsset(bool forceResource = false)
    {
        TMP_FontAsset fontAsset = null;
        switch (_currentLanguage)
        {
            case Language.KO:
                fontAsset = ResourceLoad.GetTMPFontAsset("Font_ko", forceResource);
                break;
            case Language.EN:
                fontAsset = ResourceLoad.GetTMPFontAsset("Font_en", forceResource);
                break;
            case Language.CH:
                //fontAsset = ResourceLoad.GetTMPFontAsset("Font_tw", forceResource);
                break;
        }

        if (null != fontAsset)
        {
            TMP_Settings.fallbackFontAssets.Clear();
            TMP_Settings.fallbackFontAssets.Add(fontAsset);
        }
    }

    void LogNotFound(string key)
    {
        if (true == Application.isPlaying)
        {
            // if (false == Test_Manager.Instance.TestMode)
            // {
            //     LogManager.LogWarning("Not found text:" + key);
            // }
        }
    }
}
