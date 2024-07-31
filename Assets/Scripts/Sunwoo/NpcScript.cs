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
    public Button choice1Button; // ������ 1 ��ư
    public Button choice2Button; // ������ 2 ��ư
    public float interactionRange = 3.0f; // ��ȣ�ۿ� �Ÿ�
    private GameObject player; // �÷��̾� ������Ʈ
    private bool isTalking = false; // ��ȭ ������ ����
    public string npcType;
    public int affection = 0; // NPC ȣ����

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // �±װ� "Player"�� ������Ʈ ã��
        choiceUI.SetActive(false); // ������ �� ���� UI ��Ȱ��ȭ
        dialogueUI.SetActive(false); // ������ �� ��ȭ UI ��Ȱ��ȭ
        talkButton.onClick.AddListener(OnTalkButtonClick); // ��ȭ�ϱ� ��ư Ŭ�� �̺�Ʈ ����
        persuadeButton.onClick.AddListener(OnPersuadeButtonClick); // �����ϱ� ��ư Ŭ�� �̺�Ʈ ����
        giftButton.onClick.AddListener(() => OnGiftButtonClick(npcType)); // �����ϱ� ��ư Ŭ�� �̺�Ʈ ����
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
            choiceUI.SetActive(false); // ���� UI�� �ڽ� ������Ʈ�� �����
        }
    }

    void ShowChoiceUI()
    {
        choiceUI.SetActive(true); // ���� UI Ȱ��ȭ
        affectionText.text = $"ȣ����: {affection}"; // ȣ���� �ؽ�Ʈ ������Ʈ
        dialogueText.text = ""; // ��� �ؽ�Ʈ �ʱ�ȭ
    }

    public void OnTalkButtonClick()
    {
        choiceUI.SetActive(false); // ���� UI �����
        dialogueUI.SetActive(true); // ��ȭ UI Ȱ��ȭ
        GetComponent<DialogueManager>().ActivateTalk(); // ��ȭ ����
        isTalking = true; // ��ȭ ���� ����
    }

    public void OnPersuadeButtonClick()
    {
        choiceUI.SetActive(false); // ���� UI �����
        GetComponent<NpcPersuade>().ShowPersuadeUI(); // ���� UI ǥ��
    }

    public void OnGiftButtonClick(string npc)
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
        choiceUI.SetActive(false); // ���� UI�� �ڽ� ������Ʈ�� ��Ȱ��ȭ
    }

    public void UpdatePosition(string timeOfDay)
    {
        Vector3 newPosition = Vector3.zero;

        switch (npcType)
        {
            case "�˻�":
            case "����":
                if (timeOfDay == "Morning" || timeOfDay == "Afternoon")
                {
                    SceneManager.LoadScene("main_map");
                    newPosition = new Vector3(10, 0, 20); // main_map �� ��ġ ����
                }
                else
                {
                    SceneManager.LoadScene("big_house");
                    newPosition = new Vector3(5, 0, 10); // big_house �� ��ġ ����
                }
                break;
            case "��Ŀ":
                if (timeOfDay == "Morning" || timeOfDay == "Afternoon")
                {
                    SceneManager.LoadScene("training");
                    newPosition = new Vector3(15, 0, 5); // training �� ��ġ ����
                }
                else
                {
                    SceneManager.LoadScene("big_house");
                    newPosition = new Vector3(5, 0, 10); // big_house �� ��ġ ����
                }
                break;
            case "������":
                SceneManager.LoadScene("sub2_house");
                newPosition = new Vector3(3, 0, 8); // sub2_house �� ��ġ ����
                break;
            case "�ϻ���":
                if (timeOfDay == "Evening")
                {
                    SceneManager.LoadScene("bar");
                    newPosition = new Vector3(2, 0, 6); // bar �� ��ġ ����
                }
                break;
            case "�ü�":
                if (timeOfDay == "Evening")
                {
                    SceneManager.LoadScene("training");
                    newPosition = new Vector3(7, 0, 3); // training �� ��ġ ����
                }
                break;
        }

        transform.position = newPosition; // NPC ��ġ ����
    }

    // NPC ȣ���� ����
    public void ChangeAffection(int amount)
    {
        affection += amount;
        affectionText.text = $"ȣ����: {affection}";
    }

    // ������ UI ��ư Ȱ��ȭ
    public void ShowChoices(string choice1Text, string choice2Text, UnityEngine.Events.UnityAction choice1Action, UnityEngine.Events.UnityAction choice2Action)
    {
        choiceUI.SetActive(true);
        choice1Button.gameObject.SetActive(true);
        choice2Button.gameObject.SetActive(true);
        choice1Button.GetComponentInChildren<TextMeshProUGUI>().text = choice1Text;
        choice2Button.GetComponentInChildren<TextMeshProUGUI>().text = choice2Text;
        choice1Button.onClick.RemoveAllListeners();
        choice1Button.onClick.AddListener(choice1Action);
        choice2Button.onClick.RemoveAllListeners();
        choice2Button.onClick.AddListener(choice2Action);
    }

    // ������ UI ��ư ��Ȱ��ȭ
    public void HideChoices()
    {
        choice1Button.gameObject.SetActive(false);
        choice2Button.gameObject.SetActive(false);
    }
}
