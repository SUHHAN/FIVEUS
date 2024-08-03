using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

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
    public TextMeshProUGUI choice1Text; // 선택지 1 텍스트
    public TextMeshProUGUI choice2Text; // 선택지 2 텍스트
    public float interactionRange = 3.0f; // 상호작용 거리
    public GameObject player; // 플레이어 오브젝트
    public bool isTalking = false; // 대화 중인지 여부
    public string npcType; // NPC 타입
    public double affection = 0; // NPC 호감도

    private int currentDialogueIndex = 0; // 현재 대화 인덱스
    private List<string> dialogues = new List<string>(); // 대사 목록
    private List<string> choice1Dialogues = new List<string>(); // 초이스 1 대사 목록
    private int choice1DialogueIndex = 0; // 초이스 1 대사 인덱스

    private TimeManager timeManager; // TimeManager 참조

    void Start()
    {
        // 플레이어 오브젝트를 태그로 찾기
        player = GameObject.FindGameObjectWithTag("Player");
        choiceUI.SetActive(false); // 선택 UI 숨기기
        dialogueUI.SetActive(false); // 대화 UI 숨기기

        // 버튼 클릭 이벤트 연결
        /*talkButton.onClick.AddListener(OnTalkButtonClick);
        persuadeButton.onClick.AddListener(OnPersuadeButtonClick);
        giftButton.onClick.AddListener(OnGiftButtonClick);
        choice1Button.onClick.AddListener(OnChoice1ButtonClick);
        choice2Button.onClick.AddListener(OnChoice2ButtonClick);*/

        SetChoiceButtonTexts(); // 선택지 텍스트 설정
        SetNpcName(); // NPC 이름 설정

        // TimeManager 스크립트 참조 얻기
        timeManager = FindObjectOfType<TimeManager>();

        // 초기 위치 업데이트
        UpdatePosition(timeManager.GetTimeOfDay(), timeManager.activityCount);
    }

    void SetChoiceButtonTexts()
    {
        // NPC 타입에 따라 선택지 버튼의 텍스트 설정
        switch (npcType)
        {
            case "검사":
                SetChoiceButtonText("당신이 그 유명한 용병 칼리스 맞죠?", "아뇨, 볼일은 딱히 없는데...");
                break;
            case "궁수":
                SetChoiceButtonText("너무 아름다우셔서요.", "혹시 활 쏘는 법 가르쳐줄 수 있으신가요?");
                break;
            case "탱커":
                SetChoiceButtonText("안녕하세요. 오늘 날씨가 참 좋네요!", "안녕하세요. 탱커님.");
                break;
            case "마법사":
                SetChoiceButtonText("마법은 참 위대한 것 같아요.", "안녕하세요. 날씨가 참 좋네요!");
                break;
            case "힐러":
                SetChoiceButtonText("신의 가호라뇨?", "감사합니다, 사제님.");
                break;
            case "암살자":
                SetChoiceButtonText("심심해요?", "(가만히 지켜본다)");
                break;
        }
    }

    void SetChoiceButtonText(string choice1, string choice2)
    {
        // 선택지 버튼 텍스트 설정
        choice1Button.GetComponentInChildren<TextMeshProUGUI>().text = choice1;
        choice2Button.GetComponentInChildren<TextMeshProUGUI>().text = choice2;
    }

    void SetNpcName()
    {
        // 첫 대화인지 여부에 따라 NPC 이름 설정
        npcNameText.text = IsFirstTalk() ? "???" : GetNpcName(npcType);
    }

    string GetNpcName(string type)
    {
        // NPC 타입에 따른 이름 반환
        switch (type)
        {
            case "검사": return "칼리스";
            case "탱커": return "펜릭";
            case "궁수": return "에릴란";
            case "마법사": return "크레이글";
            case "힐러": return "마르셀라";
            case "암살자": return "리아";
            default: return "???";
        }
    }

    bool IsFirstTalk()
    {
        // 캐릭터가 초면인지 확인하는 변수 저장
        List<Character> character = DataManager.instance.nowPlayer.characters.FindAll(x => x.Type != "검 사");
        
        foreach(var ii in character) {
            if(ii.Type == npcType) {
                ii.IsFirstTalk = PlayerPrefs.GetInt(npcType + "_FirstTalk", 1) == 1;
            }
        }
        DataManager.instance.SaveData();

        // 첫 대화인지 확인 (PlayerPrefs를 통해 저장된 값 확인)
        return PlayerPrefs.GetInt(npcType + "_FirstTalk", 1) == 1;
    }

    void Update()
    {
        // 플레이어와 NPC 간 거리 계산
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= interactionRange)
        {
            // 상호작용 키 입력 시 동작
            if (Input.GetKeyDown(KeyCode.Return) && !isTalking)
            {
                ShowChoiceUI(); // 선택 UI 표시
            }
            else if (Input.GetKeyDown(KeyCode.Space) && isTalking)
            {
                // 대화 진행
                if (choice1Dialogues.Count > 0 && choice1DialogueIndex < choice1Dialogues.Count)
                {
                    ShowNextChoice1Dialogue(); // 초이스 1 대화 표시
                }
                else
                {
                    ShowNextDialogue(); // 다음 대화 표시
                }
            }
        }
        else
        {
            choiceUI.SetActive(false); // 상호작용 거리 밖으로 벗어나면 선택 UI 숨기기
        }
    }

    void ShowChoiceUI()
    {
        choiceUI.SetActive(true); // 선택 UI 표시
        dialogueText.text = ""; // 대사 텍스트 초기화
    }

    public void OnTalkButtonClick()
    {
        choiceUI.SetActive(false); // 선택 UI 숨기기
        dialogueUI.SetActive(true); // 대화 UI 표시
        isTalking = true; // 대화 중 상태로 변경
        currentDialogueIndex = 0; // 대화 인덱스 초기화
        SetDialogue(npcType); // NPC 대사 설정
        ShowNextDialogue(); // 첫 번째 대화 표시

        // 첫 대화 시 PlayerPrefs에 정보 저장
        if (IsFirstTalk())
        {
            PlayerPrefs.SetInt(npcType + "_FirstTalk", 0);
            PlayerPrefs.Save();
            npcNameText.text = GetNpcName(npcType);
        }
    }

    public void OnPersuadeButtonClick()
    {
        // 설득 UI 표시
        choiceUI.SetActive(false);
        GetComponent<NpcPersuade>().ShowPersuadeUI();
    }

    public void OnGiftButtonClick()
    {
        // 선물하기 시 InventoryMain 씬으로 전환
        PlayerPrefs.SetString("NpcType", npcType);
        PlayerPrefs.Save();

        PlayerPrefs.SetString("CurType", "기타");
        PlayerPrefs.Save();

        SceneManager.LoadScene("InventoryMain");
    }

    public void HidePersuadeAndGiftButtons()
    {
        // 설득 및 선물 버튼 숨기기
        persuadeButton.gameObject.SetActive(false);
        giftButton.gameObject.SetActive(false);
    }

    public void EndDialogue()
    {
        // 대화 종료 시 동작
        isTalking = false; // 대화 중 상태 해제
        dialogueUI.SetActive(false); // 대화 UI 숨기기
        choiceUI.SetActive(false); // 선택 UI 숨기기
        choice1Button.gameObject.SetActive(false); // 초이스 1 버튼 숨기기
        choice2Button.gameObject.SetActive(false); // 초이스 2 버튼 숨기기
    }

    void SetDialogue(string type)
    {
        dialogues.Clear(); // 대사 목록 초기화
        SetNpcName(); // NPC 이름 설정

        // NPC 타입에 따른 대사 설정
        switch (type)
        {
            case "검사":
                dialogues.Add("음, 처음 보는 얼굴 같은데.");
                dialogues.Add("나한테 무슨 볼일이라도?");
                break;
            case "궁수":
                dialogues.Add("...뭐야. 나한테 볼일 있어?");
                break;
            case "탱커":
                dialogues.Add("......");
                break;
            case "마법사":
                dialogues.Add(".......");
                break;
            case "힐러":
                dialogues.Add("안녕하세요, 용병님!");
                dialogues.Add("만나서 반갑습니다. 신의 가호가 함께 하시길..");
                break;
            case "암살자":
                dialogues.Add("흠흠흠~");
                break;
        }
    }

    void ShowNextDialogue()
    {
        // 다음 대사 표시
        if (currentDialogueIndex < dialogues.Count)
        {
            dialogueText.text = dialogues[currentDialogueIndex];
            if (currentDialogueIndex == 0 && IsFirstTalk())
            {
                PlayerPrefs.SetInt(npcType + "_FirstTalk", 0);
                PlayerPrefs.Save();
                npcNameText.text = GetNpcName(npcType);
            }

            if (IsChoicePoint(dialogueText.text))
            {
                choice1Button.gameObject.SetActive(true);
                choice2Button.gameObject.SetActive(true);
            }

            currentDialogueIndex++;
        }
        else
        {
            EndDialogue(); // 대사가 끝나면 대화 종료
        }
    }

    bool IsChoicePoint(string dialogue)
    {
        // 선택지 표시 여부 확인
        return dialogue == "나한테 무슨 볼일이라도?" ||
               dialogue == "...뭐야. 나한테 볼일 있어?" ||
               dialogue == "......" ||
               dialogue == "......." ||
               dialogue == "만나서 반갑습니다. 신의 가호가 함께 하시길.." ||
               dialogue == "흠흠흠~";
    }

    public void OnChoice1ButtonClick()
    {
        choice1Dialogues.Clear(); // 초이스 1 대사 목록 초기화
        switch (npcType)
        {
            case "검사":
                SetChoice1Dialogues(new List<string>
                {
                    "하하, 내가 칼리스 맞지.",
                    "날 아는 사람이었구나, 반가워.",
                    "보아하니 당신도 용병 같은데... 이 마을에 속한 용병인가?",
                    "나도 이 마을에 좋은 의뢰가 잘 들어온다길래 잠시 이곳에 머무르고 있어",
                    "앞으로 잘 지내보자고!"
                }, 5);
                break;
            case "궁수":
                SetChoice1Dialogues(new List<string>
                {
                    "으... 뭐래."
                }, -10);
                break;
            case "탱커":
                SetChoice1Dialogues(new List<string>
                {
                    "아.안녕하시오.",
                    "...그렇군. 날씨가 좋은 줄도 모르고 지나갈 뻔 했소.",
                    "다정한 인사를 건네줘서 고맙네. 청년."
                }, 5);
                break;
            case "마법사":
                SetChoice1Dialogues(new List<string>
                {
                    "오. 안녕하세요.",
                    "저도 그렇게 생각해요! 마법은 참 위대하죠!",
                    "그리고 그 위대한 마법의 발전을 위해 다양한 연구와 실험은 불가피해요.",
                    "그렇지 않나요?"
                }, 5);
                break;
            case "힐러":
                SetChoice1Dialogues(new List<string>
                {
                    "음.. 신을 믿지 않으시나요?",
                    "그렇다면 참 아쉽네요.."
                }, 5);
                break;
            case "암살자":
                SetChoice1Dialogues(new List<string>
                {
                    "네! 어떻게 알았지~?",
                    "뭔가 재밌는 일이 생겼으면 좋겠어요~",
                    "당신은 좀 재밌어 보이긴 하네요!"
                }, 5);
                break;
        }

        choice1DialogueIndex = 0; // 초이스 1 대사 인덱스 초기화
        isTalking = true; // 대화 중 상태로 변경
        choice1Button.gameObject.SetActive(false); // 초이스 1 버튼 숨기기
        choice2Button.gameObject.SetActive(false); // 초이스 2 버튼 숨기기
        ShowNextChoice1Dialogue(); // 초이스 1 대사 표시
    }

    public void OnChoice2ButtonClick()
    {
        // 선택지 2 선택 시 동작
        choice1Dialogues.Clear();
        choice1DialogueIndex = 0;
        switch (npcType)
        {
            case "검사":
                SetDialogueText("...그럼 왜 말을 건 거지?");
                ChangeAffection(-5);
                break;
            case "궁수":
                SetChoice1Dialogues(new List<string>
                {
                    "활이라고?",
                    "음... 당신 용병이구나.",
                    "...활은 집중력이 중요하지.",
                    "가르쳐주는 건 모르겠지만 가끔 봐줄 순 있어."
                }, 5);
                break;
            case "탱커":
                SetChoice1Dialogues(new List<string>
                {
                    "...날 알고 있는가?",
                    "미안하지만 탱커 역할을 기대하고 온 거라면 돌아가게.",
                    "나는 다른 사람과 함께 일하지 않아."
                }, -5);
                break;
            case "마법사":
                SetChoice1Dialogues(new List<string>
                {
                    "아 네. 그렇네요.",
                    "...",
                    "뭐... 더 하실 말씀이라도?"
                }, -5);
                break;
            case "힐러":
                SetChoice1Dialogues(new List<string>
                {
                    "하하하. 오랜만에 듣는 칭호네요.",
                    "비록 지금은 사제가 아니지만...",
                    "그래도 여전히 신을 섬기고 있답니다~"
                }, 5);
                break;
            case "암살자":
                SetChoice1Dialogues(new List<string>
                {
                    "랄랄라~",
                    "(계속해서 노래를 흥얼거린다)"
                }, -5);
                break;
        }

        choice1DialogueIndex = 0; // 초이스 1 대사 인덱스 초기화
        isTalking = true; // 대화 중 상태로 변경
        choice1Button.gameObject.SetActive(false); // 초이스 1 버튼 숨기기
        choice2Button.gameObject.SetActive(false); // 초이스 2 버튼 숨기기
        ShowNextChoice1Dialogue(); // 초이스 1 대사 표시
    }

    void SetChoice1Dialogues(List<string> dialogues, double affectionChange)
    {
        choice1Dialogues = dialogues; // 초이스 1 대사 목록 설정
        ChangeAffection(affectionChange); // 호감도 변경
    }

    void SetDialogueText(string dialogue)
    {
        // NPC 이름 설정 및 대사 설정
        npcNameText.text = GetNpcName(npcType);
        dialogueText.text = dialogue;
    }

    void ShowNextChoice1Dialogue()
    {
        // 다음 초이스 1 대사 표시
        if (choice1DialogueIndex < choice1Dialogues.Count)
        {
            string dialogue = choice1Dialogues[choice1DialogueIndex];
            SetDialogueText(dialogue);

            choice1DialogueIndex++;
        }
        else
        {
            EndDialogue(); // 초이스 1 대사가 끝나면 대화 종료
        }
    }

    void ChangeAffection(double amount)
    {
        // 호감도 변경
        Character character = DataManager.instance.nowPlayer.characters.Find(x => x.Type == npcType);
        affection += amount;
        character.Love = affection.ToString();

        DataManager.instance.SaveData();

        affectionText.text = $"호감도: {affection}";
    }

    public void UpdatePosition(string timeOfDay, int activityCount)
    {
        // 시간대에 따라 NPC 위치 업데이트
        Vector3 newPosition = Vector3.zero;
        string currentScene = SceneManager.GetActiveScene().name;
        bool shouldBeActive = false;

        switch (npcType)
        {
            case "검사":
                if (activityCount == 0 || activityCount == 1)
                {
                    newPosition = new Vector3(-6, -0.5f, 0); // main_map에서의 위치
                    shouldBeActive = currentScene == "main_map";
                }
                else if (activityCount == 2)
                {
                    newPosition = new Vector3(-2, 0, 0); // hotel에서의 위치
                    shouldBeActive = currentScene == "hotel";
                }
                break;

            case "힐러":
                if (activityCount == 0 || activityCount == 1)
                {
                    newPosition = new Vector3(14.68f, 7.29f, 0); // main_map에서의 위치
                    shouldBeActive = currentScene == "main_map";
                }
                else if (activityCount == 2)
                {
                    newPosition = new Vector3(2, 0.85f, 0); // hotel_hall에서의 위치
                    shouldBeActive = currentScene == "hotel_hall";
                }
                break;

            case "탱커":
                if (activityCount == 0 || activityCount == 1)
                {
                    newPosition = new Vector3(-4, 3.36f, 0); // training에서의 위치
                    shouldBeActive = currentScene == "training";
                }
                else if (activityCount == 2)
                {
                    newPosition = new Vector3(0.4f, 3.5f, 0); // hotel_room1에서의 위치
                    shouldBeActive = currentScene == "hotel_room1";
                }
                break;

            case "마법사":
                newPosition = new Vector3(1.73f, 0.63f, 0); // magic_house에서의 위치
                shouldBeActive = currentScene == "magic_house";
                break;

            case "암살자":
                if (activityCount == 2)
                {
                    newPosition = new Vector3(-2.23f, -0.59f, 0); // bar에서의 위치
                    shouldBeActive = currentScene == "bar";
                }
                break;

            case "궁수":
                if (activityCount == 2)
                {
                    newPosition = new Vector3(4.2f, -1.74f, 0); // training에서의 위치
                    shouldBeActive = currentScene == "training";
                }
                break;
        }

        gameObject.SetActive(shouldBeActive);
        if (shouldBeActive)
        {
            transform.position = newPosition;
        }
    }

    (Vector3, bool) GetPositionAndState(string timeOfDay, string currentScene, string dayScene, Vector3 dayPosition, string nightScene = null, Vector3 nightPosition = default(Vector3))
    {
        // 시간대와 씬에 따라 NPC 위치와 활성화 상태 반환
        bool isDay = timeOfDay == "Morning" || timeOfDay == "Afternoon";
        bool shouldBeActive = false;
        Vector3 newPosition = Vector3.zero;

        if (currentScene == dayScene && isDay)
        {
            newPosition = dayPosition;
            shouldBeActive = true;
        }
        else if (nightScene != null && currentScene == nightScene && !isDay)
        {
            newPosition = nightPosition;
            shouldBeActive = true;
        }

        return (newPosition, shouldBeActive);
    }
}
