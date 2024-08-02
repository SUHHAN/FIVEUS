using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public int day = 1; // 현재 day 몇인지(1~15)
    public int activityCount = 0; // 하루 활동 수(3회까지 가능)
    public string timeOfDay = "Morning"; // 현재 시간(오전, 오후, 저녁)

    void Update()
    {
        if (activityCount >= 3)
        {
            AdvanceDay(); // 활동 수가 3개 이상이면 다음 날로 넘어감
        }
    }

    public void CompleteActivity()
    {
        activityCount++;
        if (activityCount == 1)
        {
            timeOfDay = "Afternoon"; // 첫 번째 활동 후 오후로 변경
        }
        else if (activityCount == 2)
        {
            timeOfDay = "Evening"; // 두 번째 활동 후 저녁으로 변경
        }
        else if (activityCount == 3)
        {
            timeOfDay = "Morning"; // 세 번째 활동 후 다시 아침으로 변경
        }
        //UpdateNPCPositions(); // NPC 위치 업데이트
    }

    public void AdvanceDay()
    {
        day++;
        if (day > 15)
        {
            day = 1; // 15일 이후에는 다시 1일로 돌아감(임시)
                     // 나중에는 엔딩씬으로 연결되게 코드 추가
        }
        activityCount = 0;
        timeOfDay = "Morning"; // 새로운 날의 시작은 아침
        //UpdateNPCPositions(); // NPC 위치 업데이트
    }

    /*public void UpdateNPCPositions()
    {
        // 현재 시간대에 따라 모든 NPC의 위치를 업데이트
        foreach (var npc in FindObjectsOfType<NpcScript>())
        {
            npc.UpdatePosition(timeOfDay);
        }
    }*/

    public string GetTimeOfDay()
    {
        return timeOfDay; // 현재 시간대 반환
    }
}
