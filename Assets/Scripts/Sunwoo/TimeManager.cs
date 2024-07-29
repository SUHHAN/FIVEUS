using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    public DateTime currentDateTime;
    private int mainActivityCount = 0;
    public int day = 1;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // ��¥�� �ð� �ʱ�ȭ (2024�� 1�� 1�� ���� 6�÷� ����).
        currentDateTime = new DateTime(2024, 1, 1, 6, 0, 0);
    }

    // ���� Ȱ���� �Ϸ�� �� ȣ��ǰ� �ϱ�
    public void CompleteMainActivity()
    {
        mainActivityCount++;
        if (mainActivityCount >= 3)
        {
            mainActivityCount = 0;
            day++;
            if (day > 15) day = 1; // 15���� ������ �ٽ� 1�Ϸ� �ʱ�ȭ(���߿��� �̺�Ʈ ������ ��ȯ�ǰ� ����)
            currentDateTime = currentDateTime.AddDays(1).Date.AddHours(6); // ���� �� ���� 6�÷� �ʱ�ȭ
        }
        else
        {
            currentDateTime = currentDateTime.AddHours(6); // ��ħ->����->���� ������ ����
        }
    }

    // ��ħ���� Ȯ��
    public bool IsMorning()
    {
        return currentDateTime.Hour >= 6 && currentDateTime.Hour < 12;
    }

    // �������� Ȯ��
    public bool IsAfternoon()
    {
        return currentDateTime.Hour >= 12 && currentDateTime.Hour < 18;
    }

    // �������� Ȯ��
    public bool IsEvening()
    {
        return currentDateTime.Hour >= 18 && currentDateTime.Hour < 24;
    }
}
