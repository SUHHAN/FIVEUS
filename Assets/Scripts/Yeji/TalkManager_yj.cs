using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro 네임스페이스 추가
using UnityEngine.UI; // UI 요소 사용을 위한 네임스페이스 추가

// 날짜 바꾸는 함수로 재활용


public class TalkManager_yj : MonoBehaviour
{
    private int todaydate_yj = 1; // 날짜 나타내는 변수
    public TextMeshProUGUI dayText_yj; // TextMeshPro UI 텍스트 요소 연결
    public GameObject dayandnight_yj;   // 날짜 바뀔때마다 화면 까맣게 하는 image


    void Start()
    {
        SetDay(1);
        UpdateDayText(); // 시작할 때 텍스트 업데이트
    }

    void UpdateDayText()
    {
        // dayText에 텍스트 설정
        dayText_yj.text = todaydate_yj + "일차 일과 시작";
    }

    public void SetDay(int dayday_yj)
    {
        todaydate_yj = dayday_yj;
        UpdateDayText(); // 텍스트 업데이트
    }

    void Update()
    {

    }

}