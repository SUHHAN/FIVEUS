using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject interactionUI; // ��ȣ�ۿ� UI (��ȭ�ϱ�, �����ϱ� ��ư)
    public Button talkButton; // ��ȭ�ϱ� ��ư
    public Button persuadeButton; // �����ϱ� ��ư
    public GameObject dialogueUI; // ��ȭ UI (��ȭ �ؽ�Ʈ�� ǥ���ϴ� UI)
    public Text dialogueText; // ��ȭ �ؽ�Ʈ
    public GameObject choiceUI; // ����â UI
    public Button choice1Button; // ���� 1 ��ư
    public Button choice2Button; // ���� 2 ��ư
    public int affection; // ȣ���� (0~100)
    private bool isPlayerNearby; // �÷��̾ ��ó�� �ִ��� ����

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
            dialogueText.text = "ȣ������ ���� ���";
        }
        else if (affection < 70)
        {
            dialogueText.text = "ȣ������ �߰��� ���";
        }
        else
        {
            dialogueText.text = "ȣ������ ���� ���";
        }

        // ��ȭ �� ����â ����
        Invoke("ShowChoices", 2.0f); // 2�� �� ����â ����
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
            dialogueText.text = "���� 1�� �������� ���� ���. ȣ������ �����մϴ�.";
        }
        else if (choice == 2)
        {
            affection -= 10;
            dialogueText.text = "���� 2�� �������� ���� ���. ȣ������ �����մϴ�.";
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
            dialogueText.text = "���濡 �����߽��ϴ�!";
        }
        else
        {
            dialogueText.text = "���濡 �����߽��ϴ�.";
        }
    }

    public void GiveGift(int giftValue)
    {
        affection += giftValue;
        if (affection < 0) affection = 0;
        if (affection > 100) affection = 100;

        dialogueText.text = "������ ���� ���� ���. ���� ȣ����: " + affection;
    }
}
