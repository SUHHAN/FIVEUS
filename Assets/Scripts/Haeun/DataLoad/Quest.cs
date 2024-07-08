using UnityEngine;

public class Quest : MonoBehaviour
{
    public DayManager dayManager;

    void Start()
    {
        // DayManager 스크립트를 할당
        dayManager = FindObjectOfType<DayManager>();
    }

    void Update()
    {
        // 예시: 퀘스트 완료 조건 체크
        if (Input.GetKeyDown(KeyCode.Q)) // Q 키를 누르면 퀘스트 완료
        {
            CompleteQuest();
        }
    }

    private void CompleteQuest()
    {
        // 퀘스트가 완료되면 DayManager의 CompleteQuest 메서드 호출
        dayManager.CompleteQuest();
        Debug.Log("퀘스트가 완료되었습니다.");
    }
}
