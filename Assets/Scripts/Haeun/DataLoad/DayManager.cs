using UnityEngine;

public class DayManager : MonoBehaviour
{
    public int currentDay = 1;

    private void Start()
    {
        LoadGameData(); // 게임 시작 시 저장된 데이터 불러오기
    }

    public void CompleteQuest()
    {
        // 퀘스트가 완료되었을 때 호출될 메서드
        OnQuestCompleted(); // 퀘스트 완료 처리
    }

    private void OnQuestCompleted()
    {
        // 퀘스트 완료 처리
        Debug.Log("퀘스트가 완료되었습니다.");

        // 일차 증가 및 데이터 저장
        OnDayComplete();
    }

    private void OnDayComplete()
    {
        currentDay++; // 일차 증가
        SaveGameData(); // 데이터 저장
        Debug.Log("일차가 증가하였습니다. 현재 일차: " + currentDay);
    }

    private void SaveGameData()
    {
        GameDataManager.Instance.SaveData(currentDay); // 데이터 매니저를 통해 데이터 저장
        Debug.Log("게임 데이터가 저장되었습니다. 현재 일차: " + currentDay);
    }

    private void LoadGameData()
    {
        GameData data = GameDataManager.Instance.LoadData(); // 데이터 매니저를 통해 데이터 불러오기
        if (data != null)
        {
            currentDay = data.currentDay; // 저장된 일차로 초기화
            Debug.Log("불러온 게임 데이터: 현재 일차 " + currentDay);
        }
        else
        {
            Debug.Log("저장된 데이터가 없습니다. 기본 값으로 초기화합니다.");
        }
    }
}
