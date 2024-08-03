using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// 예지 코멘트 : 주인공의 상태를 관리하는 창
// 주인공의 체력+피로도+재화+단서+단합력, 날짜 관리 script

public class PlayerManager_yj : MonoBehaviour
{

    // 현재 플레이어의 상태를 나타내는 객체
    public PlayerNow_yj playerNow;

    // Start is called before the first frame update
    void Start()
    {
        // 초기 플레이어 상태 설정
        InitializePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        // 플레이어 상태에 따른 업데이트 로직
    }

    // 플레이어 초기 상태 설정
    void InitializePlayer()
    {

        // this.hp_py = hp_py;
        // this.tired_py = tired_py;
        // this.money_py = money_py;
        // this.hint_py = hint_py;
        // this.team_py = team_py;
        // this.day_py = day_py;
        // this.howtoday_py = howtoday_py;
        // this.howtrain_py = howtrain_py;
        // 초기 상태 값 설정 (예시)
        playerNow = new PlayerNow_yj(DataManager.instance.nowPlayer.Player_hp
                                    , DataManager.instance.nowPlayer.Player_tired
                                    , DataManager.instance.nowPlayer.Player_money
                                    , DataManager.instance.nowPlayer.Player_hint
                                    , DataManager.instance.nowPlayer.Player_team
                                    , DataManager.instance.nowPlayer.Player_day
                                    , DataManager.instance.nowPlayer.Player_howtoday
                                    , DataManager.instance.nowPlayer.Player_howtrain);
    }

    // 플레이어 체력 증가
    public void IncreaseHealth(int amount)
    {
        
        playerNow.hp_py += amount;
        if (playerNow.hp_py > 100)
            playerNow.hp_py = 100; // 최대 체력은 100으로 제한

        SaveData();
        
    }
    // 플레이어 체력 감소
    public void DecreaseHealth(int amount)
    {
        playerNow.hp_py -= amount;
        if (playerNow.hp_py < 0 )
            SceneManager.LoadScene("DeadEndingScene");
            playerNow.hp_py = 0; // 최소 체력은 100으로 제한
        // 최소 체력 이하로 떨어지면 엔딩씬으로 연결해야 함.


        SaveData();
    }

    // 플레이어 훈련 횟수 증가
    public void IncreaseTrainingCount()
    {
        playerNow.howtrain_py++;

        SaveData();

    }

    // 플레이어 피로도 증가
    public void IncreaseTiredness(int amount)
    {
        playerNow.tired_py += amount;
        if (playerNow.tired_py >= 100) { 
            playerNow.tired_py = 100;
            playerNow.hp_py -= 30; // 피로도 100 넘어가면 체력 10 감소
            // tired는 아이템 먹지 않는 이상 안 내려감
        }
        
        SaveData();

    }

    // 플레이어 재화 증가
    public void IncreaseMoney(int amount)
    {
        if(playerNow.money_py <= 0)
            playerNow.money_py = 0;
            
        
        playerNow.money_py += amount;
        SaveData();

    }

    // 플레이어 단서 증가
    public void IncreaseHint(int amount)
    {
        playerNow.hint_py += amount;
        SaveData();

    }

    // 플레이어 단합력 증가
    public void IncreaseTeamPower(int amount)
    {
        playerNow.team_py += amount;
        SaveData();
    }

    // timeManager로 넘길게 그냐앙...그래서 주석 처리
    // 플레이어 오늘 날짜 설정
     /*public void SetDay(int day)
    {
        playerNow.day_py = day;
    }

    // 플레이어 하루 활동 횟수 증가

   public void IncreaseDailyActivityCount()
    {
        playerNow.howtoday_py++;
        if (playerNow.howtoday_py > 2)
            playerNow.howtoday_py = 0; // 하루 활동 횟수는 최대 3회로 제한

    }*/

    
    void SaveData() {
        DataManager.instance.nowPlayer.Player_hp = playerNow.hp_py;
        DataManager.instance.nowPlayer.Player_tired = playerNow.tired_py;
        DataManager.instance.nowPlayer.Player_money = playerNow.money_py;
        DataManager.instance.nowPlayer.Player_hint = playerNow.hint_py;
        DataManager.instance.nowPlayer.Player_team = playerNow.team_py;
        DataManager.instance.nowPlayer.Player_day = playerNow.day_py;
        DataManager.instance.nowPlayer.Player_howtoday = playerNow.howtoday_py;
        DataManager.instance.nowPlayer.Player_howtrain = playerNow.howtrain_py;

        DataManager.instance.SaveData();
    }
}
