using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; // �� ��ȯ

public class NpcScript : MonoBehaviour
{
    public GameObject choiceUI; // ���� UI �г�
    public GameObject dialogueUI; // ��ȭ UI �г�
    public GameObject npcAffectionUI; // ȣ���� UI �г�
    public TextMeshProUGUI dialogueText; // ��� �ؽ�Ʈ UI ����
    public TextMeshProUGUI affectionText; // ȣ���� �ؽ�Ʈ UI ����
    public Button talkButton; // ��ȭ�ϱ� ��ư ����
    public Button persuadeButton; // �����ϱ� ��ư ����
    public Button giftButton; // �����ϱ� ��ư ����
    public float interactionRange = 3.0f; // ��ȣ�ۿ� �Ÿ�
    private GameObject player; // �÷��̾� ������Ʈ
    public bool isTalking = false; // ��ȭ ������ ���θ� �ܺο��� ������ �� �ֵ��� public���� ����
    public Button choice1Button; // Choice 1 Button
    public Button choice2Button; // Choice 2 Button
    private NpcDialogue npcDialogue; // NpcDialogue ��ũ��Ʈ ����

    public enum NpcType { Warrior, Healer, Tank, Mage, Assassin, Archer } // NPC ����
    public NpcType npcType; // NPC�� ����
    public string npcName; // NPC�� �̸�
    public Sprite npcImage; // NPC�� �̹���

    // NPC ������ ���� ���� ��ȣ ���� ����
    private Dictionary<NpcType, string[]> npcDialogues = new Dictionary<NpcType, string[]>()
    {
        { NpcType.Warrior, new string[] { "�ȳ��ϼ���, �����Դϴ�.", "���⸦ �����մϴ�." } },
        { NpcType.Healer, new string[] { "�ȳ��ϼ���, �����Դϴ�.", "ġ�� �������� �����մϴ�." } },
        { NpcType.Tank, new string[] { "�ȳ��ϼ���, ��Ŀ�Դϴ�.", "���� �����մϴ�." } },
        { NpcType.Mage, new string[] { "�ȳ��ϼ���, �������Դϴ�.", "���� �������� �����մϴ�." } },
        { NpcType.Assassin, new string[] { "������~"} },
        { NpcType.Archer, new string[] { "�ȳ��ϼ���, �ü��Դϴ�.", "Ȱ�� ȭ���� �����մϴ�." } }
    };

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // �±װ� "Player"�� ������Ʈ ã��
        choiceUI.SetActive(false); // ������ �� ���� UI ��Ȱ��ȭ
        dialogueUI.SetActive(false); // ������ �� ��ȭ UI ��Ȱ��ȭ
        talkButton.onClick.AddListener(OnTalkButtonClick); // ��ȭ�ϱ� ��ư Ŭ�� �̺�Ʈ ����
        persuadeButton.onClick.AddListener(OnPersuadeButtonClick); // �����ϱ� ��ư Ŭ�� �̺�Ʈ ����
        giftButton.onClick.AddListener(OnGiftButtonClick); // �����ϱ� ��ư Ŭ�� �̺�Ʈ ����

        npcDialogue = GetComponent<NpcDialogue>(); // NpcDialogue ��ũ��Ʈ ���� ��������
        // ��ư �̺�Ʈ ����
        choice1Button.onClick.AddListener(OnChoice1ButtonClick);
        choice2Button.onClick.AddListener(OnChoice2ButtonClick);
    }

    void Update()
    {
        if (isTalking)
        {
            return;
        }

        float distance = Vector3.Distance(player.transform.position, transform.position); // �÷��̾�� NPC �� �Ÿ� ���
        if (distance <= interactionRange) // ��ȣ�ۿ� �Ÿ� ���� �ִ��� Ȯ��
        {
            if (Input.GetKeyDown(KeyCode.Return)) // ���� Ű �Է� ����
            {
                ShowChoiceUI(); // ���� UI ǥ��
            }
        }
        else
        {
            choiceUI.SetActive(false); // ���� UI�� �ڽ� ������Ʈ�� �����
        }
    }

    void ShowChoiceUI()
    {
        choiceUI.SetActive(true); // ���� UI Ȱ��ȭ
        affectionText.text = $"ȣ����: {GetComponent<NpcPersuade>().affection}"; // ȣ���� �ؽ�Ʈ ������Ʈ
        dialogueText.text = ""; // ��� �ؽ�Ʈ �ʱ�ȭ
    }

    // �ϻ����� ù ��° ������ Ŭ�� �� ȣ��Ǵ� �Լ�
    public void OnChoice1ButtonClick()
    {
        choice1Button.gameObject.SetActive(false);
        choice2Button.gameObject.SetActive(false);
        npcDialogue.SetDialogue("��! ��� �˾���~? ���� ��մ� ���� �������� ���ھ��~ ����� �� ��վ� ���̱� �ϳ׿�!");
    }

    // �ϻ����� �� ��° ������ Ŭ�� �� ȣ��Ǵ� �Լ�
    public void OnChoice2ButtonClick()
    {
        choice1Button.gameObject.SetActive(false);
        choice2Button.gameObject.SetActive(false);
        npcDialogue.SetDialogue("������~ (����ؼ� �뷡�� ���Ÿ���)");
    }

    public void OnTalkButtonClick()
    {
        choiceUI.SetActive(false); // ���� UI �����
        dialogueUI.SetActive(true); // ��ȭ UI Ȱ��ȭ
        isTalking = true; // ��ȭ ���� ����
        if (npcType == NpcType.Assassin)
        {
            npcDialogue.StartAssassinDialogue(); // �ϻ��� ��� ����
        }
        else
        {
            npcDialogue.StartDialogue(npcDialogues[npcType], npcName, npcImage); // �Ϲ� ��ȭ ����
        }
    }

    public void OnPersuadeButtonClick()
    {
        choiceUI.SetActive(false); // ���� UI �����
        GetComponent<NpcPersuade>().ShowPersuadeUI(); // ���� UI ǥ��
    }

    public void OnGiftButtonClick()
    {
        // NPC�� ������ ���� ���� ���� ǥ��
        string preferredGift = npcDialogues[npcType][1];
        dialogueText.text = $"�� NPC�� {preferredGift}";
        SceneManager.LoadScene("InventoryMain"); // InventoryMain ������ �̵�
    }

    public void HidePersuadeAndGiftButtons()
    {
        persuadeButton.gameObject.SetActive(false); // �����ϱ� ��ư �����
        giftButton.gameObject.SetActive(false); // �����ϱ� ��ư �����
    }

    public void EndDialogue()
    {
        isTalking = false; // ��ȭ ���� ����
        dialogueUI.SetActive(false); // ��ȭ UI ��Ȱ��ȭ
        choiceUI.SetActive(false); // ���� UI�� �ڽ� ������Ʈ�� ��Ȱ��ȭ
    }
}
