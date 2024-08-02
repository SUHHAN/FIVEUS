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

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // 태그가 "Player"인 오브젝트 찾기
        choiceUI.SetActive(false); // 시작할 때 선택 UI 비활성화
        dialogueUI.SetActive(false); // 시작할 때 대화 UI 비활성화
        persuadeButton.onClick.AddListener(OnPersuadeButtonClick); // 설득하기 버튼 클릭 이벤트 연결
        choice1Button.gameObject.SetActive(false); // 선택지 1 버튼 비활성화
        choice2Button.gameObject.SetActive(false); // 선택지 2 버튼 비활성화
        giftButton.onClick.AddListener(OnGiftButtonClick); // 선물하기 버튼 클릭 이벤트 연결

        choice1Button.onClick.AddListener(OnChoice1ButtonClick); // 선택지 1 버튼 클릭 이벤트 연결
        choice2Button.onClick.AddListener(OnChoice2ButtonClick); // 선택지 2 버튼 클릭 이벤트 연결

        // 선택지 버튼 텍스트 설정
        if (npcType == "검사")
        {
            choice1Button.GetComponentInChildren<TextMeshProUGUI>().text = "당신이 그 유명한 용병 칼리스 맞죠?";
            choice2Button.GetComponentInChildren<TextMeshProUGUI>().text = "아뇨, 볼일은 딱히 없는데...";
        }
        else if (npcType == "궁수")
        {
            choice1Button.GetComponentInChildren<TextMeshProUGUI>().text = "너무 아름다우셔서요.";
            choice2Button.GetComponentInChildren<TextMeshProUGUI>().text = "혹시 활 쏘는 법 가르쳐줄 수 있으신가요?";
        }
        else if (npcType == "탱커")
        {
            choice1Button.GetComponentInChildren<TextMeshProUGUI>().text = "안녕하세요. 오늘 날씨가 참 좋네요!";
            choice2Button.GetComponentInChildren<TextMeshProUGUI>().text = "안녕하세요. 탱커님.";
        }
        else if (npcType == "마법사")
        {
            choice1Button.GetComponentInChildren<TextMeshProUGUI>().text = "마법은 참 위대한 것 같아요.";
            choice2Button.GetComponentInChildren<TextMeshProUGUI>().text = "안녕하세요. 날씨가 참 좋네요!";
        }
        else if (npcType == "힐러")
        {
            choice1Button.GetComponentInChildren<TextMeshProUGUI>().text = "신의 가호라뇨?";
            choice2Button.GetComponentInChildren<TextMeshProUGUI>().text = "감사합니다, 사제님.";
        }
        else if (npcType == "암살자")
        {
            choice1Button.GetComponentInChildren<TextMeshProUGUI>().text = "심심해요?";
            choice2Button.GetComponentInChildren<TextMeshProUGUI>().text = "(가만히 지켜본다)";
        }
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

    public void OnGiftButtonClick()
    {
        PlayerPrefs.SetString("NpcType", npcType);
        PlayerPrefs.Save();

        // 다른 씬에서 curType을 저장
        PlayerPrefs.SetString("CurType", "기타"); // "장비" 대신 원하는 탭 이름 사용
        PlayerPrefs.Save();

        print(PlayerPrefs.GetString("NpcType"));
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
            npcNameText.text = "???";
            dialogues.Add("음, 처음 보는 얼굴 같은데.");
            dialogues.Add("나한테 무슨 볼일이라도?");
        }
        else if (type == "궁수")
        {
            npcNameText.text = "???";
            dialogues.Add("...뭐야. 나한테 볼일 있어?");
        }
        else if (type == "탱커")
        {
            npcNameText.text = "???";
            dialogues.Add("......");
        }
        else if (type == "마법사")
        {
            npcNameText.text = "???";
            dialogues.Add(".......");
        }
        else if (type == "힐러")
        {
            npcNameText.text = "???";
            dialogues.Add("안녕하세요, 용병님!");
            dialogues.Add("만나서 반갑습니다. 신의 가호가 함께 하시길..");
        }
        else if (type == "암살자")
        {
            npcNameText.text = "???";
            dialogues.Add("흠흠흠~");
        }
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
            else if (dialogueText.text == "...뭐야. 나한테 볼일 있어?")
            {
                choice1Button.gameObject.SetActive(true); // 선택지 1 버튼 활성화
                choice2Button.gameObject.SetActive(true); // 선택지 2 버튼 활성화
            }
            else if (dialogueText.text == "......")
            {
                choice1Button.gameObject.SetActive(true); // 선택지 1 버튼 활성화
                choice2Button.gameObject.SetActive(true); // 선택지 2 버튼 활성화
            }
            else if (dialogueText.text == ".......")
            {
                choice1Button.gameObject.SetActive(true); // 선택지 1 버튼 활성화
                choice2Button.gameObject.SetActive(true); // 선택지 2 버튼 활성화
            }
            else if (dialogueText.text == "만나서 반갑습니다. 신의 가호가 함께 하시길..")
            {
                choice1Button.gameObject.SetActive(true); // 선택지 1 버튼 활성화
                choice2Button.gameObject.SetActive(true); // 선택지 2 버튼 활성화
            }
            else if (dialogueText.text == "흠흠흠~")
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
        if (npcType == "검사")
        {
            choice1Dialogues = new List<string>
            {
                "칼리스,하하, 내가 칼리스 맞지.",
                "칼리스,날 아는 사람이었구나, 반가워.",
                "칼리스,보아하니 당신도 용병 같은데... 이 마을에 속한 용병인가?",
                "칼리스,나도 이 마을에 좋은 의뢰가 잘 들어온다길래 잠시 이곳에 머무르고 있어",
                "칼리스,앞으로 잘 지내보자고!"
            };
            ChangeAffection(2.5); // 호감도 +5
        }
        else if (npcType == "궁수")
        {
            choice1Dialogues = new List<string>
            {
                "에릴란,으... 뭐래."
            };
            ChangeAffection(-5); // 호감도 -10
        }
        else if (npcType == "탱커")
        {
            choice1Dialogues = new List<string>
            {
                "펜릭,아.안녕하시오.",
                "펜릭,...그렇군. 날씨가 좋은 줄도 모르고 지나갈 뻔 했소.",
                "펜릭,다정한 인사를 건네줘서 고맙네. 청년."
            };
            ChangeAffection(2.5); // 호감도 +5
        }
        else if (npcType == "마법사")
        {
            choice1Dialogues = new List<string>
            {
                "크레이글,오. 안녕하세요.",
                "크레이글, 저도 그렇게 생각해요! 마법은 참 위대하죠!",
                "크레이글, 그리고 그 위대한 마법의 발전을 위해 다양한 연구와 실험은 불가피해요.",
                "크레이글, 그렇지 않나요?"
            };
            ChangeAffection(2.5); // 호감도 +5
        }
        else if (npcType == "힐러")
        {
            choice1Dialogues = new List<string>
            {
                "마르셀라, 음.. 신을 믿지 않으시나요?",
                "마르셀라, 그렇다면 참 아쉽네요.."
            };
            ChangeAffection(-2.5); // 호감도 -5
        }
        else if (npcType == "암살자")
        {
            choice1Dialogues = new List<string>
            {
                "리아, 네! 어떻게 알았지~?",
                "리아, 뭔가 재밌는 일이 생겼으면 좋겠어요~",
                "리아, 당신은 좀 재밌어 보이긴 하네요!"
            };
            ChangeAffection(+2.5); // 호감도 +5
        }
        choice1DialogueIndex = 0;
        isTalking = true; // 대화 상태 유지
        choice1Button.gameObject.SetActive(false); // 선택지 1 버튼 비활성화
        choice2Button.gameObject.SetActive(false); // 선택지 2 버튼 비활성화
        ShowNextChoice1Dialogue(); // 첫 번째 대사 출력
    }

    public void OnChoice2ButtonClick()
    {
        // choice1Dialogues 리스트 초기화
        choice1Dialogues.Clear();
        choice1DialogueIndex = 0;
        if (npcType == "검사")
        {
            dialogueText.text = "...그럼 왜 말을 건 거지?"; // 선택지 2에 대한 대사
            npcNameText.text = "칼리스";
            ChangeAffection(-2.5); // 호감도 -5
        }
        else if (npcType == "궁수")
        {
            choice1Dialogues = new List<string>
            {
                "에릴란,활이라고?",
                "에릴란,음... 당신 용병이구나.",
                "에릴란,...활은 집중력이 중요하지.",
                "에릴란,가르쳐주는 건 모르겠지만 가끔 봐줄 순 있어."
            };
            ChangeAffection(2.5); // 호감도 +5
        }
        else if (npcType == "탱커")
        {
            choice1Dialogues = new List<string>
            {
                "펜릭,...날 알고 있는가?",
                "펜릭,미안하지만 탱커 역할을 기대하고 온 거라면 돌아가게.",
                "펜릭, 나는 다른 사람과 함께 일하지 않아."
            };
            ChangeAffection(-2.5); // 호감도 -5
        }
        else if (npcType == "마법사")
        {
            choice1Dialogues = new List<string>
            {
                "크레이글, 아 네. 그렇네요.",
                "크레이글, ...",
                "크레이글, 뭐... 더 하실 말씀이라도?"
            };
            ChangeAffection(-2.5); // 호감도 -5
        }
        else if (npcType == "힐러")
        {
            choice1Dialogues = new List<string>
            {
                "마르셀라, 하하하. 오랜만에 듣는 칭호네요.",
                "마르셀라, 비록 지금은 사제가 아니지만...",
                "마르셀라, 그래도 여전히 신을 섬기고 있답니다~"
            };
            ChangeAffection(2.5); // 호감도 +5
        }
        else if (npcType == "암살자")
        {
            choice1Dialogues = new List<string>
            {
                "리아, 랄랄라~",
                "리아, (계속해서 노래를 흥얼거린다)"
            };
            ChangeAffection(-2.5); // 호감도 -5
        }
        choice1DialogueIndex = 0;
        isTalking = true; // 대화 상태 유지
        choice1Button.gameObject.SetActive(false); // 선택지 1 버튼 비활성화
        choice2Button.gameObject.SetActive(false); // 선택지 2 버튼 비활성화
        ShowNextChoice1Dialogue(); // 첫 번째 대사 출력
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
    void ChangeAffection(double amount)
    {
        Character character = DataManager.instance.nowPlayer.characters.Find(x => x.Type == npcType);
        affection += amount;
        character.Love = affection.ToString();

        DataManager.instance.SaveData();

        affectionText.text = $"호감도: {affection}";
    }

    // NPC 대화 내용 표시
    void DisplayDialogue(string npcName, string description)
    {
        dialogueText.text = description; // 대화 내용 설정
        npcNameText.text = npcName; // NPC 이름 설정
    }

    void UpdatePosition(string timeOfDay)
    {
        Vector3 newPosition = Vector3.zero;
        string currentScene = SceneManager.GetActiveScene().name;
        bool shouldBeActive = false;

        switch (npcType)
        {
            case "검사":
                if (currentScene == "main_map")
                {
                    if (timeOfDay == "Morning" || timeOfDay == "Afternoon")
                    {
                        newPosition = new Vector3(-6, -0.5f, 0);
                        shouldBeActive = true; // main_map에 있을 때만 활성화
                    }
                    else
                    {
                        shouldBeActive = false; // 저녁에는 main_map에서 비활성화
                    }
                }
                else if (currentScene == "hotel")
                {
                    if (timeOfDay == "Evening")
                    {
                        newPosition = new Vector3(-2, 0, 0);
                        shouldBeActive = true; // hotel에 있을 때만 활성화
                    }
                    else
                    {
                        shouldBeActive = false; // 아침과 점심에는 hotel에서 비활성화
                    }
                }
                break;

            case "힐러":
                if (currentScene == "main_map")
                {
                    if (timeOfDay == "Morning" || timeOfDay == "Afternoon")
                    {
                        newPosition = new Vector3(14.68f, 7.29f, 0);
                        shouldBeActive = true; // main_map에 있을 때만 활성화
                    }
                    else
                    {
                        shouldBeActive = false; // 저녁에는 main_map에서 비활성화
                    }
                }
                else if (currentScene == "hotel_hall")
                {
                    if (timeOfDay == "Evening")
                    {
                        newPosition = new Vector3(2, 0.85f, 0);
                        shouldBeActive = true; // hotel_hall에 있을 때만 활성화
                    }
                    else
                    {
                        shouldBeActive = false; // 아침과 점심에는 hotel_hall에서 비활성화
                    }
                }
                break;

            case "탱커":
                if (currentScene == "training")
                {
                    if (timeOfDay == "Morning" || timeOfDay == "Afternoon")
                    {
                        newPosition = new Vector3(-4, 3.36f, 0);
                        shouldBeActive = true; // training에 있을 때만 활성화
                    }
                    else
                    {
                        shouldBeActive = false; // 저녁에는 training에서 비활성화
                    }
                }
                else if (currentScene == "hotel_room1")
                {
                    if (timeOfDay == "Evening")
                    {
                        newPosition = new Vector3(0.4f, 3.5f, 0);
                        shouldBeActive = true; // hotel_room1에 있을 때만 활성화
                    }
                    else
                    {
                        shouldBeActive = false; // 아침과 점심에는 hotel_room1에서 비활성화
                    }
                }
                break;

            case "마법사":
                if (currentScene == "magic_house")
                {
                    newPosition = new Vector3(1.73f, 0.63f, 0);
                    shouldBeActive = true; // magic_house에서 항상 활성화
                }
                break;

            case "암살자":
                if (currentScene == "bar")
                {
                    if (timeOfDay == "Evening")
                    {
                        newPosition = new Vector3(-2.23f, -0.59f, 0);
                        shouldBeActive = true; // bar에 있을 때만 활성화
                    }
                    else
                    {
                        shouldBeActive = false; // 저녁 이외의 시간에는 bar에서 비활성화
                    }
                }
                break;

            case "궁수":
                if (currentScene == "training")
                {
                    if (timeOfDay == "Evening")
                    {
                        newPosition = new Vector3(4.2f, -1.74f, 0);
                        shouldBeActive = true; // training에 있을 때만 활성화
                    }
                    else
                    {
                        shouldBeActive = false; // 저녁 이외의 시간에는 training에서 비활성화
                    }
                }
                break;
        }

        // NPC 활성화 여부에 따라 게임 오브젝트 활성화/비활성화
        gameObject.SetActive(shouldBeActive);
        if (shouldBeActive)
        {
            transform.position = newPosition; // NPC 위치 설정
        }
    }
}