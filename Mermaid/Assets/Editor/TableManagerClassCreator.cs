#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class TableManagerClassCreator : Editor
{
    [MenuItem("Tools/Create Table_Manager (table data)")]
    static void CreateSDManager()
    {
        try
        {
            var directoryInfo = new System.IO.DirectoryInfo(System.Environment.CurrentDirectory + "/Assets/Resources/Data/TableData");
            FileInfo[] fileInfo = directoryInfo.GetFiles("*.csv");

            var sdManagerClassText = System.IO.File.ReadAllText(System.Environment.CurrentDirectory + "/Assets/Editor/Table_Manager_class.txt");

            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
            foreach (FileInfo file in fileInfo)
            {
                var fileName = file.Name.Substring(0, file.Name.LastIndexOf("."));

                if (fileName.Contains("String_") || fileName.Contains("_String"))
                {
                    Debug.LogWarning(fileName);
                    continue;
                }

                stringBuilder.Append(string.Format("        _typeList.Add(typeof(Table_{0}), \"{1}\");\n", fileName, fileName));
            }

            sdManagerClassText = sdManagerClassText.Replace("#1", stringBuilder.ToString());

            var outpath = System.Environment.CurrentDirectory + "/Assets/_Scripts/TableData/Table_Manager.cs";
            
            System.IO.File.WriteAllText(outpath, sdManagerClassText);

            AssetDatabase.Refresh();
        }
        catch (System.Exception e)
        {
            Debug.LogError("CreateList:" + e);
        }

        Debug.Log("Created TableManager");
    }


}

#endif
