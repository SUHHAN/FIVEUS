using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public int day = 1; // 현재 day 몇인지(1~15)
    public int activityCount =0; // 하루 활동 수(3회까지 가능)
    private string timeOfDay = "아침"; // 현재 시간(아침, 점심, 저녁)
    public talkwithjjang_yj talkwithjjang;

    // 날짜 표시하는 패널
    public GameObject whatisdate_yj; // 날짜 시작할 때 어두워지는 화면(검정색)(조상님드래그)
    public TextMeshProUGUI todayiswhat_yj; // 오늘 며칠인지 텍스트 매일 바뀜
    /*
    public GameObject whatistime1_yj; // 현재 시간(오전) 화면
    public GameObject whatistime2_yj; // 현재 시간(점심) 화면
    public GameObject whatistime3_yj; // 현재 시간(저녁) 화면
    // public TextMeshProUGUI mornluneve_yj; //  현재 시간(오전, 오후, 저녁) 텍스트
    */
    public void Start()
    {
        activityCount = 0; // 하루 활동 수(3회까지 가능)
        Getday();
        GetTimeOfDay();
        todayiswhat_yj.text = $"{day.ToString()}일차 {timeOfDay}";
        whatisdate_yj.SetActive(true);// 시작할 때 며칠인지 까만 화면 띄워야함
        // Invoke the method to hide the whatisdate_yj panel after 2 seconds
        Invoke("HideWhatIsDatePanel", 2f);
    }
    void Update()
    {
        if (activityCount >= 6)
        {
            AdvanceDay(); // 활동 수가 3개 이상이면 다음 날로 넘어감
        }
        
    }


    public void CompleteActivity()
    {
        activityCount++;
        if(activityCount == 0){
            timeOfDay = "아침 "; // 0 아침
           // Debug.Log(timeOfDay);
        }
        else if (activityCount>0 && activityCount <= 2)
        {
            timeOfDay = "점심 "; // 첫 번째 활동 후 오후로 변경
            //Debug.Log(timeOfDay);
        }
        else if (activityCount > 2 && activityCount <= 4)
        {
            timeOfDay = "저녁 "; // 두 번째 활동 후 저녁으로 변경
           // Debug.Log(timeOfDay);
        }
        else if (activityCount >4 && activityCount <= 6)
        {
            timeOfDay = "아침 "; // 세 번째 활동 후 다시 아침으로 변경
            //Debug.Log(timeOfDay);
        }

    }
    public void UpdateDateAndTimeDisplay()
    {
            Getday();
            GetTimeOfDay();  
            todayiswhat_yj.text = $"{day.ToString()}일차 {timeOfDay}";
            talkwithjjang.choiceUI1_yj.SetActive(false);
            talkwithjjang.choiceUI2_yj.SetActive(false);
            talkwithjjang.choiceUI3_yj.SetActive(false);
            talkwithjjang.choiceUI4_yj.SetActive(false);
            talkwithjjang.Dial_changyj.SetActive(false);
            whatisdate_yj.SetActive(true);
            Invoke("HideWhatIsDatePanel", 2f);
        
    }

    public void AdvanceDay()
    {
        day++;

        if (day > 15)
        {
            day = 1; // 15일 이후에는 다시 1일로 돌아감(임시)
            SceneManager.LoadScene("FailEndingScene"); // 나중에는 엔딩씬으로 연결되게 코드 추가
        }
        activityCount = 0;
        timeOfDay = "아침 "; // 새로운 날의 시작은 아침
        // 여기에 넣는게 맞긴 한데, 다시 침대 들어가야 하니까 어디다 위치 옮겨야 하나
         //UpdateDateAndTimeDisplay();
    }

    public string GetTimeOfDay()
    {
        return timeOfDay; // 현재 시간대 반환
    }
    public int Getday()
    {
        return day;
    }
    // Method to hide whatisdate_yj panel
    void HideWhatIsDatePanel()
    {
        whatisdate_yj.SetActive(false);
    }
    public void gotosleep_yj()
    {

    }
}
