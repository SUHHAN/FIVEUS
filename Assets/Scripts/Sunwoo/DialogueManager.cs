using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueEntry
{
    public int id; // 대화 ID
    public string name; // 대화하는 캐릭터 이름
    public string dialog; // 대화 내용
    public int optional; // 선택지 여부 (0: 일반 대화, 1: 선택지, 2: 특정 ID로 이동)
    public string playerDialog; // 플레이어 선택지 텍스트
    public string effect; // 효과 (p: 호감도 증가, m: 호감도 감소)
    public int num; // 호감도 변화량
    public int next; // 다음 대화 ID (optional이 2일 때 사용)

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
    private int currentDialogueIndex = 0; // 현재 대사 인덱스
    private bool isActivated = false; // DialogueManager가 활성화되었는지 여부
    private NpcScript npcScript; // NpcScript 참조
    public GameObject dialoguePanel;
    public TextMeshProUGUI nameText; // 캐릭터 이름 텍스트
    public TextMeshProUGUI descriptionText; // 대사 텍스트

    void Awake()
    {
        dialogueEntry = new List<DialogueEntry>();
        npcScript = GetComponent<NpcScript>(); // NpcScript 참조 얻기
        LoadDialogueFromCSV(); // CSV에서 데이터를 로드하는 함수 호출
    }

    void Start()
    {
        ActivateTalk(); // 오브젝트 활성화
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
            EndDialogue(); // 대화 종료
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
        npcScript.HideChoices(); // 선택지 숨기기
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
        npcScript.EndDialogue(); // NPC 스크립트에서 대화 종료 처리
    }
}
