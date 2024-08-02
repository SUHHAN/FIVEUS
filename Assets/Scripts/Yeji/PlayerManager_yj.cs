using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ���� �ڸ�Ʈ : ���ΰ��� ���¸� �����ϴ� â
// ���ΰ��� ü��+�Ƿε�+��ȭ+�ܼ�+���շ�, ��¥ ���� script

public class PlayerManager_yj : MonoBehaviour
{

    // ���� �÷��̾��� ���¸� ��Ÿ���� ��ü
    public PlayerNow_yj playerNow;

    // Start is called before the first frame update
    void Start()
    {
        // �ʱ� �÷��̾� ���� ����
        InitializePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        // �÷��̾� ���¿� ���� ������Ʈ ����
    }

    // �÷��̾� �ʱ� ���� ����
    void InitializePlayer()
    {
        // �ʱ� ���� �� ���� (����)
        playerNow = new PlayerNow_yj(100, 0, 50, 0, 0, 1, 0, 0);
    }

    // �÷��̾� ü�� ����
    public void IncreaseHealth(int amount)
    {
        playerNow.hp_py += amount;
        if (playerNow.hp_py > 100)
            playerNow.hp_py = 100; // �ִ� ü���� 100���� ����

    }

    // �÷��̾� �Ƿε� ����
    public void IncreaseTiredness(int amount)
    {
        playerNow.tired_py += amount;
    }

    // �÷��̾� ��ȭ ����
    public void IncreaseMoney(int amount)
    {
        playerNow.money_py += amount;
    }

    // �÷��̾� �ܼ� ����
    public void IncreaseHint(int amount)
    {
        playerNow.hint_py += amount;
    }

    // �÷��̾� ���շ� ����
    public void IncreaseTeamPower(int amount)
    {
        playerNow.team_py += amount;
    }

    // �÷��̾� ���� ��¥ ����
    public void SetDay(int day)
    {
        playerNow.day_py = day;
    }

    // �÷��̾� �Ϸ� Ȱ�� Ƚ�� ����
    public void IncreaseDailyActivityCount()
    {
        playerNow.howtoday_py++;
        if (playerNow.howtoday_py > 2)
            playerNow.howtoday_py = 0; // �Ϸ� Ȱ�� Ƚ���� �ִ� 3ȸ�� ����

    }

    // �÷��̾� �Ʒ� Ƚ�� ����
    public void IncreaseTrainingCount()
    {
        playerNow.howtrain_py++;
        
    }


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
