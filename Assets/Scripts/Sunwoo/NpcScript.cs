using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement; // 씬 전환

public class NpcScript : MonoBehaviour
{
    public GameObject choiceUI; // 선택 UI 패널
    public GameObject dialogueUI; // 대화 UI 패널
    public GameObject npcAffectionUI; // 호감도 UI 패널
    public TextMeshProUGUI dialogueText; // 대사 텍스트 UI 연결
    public TextMeshProUGUI affectionText; // 호감도 텍스트 UI 연결
    public TextMeshProUGUI npcNameText; // NPC 이름 텍스트 UI 연결
    public Button talkButton; // 대화하기 버튼 연결
    public Button persuadeButton; // 설득하기 버튼 연결
    public Button giftButton; // 선물하기 버튼 연결
    public Button choice1Button; // 선택지 1 버튼
    public Button choice2Button; // 선택지 2 버튼
    public float interactionRange = 3.0f; // 상호작용 거리
    public GameObject player; // 플레이어 오브젝트
    public bool isTalking = false; // 대화 중인지 여부
    public string npcType; // NPC 타입
    public int affection; // NPC 호감도

    private int currentDialogueIndex = 0; // 현재 대화 인덱스
    private List<string> dialogues = new List<string>(); // 대사 목록
    private List<string> choice1Dialogues = new List<string>(); // 초이스 1 대사 목록
    private int choice1DialogueIndex = 0; // 초이스 1 대사 인덱스

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // 태그가 "Player"인 오브젝트 찾기
        choiceUI.SetActive(false); // 시작할 때 선택 UI 비활성화
        dialogueUI.SetActive(false); // 시작할 때 대화 UI 비활성화
        persuadeButton.onClick.AddListener(OnPersuadeButtonClick); // 설득하기 버튼 클릭 이벤트 연결
        choice1Button.gameObject.SetActive(false); // 선택지 1 버튼 비활성화
        choice2Button.gameObject.SetActive(false); // 선택지 2 버튼 비활성화
        giftButton.onClick.AddListener(() => OnGiftButtonClick(npcType)); // 선물하기 버튼 클릭 이벤트 연결

        choice1Button.onClick.AddListener(OnChoice1ButtonClick); // 선택지 1 버튼 클릭 이벤트 연결
        choice2Button.onClick.AddListener(OnChoice2ButtonClick); // 선택지 2 버튼 클릭 이벤트 연결
    }

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position); // 플레이어와 NPC 간 거리 계산
        if (distance <= interactionRange) // 상호작용 거리 내에 있는지 확인
        {
            if (Input.GetKeyDown(KeyCode.Return) && !isTalking) // 엔터 키 입력 감지 및 대화 중이 아닌 경우
            {
                ShowChoiceUI(); // 선택 UI 표시
            }
            else if (Input.GetKeyDown(KeyCode.Space) && isTalking) // 대화 중일 때 스페이스바 입력 감지
            {
                if (choice1Dialogues.Count > 0 && choice1DialogueIndex < choice1Dialogues.Count)
                {
                    ShowNextChoice1Dialogue(); // 다음 대사로 넘어가기
                }
                else
                {
                    ShowNextDialogue(); // 기본 대사로 넘어가기
                }
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
        dialogueText.text = ""; // 대사 텍스트 초기화
    }

    public void OnTalkButtonClick()
    {
        choiceUI.SetActive(false); // 선택 UI 숨기기
        dialogueUI.SetActive(true); // 대화 UI 활성화
        isTalking = true; // 대화 상태 설정
        currentDialogueIndex = 0; // 대화 인덱스 초기화
        SetDialogue(npcType); // NPC 타입에 따라 대사 설정
        ShowNextDialogue(); // 첫 번째 대사 표시
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
        choice1Button.gameObject.SetActive(false); // 선택지 1 버튼 비활성화
        choice2Button.gameObject.SetActive(false); // 선택지 2 버튼 비활성화
    }

    void SetDialogue(string type)
    {
        dialogues.Clear(); // 기존 대사 초기화

        if (type == "검사")
        {
            npcNameText.text = "Callis";
            dialogues.Add("음, 처음 보는 얼굴 같은데.");
            dialogues.Add("나한테 무슨 볼일이라도?");
        }
        // 다른 NPC 타입의 대사 추가 가능
    }

    void ShowNextDialogue()
    {
        if (currentDialogueIndex < dialogues.Count)
        {
            dialogueText.text = dialogues[currentDialogueIndex]; // 현재 대사 표시

            // 특정 대사일 때 선택지 버튼 활성화
            if (dialogueText.text == "나한테 무슨 볼일이라도?")
            {
                choice1Button.gameObject.SetActive(true); // 선택지 1 버튼 활성화
                choice2Button.gameObject.SetActive(true); // 선택지 2 버튼 활성화
            }

            currentDialogueIndex++; // 인덱스 증가
        }
        else
        {
            EndDialogue(); // 대사 끝나면 대화 종료
        }
    }

    public void OnChoice1ButtonClick()
    {
        choice1Dialogues = new List<string>
        {
            "Callis,하하. 내가 칼리스 맞지.",
            "Callis,날 아는 사람이었구나. 반가워.",
            "Callis,보아하니 당신도 용병 같은데... 이 마을에 속한 용병인가?",
            "player,아뇨. 볼일이 있어서 잠시 머무르고 있습니다",
            "Callis,아. 그렇구나",
            "Callis,나도 이 마을에 좋은 의뢰가 잘 들어온다길래 잠시 이곳에 머무르고 있어",
            "Callis,앞으로 잘 지내보자고!"
        };
        choice1DialogueIndex = 0;
        isTalking = true; // 대화 상태 유지
        choice1Button.gameObject.SetActive(false); // 선택지 1 버튼 비활성화
        choice2Button.gameObject.SetActive(false); // 선택지 2 버튼 비활성화
        ShowNextChoice1Dialogue(); // 첫 번째 대사 출력
    }

    public void OnChoice2ButtonClick()
    {
        dialogueText.text = "...그럼 왜 말을 건 거지?"; // 선택지 2에 대한 대사
        choice1Button.gameObject.SetActive(false); // 선택지 1 버튼 비활성화
        choice2Button.gameObject.SetActive(false); // 선택지 2 버튼 비활성화
    }

    void ShowNextChoice1Dialogue()
    {
        if (choice1DialogueIndex < choice1Dialogues.Count)
        {
            // 대사와 이름을 ','로 구분하여 표시
            var splitDialogue = choice1Dialogues[choice1DialogueIndex].Split(new string[] { "," }, StringSplitOptions.None);
            if (splitDialogue.Length > 1)
            {
                npcNameText.text = splitDialogue[0];
                dialogueText.text = splitDialogue[1];
            }
            else
            {
                dialogueText.text = splitDialogue[0];
            }

            choice1DialogueIndex++; // 인덱스 증가
        }
        else
        {
            EndDialogue(); // 대사 끝나면 대화 종료
        }
    }

    // NPC 호감도 변경
    void ChangeAffection(int amount)
    {
        affection += amount;
        affectionText.text = $"호감도: {affection}";
    }

    // NPC 대화 내용 표시
    void DisplayDialogue(string npcName, string description)
    {
        dialogueText.text = description; // 대화 내용 설정
        npcNameText.text = npcName; // NPC 이름 설정
    }
}

// public void UpdatePosition(string timeOfDay)
// {
//     Vector3 newPosition = Vector3.zero;

//     switch (npcType)
//     {
//         case "검사":
//         case "힐러":
//             if (timeOfDay == "Morning" || timeOfDay == "Afternoon")
//             {
//                 SceneManager.LoadScene("main_map");
//                 newPosition = new Vector3(10, 0, 20); // main_map 내 위치 설정
//             }
//             else
//             {
//                 SceneManager.LoadScene("big_house");
//                 newPosition = new Vector3(5, 0, 10); // big_house 내 위치 설정
//             }
//             break;
//         case "탱커":
//             if (timeOfDay == "Morning" || timeOfDay == "Afternoon")
//             {
//                 SceneManager.LoadScene("training");
//                 newPosition = new Vector3(15, 0, 5); // training 내 위치 설정
//             }
//             else
//             {
//                 SceneManager.LoadScene("big_house");
//                 newPosition = new Vector3(5, 0, 10); // big_house 내 위치 설정
//             }
//             break;
//         case "마법사":
//             SceneManager.LoadScene("sub2_house");
//             newPosition = new Vector3(3, 0, 8); // sub2_house 내 위치 설정
//             break;
//         case "암살자":
//             if (timeOfDay == "Evening")
//             {
//                 SceneManager.LoadScene("bar");
//                 newPosition = new Vector3(2, 0, 6); // bar 내 위치 설정
//             }
//             break;
//         case "궁수":
//             if (timeOfDay == "Evening")
//             {
//                 SceneManager.LoadScene("training");
//                 newPosition = new Vector3(7, 0, 3); // training 내 위치 설정
//             }
//             break;
//     }

//     transform.position = newPosition; // NPC 위치 설정
// }
