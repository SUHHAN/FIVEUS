using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting; // TextMeshPro ���ӽ����̽� �߰�
using UnityEngine.SceneManagement;
using System.Xml.Serialization;

// �Ƿ� + �ܼ�
// �⺻Ȱ�� 5��° �Ƿ� ����(5�ε� 5�� �����ϱ� 7������ ����)
public class QuestData_yj :MonoBehaviour
{
    // random gold 
    private bool isbasicdial_yj = false; // ��� ġ�� �ִ��� ����

    public GameObject choiceUI3_yj; // �⺻Ȱ��3 UI �г�
    public GameObject choiceUI7_yj; // �⺻Ȱ��7 UI �г�

    public GameObject Dial_changyj; // �⺻��� ��� ��ȭâ
    public TextMeshProUGUI dialoguename_yj; // name text
    public TextMeshProUGUI dialogueText_yj; // line text
    public float interactionRange = 3f; // ��ȣ�ۿ� �Ÿ�

    private GameObject player; // �÷��̾� ������Ʈ
    private GameObject currentNPC; // ���� ��ȣ�ۿ��ϴ� NPC ���� ����

    public GameObject npc3_yj; // ��Ʈ
    public GameObject npc7_yj; // ��ü��
    public TextMeshProUGUI QuestEditText_yj; // result edit text

    public Button noButton3; // �ƴϿ� ��ư ����3
    public Button noButton7; // �ƴϿ� ��ư ����7
    public Button noButton5; // ���â �ݱ� ��ư ����5

    public Button findhintButton_yj; // 3. �ܼ� ���� ��ư ����
    public Button myreasonButton_yj; //7. �Ƿ� �ϱ� ��ư ����
    public Button gotobedButton_yj; //5. ħ�� �̵� ��ư ����

    public GameObject resultUI_yj; // ��� UI �г�
    public GameObject resultUI2_yj;
    public TextMeshProUGUI resuedit_yj; // result edit text
    public TextMeshProUGUI resuedit2_yj; // result edit text

    // �⺻Ȱ�� ��� �����ߴ��� ���� �ϴ�..�÷��̾ �θ���
    // public PlayerNow_yj nowplayer_yj;
    public PlayerManager_yj playermanager_yj; // ü�°�����
    public TimeManager timemanager_yj; // ��¥ ���� + �⺻Ȱ�� ��������

    private int questmoneyy_yj;// (���⼭�� ����) : �Ƿ� ����
    
    private static QuestData_yj _instance;

    public static QuestData_yj Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<QuestData_yj>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("talkwithjjang_yj");
                    _instance = obj.AddComponent<QuestData_yj>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);

        }
    }
    // Start is called before the first frame update
    void Start()
    {

        findhintButton_yj.onClick.AddListener(OnhintButtonClick);
        myreasonButton_yj.onClick.AddListener(OnQuestButtonClick);
        gotobedButton_yj.onClick.AddListener(OngobedButtonClick);

        noButton3.onClick.AddListener(OnNo3ButtonClick);
        noButton5.onClick.AddListener(OnNo5ButtonClick);
        noButton7.onClick.AddListener(OnNo7ButtonClick);

        isbasicdial_yj = false;
        playermanager_yj.playerNow.howtoday_py = 0;
        playermanager_yj.playerNow.howtrain_py = 0;
        questmoneyy_yj =0;
        player = GameObject.FindGameObjectWithTag("Player"); // �±װ� "Player"�� ������Ʈ ã��
        HideUI(); // ������ �� UI �����
    }

    // Update is called once per frame
    void Update()
    {
        
        CheckNPCInteraction();
        HandleUserInput();
        //timeManager_yj.UpdateDateAndTimeDisplay(); 
    }

    void HandleUserInput()
    {
        if (currentNPC != null)
        {
            HandleNPCDialogue_yj(currentNPC); // npc���� ������ ���� ��ȭâ�� ���

            if (Input.GetKeyDown(KeyCode.Space))
            {
                //QuestMomey_yj();
                //Debug.Log("space");
                if (isbasicdial_yj)
                {
                   //Debug.Log("HandleNPCchoice");
                    HandleNPCchoice_yj(currentNPC);

                }

            }
        }
    }

    void CheckNPCInteraction()
    {
        if (npc3_yj != null)
        {
            float distanceNPC3 = Vector3.Distance(player.transform.position, npc3_yj.transform.position);
            
            if (distanceNPC3 <= interactionRange)
            {
                currentNPC = npc3_yj;
            }
            else
            {
                currentNPC = null;
            }
        }
        float distanceNPC7 = Vector3.Distance(player.transform.position, npc7_yj.transform.position);
        Debug.Log("distanceNPC7 : " + distanceNPC7);

        

        if (distanceNPC7 <= interactionRange)
        {
            currentNPC = npc7_yj;
        }
    }
    // �Ƿ� ���� �����ϴ� �޼ҵ�(500-1000���)
    int QuestMomey_yj()
    {
        // �̰��� �����ϰ� questmoneyy_yj�� �����ϴ� �ڵ� �ۼ�
        System.Random random = new System.Random();
        int randomValue = random.Next(5, 11);
        int goldcal_yj = randomValue * 100;
        return goldcal_yj;
    }

    void HandleNPCDialogue_yj(GameObject npc_yjyj)
    {
        if (isbasicdial_yj == false)
            Dial_changyj.SetActive(true);
        else if (resultUI_yj.activeSelf || choiceUI7_yj.activeSelf || choiceUI3_yj.activeSelf)
            Dial_changyj.SetActive(false);
        // ���� NPC�� ���� ��ȭ ó��
        if (npc_yjyj == npc7_yj) // �Ƿ�
        {
            dialoguename_yj.text = "���ο� �Ƿ�"; // �Ƿ� �̸� ��� 

            // ����Ʈ �ݾ��� �Լ� ȣ���Ͽ� ��������, ���ڿ� ���� ���
            if (questmoneyy_yj == 0)
            {
                questmoneyy_yj = QuestMomey_yj();
            }
            dialogueText_yj.text = $"���ο� �Ƿڰ� ���Դ�. \n{questmoneyy_yj}���?! ��û���ݾ�! \n[�����̽��ٸ� ���� �����ϼ���]";
            isbasicdial_yj = true; // �⺻��� ġ�� �ִ� ��
        }
        else if (npc_yjyj == npc3_yj) // �ܼ�
        {
            dialoguename_yj.text = "�ܼ�"; // �ܼ� �̸� ��� 
            dialogueText_yj.text = "�ܼ��� ã�Ҵ�.\n�κ��丮���� ������ Ȯ���� ����.\n[�����̽��ٸ� ��������]"; // �ܼ� �⺻ ��� ���                                      
            isbasicdial_yj = true; // �⺻��� ġ�� �ִ� ��
        }
    }

    void HandleNPCchoice_yj(GameObject npc_yjyj)
    {
        // ���� NPC�� ���� ������ó��
        if (npc_yjyj == npc7_yj) // �Ƿ�
        {
            Dial_changyj.SetActive(false);
            isbasicdial_yj = false;
            QuestEditText_yj.text = $"���� : {questmoneyy_yj}G";
            choiceUI7_yj.SetActive(true);
        }
        else if (npc_yjyj == npc3_yj) // �ܼ�
        {
            Dial_changyj.SetActive(false);
            isbasicdial_yj = false;
            choiceUI3_yj.SetActive(true);
        }

    }
    // �⺻Ȱ��3 : �ܼ� ���ڴ� ���� ��
    public void OnhintButtonClick()
    {
        Debug.Log("�ܼ� Ŭ��");
        choiceUI3_yj.SetActive(false);
        timemanager_yj.CompleteActivity(); // �Ϸ� �⺻ Ȱ�� ���� Ƚ�� 1 ����
        resuedit_yj.text = $"�⺻Ȱ�� Ƚ�� : {timemanager_yj.activityCount / 2} / 3";
        if (timemanager_yj.activityCount >= 5)
        {
            resuedit2_yj.text = "�Ϸ�ġ �⺻ Ȱ�� 3���� ��� �ϼ��ϼ̽��ϴ�!\n[���ΰ� ��]�� [ħ��]�� ���ư� �޽��� �����ּ���!";
            resultUI2_yj.SetActive(true);
        }

        resultUI_yj.SetActive(true);
        Invoke("HideResultPanel()", 2f);
        SaveData();
        // �κ��丮�� ��Ʈ ���� �߰�
        ItemManager.instance.GetHint_inv();

        SceneManager.LoadScene("InventoryMain"); // �κ��丮 ������ �̵�
        // ã�� �ܼ� ������ �� �� �ø�. �̰� �κ��丮�� ���� �Ŀ� �����ؾ� �ҵ�
    }

    // �⺻Ȱ��7 : �Ƿ��Ѵ� ���� ��
    public void OnQuestButtonClick()
    {
        //Dial_changyj.SetActive(false);
        choiceUI7_yj.SetActive(false); // ���� UI ��Ȱ��ȭ

        // ���� ���
        
        playermanager_yj.DecreaseHealth(20);// �Ϸ� ü�� 20 ����
        playermanager_yj.IncreaseMoney(questmoneyy_yj);// ��ȭ ����
        timemanager_yj.CompleteActivity(); // �Ϸ� �⺻ Ȱ�� ���� Ƚ�� 1 ����

        // ���â ������Ʈ
        resuedit_yj.text = $"�⺻Ȱ�� Ƚ�� : {timemanager_yj.activityCount / 2} / 3\n�� ��� : {playermanager_yj.playerNow.money_py} G"; // �⺻ Ȱ�� �ؽ�Ʈ ������Ʈ

        if (timemanager_yj.activityCount >= 5)
        {
            resuedit2_yj.text = "�Ϸ�ġ �⺻ Ȱ�� 3���� ��� �ϼ��ϼ̽��ϴ�!\n[���ΰ� ��]�� [ħ��]�� ���ư� �޽��� �����ּ���!";
            resultUI2_yj.SetActive(true);
        }

        resultUI_yj.SetActive(true);
        SaveData();
    }

    public void OngobedButtonClick()
    {
        SceneManager.LoadScene("main_house");
    }
    public void OnNo7ButtonClick()
    {
        choiceUI7_yj.SetActive(false); // �Ƿ� UI ����â ��Ȱ��ȭ
        isbasicdial_yj = false;
    }
    public void OnNo3ButtonClick()
    {
        choiceUI3_yj.SetActive(false); // �ܼ� UI ����â ��Ȱ��ȭ
        isbasicdial_yj = false;
    }
    public void OnNo5ButtonClick()
    {
        resultUI_yj.SetActive(false); // ��� UI ����â ��Ȱ��ȭ
        resultUI2_yj.SetActive(false); // ���2 UI ����â ��Ȱ��ȭ
        isbasicdial_yj = false;
    }


    public void HideUI()
    {
        choiceUI3_yj.SetActive(false);
        choiceUI7_yj.SetActive(false);
        Dial_changyj.SetActive(false);
        resultUI_yj.SetActive(false);
    }
    void HideResultPanel()
    {
        resultUI_yj.SetActive(false);
    }
    void SaveData()
    {
        DataManager.instance.nowPlayer.Player_hp = playermanager_yj.playerNow.hp_py;
        DataManager.instance.nowPlayer.Player_tired = playermanager_yj.playerNow.tired_py;
        DataManager.instance.nowPlayer.Player_money = playermanager_yj.playerNow.money_py;
        DataManager.instance.nowPlayer.Player_hint = playermanager_yj.playerNow.hint_py;
        DataManager.instance.nowPlayer.Player_team = playermanager_yj.playerNow.team_py;
        DataManager.instance.nowPlayer.Player_day = playermanager_yj.playerNow.day_py;
        DataManager.instance.nowPlayer.Player_howtoday = playermanager_yj.playerNow.howtoday_py;
        DataManager.instance.nowPlayer.Player_howtrain = playermanager_yj.playerNow.howtrain_py;

        DataManager.instance.SaveData();
    }
}
