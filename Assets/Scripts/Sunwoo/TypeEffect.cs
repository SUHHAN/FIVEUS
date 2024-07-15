using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeEffect : MonoBehaviour
{
    public GameObject endCursor; // 종료 커서
    public int charPerSeconds; // 초당 출력할 문자 수
    string targetMsg; // 목표 문자열
    [SerializeField] TextMeshProUGUI msgText; // 메시지 줄력할 컴포넌트
    int idx; // 현재 출력할 문자 인덱스
    float interval; // 문자 출력 간격

    private void Awake()
    {
        //msgText = GetComponent<TextMeshProUGUI>();
    }

    // 효과를 시작하는 메서드
    public void SetMsg(string msg)
    {
        targetMsg = msg;
        EffectStart();
    }

    // 효과를 시작하는 메서드
    void EffectStart()
    {
        msgText.text = ""; // 텍스트 초기화
        idx = 0; // 인덱스 0으로 초기화
        interval = 1.0f / charPerSeconds; // 문자 출력 간격 계산
        endCursor.SetActive(false); // 종료 커서 비활성화

        Invoke("Effecting", 1 / charPerSeconds); // 일정한 간격으로 Effecting 메서드 호출
    }

    // 문자 출력 메서드
    void Effecting()
    {
        if (msgText.text == targetMsg) // 모든 문자 출력했을 경우
        {
            EffectEnd(); // 효과 종료
            return;
        }
        msgText.text += targetMsg[idx]; // 현재 인덱스에 문자 추가
        idx++; // 인덱스 증가

        Invoke("Effecting", interval); // 다음 문자를 출력하기 위해 다시 호출
    }

    // 효과를 종료하는 메서드
    void EffectEnd()
    {
        endCursor.SetActive(true); // 종료 커서 활성화
    }
}
