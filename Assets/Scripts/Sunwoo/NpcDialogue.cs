using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NpcDialogue : MonoBehaviour
{
    public GameObject dialogueUI; // 대화 UI 패널
    public GameObject choiceUI; // 선택지 UI 패널
    public TextMeshProUGUI dialogueText; // 대사 텍스트 UI 연결
    public TextMeshProUGUI nameText; // 이름 텍스트 UI 연결
    public Button choice1Button; // 선택지 1 버튼 연결
    public Button choice2Button; // 선택지 2 버튼 연결
    public TextAsset dialogueCSV; // CSV 파일(TextAsset) 연결

    private List<DialogueEntry> dialogues;
    private int currentDialogueIndex = 0;
    private NpcScript npcScript; // NpcScript 스크립트 참조

    void Start()
    {
        dialogueUI.SetActive(false); // 대화 UI 비활성화
        choiceUI.SetActive(false); // 선택지 UI 비활성화
        choice1Button.onClick.AddListener(OnChoice1ButtonClick); // 선택지 1 버튼 클릭 이벤트 연결
        choice2Button.onClick.AddListener(OnChoice2ButtonClick); // 선택지 2 버튼 클릭 이벤트 연결

        npcScript = GetComponent<NpcScript>(); // NpcScript 스크립트 참조 얻기
        LoadDialogueFromCSV(); // CSV 파일 로드
    }

    void Update()
    {
        if (dialogueUI.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            currentDialogueIndex++;
            ShowNextDialogue(); // 엔터 키 입력 시 다음 대사 표시
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
        dialogueUI.SetActive(true); // 대화 UI 활성화
        ShowNextDialogue();
    }

    void ShowNextDialogue()
    {
        if (currentDialogueIndex >= dialogues.Count)
        {
            EndDialogue(); // 대사 리스트를 벗어나면 대화 종료
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
                currentDialogueIndex = currentEntry.next - 1; // next가 0이 아닌 경우 해당 id로 이동
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

        currentDialogueIndex = choiceEntry.next - 1; // next id로 이동
        ShowNextDialogue();
    }

    void EndDialogue()
    {
        dialogueUI.SetActive(false);
        choiceUI.SetActive(false);
        npcScript.EndDialogue(); // 대화 종료 알림
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
