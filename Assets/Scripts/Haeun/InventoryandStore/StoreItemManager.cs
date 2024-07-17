using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using System;
using TMPro;

[System.Serializable]
public class Serialization_Store<T>
{
    public Serialization_Store(List<T> _target) => target = _target;
    public List<T> target;
}

[System.Serializable]
public class StoreItem
{
    public StoreItem(string _Id, string _Name, string _Description, string _Type, bool _isUsing)
    {
        Id = _Id; Name = _Name; Description = _Description; Type = _Type; isUsing =  _isUsing;
    }

    public string Id, Name, Description, Type;
    public bool isUsing;
}

public class StoreItemManager : MonoBehaviour
{
    [SerializeField] private List<StoreItem> AllItemList = new List<StoreItem>();
    [SerializeField] private List<StoreItem> MyItemList = new List<StoreItem>();
    [SerializeField] private List<StoreItem> CurItemList = new List<StoreItem>();
    public string curType = "장비"; // 현재 고른 탭의 타입이 무엇인지
    public GameObject[] slot;
    public Image[] TabImage;
    public Color TabSelectColor =  new Color32(186, 227, 255, 255);

    public Color TabIdleColor = new Color32(255, 255, 255, 255);
    private string filePath;
    
    void Start()
    {
        filePath = Application.persistentDataPath + "/MyItemtext.txt";
        print(filePath);

        LoadItemsFromCSV("ItemSong"); // CSV 파일 이름, 확장자는 제외

        Debug.Log($"Loaded {AllItemList.Count} items.");

        LoadItem();

        



        // MyItemList의 내용을 확인하기 위한 디버그 로그
        Debug.Log("MyItemList 내용 로드 후:");
        foreach (var item in MyItemList)
        {
            Debug.Log($"ID: {item.Id}, Name: {item.Name}, Description: {item.Description}");
        }
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
                string type = entry["type"].ToString();
                bool isUsing;

                if (!bool.TryParse(entry["isUsing"].ToString(), out isUsing))
                {
                    isUsing = false; // 파싱 실패 시 기본값 설정
                }

                AllItemList.Add(new StoreItem(id, name, description, type, isUsing));
            }
        }
        else
        {
            Debug.LogError("CSV 데이터를 불러오지 못했습니다.");
        }
    }

    // 탭에 따른 탭 선택 변경
    public void TapClick(string tabName)
    {
        // 현재 아이템 리스트에 클릭한 타입만 추가하기
        curType = tabName;
        CurItemList = AllItemList.FindAll(x => x.Type == tabName);

        // 슬롯과 텍스트를 보일 수 있도록 만들기
        for (int i = 0; i < slot.Length; i++)
        {
            bool active = i < CurItemList.Count;
            slot[i].SetActive(active);

            if (active)
            {
                TextMeshProUGUI textComponent = slot[i].GetComponentInChildren<TextMeshProUGUI>();
                if (textComponent != null)
                {
                    textComponent.text = CurItemList[i].Name;
                }
                else
                {
                    Debug.LogError("TextMeshProUGUI component not found in slot's children.");
                }
            }
        }


        int tabNum = 0;
        switch (tabName) {
            case "장비" : tabNum = 0; break;
            case "물약" : tabNum = 1; break;
            case "단서" : tabNum = 2; break;
            case "기타" : tabNum = 3; break;
        }
        for (int i = 0; i <TabImage.Length; i++) {
            TabImage[i].color = i == tabNum ? TabSelectColor : TabIdleColor;
        }
    }

    // 리셋 버튼을 눌렀을 경우
    public void ReSetItemClick()
    {
        StoreItem BasicItem = AllItemList.Find(x => x.Name == "기본템");
        if (BasicItem != null)
        {
            MyItemList = new List<StoreItem>() { BasicItem };
        }
        else
        {
            Debug.LogError("Name이 기본템인 아이템을 찾을 수 없습니다.");
            MyItemList.Clear(); // 빈 리스트로 설정하여 빈 값을 저장하지 않도록 합니다.
        }
        SaveItem();
    }

    void SaveItem()
    {
        string jdata = JsonUtility.ToJson(new Serialization_Store<StoreItem>(MyItemList));
        File.WriteAllText(filePath, jdata);
    }

    void LoadItem()
    {
        if (!File.Exists(filePath))
        {
            ReSetItemClick(); // 초기 파일 생성 및 저장
            return;
        }

        string jdata = File.ReadAllText(filePath);
        MyItemList = JsonUtility.FromJson<Serialization_Store<StoreItem>>(jdata).target;

        // Inspector에서 리스트가 업데이트되도록 합니다.
        UnityEditor.EditorUtility.SetDirty(this);

        TapClick(curType);
    }

}
