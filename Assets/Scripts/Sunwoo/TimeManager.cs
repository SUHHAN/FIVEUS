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
        // 날짜와 시간 초기화 (2024년 1월 1일 오전 6시로 시작).
        currentDateTime = new DateTime(2024, 1, 1, 6, 0, 0);
    }

    // 메인 활동이 완료될 때 호출되게 하기
    public void CompleteMainActivity()
    {
        mainActivityCount++;
        if (mainActivityCount >= 3)
        {
            mainActivityCount = 0;
            day++;
            if (day > 15) day = 1; // 15일을 넘으면 다시 1일로 초기화(나중에는 이벤트 씬으로 전환되게 수정)
            currentDateTime = currentDateTime.AddDays(1).Date.AddHours(6); // 다음 날 오전 6시로 초기화
        }
        else
        {
            currentDateTime = currentDateTime.AddHours(6); // 아침->점심->저녁 순으로 변경
        }
    }

    // 아침인지 확인
    public bool IsMorning()
    {
        return currentDateTime.Hour >= 6 && currentDateTime.Hour < 12;
    }

    // 점심인지 확인
    public bool IsAfternoon()
    {
        return currentDateTime.Hour >= 12 && currentDateTime.Hour < 18;
    }

    // 저녁인지 확인
    public bool IsEvening()
    {
        return currentDateTime.Hour >= 18 && currentDateTime.Hour < 24;
    }
}
