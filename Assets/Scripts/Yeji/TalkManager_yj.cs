using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro ���ӽ����̽� �߰�
using UnityEngine.UI; // UI ��� ����� ���� ���ӽ����̽� �߰�

// ProDialogue Ŭ���� ���� (���ѷα� ��� ����)
public class ProDialogue_yj
{
    public int id; // ��ȣ
    public string name; // �ι�
    public string line; // ���

    public ProDialogue_yj(int id, string name, string line)
    {
        this.id = id;
        this.name = name;
        this.line = line;
    }
}


public class TalkManager_yj : MonoBehaviour
{
    /*   Dictionary<int, string[]> talkData_yj;
       Dictionary<int, Sprite> portraitData_yj;

       public Sprite[] portraitArr_yj;
       // Start is called before the first frame update
       void Awake()
       {
           talkData_yj = new Dictionary<int, string[]>();
           portraitData_yj = new Dictionary<int, Sprite>();
           GenerateData();
       }

       void GenerateData()
       {
           // ��Ʈ ������(������ ���� ����. ��� �� ��������� ��������ڴ�)
           talkData_yj.Add(100, new string[] { "�ܼ���. ������ ����()" });
           // �Ʒô��� ������ ��
           talkData_yj.Add(1000, new string[] { "Hello?:0", "It's your first time here, right?:1" });
           // ������ ������ ��
           talkData_yj.Add(2000, new string[] { "Let's exercise!:1", "Muscle! hustle!:2" });

           // quest talk
           talkData_yj.Add(10 + 1000, new string[] { "Welcome!:0", "Talk to KightsJJang! :1" });
           talkData_yj.Add(11 + 2000, new string[] { "Hey!:0", "Give me THAT coin! :1" });
           talkData_yj.Add(20 + 1000, new string[] { "coin?:0", "I found it! :3" });
           talkData_yj.Add(20 + 5000, new string[] { "I found THAT coin! :1" });
           talkData_yj.Add(21 + 2000, new string[] { "Thanks! :2"});

           portraitData_yj.Add(1000 + 0, portraitArr_yj[0]);
           portraitData_yj.Add(1000 + 1, portraitArr_yj[1]);
           portraitData_yj.Add(1000 + 2, portraitArr_yj[2]);
           portraitData_yj.Add(1000 + 3, portraitArr_yj[3]);

           portraitData_yj.Add(2000 + 0, portraitArr_yj[4]);
           portraitData_yj.Add(2000 + 1, portraitArr_yj[5]);
           portraitData_yj.Add(2000 + 2, portraitArr_yj[6]);
           portraitData_yj.Add(2000 + 3, portraitArr_yj[7]);
       }

       public string GetTalk_yj(int id_yj, int talkIndex_yj)
       {
           if (talkIndex_yj == talkData_yj[id_yj].Length)
               return null;
           else
               return talkData_yj[id_yj][talkIndex_yj];
       }

       public Sprite GetPortait_yj(int id_yj, int portraitIndex_yj)
       {
           return portraitData_yj[id_yj + portraitIndex_yj];
       }
       void Start()
       {

       }

       // Update is called once per frame
       void Update()
       {

       }
   }
   */
    // �⺻ Ȱ�� ������ ��Ʈ��Ʈ ������ ���� ��� ó��(������ 5���ۿ� ����)
    // ���̵� ���� ���� : 6000������� ������
    // 6001 : �Ʒô���, 6002 : ������, 6003 : �ܼ�


    // �⺻Ȱ��1 : �Ʒô��� �⺻ ���1
    ProDialogue_yj serif1_1 = new ProDialogue_yj(6001, "�Ʒô���","����-������! �Ʒ��� �غ�� �Ƴ�?");
    // �⺻Ȱ��1 : �Ʒô��� �⺻ ���2
    ProDialogue_yj serif1_2 = new ProDialogue_yj(6001, "�Ʒô���", "�ȵǸ� �ɶ�����! �Ʒ� �����̴�!");

    // �⺻Ȱ��2 : ������ �⺻ ���1
    ProDialogue_yj serif2_1 = new ProDialogue_yj(6002, "������", "��ġ�� ��� ������� �״´�! \n�����Ʒ� �����̴�!!");
    // �⺻Ȱ��2 : ������ �⺻ ���2
    ProDialogue_yj serif2_2 = new ProDialogue_yj(6002, "������", "3 -1 = 0! �츮�� �ϳ���!  \n�����Ʒ� �����̴�!!");

    // �⺻Ȱ��3 : �ܼ� ������ �� �⺻ ���
    ProDialogue_yj serif3 = new ProDialogue_yj(6003, "�ܼ�", "�ܼ��� ã�Ҵ�. ������ ���캸��.");

    // ������ ������ ����Ʈ
    private List<ProDialogue> proDialogue;

    public GameObject opening;
    public TextMeshProUGUI openingText; // TextMeshPro UI �ؽ�Ʈ ���

    public GameObject narration;
    public TextMeshProUGUI narrationText; // TextMeshPro UI �ؽ�Ʈ ���

    public GameObject dialogue;
    public GameObject nameObj; // �̸� ���
    public TextMeshProUGUI nameText; // TextMeshPro UI �ؽ�Ʈ ���
    public TextMeshProUGUI descriptionText; // TextMeshPro UI �ؽ�Ʈ ���

    //public GameObject resultPanel; // ���� ����� ǥ���� �г�

    //public GameObject home; // �� ��� ȭ��

    private int currentDialogueIndex = 0; // ���� ��� �ε���
    private bool isActivated = false; // TalkManager�� Ȱ��ȭ�Ǿ����� ����

    void Awake()
    {
        proDialogue = new List<ProDialogue>();
        // LoadDialogueFromCSV(); // CSV���� �����͸� �ε��ϴ� �Լ� ȣ��
        LoadDialogueManually(); // CSV ���� ���� ��ȭ ���� �Է�
    }

    void Start()
    {
        ActivateTalk(); // ������Ʈ Ȱ��ȭ
    }

    void Update()
    {
        /*if (isActivated && currentDialogueIndex == 0)
        {
            PrintProDialogue(currentDialogueIndex);
        }*/
        if (isActivated && Input.GetKeyDown(KeyCode.Space))
        {
            currentDialogueIndex++;
            PrintProDialogue(currentDialogueIndex);
        }
    }

    // ��ȭ ���� �Է�
    void LoadDialogueManually()
    {
        // �������� ��ȭ �Է�
        proDialogue.Add(new ProDialogue(0, "������", "�����Ͻðڽ��ϱ�?"));
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

        dialogue.SetActive(true);
        nameText.text = currentDialogue.name;
        descriptionText.text = currentDialogue.line;

        // �ι��� ���� ���/�����̼�/�ؽ�Ʈ â Ȱ��ȭ

        // �⺻Ȱ�� 1 : �Ʒô��� -> �Ʒ��ϱ�
        if (currentDialogue.name == "�Ʒô���")
        {
            narration.SetActive(true);
            dialogue.SetActive(false);
            opening.SetActive(false);
            narrationText.text = currentDialogue.line;
            //ShowConfirmationPanel(); // "��" �Ǵ� "�ƴϿ�" ���� �г� ǥ��
        }
        // �⺻Ȱ�� 2 : ������ -> �����ϱ�
        else if (currentDialogue.name == "������")
        {
            narration.SetActive(true);
            dialogue.SetActive(false);
            opening.SetActive(false);
            narrationText.text = currentDialogue.line;
            //ShowConfirmationPanel(); // "��" �Ǵ� "�ƴϿ�" ���� �г� ǥ��
        }
        // �⺻Ȱ�� 3 : �������� -> �濡 ���� ������ ����
        else if (currentDialogue.name == "information")
        {
            narration.SetActive(true);
            dialogue.SetActive(false);
            opening.SetActive(false);
            narrationText.text = currentDialogue.line;
            //ShowConfirmationPanel(); // "��" �Ǵ� "�ƴϿ�" ���� �г� ǥ��
        }
        // �Ƹ� ��ũ��Ʈ ������ ó���� �Ŵϱ� ���̵� üũ ���ص� �� �� ���� ��
        // CheckTalk(currentDialogue.id);
    }

    // ���� �г��� �ٸ� Ŭ�������� �޾ƿ� �� ������ �����
    void ShowConfirmationPanel()
    {
        // �� �Ǵ� �ƴϿ� ���� �г��� ǥ���ϰ� ��ư �̺�Ʈ ����
        //resultPanel.SetActive(true);

        // �� ��ư Ŭ�� �� ó��
        /*resultPanel.transform.Find("YesButton").GetComponent<Button>().onClick.AddListener(() => {
            HandleAffiliation(true); // �����ϱ� ó��
        });

        // �ƴϿ� ��ư Ŭ�� �� ó��
        resultPanel.transform.Find("NoButton").GetComponent<Button>().onClick.AddListener(() => {
            HandleAffiliation(false); // ���� ���ϱ� ó��
        });*/
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

    // ���շ� ���� �Լ�
   /*
    public void IncreaseTeamPower(int amount)
    {
        PlayerManager_yj playerManager = FindObjectOfType<PlayerManager_yj>();
        if (playerManager != null)
        {
            playerManager.IncreaseTeamPower(amount);
        }
        else
        {
            Debug.LogError("PlayerManager_yj not found in the scene.");
        }
    }*/
}
