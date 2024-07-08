using UnityEngine;
using System.IO;

[System.Serializable]
public class GameData
{
    public int currentDay;
}

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance { get; private set; }

    private string dataPath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            dataPath = Application.persistentDataPath + "/savefile.json";
            Debug.Log("GameDataManager 인스턴스 초기화 완료");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveData(int currentDay)
    {
        GameData data = new GameData { currentDay = currentDay };
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(dataPath, json);
        Debug.Log("데이터 저장됨: " + dataPath);
    }

    public GameData LoadData()
    {
        if (File.Exists(dataPath))
        {
            string json = File.ReadAllText(dataPath);
            GameData data = JsonUtility.FromJson<GameData>(json);
            Debug.Log("저장된 데이터 불러옴: " + data.currentDay + "일차");
            return data;
        }
        else
        {
            Debug.Log("저장된 데이터가 없습니다.");
            return null;
        }
    }
}
