using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro ���ӽ����̽� �߰�

// ProDialogue Ŭ���� ���� (���ѷα� ��� ����)
public class ProDialogue
{
    public int id; // ��ȣ
    public string name; // �ι�
    public string line; // ���
    public string image; // �̹���

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
    // ������ ������ ����Ʈ
    private List<ProDialogue> proDialogue;

    public GameObject opening;
    public TextMeshProUGUI openingText; // TextMeshPro UI �ؽ�Ʈ ���

    public GameObject narration;
    public TextMeshProUGUI narrationText; // TextMeshPro UI �ؽ�Ʈ ���

    public GameObject dialogue;
    public GameObject imageObj; // �ʻ�ȭ �̹��� ���
    public GameObject nameObj; // �̸� ���
    public TextMeshProUGUI nameText; // TextMeshPro UI �ؽ�Ʈ ���
    public TextMeshProUGUI descriptionText; // TextMeshPro UI �ؽ�Ʈ ���

    public GameObject home; // �� ��� ȭ��

    private int currentDialogueIndex = 0; // ���� ��� �ε���
    private bool isActivated = false; // TalkManager�� Ȱ��ȭ�Ǿ����� ����

    void Awake()
    {
        proDialogue = new List<ProDialogue>();
        LoadDialogueFromCSV(); // CSV���� �����͸� �ε��ϴ� �Լ� ȣ��
    }

    void Start()
    {
        ActivateTalk(); // ������Ʈ Ȱ��ȭ
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
            return; // ��� ����Ʈ�� ����� ������Ʈ ��Ȱ��ȭ �� ����
        }

        ProDialogue currentDialogue = proDialogue[index];

        if (index < 2)
        {
            narration.SetActive(false);
            dialogue.SetActive(false);
            opening.SetActive(true);
            openingText.text = currentDialogue.line;
        }
        //������ ��� ���ĺ��� �ι��� ���� ���/�����̼�/�ؽ�Ʈ â Ȱ��ȭ
        else if (currentDialogue.name == "�����̼�")
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
