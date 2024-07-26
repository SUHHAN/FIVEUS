using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.Profiling;
using UnityEngine.UIElements;

public class DataManager : MonoBehaviour
{
    // 저장하는 방법
    // 1. 저장할 데이터가 존재
    // 2. 데이터를 제이슨으로 변환
    // 3. 제이슨을 외부에 저장

    // 불러오는 방법
    // 1. 외부에 저장된 제이슨을 가져옴
    // 2. 제이슨을 데이터 형태로 변환
    // 3. 불러온 데이터를 사용


    public class PlayerData
    {
        // 일단 이런 데이터들을 모아두기
        public string name;             // 플레이어 이름 저장 변수
        public string job = "검사";      // 주인공의 전직 직업(기본 : 검사)
        public int coin;                 // 임시 재화 저장 변수
        public int item; // 일단 대응되는 무기
    }

    public PlayerData nowPlayer = new PlayerData();
    public string path;
    public int nowSlot;

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
        path = Application.persistentDataPath + "/save"; // 직접 경로를 설정하기 어려운 경우는, 유니티에서 기본적으로 제공하는 path 사용
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SaveData()
    {
        // 데이터를 현재 폴더 내에 생성. 
        string data = JsonUtility.ToJson(nowPlayer);
        print(path);
        File.WriteAllText(path + nowSlot.ToString(), data);
    }

    public void LoadData() 
    {
        // 데이터를 내가 원하는 형태로 가지고 올 수 있음. / 현재는 nowPlayer에 저장되어 있음.
        string data = File.ReadAllText(path + nowSlot.ToString());
        nowPlayer = JsonUtility.FromJson<PlayerData>(data); // 불러온 데이터가 PlayerData 형태로 저장되어 있음.
    }

    public void ClearData() {

        nowSlot = -1;

        // 이렇게 되면 정보값이 모두 사라지게 된다.
        nowPlayer = new PlayerData();
    }
}
