using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueEntry
{
    public int id; // ��ȭ ID
    public string name; // ��ȭ�ϴ� ĳ���� �̸�
    public string dialog; // ��ȭ ����
    public int optional; // ������ ���� (0: �Ϲ� ��ȭ, 1: ������, 2: Ư�� ID�� �̵�)
    public string playerDialog; // �÷��̾� ������ �ؽ�Ʈ
    public string effect; // ȿ�� (p: ȣ���� ����, m: ȣ���� ����)
    public int num; // ȣ���� ��ȭ��
    public int next; // ���� ��ȭ ID (optional�� 2�� �� ���)

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
    private List<DialogueEntry> dialogueEntry;
    private int currentDialogueIndex = 0; // ���� ��� �ε���
    private bool isActivated = false; // DialogueManager�� Ȱ��ȭ�Ǿ����� ����
    private NpcScript npcScript; // NpcScript ����
    public GameObject dialoguePanel;
    public TextMeshProUGUI nameText; // ĳ���� �̸� �ؽ�Ʈ
    public TextMeshProUGUI descriptionText; // ��� �ؽ�Ʈ

    void Awake()
    {
        dialogueEntry = new List<DialogueEntry>();
        npcScript = GetComponent<NpcScript>(); // NpcScript ���� ���
        LoadDialogueFromCSV(); // CSV���� �����͸� �ε��ϴ� �Լ� ȣ��
    }

    void Start()
    {
        ActivateTalk(); // ������Ʈ Ȱ��ȭ
    }

    void Update()
    {
        if (isActivated && Input.GetKeyDown(KeyCode.Space))
        {
            ProceedToNextDialogue();
        }
    }

    void LoadDialogueFromCSV()
    {
        List<Dictionary<string, object>> data_Dialog = CSVReader.Read("SwDialogue_Callis");

        foreach (var row in data_Dialog)
        {
            int id = int.Parse(row["id"].ToString().Trim());
            string name = row["name"].ToString();
            string dialog = row["dialog"].ToString();
            int optional = int.Parse(row["optional"].ToString().Trim());
            string playerDialog = row["playerDialog"].ToString();
            string effect = row["effect"].ToString();
            int num = string.IsNullOrEmpty(row["num"].ToString().Trim()) ? 0 : int.Parse(row["num"].ToString().Trim());
            int next = int.Parse(row["next"].ToString().Trim());

            dialogueEntry.Add(new DialogueEntry(id, name, dialog, optional, playerDialog, effect, num, next));
        }
    }

    void PrintDialogueEntry(int index)
    {
        if (index >= dialogueEntry.Count)
        {
            EndDialogue(); // ��ȭ ����
            return;
        }

        DialogueEntry currentDialogue = dialogueEntry[index];
        nameText.text = currentDialogue.name;
        descriptionText.text = currentDialogue.dialog;

        ApplyEffect(currentDialogue.effect, currentDialogue.num);

        if (currentDialogue.optional == 1)
        {
            string choice1Text = currentDialogue.playerDialog;
            string choice2Text = dialogueEntry[index + 1].playerDialog;
            npcScript.ShowChoices(choice1Text, choice2Text, () => ChooseOption(index), () => ChooseOption(index + 1));
        }
        else if (currentDialogue.optional == 2)
        {
            currentDialogueIndex = dialogueEntry.FindIndex(dialogue => dialogue.id == currentDialogue.next);
            PrintDialogueEntry(currentDialogueIndex);
        }
    }

    void ApplyEffect(string effect, int num)
    {
        if (effect == "p")
        {
            npcScript.ChangeAffection(num);
        }
        else if (effect == "m")
        {
            npcScript.ChangeAffection(-num);
        }
    }

    void ChooseOption(int index)
    {
        npcScript.HideChoices(); // ������ �����
        currentDialogueIndex = dialogueEntry.FindIndex(dialogue => dialogue.id == dialogueEntry[index].next);
        PrintDialogueEntry(currentDialogueIndex);
    }

    void ProceedToNextDialogue()
    {
        currentDialogueIndex++;
        if (currentDialogueIndex >= dialogueEntry.Count || dialogueEntry[currentDialogueIndex].optional != 0)
        {
            currentDialogueIndex--;
        }
        else
        {
            PrintDialogueEntry(currentDialogueIndex);
        }
    }

    public void ActivateTalk()
    {
        this.gameObject.SetActive(true);
        isActivated = true;
        currentDialogueIndex = 0;
        PrintDialogueEntry(currentDialogueIndex);
    }

    public void EndDialogue()
    {
        isActivated = false;
        dialoguePanel.SetActive(false);
        npcScript.EndDialogue(); // NPC ��ũ��Ʈ���� ��ȭ ���� ó��
    }
}
