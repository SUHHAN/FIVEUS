using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro ���ӽ����̽� �߰�

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
    private NpcPersuade npcPersuade; // NpcPersuade ��ũ��Ʈ ����

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // �±װ� "Player"�� ������Ʈ ã��
        npcPersuade = GetComponent<NpcPersuade>(); // NpcPersuade ��ũ��Ʈ ���� ���
        choiceUI.SetActive(false); // ������ �� ���� UI ��Ȱ��ȭ
        dialogueUI.SetActive(false); // ������ �� ��ȭ UI ��Ȱ��ȭ
        talkButton.onClick.AddListener(OnTalkButtonClick); // ��ȭ�ϱ� ��ư Ŭ�� �̺�Ʈ ����
        persuadeButton.onClick.AddListener(OnPersuadeButtonClick); // �����ϱ� ��ư Ŭ�� �̺�Ʈ ����
        giftButton.onClick.AddListener(OnGiftButtonClick); // �����ϱ� ��ư Ŭ�� �̺�Ʈ ����
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
        npcPersuade.ShowPersuadeUI(); // ���� UI ǥ��
    }

    public void OnGiftButtonClick()
    {
        // �����ϱ� ��� ����
    }

    public void HidePersuadeAndGiftButtons()
    {
        persuadeButton.gameObject.SetActive(false); // �����ϱ� ��ư �����
        giftButton.gameObject.SetActive(false); // �����ϱ� ��ư �����
    }

    void EndDialogue()
    {
        dialogueUI.SetActive(false); // ��ȭ UI �����
        isTalking = false; // ��ȭ ���� ����
    }
}
