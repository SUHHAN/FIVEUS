using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueBar_yj : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameTxt; // 이름 표시할 컴포넌트
    [SerializeField] TypeEffect talk; // 대사 표시할 컴포넌트
    string[] str = { }; // 예시 대화 배열

    void Start()
    {
        ActiveDialogue(0, "기사단장", ref str); // 대화창 활성화 메서드 호출
    }

    void Update()
    {

    }

    // 대화창 활성화 메서드
    void ActiveDialogue(int idx, string nameData, ref string[] talkData)
    {
        if (nameData == null && talkData == null)
        {
            // 데이터를 가져오지 못했을 경우, 필요한 데이터를 관리하는 곳에서 가져옵니다.
        }

        nameTxt.text = nameData; // 이름 텍스트 설정

        // 대화 데이터 출력
        for (int i = 0; i < talkData.Length; i++)
        {
            talk.SetMsg(talkData[i]);
            //특정 키 누르면 다음 대화 진행 가능.
            //대화 스킵 여부는 논의 후 결정 (InputSystem에 그러면 추후 추가해야 함)
        }
    }

    //상호작용하면 대화창 활성화
    /*
     * UI Manager에서 플레이어와 다른 오브젝트간 충돌 / 특정 대화 이벤트 발생시 활성화
     dialogueBar.SetAcvite(true);
     */
}
