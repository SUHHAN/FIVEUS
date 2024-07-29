using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ���� �ڸ�Ʈ : ���ΰ��� ���¸� �����ϴ� â
// ���ΰ��� ü��+�Ƿε�+��ȭ+�ܼ�+���շ�, ��¥ ���� script
public class PlayerNow_yj
{
    // ����� ��ġ ���� ���� py�� player�� ����. 

    public int hp_py; // ü��
    // ü���� �ִ�ġ�� 100�� ���� �� ������ ���ѵ�
    // (���� �ڵ忡�� ����)

    public int tired_py; // �Ƿε�
    public int money_py; // ��ȭ
    public int hint_py; // �ܼ�
    public int team_py; // ���շ�
                        // ���շ��� �� ��Ƽ������ ��� ��ġ���� ���̴�.

    public int day_py; // ���� ��¥ ����(1�Ϻ��� 15�ϱ���)
    // 15���� �Ǹ� ���� ������ ����

    public int howtoday_py = 0; // �Ϸ翡 3�������� �⺻Ȱ���� �󸶳� �ߴ��� ����
    // ���� 0���� �����ϰ�, �� Ȱ���� ������ 1�� �����Ѵ�.
    // ��, �Ϸ簡 ���� �� 2���� �����ؾ� �Ѵ�. �׸��� �Ϸ簡 �������� �ٽ� 0���� �ʱ�ȭ

    public int howtrain_py; // �ɷ�ġ�� ������Ű�� "�Ʒ�"�� ��� �ߴ��� ����Ǵ� ����
    // ��, �Ϸ簡 ������ �ʱ�ȭ���� �ʴ´�.
    // �Ƹ� ���� ������ �� ���� ��Ƽ ��ġ ����� �� ����� ��?

    public PlayerNow_yj(int hp_py, int tired_py, int money_py, int hint_py, int team_py, int day_py, int howtoday_py, int howtrain_py)
    {
        this.hp_py = hp_py;
        this.tired_py = tired_py;
        this.money_py = money_py;
        this.hint_py = hint_py;
        this.team_py = team_py;
        this.day_py = day_py;
        this.howtoday_py = howtoday_py;
        this.howtrain_py = howtrain_py;
    }
}

public class PlayerManager_yj : MonoBehaviour
{

    // ���� �÷��̾��� ���¸� ��Ÿ���� ��ü
    private PlayerNow_yj playerNow;

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
