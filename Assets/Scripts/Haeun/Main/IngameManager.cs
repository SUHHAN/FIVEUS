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

        DataManager.instance.SaveData();
    }

    public void OnTestLoveButton() {
        // 암살자 선물하기 버튼 생성
        OnGiftButtonClick("암살자");
    }

    public void OnGiftButtonClick(string npc)
    {   
        PlayerPrefs.SetString("NpcType", npc);
        PlayerPrefs.Save(); 
        
        SceneManager.LoadScene("InventoryMain"); // InventoryMain 씬으로 이동
    }
}
