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
    public TextMeshProUGUI dialogueText; // ��� �ؽ�Ʈ UI ����
    public Button talkButton; // ��ȭ�ϱ� ��ư ����
    public Button persuadeButton; // �����ϱ� ��ư ����
    public Button giftButton; // �����ϱ� ��ư ����
    public float interactionRange = 3.0f; // ��ȣ�ۿ� �Ÿ�
    private GameObject player; // �÷��̾� ������Ʈ
    private bool isTalking = false; // ��ȭ ������ ����
    private DialogueManager dialogueManager; // DialogueManager ��ũ��Ʈ ����
    public string npcType;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // �±װ� "Player"�� ������Ʈ ã��
        dialogueManager = GetComponent<DialogueManager>(); // DialogueManager ��ũ��Ʈ ���� ���
        choiceUI.SetActive(false); // ������ �� ���� UI ��Ȱ��ȭ
        dialogueUI.SetActive(false); // ������ �� ��ȭ UI ��Ȱ��ȭ
        talkButton.onClick.AddListener(OnTalkButtonClick); // ��ȭ�ϱ� ��ư Ŭ�� �̺�Ʈ ����
        persuadeButton.onClick.AddListener(OnPersuadeButtonClick); // �����ϱ� ��ư Ŭ�� �̺�Ʈ ����
        giftButton.onClick.AddListener(OnGiftButtonClick); // �����ϱ� ��ư Ŭ�� �̺�Ʈ ����
        UpdatePosition(FindObjectOfType<TimeManager>().GetTimeOfDay()); // �ʱ� ��ġ ����
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
            choiceUI.SetActive(false); // ���� UI �����
        }
    }

    void ShowChoiceUI()
    {
        choiceUI.SetActive(true); // ���� UI Ȱ��ȭ
        dialogueText.text = ""; // ��� �ؽ�Ʈ �ʱ�ȭ
    }

    public void OnTalkButtonClick()
    {
        choiceUI.SetActive(false); // ���� UI �����
        dialogueUI.SetActive(true); // ��ȭ UI Ȱ��ȭ
        dialogueManager.StartDialogue(); // ��ȭ ����
        isTalking = true; // ��ȭ ���� ����
    }

    public void OnPersuadeButtonClick()
    {
        choiceUI.SetActive(false); // ���� UI �����
        GetComponent<NpcPersuade>().ShowPersuadeUI(); // ���� UI ǥ��
    }

    public void OnGiftButtonClick()
    {
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
    }

    public void UpdatePosition(string timeOfDay)
    {
        switch (npcType)
        {
            case "�˻�":
            case "����":
                if (timeOfDay == "Morning" || timeOfDay == "Afternoon")
                {
                    SceneManager.LoadScene("main_map");
                }
                else
                {
                    SceneManager.LoadScene("big_house");
                }
                break;
            case "��Ŀ":
                if (timeOfDay == "Morning" || timeOfDay == "Afternoon")
                {
                    SceneManager.LoadScene("training");
                }
                else
                {
                    SceneManager.LoadScene("big_house");
                }
                break;
            case "������":
                SceneManager.LoadScene("sub2_house");
                break;
            case "�ϻ���":
                if (timeOfDay == "Evening")
                {
                    SceneManager.LoadScene("bar");
                }
                break;
            case "�ü�":
                if (timeOfDay == "Evening")
                {
                    SceneManager.LoadScene("training");
                }
                break;
        }
    }
}