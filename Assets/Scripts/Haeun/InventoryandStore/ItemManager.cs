using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;


[System.Serializable]
public class GameItem
{
    public GameItem(string _Id, string _Name, string _Description)
    {
        Id = _Id; Name = _Name; Description = _Description;
    }

    public string Id, Name, Description;
}

public class ItemManager : MonoBehaviour
{
    [SerializeField] private List<GameItem> AllItemList = new List<GameItem>();
    [SerializeField] private List<GameItem> MyItemList = new List<GameItem>();

    void Start()
    {
        LoadItemsFromCSV("Item"); // CSV 파일 이름, 확장자는 제외
        //SaveItem();
        SaveItem();

        Debug.Log($"Loaded {AllItemList.Count} items.");

        // // 예시로 디버깅을 위해 MyItemList를 채우는 코드 추가
        // MyItemList.Add(new GameItem("1", "Item A", "Description A"));
        // MyItemList.Add(new GameItem("2", "Item B", "Description B"));
    }

    void LoadItemsFromCSV(string fileName)
    {
        var data = CSVReader.Read(fileName);

        if (data != null)
        {
            foreach (var entry in data)
            {
                string id = entry["id"].ToString();
                string name = entry["name"].ToString();
                string description = entry["description"].ToString();

                AllItemList.Add(new GameItem(id, name, description));
            }
        }
        else
        {
            Debug.LogError("Failed to load CSV data.");
        }
    }

    void SaveItem()
    {
        string jdata = JsonConvert.SerializeObject(AllItemList, Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Resources/MyItemList.txt", jdata);
    }

    void LoadItem() {
        string jdata = File.ReadAllText(Application.dataPath + "/Resources/MyItemList.txt");
        MyItemList = JsonConvert.DeserializeObject<List<GameItem>>(jdata);
    }
}