using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

// ��ȭ �����͸� ������ Ŭ���� ����
public class DialogueEntry
{
    public int id; // ��ȭ ID
    public string name; // ��ȭ�ϴ� ĳ���� �̸�
    public string dialog; // ��ȭ ����
    public int optional; // ������ ���� (0: �Ϲ� ��ȭ, 1: ������, -1: Ư�� ID�� �̵�)
    public string playerDialog; // �÷��̾� ������ �ؽ�Ʈ
    public string effect; // ȿ�� (������� ����)
    public int num; // ��Ÿ ��ȣ (������� ����)
    public int next; // ���� ��ȭ ID (optional�� -1�� �� ���)

    // �⺻ ������
    public DialogueEntry(int id, string name, string dialog, int optional, string playerDialog, string effect, int num, int next)
    {
        this.id = id;
        this.name = name;
        this.dialog = dialog;
        this.optional = optional;
        this.playerDialog = playerDialog;
        this.effect = effect;
        this.num = num;
        this.next = next;
    }
}

public class DialogueManager : MonoBehaviour
{
    // ��ȭ �����͸� ������ ����Ʈ
    private List<DialogueEntry> dialogueEntry;
    private int currentDialogueIndex = 0; // ���� ��� �ε���
    private bool isActivated = false; // DialogueManager�� Ȱ��ȭ�Ǿ����� ����

    public GameObject dialogue;
    public GameObject nameObj; // �̸� ���
    public TextMeshProUGUI nameText; // TextMeshPro UI �ؽ�Ʈ ���
    public TextMeshProUGUI descriptionText; // TextMeshPro UI �ؽ�Ʈ ���

    public Button choice1Button;
    public Button choice2Button;
    public GameObject choicePanel;
    public GameObject dialoguePanel;

    void Awake()
    {
        dialogueEntry = new List<DialogueEntry>();
        LoadDialogueFromCSV(); // CSV���� �����͸� �ε��ϴ� �Լ� ȣ��
    }

    // �ʱ�ȭ
    void Start()
    {
        ActivateTalk(); // ������Ʈ Ȱ��ȭ
    }

    void Update()
    {
        if (isActivated && Input.GetKeyDown(KeyCode.Space))
        {
            currentDialogueIndex++;
            PrintDialogueEntry(currentDialogueIndex);
        }
    }

    void LoadDialogueFromCSV()
    {
        List<Dictionary<string, object>> data_Dialog = CSVReader.Read("Dialogue_Ria");

        foreach (var row in data_Dialog)
        {
            int id = int.Parse(row["id"].ToString().Trim());
            string name = row["name"].ToString();
            string dialog = row["dialogue"].ToString();
            int optional = int.Parse(row["optional"].ToString().Trim());
            string playerDialog = row["playerDialog"].ToString();
            string effect = row["effect"].ToString();
            int num = int.Parse(row["num"].ToString().Trim());
            int next = int.Parse(row["next"].ToString().Trim());

            dialogueEntry.Add(new DialogueEntry(id, name, dialog, optional, playerDialog, effect, num, next));
        }
    }

    void PrintDialogueEntry(int index)
    {
        if (index >= dialogueEntry.Count)
        {
            dialogue.SetActive(false);
            return; // ��� ����Ʈ�� ����� ������Ʈ ��Ȱ��ȭ �� ����
        }

        DialogueEntry currentDialogue = dialogueEntry[index];
        nameText.text = currentDialogue.name;
        descriptionText.text = currentDialogue.dialog;

        if (currentDialogue.optional == 1)
        {
            // �������� �ִ� ���
            choicePanel.SetActive(true);
            dialoguePanel.SetActive(false);
            choice1Button.GetComponentInChildren<TextMeshProUGUI>().text = currentDialogue.playerDialog;
            choice2Button.GetComponentInChildren<TextMeshProUGUI>().text = dialogueEntry[currentDialogue.next].playerDialog;

            choice1Button.onClick.RemoveAllListeners();
            choice1Button.onClick.AddListener(() => ChooseOption(currentDialogue.id));
            choice2Button.onClick.RemoveAllListeners();
            choice2Button.onClick.AddListener(() => ChooseOption(currentDialogue.next));
        }
        else
        {
            // �Ϲ� ����� ���
            choicePanel.SetActive(false);
            dialoguePanel.SetActive(true);
        }
    }

    void ChooseOption(int nextId)
    {
        currentDialogueIndex = dialogueEntry.FindIndex(dialogue => dialogue.id == nextId);
        PrintDialogueEntry(currentDialogueIndex);
    }

    public void ActivateTalk()
    {
        this.gameObject.SetActive(true);
        isActivated = true;
        currentDialogueIndex = 0;
        PrintDialogueEntry(currentDialogueIndex);
    }

    public void DeactivateTalk()
    {
        this.gameObject.SetActive(false);
        isActivated = false;
    }

    public void StartDialogue()
    {
        ActivateTalk();
    }
}
