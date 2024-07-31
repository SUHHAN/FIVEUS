using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance;
    [SerializeField] private List<Character> AllCharacterList = new List<Character>();
    

    void Awake() {
        // 싱글톤 인스턴스 생성
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 전환되어도 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 존재하면 자신을 파괴
            return;
        }
    }

    void Start()
    {
        LoadCharacter();
    }




    

    void SaveCharacter()
    {
        DataManager.instance.SaveData();
    }

    void LoadCharacter()
    {
        AllCharacterList = DataManager.instance.nowPlayer.characters;
    }
}
