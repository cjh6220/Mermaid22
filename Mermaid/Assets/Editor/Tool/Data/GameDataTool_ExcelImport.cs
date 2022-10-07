
using UnityEditor;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using OfficeOpenXml;
using Excel;
using System.Data;

[Serializable]
public class GameDataTool_ExcelImport
{
    public const string SaveFileName_CheckList = "tool_excel_checklist";
    public const string SaveFileName_Path = "tool_excel_path";

    public class SaveData
    {
        public class Info
        {
            public string ExcelName;
            public string ExcelPath;
            public bool Check = false;
        }

        public List<Info> InfoList = new List<Info>();

        public void Reset()
        {
            InfoList.Clear();
        }

        public void AddInfo(string excelName, string excelPath, bool check = false)
        {
            InfoList.Add(new Info() { ExcelName = excelName, ExcelPath = excelPath, Check = check });
        }

        public Info GetInfo(string excelName)
        {
            return InfoList.Find((infoData) => infoData.ExcelName.Equals(excelName));
        }
    }

    SaveData _saveInfo = new SaveData();
    int _index = 0;

    [Serializable]
    public class ExcelInfo
    {
        [ToggleGroup("Enabled", "$Label")]
        public bool Enabled;

        [HideInInspector]
        public string Label;

        [HideInInspector]
        public string Path;

        // [TableMatrix(Transpose = true)]
        // [ShowInInspector]
        // [ReadOnly]
        // public string[,] Data;
    }

    public GameDataTool_ExcelImport(int index)
    {
        ExcelForderPath = null;

        _index = index;

        Load();
    }

    public void Load()
    {
        LoadInfo();

        if (0 < _saveInfo.InfoList.Count)
        {
            for (int i = 0; i < _saveInfo.InfoList.Count; i++)
            {
                ExcelInfoList.Add(new ExcelInfo { Label = _saveInfo.InfoList[i].ExcelName, Path = _saveInfo.InfoList[i].ExcelPath, Enabled = _saveInfo.InfoList[i].Check });
            }
        }
        else
        {
            LoadExcel();
        }
    }

    [PropertyOrder(1)]
    [LabelText("엑셀 폴더 경로")]
    [FolderPath(RequireExistingPath = true)]
    public string ExcelForderPath;

    [PropertyOrder(2)]
    [ShowInInspector]
    [LabelText("새로고침")]
    [Button(ButtonSizes.Small)]

    //    [GUIColor(0.0f, 0.5f, 1.0f)]
    void LoadExcel()
    {
        SaveInfo();

        _saveInfo.Reset();

        if (null != ExcelForderPath)
        {
            try
            {
                var dirctoryInfo = new System.IO.DirectoryInfo(ExcelForderPath);

                ExcelInfoList.Clear();
                foreach (var file in dirctoryInfo.GetFiles())
                {
                    if (file.FullName.Contains(".xlsx") && false == file.FullName.Contains("~$"))
                    {
                        ExcelInfoList.Add(new ExcelInfo { Label = file.Name, Path = file.FullName });
                    }
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);

                string fileSavePath = Application.persistentDataPath + "/" + SaveFileName_Path + _index + ".txt";
                System.IO.File.Delete(fileSavePath);

                fileSavePath = Application.persistentDataPath + "/" + SaveFileName_CheckList + _index + ".txt";
                System.IO.File.Delete(fileSavePath);

                ExcelForderPath = null;
            }
        }
    }

    [PropertyOrder(3)]
    [OnInspectorGUI] private void Space1() { GUILayout.Space(5); }

    [PropertyOrder(4)]
    [ShowInInspector]
    [LabelText("전체 선택")]
    [Button(ButtonSizes.Medium)]
    [ResponsiveButtonGroup]
    void SelectAll()
    {
        for (int i = 0; i < ExcelInfoList.Count; i++)
        {
            ExcelInfoList[i].Enabled = true;
        }
    }

    [PropertyOrder(4)]
    [ShowInInspector]
    [LabelText("전체 해제")]
    [Button(ButtonSizes.Medium)]
    [ResponsiveButtonGroup]
    void UnselectAll()
    {
        for (int i = 0; i < ExcelInfoList.Count; i++)
        {
            ExcelInfoList[i].Enabled = false;
        }
    }


    [PropertyOrder(5)]
    [ShowInInspector]
    [LabelText("가져오기")]
    [Button(ButtonSizes.Large)]
    [GUIColor(0.0f, 0.5f, 1.0f)]
    void ImportExcel()
    {
        _saveInfo.Reset();
        for (int i = 0; i < ExcelInfoList.Count; i++)
        {
            _saveInfo.AddInfo(ExcelInfoList[i].Label, ExcelInfoList[i].Path, ExcelInfoList[i].Enabled);
        }

        SaveInfo();

        string tablePath = System.Environment.CurrentDirectory + "/Assets/Resources/Data/TableData";
        string classPath = System.Environment.CurrentDirectory + "/Assets/_Scripts/TableData";

        for (int i = 0; i < ExcelInfoList.Count; i++)
        {
            if (ExcelInfoList[i].Enabled)
            { 
                System.IO.FileStream file = null;

                try
                {
                    file = new System.IO.FileStream(ExcelInfoList[i].Path, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite);
                }
                catch (System.Exception e)
                {
                    file = null;
                    Debug.LogError(ExcelInfoList[i].Path + "\nFileStream Exception:" + e);
                }

                if (null == file)
                {
                    continue;
                }

                try
                {
                    using (ExcelPackage package = new ExcelPackage(file))
                    {
                        foreach (var worksheet in package.Workbook.Worksheets)
                        {
                            Debug.Log("-----" + worksheet.Name);
                            WriteFile(worksheet, tablePath, classPath, true);
                        }
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogError(ExcelInfoList[i].Path + "\nExcelPackage Exception:" + e);
                }
                finally
                {
                    file.Close();
                }
            }
        }

        AssetDatabase.Refresh();

        Debug.Log("<color=magenta>Import 완료</color>");
    }

    int GetColumnsCount(ExcelWorksheet ws)
    {
        int count = 0;
        for (; count < ws.Dimension.Columns; count++)
        {
            object obj = ws.Cells[1, count + 1].Value;
            if (obj == null || obj.ToString() == string.Empty)
                return count;
        }
        return count;
    }

    int GetRowsCount(ExcelWorksheet ws)
    {
        int count = 0;
        for (; count < ws.Dimension.Rows; count++)
        {
            object obj = ws.Cells[count + 1, 1].Value;
            if (obj == null || obj.ToString() == string.Empty)
                return count;
        }
        return count;
    }

    [PropertyOrder(7)]
    [OnInspectorGUI] private void Space2() { GUILayout.Space(5); }

    [PropertyOrder(8)]
    [ListDrawerSettings(Expanded = true, NumberOfItemsPerPage = 100)]
    public List<ExcelInfo> ExcelInfoList = new List<ExcelInfo>();

    void LoadInfo()
    {
        string fileSavePath = Application.persistentDataPath + "/" + SaveFileName_Path + _index + ".txt";

        try
        {
            ExcelForderPath = System.IO.File.ReadAllText(fileSavePath);
        }
        catch (System.Exception e)
        {            
            //Debug.LogError(e);
        }

        fileSavePath = Application.persistentDataPath + "/" + SaveFileName_CheckList + _index + ".txt";

        try
        {
            var text = System.IO.File.ReadAllText(fileSavePath);

            if (false == string.IsNullOrEmpty(text))
            {
                
                _saveInfo = JsonUtility.FromJson<SaveData>(text);
            }
        }
        catch (System.Exception e)
        {
            //Debug.LogError(e);
        }
    }

    void SaveInfo()
    {
        if (null != ExcelForderPath)
        {
            string fileSavePath = Application.persistentDataPath + "/" + SaveFileName_Path + _index + ".txt";

            try
            {
                System.IO.File.WriteAllText(fileSavePath, ExcelForderPath);
            }
            catch (System.Exception e)
            {
                //Debug.LogError(e);
            }
        }

        {
            string fileSavePath = Application.persistentDataPath + "/" + SaveFileName_CheckList + _index + ".txt";

            try
            {
                System.IO.File.WriteAllText(fileSavePath, JsonUtility.ToJson(_saveInfo));
            }
            catch (System.Exception e)
            {
                //Debug.LogError(e);
            }
        }
    }

    void WriteFile(ExcelWorksheet wb, string tablePath, string classPath, bool textImportText = false)
    {
        int rowNum = wb.Dimension.Rows;
        int colNum = wb.Dimension.Columns;
        ExcelRange data = wb.Cells;

        var csv = new System.Text.StringBuilder();

        List<bool> exportEnable = new List<bool>();
        List<string> dataType = new List<string>();
        List<string> dataName = new List<string>();
        Dictionary<int, bool> disableRow = new Dictionary<int, bool>();

        string saveName = "";

        bool empty = false;
        bool lineCheck = false;

        // for (int r = 1; r <= rowNum; r++)
        // {
        //     for (int c = 1; c <= colNum; c++)
        //     {
        //         Debug.Log(r + "/" + c + ":" + wb.Cells[r, c].Value.ToString());
        //     }
        // }        

        for (int r = 1; r <= rowNum; r++)
        {
            switch (r)
            {
                case 1:
                    if (null == data[1, 1].Value)
                    {
                        return;
                    }

                    saveName = data[1, 1].Value.ToString();

                    if (string.IsNullOrEmpty(saveName))
                    {
                        return;
                    }
                    saveName = saveName.Substring(0, 1).ToUpper() + saveName.Substring(1, saveName.Length - 1);
                    if (false == saveName.Equals("String_table") && false == saveName.Equals("ErrorCode_String"))
                    {
                        Debug.Log("<color=blue>" + saveName + ".csv </color>");
                    }
                    break;
                case 2:
                    {
                        for (int c = 1; c <= colNum; c++)
                        {
                            if (null != data[r, c].Value)
                            {
                                dataType.Add(data[r, c].Value.ToString());
                                exportEnable.Add(true);
                            }
                            else
                            {
                                dataType.Add("");
                                exportEnable.Add(false);
                            }
                        }
                    }
                    break;
                case 3:
                    {
                        for (int c = 1; c <= colNum; c++)
                        {
                            if (null != data[r, c].Value && true == exportEnable[c - 1])
                            {
                                dataName.Add(data[r, c].Value.ToString().Replace(' ', '_'));
                            }
                            else
                            {
                                dataName.Add("");
                            }
                        }
                    }
                    break;
                case 4:
                    {
                        for (int c = 1; c <= colNum; c++)
                        {
                            if (null != data[r, c].Value && true == exportEnable[c - 1])
                            {
                                exportEnable[c - 1] = data[r, c].Value.ToString().Equals("1") || data[r, c].Value.ToString().Equals("0");
                            }
                        }
                    }
                    break;
                default:
                    {
                        for (int c = 1; c <= colNum; c++)
                        {
                            string cellText = "";

                            if (c == 1)
                            {
                                if (data[r, c].Value != null)
                                {
                                    if (data[r, c].Value.ToString().Contains("#"))
                                    {
                                        break;
                                    }
                                }
                            }

                            if (c == 1)
                            {
                                if (data[r, c].Value != null)
                                {
                                    if (data[r, c].Value.ToString().Equals(""))
                                    {
                                        empty = true;
                                        break;
                                    }
                                }
                                else
                                {
                                    empty = true;
                                    break;
                                }

                                if (lineCheck)
                                {
                                    csv.Append('\n');
                                }
                            }

                            lineCheck = true;

                            if (false == exportEnable[c - 1])
                            {
                                dataType[c - 1] = "";
                                continue;
                            }

                            if (data[r, c].Value != null)
                            {
                                cellText = data[r, c].Value.ToString();
                                cellText = cellText.Replace("\r", "");
                                cellText = cellText.Replace("\n", "");
                            }
                            else
                            {
                                switch (dataType[c - 1])
                                {
                                    case "int":
                                        cellText = "0";
                                        break;
                                    case "float":
                                        cellText = "0.0";
                                        break;
                                    case "string":
                                        cellText = "";
                                        break;
                                    case "bool":
                                        cellText = "true";
                                        break;
                                }
                            }



                            csv.Append(cellText);

                            if (colNum != c)
                            {
                                if (saveName.Equals("String_table") || saveName.Equals("ErrorCode_String"))
                                {
                                    csv.Append("\t");
                                }
                                else
                                {
                                    csv.Append(",");
                                }
                            }

                        }
                    }
                    break;
            }

            if (empty)
            {
                break;
            }
        }

        int exportColumnCount = 0;
        foreach (var checkData in exportEnable)
        {
            if (true == checkData)
            {
                exportColumnCount++;
            }
        }

        if (1 < exportColumnCount)
        {
            if (true == saveName.Equals("String_table"))
            {
                var csvResult = csv.ToString();



                var text = csvResult.Replace("\r", "");
                var lines = text.Split('\n');

                var split = lines[0].Split('\t');
                int checkCount = split.Length;

                if (1 < checkCount)
                {
                    var checkColumn = 1;

                    while (checkCount > checkColumn)
                    {
                        var fileBuilder = new System.Text.StringBuilder();
                        for (int i = 0; i < lines.Length; i++)
                        {
                            var lineSplit = lines[i].Split('\t');
                            fileBuilder.Append(lineSplit[0] + '\t' + lineSplit[checkColumn] + '\n');
                        }

                        saveName = "String_" + dataName[checkColumn + 1];
                        Debug.Log("<color=blue>" + saveName + ".csv </color>");

                        System.IO.File.WriteAllText(tablePath + "/" + saveName + ".csv", fileBuilder.ToString());

                        checkColumn++;
                    }

                    if (true == textImportText)
                    {
                        // try
                        // {
                        //     checkColumn = 1;

                        //     var scripts = System.IO.File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + "/Assets/Resources/Data/StoryData/ScriptData/Script.csv");
                        //     var scriptLines = scripts.Split('\n');

                        //     while (checkCount > checkColumn)
                        //     {
                        //         var fileBuilder = new System.Text.StringBuilder();

                        //         int checkScriptIndex = -1;
                        //         switch (checkColumn)
                        //         {
                        //             case (int)TextManager.Language.KO:
                        //                 checkScriptIndex = 6;
                        //                 break;
                        //             case (int)TextManager.Language.EN:
                        //                 checkScriptIndex = 7;
                        //                 break;
                        //             case (int)TextManager.Language.TW:
                        //                 checkScriptIndex = 9;
                        //                 break;
                        //         }

                        //         if (0 < checkScriptIndex)
                        //         {
                        //             for (int i = 0; i < scriptLines.Length; i++)
                        //             {
                        //                 var line = scriptLines[i].Split('\t');

                        //                 if (line.Length > checkScriptIndex)
                        //                 {
                        //                     var saveString = line[checkScriptIndex];

                        //                     for (int checkNumber = 0; checkNumber < 10; checkNumber++)
                        //                     {
                        //                         saveString = saveString.Replace(checkNumber.ToString(), "");
                        //                     }

                        //                     fileBuilder.Append(saveString);
                        //                 }
                        //             }
                        //         }



                        //         for (int i = 0; i < lines.Length; i++)
                        //         {
                        //             var lineSplit = lines[i].Split('\t');

                        //             var saveString = lineSplit[checkColumn];

                        //             for (int checkNumber = 0; checkNumber < 10; checkNumber++)
                        //             {
                        //                 saveString = saveString.Replace(checkNumber.ToString(), "");
                        //             }

                        //             fileBuilder.Append(saveString);
                        //         }

                        //         saveName = "OnlyFontCreation_String_" + dataName[checkColumn + 2];
                        //         Debug.Log("<color=blue>" + saveName + ".csv </color>");

                        //         System.IO.File.WriteAllText(tablePath + "/" + saveName + ".csv", fileBuilder.ToString());

                        //         checkColumn++;
                        //     }
                        // }
                        // catch (System.Exception e)
                        // {
                        //     Debug.LogError(e);
                        // }
                    }
                }
                else
                {
                    System.IO.File.WriteAllText(tablePath + "/" + saveName + ".csv", csv.ToString());
                }



            }
            else if (true == saveName.Equals("ErrorCode_String"))
            {
                var csvResult = csv.ToString();

                var text = csvResult.Replace("\r", "");
                var lines = text.Split('\n');

                var split = lines[0].Split('\t');
                int checkCount = split.Length;

                if (1 < checkCount)
                {
                    var checkColumn = 1;

                    while (checkCount > checkColumn)
                    {
                        var fileBuilder = new System.Text.StringBuilder();
                        for (int i = 0; i < lines.Length; i++)
                        {
                            var lineSplit = lines[i].Split('\t');
                            fileBuilder.Append(lineSplit[0] + '\t' + lineSplit[checkColumn] + '\n');
                        }

                        saveName = "String_ErrorCode_" + dataName[checkColumn + 2];
                        Debug.Log("<color=blue>" + saveName + ".csv </color>");

                        System.IO.File.WriteAllText(tablePath + "/" + saveName + ".csv", fileBuilder.ToString());

                        checkColumn++;
                    }
                }
                else
                {
                    System.IO.File.WriteAllText(tablePath + "/" + saveName + ".csv", csv.ToString());
                }



            }
            else
            {
#if UNITY_EDITOR
                Debug.Log(csv.ToString());
#endif

                var csvResult = Encrypt.AESEncrypt256(csv.ToString());

                System.IO.File.WriteAllText(tablePath + "/" + saveName + ".csv", csvResult);


                System.Text.StringBuilder classInit = new System.Text.StringBuilder();
                classInit.Append("    public Table_" + saveName + "(string[] data)\n    {\n");

                int index = 0;
                bool idCheck = false;
                for (int i = 0; i < dataType.Count; i++)
                {
                    switch (dataType[i])
                    {
                        case "int":
                            {
                                classInit.Append(string.Format("        if (false == string.IsNullOrEmpty(data[{0}]))\n", index));
                                classInit.Append(string.Format("            {0} = System.Convert.ToInt32(data[{1}]);\n", dataName[i], index));
                                if ((dataName[i].Equals("id") || dataName[i].Equals("level")) && false == idCheck)
                                {
                                    idCheck = true;

                                    classInit.Append(string.Format("        if (false == string.IsNullOrEmpty(data[{0}]))\n", index));
                                    classInit.Append(string.Format("            DataID = System.Convert.ToInt32(data[{0}]);\n", index));
                                }

                                index++;
                            }
                            break;
                        case "float":
                            classInit.Append(string.Format("        if (false == string.IsNullOrEmpty(data[{0}]))\n", index));
                            classInit.Append(string.Format("            {0} = System.Convert.ToSingle(data[{1}]);\n", dataName[i], index));
                            index++;
                            break;
                        case "string":
                            classInit.Append(string.Format("        {0} = data[{1}];\n", dataName[i], index));
                            index++;
                            break;
                        case "bool":
                            classInit.Append(string.Format("        if (false == string.IsNullOrEmpty(data[{0}]))\n", index));
                            classInit.Append(string.Format("            {0} = System.Convert.ToBoolean(data[{1}]);\n", dataName[i], index));
                            index++;
                            break;
                    }
                }

                classInit.Append("    }\n");

                System.Text.StringBuilder classVariable = new System.Text.StringBuilder();

                for (int i = 0; i < dataType.Count; i++)
                {
                    switch (dataType[i])
                    {
                        case "int":
                            classVariable.Append(string.Format("    public ObscuredInt {0};\n", dataName[i]));
                            break;
                        case "float":
                            classVariable.Append(string.Format("    public ObscuredFloat {0};\n", dataName[i]));
                            break;
                        case "string":
                            classVariable.Append(string.Format("    public ObscuredString {0};\n", dataName[i]));
                            break;
                        case "bool":
                            classVariable.Append(string.Format("    public ObscuredBool {0};\n", dataName[i]));
                            break;
                    }
                }

                var classText = string.Format("using System.Collections;\nusing System.Collections.Generic;\nusing CodeStage.AntiCheat.ObscuredTypes;\n\npublic class Table_{0} : Table_Base\n{{\n{1}\n{2}}}",
                                            saveName, classInit.ToString(), classVariable.ToString());

                System.IO.File.WriteAllText(classPath + "/Table_" + saveName + ".cs", classText);
            }
        }
    }
}
