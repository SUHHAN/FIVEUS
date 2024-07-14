using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject interactionUI; // 상호작용 UI (대화하기, 설득하기 버튼)
    public Button talkButton; // 대화하기 버튼
    public Button persuadeButton; // 설득하기 버튼
    public GameObject dialogueUI; // 대화 UI (대화 텍스트를 표시하는 UI)
    public Text dialogueText; // 대화 텍스트
    public GameObject choiceUI; // 선택창 UI
    public Button choice1Button; // 선택 1 버튼
    public Button choice2Button; // 선택 2 버튼
    public int affection; // 호감도 (0~100)
    private bool isPlayerNearby; // 플레이어가 근처에 있는지 여부

    void Start()
    {
        interactionUI.SetActive(false);
        dialogueUI.SetActive(false);
        choiceUI.SetActive(false);

        talkButton.onClick.AddListener(Talk);
        persuadeButton.onClick.AddListener(Persuade);
        choice1Button.onClick.AddListener(() => MakeChoice(1));
        choice2Button.onClick.AddListener(() => MakeChoice(2));
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            interactionUI.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            interactionUI.SetActive(false);
            dialogueUI.SetActive(false);
            choiceUI.SetActive(false);
        }
    }

    void Talk()
    {
        interactionUI.SetActive(false);
        dialogueUI.SetActive(true);

        if (affection < 30)
        {
            dialogueText.text = "호감도가 낮은 대사";
        }
        else if (affection < 70)
        {
            dialogueText.text = "호감도가 중간인 대사";
        }
        else
        {
            dialogueText.text = "호감도가 높은 대사";
        }

        // 대화 중 선택창 띄우기
        Invoke("ShowChoices", 2.0f); // 2초 후 선택창 띄우기
    }

    void ShowChoices()
    {
        choiceUI.SetActive(true);
        dialogueUI.SetActive(false);
    }

    void MakeChoice(int choice)
    {
        choiceUI.SetActive(false);
        dialogueUI.SetActive(true);

        if (choice == 1)
        {
            affection += 10;
            dialogueText.text = "선택 1을 선택했을 때의 대사. 호감도가 증가합니다.";
        }
        else if (choice == 2)
        {
            affection -= 10;
            dialogueText.text = "선택 2를 선택했을 때의 대사. 호감도가 감소합니다.";
        }
    }

    void Persuade()
    {
        interactionUI.SetActive(false);
        dialogueUI.SetActive(true);

        int successChance = 0;

        if (affection >= 50 && affection < 60)
        {
            successChance = 50;
        }
        else if (affection >= 60 && affection < 70)
        {
            successChance = 60;
        }
        else if (affection >= 70 && affection < 80)
        {
            successChance = 70;
        }
        else if (affection >= 80 && affection < 90)
        {
            successChance = 80;
        }
        else if (affection >= 90 && affection < 100)
        {
            successChance = 90;
        }
        else if (affection == 100)
        {
            successChance = 100;
        }

        if (Random.Range(0, 100) < successChance)
        {
            dialogueText.text = "설득에 성공했습니다!";
        }
        else
        {
            dialogueText.text = "설득에 실패했습니다.";
        }
    }

    public void GiveGift(int giftValue)
    {
        affection += giftValue;
        if (affection < 0) affection = 0;
        if (affection > 100) affection = 100;

        dialogueText.text = "선물을 받은 후의 대사. 현재 호감도: " + affection;
    }
}
