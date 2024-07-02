using UnityEngine;
using GoogleSheetsToUnity;
using System.Collections.Generic;
using System;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public struct DialogueData
{
    public int id;
    public string name;
    [TextArea] public string dialogue;

    public DialogueData(int id, string name, string dialogue)
    {
        this.id = id;
        this.name = name;
        this.dialogue = dialogue;
    }
}

[CreateAssetMenu(fileName = "Reader", menuName = "Scriptable Object/DialogueDataReader", order = int.MaxValue)]
public class DialogueDataReader : DataReaderBase
{
    [Header("스프레드시트에서 읽혀져 직렬화 된 오브젝트")][SerializeField] public List<DialogueData> DialogueList = new List<DialogueData>();

    internal void UpdateStats(List<GSTU_Cell> list, int DialogueID)
    {
        int id = -1;
        string name = null, dialogue = null;

        for (int i = 0; i < list.Count; i++)
        {
            switch (list[i].columnId)
            {
                case "id":
                    {
                        id = int.Parse(list[i].value);
                        break;
                    }
                case "name":
                    {
                        name = list[i].value;
                        break;
                    }
                case "dialogue":
                    {
                        dialogue = list[i].value;
                        break;
                    }
            }
        }

        DialogueList.Add(new DialogueData(id, name, dialogue));
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(DialogueDataReader))]
public class DialogueDataReaderEditor : Editor
{
    DialogueDataReader data;

    void OnEnable()
    {
        data = (DialogueDataReader)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Label("\n\n스프레드 시트 읽어오기");

        if (GUILayout.Button("데이터 읽기(API 호출)"))
        {
            UpdateStats(UpdateMethodOne);
            data.DialogueList.Clear();
        }
    }

    void UpdateStats(UnityAction<GstuSpreadSheet> callback, bool mergedCells = false)
    {
        SpreadsheetManager.Read(new GSTU_Search(data.associatedSheet, data.associatedWorksheet), callback, mergedCells);
    }

    void UpdateMethodOne(GstuSpreadSheet ss)
    {
        for (int i = data.START_ROW_LENGTH; i <= data.END_ROW_LENGTH; ++i)
        {
            data.UpdateStats(ss.rows[i], i);
        }

        EditorUtility.SetDirty(target);
    }
}
#endif