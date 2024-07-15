using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public class Item
{
    public Item(string _Id, string _Name, string _Explain)
    {
        Id = _Id; Name = _Name; Explain = _Explain;
    }

    public string Id, Name, Explain;
}

public class GameManager_CSV : MonoBehaviour
{
    public TextAsset ItemDatabase;
    [SerializeField] private List<Item> AllItemList = new List<Item>();
    [SerializeField] private List<Item> MyItmeList = new List<Item>();

    void Start()
    {
        LoadItemsFromCSV();
        Save();
        Debug.Log($"Loaded {AllItemList.Count} items.");
    }

    void LoadItemsFromCSV()
    {
        // 첫 번째 행을 제외하고 나머지 행을 리스트화
        string[] lines = ItemDatabase.text.Split('\n');
        for (int i = 1; i < lines.Length; i++)
        {
            string[] row = lines[i].Split(',');
            if (row.Length >= 3)  // 데이터가 세 개 이상 있는 경우만 추가
            {
                AllItemList.Add(new Item(row[0].Trim(), row[1].Trim(), row[2].Trim()));
            }
        }
    }

    void Save()
    {
        string jdata = JsonConvert.SerializeObject(AllItemList, Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Resources/AllItmeList.txt", jdata);
    }
}

