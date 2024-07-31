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
    public bool isTalking = false; // 대화 중인지 여부를 외부에서 접근할 수 있도록 public으로 변경
    public Button choice1Button; // Choice 1 Button
    public Button choice2Button; // Choice 2 Button
    private NpcDialogue npcDialogue; // NpcDialogue 스크립트 참조

    public enum NpcType { Warrior, Healer, Tank, Mage, Assassin, Archer } // NPC 유형
    public NpcType npcType; // NPC의 유형
    public string npcName; // NPC의 이름
    public Sprite npcImage; // NPC의 이미지

    // NPC 유형에 따른 대사와 선호 선물 정의
    private Dictionary<NpcType, string[]> npcDialogues = new Dictionary<NpcType, string[]>()
    {
        { NpcType.Warrior, new string[] { "안녕하세요, 전사입니다.", "무기를 좋아합니다." } },
        { NpcType.Healer, new string[] { "안녕하세요, 힐러입니다.", "치유 아이템을 좋아합니다." } },
        { NpcType.Tank, new string[] { "안녕하세요, 탱커입니다.", "방어구를 좋아합니다." } },
        { NpcType.Mage, new string[] { "안녕하세요, 마법사입니다.", "마법 아이템을 좋아합니다." } },
        { NpcType.Assassin, new string[] { "흠흠흠~"} },
        { NpcType.Archer, new string[] { "안녕하세요, 궁수입니다.", "활과 화살을 좋아합니다." } }
    };

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // 태그가 "Player"인 오브젝트 찾기
        choiceUI.SetActive(false); // 시작할 때 선택 UI 비활성화
        dialogueUI.SetActive(false); // 시작할 때 대화 UI 비활성화
        talkButton.onClick.AddListener(OnTalkButtonClick); // 대화하기 버튼 클릭 이벤트 연결
        persuadeButton.onClick.AddListener(OnPersuadeButtonClick); // 설득하기 버튼 클릭 이벤트 연결
        giftButton.onClick.AddListener(OnGiftButtonClick); // 선물하기 버튼 클릭 이벤트 연결

        npcDialogue = GetComponent<NpcDialogue>(); // NpcDialogue 스크립트 참조 가져오기
        // 버튼 이벤트 설정
        choice1Button.onClick.AddListener(OnChoice1ButtonClick);
        choice2Button.onClick.AddListener(OnChoice2ButtonClick);
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

    // 암살자의 첫 번째 선택지 클릭 시 호출되는 함수
    public void OnChoice1ButtonClick()
    {
        choice1Button.gameObject.SetActive(false);
        choice2Button.gameObject.SetActive(false);
        npcDialogue.SetDialogue("네! 어떻게 알았지~? 뭔가 재밌는 일이 생겼으면 좋겠어요~ 당신은 좀 재밌어 보이긴 하네요!");
    }

    // 암살자의 두 번째 선택지 클릭 시 호출되는 함수
    public void OnChoice2ButtonClick()
    {
        choice1Button.gameObject.SetActive(false);
        choice2Button.gameObject.SetActive(false);
        npcDialogue.SetDialogue("랄랄라~ (계속해서 노래를 흥얼거린다)");
    }

    public void OnTalkButtonClick()
    {
        choiceUI.SetActive(false); // 선택 UI 숨기기
        dialogueUI.SetActive(true); // 대화 UI 활성화
        isTalking = true; // 대화 상태 설정
        if (npcType == NpcType.Assassin)
        {
            npcDialogue.StartAssassinDialogue(); // 암살자 대사 시작
        }
        else
        {
            npcDialogue.StartDialogue(npcDialogues[npcType], npcName, npcImage); // 일반 대화 시작
        }
    }

    public void OnPersuadeButtonClick()
    {
        choiceUI.SetActive(false); // 선택 UI 숨기기
        GetComponent<NpcPersuade>().ShowPersuadeUI(); // 설득 UI 표시
    }

    public void OnGiftButtonClick()
    {
        // NPC의 유형에 따른 선물 정보 표시
        string preferredGift = npcDialogues[npcType][1];
        dialogueText.text = $"이 NPC는 {preferredGift}";
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
}
