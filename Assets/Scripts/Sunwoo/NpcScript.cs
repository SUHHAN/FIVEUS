using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; // 씬 전환

public class NpcScript : MonoBehaviour
{
    public GameObject choiceUI; // 선택 UI 패널
    public GameObject dialogueUI; // 대화 UI 패널
    public GameObject npcAffectionUI; // 호감도 UI 패널
    public TextMeshProUGUI dialogueText; // 대사 텍스트 UI 연결
    public TextMeshProUGUI affectionText; // 호감도 텍스트 UI 연결
    public Button talkButton; // 대화하기 버튼 연결
    public Button persuadeButton; // 설득하기 버튼 연결
    public Button giftButton; // 선물하기 버튼 연결
    public float interactionRange = 3.0f; // 상호작용 거리
    private GameObject player; // 플레이어 오브젝트
    private bool isTalking = false; // 대화 중인지 여부
    private DialogueManager dialogueManager; // DialogueManager 스크립트 참조
    public string npcType;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // 태그가 "Player"인 오브젝트 찾기
        dialogueManager = GetComponent<DialogueManager>(); // DialogueManager 스크립트 참조 얻기
        choiceUI.SetActive(false); // 시작할 때 선택 UI 비활성화
        dialogueUI.SetActive(false); // 시작할 때 대화 UI 비활성화
        talkButton.onClick.AddListener(OnTalkButtonClick); // 대화하기 버튼 클릭 이벤트 연결
        persuadeButton.onClick.AddListener(OnPersuadeButtonClick); // 설득하기 버튼 클릭 이벤트 연결
        giftButton.onClick.AddListener(() => OnGiftButtonClick(npcType)); // 선물하기 버튼 클릭 이벤트 연결
        UpdatePosition(FindObjectOfType<TimeManager>().GetTimeOfDay()); // 초기 위치 설정
    }

    void Update()
    {
        if (isTalking)
        {
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
            choiceUI.SetActive(false); // 선택 UI와 자식 오브젝트들 숨기기
        }
    }

    void ShowChoiceUI()
    {
        choiceUI.SetActive(true); // 선택 UI 활성화
        affectionText.text = $"호감도: {GetComponent<NpcPersuade>().affection}"; // 호감도 텍스트 업데이트
        dialogueText.text = ""; // 대사 텍스트 초기화
    }

    public void OnTalkButtonClick()
    {
        choiceUI.SetActive(false); // 선택 UI 숨기기
        dialogueUI.SetActive(true); // 대화 UI 활성화
        dialogueManager.StartDialogue(); // 대화 시작
        isTalking = true; // 대화 상태 설정
    }

    public void OnPersuadeButtonClick()
    {
        choiceUI.SetActive(false); // 선택 UI 숨기기
        GetComponent<NpcPersuade>().ShowPersuadeUI(); // 설득 UI 표시
    }

    public void OnGiftButtonClick(string npc)
    {           
        SceneManager.LoadScene("InventoryMain"); // InventoryMain 씬으로 이동
    }

    public void HidePersuadeAndGiftButtons()
    {
        persuadeButton.gameObject.SetActive(false); // 설득하기 버튼 숨기기
        giftButton.gameObject.SetActive(false); // 선물하기 버튼 숨기기
    }

    public void EndDialogue()
    {
        isTalking = false; // 대화 상태 해제
        dialogueUI.SetActive(false); // 대화 UI 비활성화
        choiceUI.SetActive(false); // 선택 UI와 자식 오브젝트들 비활성화
    }

    public void UpdatePosition(string timeOfDay)
    {
        Vector3 newPosition = Vector3.zero;

        switch (npcType)
        {
            case "검사":
            case "힐러":
                if (timeOfDay == "Morning" || timeOfDay == "Afternoon")
                {
                    SceneManager.LoadScene("main_map");
                    newPosition = new Vector3(10, 0, 20); // main_map 내 위치 설정
                }
                else
                {
                    SceneManager.LoadScene("big_house");
                    newPosition = new Vector3(5, 0, 10); // big_house 내 위치 설정
                }
                break;
            case "탱커":
                if (timeOfDay == "Morning" || timeOfDay == "Afternoon")
                {
                    SceneManager.LoadScene("training");
                    newPosition = new Vector3(15, 0, 5); // training 내 위치 설정
                }
                else
                {
                    SceneManager.LoadScene("big_house");
                    newPosition = new Vector3(5, 0, 10); // big_house 내 위치 설정
                }
                break;
            case "마법사":
                SceneManager.LoadScene("sub2_house");
                newPosition = new Vector3(3, 0, 8); // sub2_house 내 위치 설정
                break;
            case "암살자":
                if (timeOfDay == "Evening")
                {
                    SceneManager.LoadScene("bar");
                    newPosition = new Vector3(2, 0, 6); // bar 내 위치 설정
                }
                break;
            case "궁수":
                if (timeOfDay == "Evening")
                {
                    SceneManager.LoadScene("training");
                    newPosition = new Vector3(7, 0, 3); // training 내 위치 설정
                }
                break;
        }

        transform.position = newPosition; // NPC 위치 설정
    }
}
