using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public string npcName; // NPC �̸�
    public int affection = 0; // NPC ȣ����
    public string[] dialogues; // ȣ������ ���� ��� �迭
    public GameObject dialogueUI; // ��ȭ UI
    public GameObject choiceUI; // ������ UI
    public GameObject giftUI; // ���� UI
    public GameObject persuadeUI; // ���� UI
    public Text dialogueText; // ��ȭ �ؽ�Ʈ

    private bool isNearPlayer = false; // �÷��̾ ������ �ִ��� ����
    private ToastManager toastManager; // �佺Ʈ �Ŵ��� �ν��Ͻ�

    void Start()
    {
        toastManager = FindObjectOfType<ToastManager>(); // ToastManager �ν��Ͻ� ã��
    }

    void Update()
    {
        // �÷��̾ ������ �ְ� E Ű�� ������ �� ������ ǥ��
        if (isNearPlayer && Input.GetKeyDown(KeyCode.E))
        {
            ShowChoices();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // �÷��̾ Ʈ���� ������ ������ ��
        if (other.CompareTag("Player"))
        {
            isNearPlayer = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // �÷��̾ Ʈ���� �������� ������ ��
        if (other.CompareTag("Player"))
        {
            isNearPlayer = false;
            HideChoices();
        }
    }

    public void ShowChoices()
    {
        choiceUI.SetActive(true); // ������ UI ǥ��
    }

    public void HideChoices()
    {
        choiceUI.SetActive(false); // ������ UI �����
    }

    public void Talk()
    {
        HideChoices();
        dialogueUI.SetActive(true); // ��ȭ UI ǥ��
        UpdateDialogue(); // ��ȭ ������Ʈ
    }

    public void UpdateDialogue()
    {
        string dialogue = GetDialogueBasedOnAffection(); // ȣ������ ���� ��� ��������
        dialogueText.text = dialogue; // ��� �ؽ�Ʈ ����
        if (affection == 100)
        {
            choiceUI.SetActive(false); // ȣ������ 100�̸� ������ UI �����
        }
        else
        {
            ShowChoices(); // �׷��� ������ ������ UI ǥ��
        }
        EndDialogue();
    }

    public void EndDialogue()
    {
        // ��ȭ�� ���� �� ȣ���� ��ȭ �޽��� ǥ��
        string affectionChangeMessage = "ȣ������ " + affection + "��ŭ �Ǿ����ϴ�.";
        toastManager.ShowToast(affectionChangeMessage);
    }

    public string GetDialogueBasedOnAffection()
    {
        // ȣ������ ���� ��� ��ȯ
        if (affection >= 100) return "�ʰ� �ʹ� ����!";
        else if (affection >= 90) return dialogues[6];
        else if (affection >= 80) return dialogues[5];
        else if (affection >= 70) return dialogues[4];
        else if (affection >= 60) return dialogues[3];
        else if (affection >= 50) return dialogues[2];
        else return dialogues[1];
    }

    public void Gift()
    {
        HideChoices();
        giftUI.SetActive(true); // ���� UI ǥ��
    }

    public void ReceiveGift(string giftName)
    {
        int affectionChange = GetGiftAffectionChange(giftName); // ������ ���� ȣ���� ��ȭ ��������
        if (affectionChange > 0)
        {
            affection += affectionChange; // ȣ���� ����
            toastManager.ShowToast("ȣ������ " + affectionChange + "��ŭ �����߽��ϴ�."); // ȣ���� ��ȭ �޽��� ǥ��
        }
        giftUI.SetActive(false); // ���� UI �����
    }

    public int GetGiftAffectionChange(string giftName)
    {
        // Ư�� ������ ���� ȣ���� ��ȭ ����
        // ��: if (giftName == "Ư�� ����") return 10;
        return 0; // �⺻������ ȣ���� ��ȭ ����
    }

    public void Persuade()
    {
        HideChoices();
        persuadeUI.SetActive(true); // ���� UI ǥ��
    }

    public void AttemptPersuasion()
    {
        int successChance = affection; // ȣ������ ����� ���� Ȯ��
        int randomValue = Random.Range(0, 100); // 0���� 100 ������ ���� �� ����
        if (randomValue < successChance)
        {
            // ���� ����
            toastManager.ShowToast("���濡 �����߽��ϴ�!");
            // ���� �����ϴ� ���� �߰�
        }
        else
        {
            // ���� ����
            toastManager.ShowToast("���濡 �����߽��ϴ�.");
        }
        persuadeUI.SetActive(false); // ���� UI �����
    }
}
