#if UNITY_EDITOR

using Sirenix.OdinInspector.Editor;
using System.Linq;
using UnityEngine;
using Sirenix.Utilities.Editor;
using Sirenix.Serialization;
using UnityEditor;
using Sirenix.Utilities;
using Sirenix.OdinInspector;


public class GameDataTool : OdinMenuEditorWindow
{
    GameDataTool_ExcelImport _tableAlpha;
    GameDataTool_ExcelImport _tableBeta;
    GameDataTool_ExcelImport _tableLive;

    [MenuItem("게임 툴/가져오기/테이블 데이터 (Excel)")]
    private static void OpenWindow()
    {
        var window = GetWindow<GameDataTool>();

        // Nifty little trick to quickly position the window in the middle of the editor.
        window.position = GUIHelper.GetEditorWindowRect().AlignCenter(700, 700);
    }

    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree(true);

        _tableAlpha = new GameDataTool_ExcelImport(1);
        tree.Add("알파", _tableAlpha, EditorIcons.Clock);

        _tableBeta = new GameDataTool_ExcelImport(2);
        tree.Add("베타", _tableBeta, EditorIcons.Car);

        _tableLive = new GameDataTool_ExcelImport(3);
        tree.Add("라이브", _tableLive, EditorIcons.Play);

        return tree;
    }



    [ShowInInspector]
    [HideLabel]
    public GameDataTool_ExcelImport ExcelImport = null;
}

#endif