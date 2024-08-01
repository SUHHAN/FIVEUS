using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
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


    // // 각 속성의 새 값을 계산하는 메서드
    // private string CalculateNewValue(string originalValue, string teamCount)
    // {
    //     // 각 능력치는 [ (원래 능력치 * 0.1 * 팀 단합력) + 원래 능력치 ]로 바꿔서 저장하기 -> 임의로 정함.
    //     int originalIntValue = int.Parse(originalValue);
    //     int teamCountInt = int.Parse(teamCount);
    //     int newValue = (int)(originalIntValue * 0.05 * teamCountInt) + originalIntValue;
    //     return newValue.ToString();
    // }

    // public void abilityChange()
    // {
    //     List<Character> PartyCharacters = DataManager.instance.nowPlayer.characters.FindAll(x => x.isUsing == true);

    //     foreach (var ii in PartyCharacters)
    //     {
    //         // 각 속성에 대해 계산을 수행하고 결과를 string으로 다시 저장
    //         ii.CON = CalculateNewValue(ii.CON, ii.TeamCount);
    //         ii.DEF = CalculateNewValue(ii.DEF, ii.TeamCount);
    //         ii.DEX = CalculateNewValue(ii.DEX, ii.TeamCount);
    //         ii.INT = CalculateNewValue(ii.INT, ii.TeamCount);
    //         ii.STR = CalculateNewValue(ii.STR, ii.TeamCount);

    //         // 최종 ATK 값을 계산하여 string으로 저장
    //         int totalATK = int.Parse(ii.CON) + int.Parse(ii.DEF) + int.Parse(ii.DEX) + int.Parse(ii.INT) + int.Parse(ii.STR);

    //         ii.ATK = totalATK.ToString();
    //     }
    // }

    
    public void Team_Activity() {
        List<Character> PartyCharacters = DataManager.instance.nowPlayer.characters.FindAll(x=> x.isUsing == true); 
        Character playerTC = DataManager.instance.nowPlayer.characters.Find(x=> x.Id == "0");

        int teamSum = DataManager.instance.nowPlayer.Player_team;

        int Team_Sum = 0;
        int TeamATK_Sum = 0;
        
        // 플레이어 제외 단합 횟수 구하기
        // 각각 팀원의 단합 횟수 +1 
        foreach(var ii in PartyCharacters) 
        { 
            int TeamCount = int.Parse(ii.TeamCount);
            TeamCount += 1;
            ii.TeamCount = TeamCount.ToString();

        }

        // foreach(var ii in PartyCharacters) 
        // { 
        //     Team_Sum += int.Parse(ii.TeamCount);
        //     TeamATK_Sum += int.Parse(ii.ATK);
        // }

        // // 팀의 총 단합력 계산
        // Team_Sum -= int.Parse(playerTC.TeamCount);

        // int PartyTeam = Team_Sum * TeamATK_Sum/10;
        // int PartyATK = PartyTeam + TeamATK_Sum;
        
        // DataManager.instance.nowPlayer.Player_team = PartyTeam;
        // DataManager.instance.nowPlayer.Party_ATK = PartyATK;
        
        foreach(var ii in PartyCharacters) 
        { 
            Team_Sum += int.Parse(ii.TeamCount) / 10 * int.Parse(ii.ATK);

            TeamATK_Sum += int.Parse(ii.ATK);
        }

        // 팀의 총 단합력 계산
        Team_Sum -= int.Parse(playerTC.TeamCount) / 10 * int.Parse(playerTC.ATK);

        int PartyTeam = Team_Sum;
        int PartyATK = PartyTeam + TeamATK_Sum;
        
        DataManager.instance.nowPlayer.Player_team = PartyTeam;
        DataManager.instance.nowPlayer.Party_ATK = PartyATK;


        SaveCharacter();
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
