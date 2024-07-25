using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro 네임스페이스 추가

public class NpcScript : MonoBehaviour
{
    public GameObject choiceUI; // 선택 UI 패널
    public GameObject dialogueUI; // 대화 UI 패널
    public GameObject persuadeUI; // 설득 UI 패널
    public GameObject resultUI; // 결과 UI 패널
    public TextMeshProUGUI dialogueText; // 대사 텍스트 UI 연결
    public TextMeshProUGUI persuadeText; // 설득 텍스트 UI 연결
    public TextMeshProUGUI resultText; // 결과 텍스트 UI 연결
    public Button talkButton; // 대화하기 버튼 연결
    public Button persuadeButton; // 설득하기 버튼 연결
    public Button yesButton; // 예 버튼 연결
    public Button noButton; // 아니오 버튼 연결
    public Button attemptPersuasionButton; // 설득 시도 버튼 연결
    public float interactionRange = 3.0f; // 상호작용 거리
    private GameObject player; // 플레이어 오브젝트
    private bool isTalking = false; // 대화 중인지 여부
    public int affection = 50; // 호감도
    private int remainingAttempts = 3; // 남은 설득 시도 횟수

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // 태그가 "Player"인 오브젝트 찾기
        choiceUI.SetActive(false); // 시작할 때 선택 UI 비활성화
        dialogueUI.SetActive(false); // 시작할 때 대화 UI 비활성화
        persuadeUI.SetActive(false); // 시작할 때 설득 UI 비활성화
        resultUI.SetActive(false); // 시작할 때 결과 UI 비활성화
        talkButton.onClick.AddListener(OnTalkButtonClick); // 대화하기 버튼 클릭 이벤트 연결
        persuadeButton.onClick.AddListener(OnPersuadeButtonClick); // 설득하기 버튼 클릭 이벤트 연결
        yesButton.onClick.AddListener(OnYesButtonClick); // 예 버튼 클릭 이벤트 연결
        noButton.onClick.AddListener(OnNoButtonClick); // 아니오 버튼 클릭 이벤트 연결
    }

    void Update()
    {
        if (isTalking)
        {
            if (Input.GetKeyDown(KeyCode.Return)) // 엔터 키 입력 감지
            {
                EndDialogue(); // 대화 종료
            }
            return;
        }

        float distance = Vector3.Distance(player.transform.position, transform.position); // 플레이어와 NPC 간 거리 계산
        if (distance <= interactionRange) // 상호작용 거리 내에 있는지 확인
        {
            if (Input.GetKeyDown(KeyCode.Return)) // 엔터 키 입력 감지
            {
                ShowChoiceUI(); // 선택 UI 표시
            }
        }
        else
        {
            choiceUI.SetActive(false); // 선택 UI 숨기기
        }
    }

    void ShowChoiceUI()
    {
        choiceUI.SetActive(true); // 선택 UI 활성화
        dialogueText.text = ""; // 대사 텍스트 초기화
    }

    public void OnTalkButtonClick()
    {
        choiceUI.SetActive(false); // 선택 UI 숨기기
        dialogueUI.SetActive(true); // 대화 UI 활성화
        dialogueText.text = "안녕, 반가워!"; // 대사 표시
        isTalking = true; // 대화 상태 설정
    }

    public void OnPersuadeButtonClick()
    {
        choiceUI.SetActive(false); // 선택 UI 숨기기
        Persuade(); // 설득 UI 표시
    }

    public void Persuade()
    {
        HideChoices();
        persuadeUI.SetActive(true); // 설득 UI 표시
        persuadeText.text = $"설득하시겠습니까? 남은 횟수: {remainingAttempts}";
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
        if (remainingAttempts > 0)
        {
            remainingAttempts--;
            int successChance = affection; // 호감도에 비례한 성공 확률
            int randomValue = Random.Range(0, 100); // 0에서 100 사이의 랜덤 값 생성
            if (randomValue < successChance)
            {
                // 설득 성공
                resultText.text = "성공했습니다!";
            }
            else
            {
                // 설득 실패
                resultText.text = $"실패했습니다! 남은 횟수: {remainingAttempts}";
            }
            persuadeUI.SetActive(false); // 설득 UI 숨기기
            resultUI.SetActive(true); // 결과 UI 표시
        }
        else
        {
            resultText.text = "설득 시도 기회를 모두 사용했습니다.";
            persuadeUI.SetActive(false); // 설득 UI 숨기기
            resultUI.SetActive(true); // 결과 UI 표시
        }
    }

    void HideChoices()
    {
        choiceUI.SetActive(false); // 선택 UI 숨기기
        dialogueUI.SetActive(false); // 대화 UI 숨기기
        persuadeUI.SetActive(false); // 설득 UI 숨기기
    }

    void EndDialogue()
    {
        dialogueUI.SetActive(false); // 대화 UI 숨기기
        isTalking = false; // 대화 상태 해제
    }
}
