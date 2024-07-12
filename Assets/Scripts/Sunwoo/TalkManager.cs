using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro 네임스페이스 추가

// ProDialogue 클래스 정의 (프롤로그 대사 저장)
public class ProDialogue
{
    public int id; // 번호
    public string name; // 인물
    public string line; // 대사
    public string image; // 이미지

    public ProDialogue(int id, string name, string line, string image)
    {
        this.id = id;
        this.name = name;
        this.line = line;
        this.image = image;
    }
}

public class TalkManager : MonoBehaviour
{
    // 대사들을 저장할 리스트
    private List<ProDialogue> proDialogue;

    public GameObject opening;
    public TextMeshProUGUI openingText; // TextMeshPro UI 텍스트 요소

    public GameObject narration;
    public TextMeshProUGUI narrationText; // TextMeshPro UI 텍스트 요소

    public GameObject dialogue;
    public GameObject imageObj; // 초상화 이미지 요소
    public GameObject nameObj; // 이름 요소
    public TextMeshProUGUI nameText; // TextMeshPro UI 텍스트 요소
    public TextMeshProUGUI descriptionText; // TextMeshPro UI 텍스트 요소

    public GameObject home; // 집 배경 화면

    private int currentDialogueIndex = 0; // 현재 대사 인덱스
    private bool isActivated = false; // TalkManager가 활성화되었는지 여부

    void Awake()
    {
        proDialogue = new List<ProDialogue>();
        LoadDialogueFromCSV(); // CSV에서 데이터를 로드하는 함수 호출
    }

    void Start()
    {
        ActivateTalk(); // 오브젝트 활성화
    }

    void Update()
    {
        if (isActivated && currentDialogueIndex == 0)
        {
            PrintProDialogue(currentDialogueIndex);
        }
        if (isActivated && Input.GetKeyDown(KeyCode.Space))
        {
            currentDialogueIndex++;
            PrintProDialogue(currentDialogueIndex);
        }
    }

    void LoadDialogueFromCSV()
    {
        List<Dictionary<string, object>> data_Dialog = CSVReader.Read("Tutorial");

        foreach (var row in data_Dialog)
        {
            int id = int.Parse(row["id"].ToString().Trim());
            string name = row["name"].ToString();
            string line = row["dialogue"].ToString();
            string image = row["image"].ToString();

            proDialogue.Add(new ProDialogue(id, name, line, image));
        }
    }

    void PrintProDialogue(int index)
    {
        if (index >= proDialogue.Count)
        {
            narration.SetActive(false);
            dialogue.SetActive(false);
            return; // 대사 리스트를 벗어나면 오브젝트 비활성화 후 리턴
        }

        ProDialogue currentDialogue = proDialogue[index];

        if (index < 2)
        {
            narration.SetActive(false);
            dialogue.SetActive(false);
            opening.SetActive(true);
            openingText.text = currentDialogue.line;
        }
        //오프닝 대사 이후부터 인물에 따라 대사/나레이션/텍스트 창 활성화
        else if (currentDialogue.name == "나레이션")
        {
            narration.SetActive(true);
            dialogue.SetActive(false);
            opening.SetActive(false);
            narrationText.text = currentDialogue.line;
        }
        else
        {
            narration.SetActive(false);
            dialogue.SetActive(true);
            opening.SetActive(false);
            nameText.text = currentDialogue.name;
            descriptionText.text = currentDialogue.line;
        }

        CheckTalk(currentDialogue.id);
    }

    public void ActivateTalk()
    {
        this.gameObject.SetActive(true);
        isActivated = true;
    }

    void DeactivateTalk()
    {
        this.gameObject.SetActive(false);
        isActivated = false;
    }

    void CheckTalk(int id)
    {
        if (id >= 15)
        {
            home.SetActive(true);
        }
        else
        {
            home.SetActive(false);
        }

        if (currentDialogueIndex >= proDialogue.Count)
        {
            DeactivateTalk();
        }
    }
}
