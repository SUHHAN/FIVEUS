using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNow_yj : MonoBehaviour
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
