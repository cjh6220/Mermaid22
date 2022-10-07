using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using OfficeOpenXml;
using System.Runtime.Serialization.Formatters.Binary;

public class Table_Load : MessageListener
{
    protected override void AddMessageListener()
    {
        base.AddMessageListener();

        AddListener(MessageID.OnClick_Update_Table);
        AddListener(MessageID.OnClick_Start);
    }

    protected override void OnMessage(MessageID msgID, object sender, object data)
    {
        base.OnMessage(msgID, sender, data);

        switch(msgID)
        {
            case MessageID.OnClick_Update_Table:
                {
                    UpdateTable();
                }
                break;
            case MessageID.OnClick_Start:
                {
                    SendMessageDelay(MessageID.Call_Scene_Load, Type_SceneName.LobbyScene);
                }
                break;
        }
    }

    void UpdateTable()
    {
        var dataPath = Application.persistentDataPath + "/Gift.xlsx";

        if (!File.Exists(dataPath))
        {
            SendMessage(MessageID.Event_Set_Log, "File Is Not Exist");
        }
        else
        {
            string tablePath = System.Environment.CurrentDirectory + "/Assets/Resources/Data/TableData";
            string classPath = System.Environment.CurrentDirectory + "/Assets/_Scripts/TableData";

            System.IO.FileStream file = null;

            try
            {
                file = new System.IO.FileStream(dataPath, FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite);
            }
            catch (System.Exception e)
            {
                file = null;

                BinaryFormatter bf = new BinaryFormatter();
                FileStream newFile = File.Create(Application.persistentDataPath + "/FileStream Error.txt");
                string str = dataPath + "\nFileStream Exception:" + e;
                bf.Serialize(newFile, str);
                newFile.Close();

                SendMessage(MessageID.Event_Set_Log, dataPath + "\nFileStream Exception:" + e);
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
                BinaryFormatter bf = new BinaryFormatter();
                FileStream newFile = File.Create(Application.persistentDataPath + "/ExcelPackage Error.txt");
                string str = dataPath + "\nExcelPackage Exception:" + e;
                bf.Serialize(newFile, str);
                newFile.Close();

                SendMessage(MessageID.Event_Set_Log, dataPath + "\nExcelPackage Exception:" + e);
            }
            finally
            {
                SendMessage(MessageID.Event_Set_Log, "File Load Success");
                file.Close();
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
