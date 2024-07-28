using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NpcDialogue : MonoBehaviour
{
    public GameObject dialogueUI; // ��ȭ UI �г�
    public GameObject choiceUI; // ������ UI �г�
    public TextMeshProUGUI dialogueText; // ��� �ؽ�Ʈ UI ����
    public TextMeshProUGUI nameText; // �̸� �ؽ�Ʈ UI ����
    public Button choice1Button; // ������ 1 ��ư ����
    public Button choice2Button; // ������ 2 ��ư ����
    public TextAsset dialogueCSV; // CSV ����(TextAsset) ����

    private List<DialogueEntry> dialogues;
    private int currentDialogueIndex = 0;
    private NpcScript npcScript; // NpcScript ��ũ��Ʈ ����

    void Start()
    {
        dialogueUI.SetActive(false); // ��ȭ UI ��Ȱ��ȭ
        choiceUI.SetActive(false); // ������ UI ��Ȱ��ȭ
        choice1Button.onClick.AddListener(OnChoice1ButtonClick); // ������ 1 ��ư Ŭ�� �̺�Ʈ ����
        choice2Button.onClick.AddListener(OnChoice2ButtonClick); // ������ 2 ��ư Ŭ�� �̺�Ʈ ����

        npcScript = GetComponent<NpcScript>(); // NpcScript ��ũ��Ʈ ���� ���
        LoadDialogueFromCSV(); // CSV ���� �ε�
    }

    void Update()
    {
        if (dialogueUI.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            currentDialogueIndex++;
            ShowNextDialogue(); // ���� Ű �Է� �� ���� ��� ǥ��
        }
    }

    void LoadDialogueFromCSV()
    {
        dialogues = new List<DialogueEntry>();
        List<Dictionary<string, object>> data_Dialog = CSVReader.Read(dialogueCSV.name);

        foreach (var row in data_Dialog)
        {
            try
            {
                int id = int.Parse(row["id"].ToString().Trim());
                string name = row["name"].ToString().Trim();
                string dialog = row["dialog"].ToString().Trim();
                int optional = int.Parse(row["optional"].ToString().Trim());
                string playerDialog = row["playerDialog"].ToString().Trim();
                string effect = row["effect"].ToString().Trim();
                int num = int.Parse(row["num"].ToString().Trim());
                int next = int.Parse(row["next"].ToString().Trim());

                DialogueEntry entry = new DialogueEntry(id, name, dialog, optional, playerDialog, effect, num, next);
                dialogues.Add(entry);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error parsing row: {row}");
                Debug.LogError(e.Message);
            }
        }
    }

    public void StartDialogue()
    {
        currentDialogueIndex = 0;
        dialogueUI.SetActive(true); // ��ȭ UI Ȱ��ȭ
        ShowNextDialogue();
    }

    void ShowNextDialogue()
    {
        if (currentDialogueIndex >= dialogues.Count)
        {
            EndDialogue(); // ��� ����Ʈ�� ����� ��ȭ ����
            return;
        }

        DialogueEntry currentEntry = dialogues[currentDialogueIndex];

        if (currentEntry.optional == 1)
        {
            choiceUI.SetActive(true);
            dialogueUI.SetActive(false);
            choice1Button.GetComponentInChildren<TextMeshProUGUI>().text = dialogues[currentDialogueIndex + 1].playerDialog;
            choice2Button.GetComponentInChildren<TextMeshProUGUI>().text = dialogues[currentDialogueIndex + 2].playerDialog;
        }
        else
        {
            choiceUI.SetActive(false);
            dialogueUI.SetActive(true);
            nameText.text = currentEntry.name;
            dialogueText.text = currentEntry.dialog;

            if (currentEntry.optional == -1)
            {
                currentDialogueIndex = currentEntry.next - 1; // next�� 0�� �ƴ� ��� �ش� id�� �̵�
                if (currentEntry.next == 0)
                {
                    EndDialogue();
                }
            }
        }
    }

    void OnChoice1ButtonClick()
    {
        HandleChoice(1);
    }

    void OnChoice2ButtonClick()
    {
        HandleChoice(2);
    }

    void HandleChoice(int choiceIndex)
    {
        currentDialogueIndex += choiceIndex;
        DialogueEntry choiceEntry = dialogues[currentDialogueIndex];

        if (choiceEntry.effect == "P")
        {
            // Positive effect handling
        }
        else if (choiceEntry.effect == "M")
        {
            // Negative effect handling
        }

        currentDialogueIndex = choiceEntry.next - 1; // next id�� �̵�
        ShowNextDialogue();
    }

    void EndDialogue()
    {
        dialogueUI.SetActive(false);
        choiceUI.SetActive(false);
        npcScript.EndDialogue(); // ��ȭ ���� �˸�
    }

    [System.Serializable]
    public class DialogueEntry
    {
        public int id;
        public string name;
        public string dialog;
        public int optional;
        public string playerDialog;
        public string effect;
        public int num;
        public int next;

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
}
