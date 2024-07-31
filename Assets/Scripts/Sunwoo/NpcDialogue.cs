using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NpcDialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // ��� �ؽ�Ʈ UI ����
    public TextMeshProUGUI npcNameText; // NPC �̸� �ؽ�Ʈ UI ����
    public Image npcImageView; // NPC �̹��� UI ����
    private Queue<string> dialogueQueue; // ��� ť
    private NpcScript npcScript; // NpcScript ����

    void Start()
    {
        dialogueQueue = new Queue<string>();
        npcScript = GetComponent<NpcScript>();

        // NPC �̸��� �̹����� �ʱ�ȭ
        npcNameText.text = "";
        npcImageView.sprite = null;
    }

    // ��ȭ�� �����ϰ� ���, �̸�, �̹����� �޾ƿ��� �Լ�
    public void StartDialogue(string[] dialogues, string npcName, Sprite npcImage)
    {
        dialogueQueue.Clear();
        foreach (string dialogue in dialogues)
        {
            dialogueQueue.Enqueue(dialogue);
        }

        // ��ȭ�� ���۵Ǹ鼭 NPC �̸��� �̹��� ����
        npcNameText.text = npcName; // NPC �̸� ǥ��
        npcImageView.sprite = npcImage; // NPC �̹��� ǥ��

        DisplayNextSentence();
    }

    // �ϻ��� ��ȭ�� �����ϴ� �Լ�
    public void StartAssassinDialogue()
    {
        dialogueQueue.Clear();
        dialogueQueue.Enqueue("������~");
        DisplayNextSentence();
        npcScript.choice1Button.gameObject.SetActive(true);
        npcScript.choice2Button.gameObject.SetActive(true);
        npcScript.choice1Button.GetComponentInChildren<TextMeshProUGUI>().text = "�ɽ��ؿ�?";
        npcScript.choice2Button.GetComponentInChildren<TextMeshProUGUI>().text = "(������ ���Ѻ���)";
    }

    // ���� ��縦 ǥ���ϴ� �Լ�
    public void DisplayNextSentence()
    {
        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = dialogueQueue.Dequeue();
        dialogueText.text = sentence;
    }

    // ��ȭ�� �����ϴ� �Լ�
    private void EndDialogue()
    {
        npcScript.EndDialogue(); // NpcScript�� EndDialogue �Լ� ȣ��
    }

    void Update()
    {
        if (npcScript.isTalking && Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextSentence();
        }
    }

    // Ư�� ��縦 �����ϴ� �Լ�
    public void SetDialogue(string dialogue)
    {
        dialogueQueue.Clear();
        dialogueQueue.Enqueue(dialogue);
        DisplayNextSentence();
    }
}
