using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro ���ӽ����̽� �߰�
using UnityEngine.UI; // UI ��� ����� ���� ���ӽ����̽� �߰�

// ��¥ �ٲٴ� �Լ��� ��Ȱ��


public class TalkManager_yj : MonoBehaviour
{
    private int todaydate_yj = 1; // ��¥ ��Ÿ���� ����
    public TextMeshProUGUI dayText_yj; // TextMeshPro UI �ؽ�Ʈ ��� ����
    public GameObject dayandnight_yj;   // ��¥ �ٲ𶧸��� ȭ�� ��İ� �ϴ� image


    void Start()
    {
        SetDay(1);
        UpdateDayText(); // ������ �� �ؽ�Ʈ ������Ʈ
    }

    void UpdateDayText()
    {
        // dayText�� �ؽ�Ʈ ����
        dayText_yj.text = todaydate_yj + "���� �ϰ� ����";
    }

    public void SetDay(int dayday_yj)
    {
        todaydate_yj = dayday_yj;
        UpdateDayText(); // �ؽ�Ʈ ������Ʈ
    }

    void Update()
    {

    }

}