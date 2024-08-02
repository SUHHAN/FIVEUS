using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueBar : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameTxt; // 이름 표시할 컴포넌트
    [SerializeField] TypeEffect talk; // 대사 표시할 컴포넌트
    string[] str = {}; // 예시 대화 배열

    void Start()
    {
        // '주인공'이라는 이름과 str 배열을 대화로 사용하여 대화창 활성화
        ActiveDialogue(0, "주인공", ref str);
    }

    void Update()
    {
        // Update 함수가 필요하지 않다면 비워두거나 제거 가능
    }

    // 대화창 활성화 메서드
    void ActiveDialogue(int idx, string nameData, ref string[] talkData)
    {
        if (nameTxt == null || talk == null || talkData == null || talkData.Length == 0)
        {
            Debug.LogError("Missing components or data.");
            return; // 필요한 데이터가 없을 경우, 함수 실행 중지
        }

        nameTxt.text = nameData; // 이름 텍스트 설정

        // 대화 데이터 출력
        StartCoroutine(DisplayTalk(talkData)); // 코루틴으로 대사 출력
    }

    IEnumerator DisplayTalk(string[] talkData)
    {
        for (int i = 0; i < talkData.Length; i++)
        {
            talk.SetMsg(talkData[i]);
            // TypeEffect의 대사 출력이 완료될 때까지 대기
            while (!talk.IsComplete())
            {
                yield return null;
            }
            yield return new WaitForSeconds(1f); // 대사 간 대기 시간
        }
    }
}
