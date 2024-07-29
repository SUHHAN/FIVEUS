using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

// 대화 데이터를 저장할 클래스 정의
public class DialogueEntry
{
    public int id; // 대화 ID
    public string name; // 대화하는 캐릭터 이름
    public string dialog; // 대화 내용
    public int optional; // 선택지 여부 (0: 일반 대화, 1: 선택지, -1: 특정 ID로 이동)
    public string playerDialog; // 플레이어 선택지 텍스트
    public string effect; // 효과 (사용하지 않음)
    public int num; // 기타 번호 (사용하지 않음)
    public int next; // 다음 대화 ID (optional이 -1일 때 사용)

    // 기본 생성자
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
    // 대화 데이터를 저장할 리스트
    private List<DialogueEntry> dialogueEntry;
    private int currentDialogueIndex = 0; // 현재 대사 인덱스
    private bool isActivated = false; // DialogueManager가 활성화되었는지 여부

    public GameObject dialogue;
    public GameObject nameObj; // 이름 요소
    public TextMeshProUGUI nameText; // TextMeshPro UI 텍스트 요소
    public TextMeshProUGUI descriptionText; // TextMeshPro UI 텍스트 요소

    public Button choice1Button;
    public Button choice2Button;
    public GameObject choicePanel;
    public GameObject dialoguePanel;

    void Awake()
    {
        dialogueEntry = new List<DialogueEntry>();
        LoadDialogueFromCSV(); // CSV에서 데이터를 로드하는 함수 호출
    }

    // 초기화
    void Start()
    {
        ActivateTalk(); // 오브젝트 활성화
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
            return; // 대사 리스트를 벗어나면 오브젝트 비활성화 후 리턴
        }

        DialogueEntry currentDialogue = dialogueEntry[index];
        nameText.text = currentDialogue.name;
        descriptionText.text = currentDialogue.dialog;

        if (currentDialogue.optional == 1)
        {
            // 선택지가 있는 경우
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
            // 일반 대사인 경우
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
