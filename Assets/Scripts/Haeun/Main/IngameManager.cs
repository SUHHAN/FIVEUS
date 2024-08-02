using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IngameManager : MonoBehaviour
{
    private NpcScript NpcScript;
    
    void Start()
    {
        // Additively load the GUI scene
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }


    public void ReturnMain() {
        SceneManager.LoadScene("MainScene");
    }
    public void changeStore() {
        SceneManager.LoadScene("StoreMain");
    }
    public void changeInventory() {
        SceneManager.LoadScene("InventoryMain");
    }

    public void OnTestButton() {
        // hint 하나 얻기
        ItemManager.instance.GetHint_inv();
    }

    public void OnTestInitButton() {
        // 테스트를 위한 재화 및 수치들 조정
        DataManager.instance.nowPlayer.Player_money = 100000;
        DataManager.instance.nowPlayer.Player_tired = 99;
        DataManager.instance.nowPlayer.Player_hp = 1;

        List<Character> charac = DataManager.instance.nowPlayer.characters;
        foreach(var ii in charac) {
            ii.Success = true;
        }

        DataManager.instance.SaveData();
    }

    public void OnTestTeamButton()
    {   
        CharacterManager.instance.Team_Activity();
    }


    public void OnTestLoveButton() {
        // 암살자 선물하기 버튼 생성
        OnGiftButtonClick("암살자");
    }

    public void OnGiftButtonClick(string npc)
    {   
        PlayerPrefs.SetString("NpcType", npc);
        PlayerPrefs.Save(); 

        // 다른 씬에서 curType을 저장
        PlayerPrefs.SetString("CurType", "기타"); // "장비" 대신 원하는 탭 이름 사용
        PlayerPrefs.Save();
        
        print(PlayerPrefs.GetString("NpcType"));
        SceneManager.LoadScene("InventoryMain"); // InventoryMain 씬으로 이동
    }
}
