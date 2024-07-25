using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro ���ӽ����̽� �߰�

public class NpcScript : MonoBehaviour
{
    public GameObject choiceUI; // ���� UI �г�
    public GameObject dialogueUI; // ��ȭ UI �г�
    public GameObject persuadeUI; // ���� UI �г�
    public GameObject resultUI; // ��� UI �г�
    public TextMeshProUGUI dialogueText; // ��� �ؽ�Ʈ UI ����
    public TextMeshProUGUI persuadeText; // ���� �ؽ�Ʈ UI ����
    public TextMeshProUGUI resultText; // ��� �ؽ�Ʈ UI ����
    public Button talkButton; // ��ȭ�ϱ� ��ư ����
    public Button persuadeButton; // �����ϱ� ��ư ����
    public Button yesButton; // �� ��ư ����
    public Button noButton; // �ƴϿ� ��ư ����
    public Button attemptPersuasionButton; // ���� �õ� ��ư ����
    public float interactionRange = 3.0f; // ��ȣ�ۿ� �Ÿ�
    private GameObject player; // �÷��̾� ������Ʈ
    private bool isTalking = false; // ��ȭ ������ ����
    public int affection = 50; // ȣ����
    private int remainingAttempts = 3; // ���� ���� �õ� Ƚ��

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // �±װ� "Player"�� ������Ʈ ã��
        choiceUI.SetActive(false); // ������ �� ���� UI ��Ȱ��ȭ
        dialogueUI.SetActive(false); // ������ �� ��ȭ UI ��Ȱ��ȭ
        persuadeUI.SetActive(false); // ������ �� ���� UI ��Ȱ��ȭ
        resultUI.SetActive(false); // ������ �� ��� UI ��Ȱ��ȭ
        talkButton.onClick.AddListener(OnTalkButtonClick); // ��ȭ�ϱ� ��ư Ŭ�� �̺�Ʈ ����
        persuadeButton.onClick.AddListener(OnPersuadeButtonClick); // �����ϱ� ��ư Ŭ�� �̺�Ʈ ����
        yesButton.onClick.AddListener(OnYesButtonClick); // �� ��ư Ŭ�� �̺�Ʈ ����
        noButton.onClick.AddListener(OnNoButtonClick); // �ƴϿ� ��ư Ŭ�� �̺�Ʈ ����
    }

    void Update()
    {
        if (isTalking)
        {
            if (Input.GetKeyDown(KeyCode.Return)) // ���� Ű �Է� ����
            {
                EndDialogue(); // ��ȭ ����
            }
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
        dialogueText.text = "�ȳ�, �ݰ���!"; // ��� ǥ��
        isTalking = true; // ��ȭ ���� ����
    }

    public void OnPersuadeButtonClick()
    {
        choiceUI.SetActive(false); // ���� UI �����
        Persuade(); // ���� UI ǥ��
    }

    public void Persuade()
    {
        HideChoices();
        persuadeUI.SetActive(true); // ���� UI ǥ��
        persuadeText.text = $"�����Ͻðڽ��ϱ�? ���� Ƚ��: {remainingAttempts}";
    }

    public void OnYesButtonClick()
    {
        AttemptPersuasion(); // ���� �õ�
    }

    public void OnNoButtonClick()
    {
        persuadeUI.SetActive(false); // ���� UI �����
    }

    public void AttemptPersuasion()
    {
        if (remainingAttempts > 0)
        {
            remainingAttempts--;
            int successChance = affection; // ȣ������ ����� ���� Ȯ��
            int randomValue = Random.Range(0, 100); // 0���� 100 ������ ���� �� ����
            if (randomValue < successChance)
            {
                // ���� ����
                resultText.text = "�����߽��ϴ�!";
            }
            else
            {
                // ���� ����
                resultText.text = $"�����߽��ϴ�! ���� Ƚ��: {remainingAttempts}";
            }
            persuadeUI.SetActive(false); // ���� UI �����
            resultUI.SetActive(true); // ��� UI ǥ��
        }
        else
        {
            resultText.text = "���� �õ� ��ȸ�� ��� ����߽��ϴ�.";
            persuadeUI.SetActive(false); // ���� UI �����
            resultUI.SetActive(true); // ��� UI ǥ��
        }
    }

    void HideChoices()
    {
        choiceUI.SetActive(false); // ���� UI �����
        dialogueUI.SetActive(false); // ��ȭ UI �����
        persuadeUI.SetActive(false); // ���� UI �����
    }

    void EndDialogue()
    {
        dialogueUI.SetActive(false); // ��ȭ UI �����
        isTalking = false; // ��ȭ ���� ����
    }
}
