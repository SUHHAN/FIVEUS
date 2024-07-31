using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Unity.Profiling;
using UnityEngine.UIElements;
using System.Data.Common;

[System.Serializable]
public class Item
{
    public Item(string _Id, string _Name, string _Description, string _Type, bool _isUsing, string _Price, bool _isSelling, string _value, string _quantity)
    {
        Id = _Id; Name = _Name; Description = _Description; Type = _Type; isUsing = _isUsing; Price = _Price; 
        isSelling = _isSelling; value = _value; quantity = _quantity;
    }

    public string Id, Name, Description, Type, Price;
    public bool isUsing, isSelling;
    public string value, quantity;
}

[System.Serializable]
public class Character
{
    public Character(string _Id, string _Name, string _Description, string _HP, string _STR, string _DEX, string _INT, string _CON, string _DEF, string _ATK, bool _isUsing, string _Type, string _Love, bool _Success, string _TeamCount)
    {
        Id = _Id; Name = _Name; Description = _Description; 
        HP = _HP; STR = _STR; DEX = _DEX; 
        INT = _INT; CON = _CON; DEF = _DEF; ATK = _ATK;
        isUsing = _isUsing; Type = _Type; Love = _Love; Success = _Success;
        TeamCount = _TeamCount;
    }

    // 캐릭터 관련 변수들
    public string Id, Name, Description, HP, STR, DEX, INT, CON, DEF, ATK;
    public string Type, Love, TeamCount;
    public bool isUsing, Success;
}

[System.Serializable]
public class PlayerData
{
    // 일단 이런 데이터들을 모아두기
    public string Player_name = "용사님";             // 플레이어 이름 데이터 변수
    public int Player_day = 1;              // 날짜 데이터 변수
    public int Player_team = 0;             // 단합력 데이터 변수
    public int Player_hp = 100;               // 체력 데이터 변수
    public int Player_tired = 0;            // 피로도 데이터 변수
    public int Player_money = 0;            // 피로도 데이터 변수
    public int Player_hint = 0;             // 힌트 데이터 변수
    public int Player_howtoday = 0;            // 기본 활동 일차 데이터 변수
    public int Player_howtrain = 0;            // 기본 활동 훈련 변수 데이터 변수

    // 캐릭터 관련
    public List<Character> characters = new List<Character>();

    // 아이템 관련
    public List<Item> Items = new List<Item>();
}

public class DataManager : MonoBehaviour
{
    public PlayerData nowPlayer = new PlayerData();
    public string path;
    public int nowSlot;

    private PlayerManager_yj PlayerManager_yj; // PlayerManager_yj 스크립트 참조

    
    // csv 정보 읽어오기 변수
    [SerializeField] private List<Item> CSVitem = new List<Item>();
    [SerializeField] private List<Character> CSVCharacter = new List<Character>();

    // 데이터 변경 이벤트
    public event Action OnDataChanged;

    // 싱글톤 구성 & 게임 오브젝트가 터지지 않도록 하게 함.
    public static DataManager instance;
    private void Awake() {
        #region 싱글톤
        if(instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject); // 게임 오브젝트가 터지지 않도록
        #endregion
        path = Application.persistentDataPath + "/saveGameData"; // 직접 경로를 설정하기 어려운 경우는, 유니티에서 기본적으로 제공하는 path 사용
    }

    public void SaveData()
    {   
        // 데이터를 현재 폴더 내에 생성. 
        string data = JsonUtility.ToJson(nowPlayer);
        print(data);
        File.WriteAllText(path + nowSlot.ToString(), data);
        OnDataChanged?.Invoke(); // 데이터 저장 시 이벤트 호출

    }

    public void LoadData()
    {
        //nowPlayer = new PlayerData();
        string data = File.ReadAllText(path + nowSlot.ToString());
        Debug.Log($"Loaded Data: {data}"); // JSON 데이터를 출력
        nowPlayer = JsonUtility.FromJson<PlayerData>(data); // 불러온 데이터가 PlayerData 형태로 저장되어 있음.

        // 중요한 데이터 로그 출력
        Debug.Log($"Player Name: {nowPlayer.Player_name}");
        Debug.Log($"Player Day: {nowPlayer.Player_day}");
        Debug.Log($"Number of Items: {nowPlayer.Items.Count}");
        Debug.Log($"Number of Characters: {nowPlayer.characters.Count}");

        OnDataChanged?.Invoke(); // 데이터 로드 시 이벤트 호출
    }

    public void ClearData() {
        nowSlot = -1;
        // 이렇게 되면 현재 플레이어에 담긴 정보값이 모두 사라지게 된다.
        nowPlayer = new PlayerData();
        OnDataChanged?.Invoke(); // 데이터 초기화 시 이벤트 호출
    }

    public void LoadItemsFromCSV(string fileName)
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
                bool isUsing, isSelling;
                string price = entry["price"].ToString();

                if (!bool.TryParse(entry["isUsing"].ToString(), out isUsing))
                {
                    isUsing = false; // 파싱 실패 시 기본값 설정
                }
                if (!bool.TryParse(entry["isSelling"].ToString(), out isSelling))
                {
                    isSelling = false; // 파싱 실패 시 기본값 설정
                };
                string value = entry["value"].ToString();
                string quantity = entry["quantity"].ToString();

                var newItem = new Item(id, name, description, type, isUsing, price, isSelling, value, quantity);
                CSVitem.Add(newItem);
                nowPlayer.Items.Add(newItem); // nowPlayer의 Items 리스트에 추가
            }
        }
        else
        {
            Debug.LogError("CSV 데이터를 불러오지 못했습니다.");
        }
    }

    public void LoadCharactersFromCSV(string fileName, string playerName)
    {
        var data = CSVReader.Read(fileName);

        if (data != null)
        {
            foreach (var entry in data)
            {
                string name;
                if (playerName != null && entry["name"].ToString() == "{player_name}") {
                    name = playerName;
                }
                else {
                    name = entry["name"].ToString();
                }

                // Name, Description, HP, STR, DEX, INT, CON, DEF, ATK, isUsing, Type, love, Success;
                string id = entry["id"].ToString();
                string description = entry["description"].ToString();
                string HP = entry["HP"].ToString();
                string STR = entry["STR"].ToString();
                string DEX = entry["DEX"].ToString();
                string INT = entry["INT"].ToString();
                string CON = entry["CON"].ToString();
                string DEF = entry["DEF"].ToString();
                string ATK = entry["ATK"].ToString();
                string type = entry["type"].ToString();
                bool isUsing;
                bool Success;
                string love = entry["love"].ToString();
                string TeamCount = entry["TeamCount"].ToString();
                
                if (!bool.TryParse(entry["isUsing"].ToString(), out isUsing))
                {
                    isUsing = false; // 파싱 실패 시 기본값 설정
                }
                if (!bool.TryParse(entry["Success"].ToString(), out Success))
                {
                    Success = false; // 파싱 실패 시 기본값 설정
                }

                var newCharacter = new Character(id, name, description, HP, STR, DEX, INT, CON, DEF, ATK, isUsing, type, love, Success, TeamCount);
                CSVCharacter.Add(newCharacter);
                nowPlayer.characters.Add(newCharacter); // nowPlayer의 characters 리스트에 추가
            }
        }
        else
        {
            Debug.LogError("CSV 데이터를 불러오지 못했습니다.");
        }
    }
}