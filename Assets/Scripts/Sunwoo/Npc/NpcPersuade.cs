using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro 네임스페이스 추가

public class NpcPersuade : MonoBehaviour
{
    public GameObject persuadeUI; // 설득 UI 패널
    public GameObject resultUI; // 결과 UI 패널
    public TextMeshProUGUI persuadeText; // 설득 텍스트 UI 연결
    public TextMeshProUGUI resultText; // 결과 텍스트 UI 연결
    public Button yesButton; // 예 버튼 연결
    public Button noButton; // 아니오 버튼 연결
    private double remainingAttempts = 3; // 남은 설득 시도 횟수
    public bool success = false; // 설득 성공 여부
    private NpcScript npcScript; // NpcScript 스크립트 참조

    void Start()
    {
        npcScript = GetComponent<NpcScript>(); // NpcScript 스크립트 참조 얻기
        persuadeUI.SetActive(false); // 시작할 때 설득 UI 비활성화
        resultUI.SetActive(false); // 시작할 때 결과 UI 비활성화
        yesButton.onClick.AddListener(OnYesButtonClick); // 예 버튼 클릭 이벤트 연결
        noButton.onClick.AddListener(OnNoButtonClick); // 아니오 버튼 클릭 이벤트 연결
    }

    public void ShowPersuadeUI()
    {
        persuadeUI.SetActive(true); // 설득 UI 표시
        persuadeText.text = $"설득하시겠습니까? 남은 기회: {remainingAttempts}";
        yesButton.gameObject.SetActive(true); // 예 버튼 표시
        noButton.gameObject.SetActive(true); // 아니오 버튼 표시
    }

    public void OnYesButtonClick()
    {
        AttemptPersuasion(); // 설득 시도
    }

    public void OnNoButtonClick()
    {
        persuadeUI.SetActive(false); // 설득 UI 숨기기
    }

    public void AttemptPersuasion()
    {
        remainingAttempts -= 0.5; // 시도 후 남은 기회 1 감소
        if (remainingAttempts >= 0)
        {
            double successChance = npcScript.affection; // NpcScript의 affection을 성공 확률로 사용
            double randomValue = Random.Range(0, 100); // 0에서 100 사이의 랜덤 값 생성

            if (randomValue < successChance)
            {
                // 설득 성공
                resultText.text = "성공했습니다!";
                success = true;
            }
            else // 설득 실패
            {
                resultText.text = $"실패했습니다!";
                success = false; // 설득 실패 여부 설정
            }
            persuadeUI.SetActive(false); // 설득 UI 숨기기
            resultUI.SetActive(true); // 결과 UI 표시
            StartCoroutine(HideResultUIAfterDelay(2f)); // 2초 뒤 결과 UI 숨기기
        }
        else
        {
            resultText.text = "설득 시도 기회를 모두 사용했습니다.";
            persuadeUI.SetActive(false); // 설득 UI 숨기기
            resultUI.SetActive(true); // 결과 UI 표시
            StartCoroutine(HideResultUIAfterDelay(3f)); // 3초 뒤 결과 UI 숨기기
        }
    }

    private IEnumerator HideResultUIAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // delay 만큼 대기
        resultUI.SetActive(false); // 결과 UI 숨기기
    }
}
