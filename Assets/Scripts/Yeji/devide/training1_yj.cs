using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
<<<<<<< Updated upstream
using Unity.VisualScripting; // TextMeshPro ���ӽ����̽� �߰�
using UnityEngine.SceneManagement;
using System.Xml.Serialization;

// ���� ���� �����°�(�Ʒ�1 + �ܼ�3)

public class training1_yj : MonoBehaviour
{
    private bool isbasicdial_yj = false; // ��� ġ�� �ִ��� ����

    public GameObject choiceUI1_yj; // �⺻Ȱ��1 UI �г�


    public GameObject Dial_changyj; // �⺻��� ��� ��ȭâ
    public TextMeshProUGUI dialoguename_yj; // name text
    public TextMeshProUGUI dialogueText_yj; // line text
    public float interactionRange = 3f; // ��ȣ�ۿ� �Ÿ�

    public GameObject player; // �÷��̾� ������Ʈ
    private GameObject currentNPC; // ���� ��ȣ�ۿ��ϴ� NPC ���� ����

    public GameObject npc1_yj; // �Ʒô���

    public Button noButton1; // �ƴϿ� ��ư ����1
    public Button noButton5; // ���â �ݱ� ��ư ����5

    public Button trainingButton_yj; // 1. �Ʒ� �õ� ��ư ����
    public Button gotobedButton_yj; //5. ħ�� �̵� ��ư ����

    public GameObject resultUI_yj; // ��� UI �г�
=======
using Unity.VisualScripting; // TextMeshPro ���ӽ����̽� �߰�
using UnityEngine.SceneManagement;
using System.Xml.Serialization;

// ���� ���� �����°�(�Ʒ�1 + �ܼ�3)

public class training1_yj : MonoBehaviour
{
    private bool isbasicdial_yj = false; // ��� ġ�� �ִ��� ����

    public GameObject choiceUI1_yj; // �⺻Ȱ��1 UI �г�
    public GameObject choiceUI3_yj; // �⺻Ȱ��3 UI �г�

    public GameObject Dial_changyj; // �⺻��� ��� ��ȭâ
    public TextMeshProUGUI dialoguename_yj; // name text
    public TextMeshProUGUI dialogueText_yj; // line text
    public float interactionRange = 3f; // ��ȣ�ۿ� �Ÿ�

    public GameObject player; // �÷��̾� ������Ʈ
    private GameObject currentNPC; // ���� ��ȣ�ۿ��ϴ� NPC ���� ����

    public GameObject npc1_yj; // �Ʒô���
    public GameObject npc3_yj; // ��Ʈ

    public Button noButton1; // �ƴϿ� ��ư ����1
    public Button noButton3; // �ƴϿ� ��ư ����3
    public Button noButton5; // ���â �ݱ� ��ư ����5

    public Button trainingButton_yj; // 1. �Ʒ� �õ� ��ư ����
    public Button findhintButton_yj; // 3. �ܼ� ���� ��ư ����
    public Button gotobedButton_yj; //5. ħ�� �̵� ��ư ����

    public GameObject resultUI_yj; // ��� UI �г�
>>>>>>> Stashed changes
    public GameObject resultUI2_yj;
    public TextMeshProUGUI resuedit_yj; // result edit text
    public TextMeshProUGUI resuedit2_yj; // result edit text

<<<<<<< Updated upstream
    // �⺻Ȱ�� ��� �����ߴ��� ���� �ϴ�..�÷��̾ �θ���
    // public PlayerNow_yj nowplayer_yj;
    public PlayerManager_yj playermanager_yj; // ü�°�����
    public TimeManager timemanager_yj; // ��¥ ���� + �⺻Ȱ�� ��������
=======
    // �⺻Ȱ�� ��� �����ߴ��� ���� �ϴ�..�÷��̾ �θ���
    // public PlayerNow_yj nowplayer_yj;
    public PlayerManager_yj playermanager_yj; // ü�°�����
    public TimeManager timemanager_yj; // ��¥ ���� + �⺻Ȱ�� ��������
>>>>>>> Stashed changes

    private static training1_yj _instance;

    public static training1_yj Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<training1_yj>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("talkwithjjang_yj");
                    _instance = obj.AddComponent<training1_yj>();
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
        trainingButton_yj.onClick.AddListener(OntrainButtonClick);
<<<<<<< Updated upstream
        gotobedButton_yj.onClick.AddListener(OngobedButtonClick);

        noButton1.onClick.AddListener(OnNo1ButtonClick);
=======
        findhintButton_yj.onClick.AddListener(OnhintButtonClick);
        gotobedButton_yj.onClick.AddListener(OngobedButtonClick);

        noButton1.onClick.AddListener(OnNo1ButtonClick);
        noButton3.onClick.AddListener(OnNo3ButtonClick);
>>>>>>> Stashed changes
        noButton5.onClick.AddListener(OnNo5ButtonClick);

        isbasicdial_yj = false;
        playermanager_yj.playerNow.howtoday_py = 0;
        playermanager_yj.playerNow.howtrain_py = 0;
<<<<<<< Updated upstream
        player = GameObject.FindGameObjectWithTag("Player"); // �±װ� "Player"�� ������Ʈ ã��
        HideUI(); // ������ �� UI �����
=======
        player = GameObject.FindGameObjectWithTag("Player"); // �±װ� "Player"�� ������Ʈ ã��
        HideUI(); // ������ �� UI �����
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
            HandleNPCDialogue_yj(currentNPC); // npc���� ������ ���� ��ȭâ�� ���
=======
            HandleNPCDialogue_yj(currentNPC); // npc���� ������ ���� ��ȭâ�� ���
>>>>>>> Stashed changes

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isbasicdial_yj)
                    HandleNPCchoice_yj(currentNPC);
            }
        }
    }

    void CheckNPCInteraction()
    {
        float distanceNPC1 = Vector3.Distance(player.transform.position, npc1_yj.transform.position);
<<<<<<< Updated upstream
        
=======
        float distanceNPC3 = Vector3.Distance(player.transform.position, npc3_yj.transform.position);

>>>>>>> Stashed changes
        if (distanceNPC1 <= interactionRange)
        {
            currentNPC = npc1_yj;
        }
<<<<<<< Updated upstream
=======
        else if (distanceNPC3 <= interactionRange)
        {
            currentNPC = npc3_yj;
        }
>>>>>>> Stashed changes
        else
        {
            currentNPC = null;
        }
    }

    void HandleNPCDialogue_yj(GameObject npc_yjyj)
    {
        if (isbasicdial_yj == false)
            Dial_changyj.SetActive(true);
<<<<<<< Updated upstream
        else if (resultUI_yj.activeSelf || choiceUI1_yj.activeSelf)
            Dial_changyj.SetActive(false);
        // ���� NPC�� ���� ��ȭ ó��
        if (npc_yjyj == npc1_yj) // �Ʒô���
        {
            dialoguename_yj.text = "�Ʒô���"; // �Ʒô��� �̸� ��� 
            dialogueText_yj.text = "����-������! \n�Ʒ��� �غ�� �Ƴ�? \n[�����̽��ٸ� ���� �Ʒ��� �����ϼ���]"; // �Ʒô��� �⺻ ��� ���
            isbasicdial_yj = true; // �⺻��� ġ�� �ִ� ��

        }
=======
        else if (resultUI_yj.activeSelf || choiceUI1_yj.activeSelf || choiceUI3_yj.activeSelf)
            Dial_changyj.SetActive(false);
        // ���� NPC�� ���� ��ȭ ó��
        if (npc_yjyj == npc1_yj) // �Ʒô���
        {
            dialoguename_yj.text = "�Ʒô���"; // �Ʒô��� �̸� ��� 
            dialogueText_yj.text = "����-������! \n�Ʒ��� �غ�� �Ƴ�? \n[�����̽��ٸ� ���� �Ʒ��� �����ϼ���]"; // �Ʒô��� �⺻ ��� ���
            isbasicdial_yj = true; // �⺻��� ġ�� �ִ� ��

        }
        else if (npc_yjyj == npc3_yj) // �ܼ�
        {
            dialoguename_yj.text = "�ܼ�"; // �ܼ� �̸� ��� 
            dialogueText_yj.text = "�ܼ��� ã�Ҵ�.\n�κ��丮���� ������ Ȯ���� ����.\n[�����̽��ٸ� ��������]"; // �ܼ� �⺻ ��� ���                                      
            isbasicdial_yj = true; // �⺻��� ġ�� �ִ� ��
        }
>>>>>>> Stashed changes
    }

    void HandleNPCchoice_yj(GameObject npc_yjyj)
    {
<<<<<<< Updated upstream
        // ���� NPC�� ���� ������ó��
        if (npc_yjyj == npc1_yj) // �Ʒô���
        {
            //Debug.Log("�Ʒô����̶� �����");
=======
        // ���� NPC�� ���� ������ó��
        if (npc_yjyj == npc1_yj) // �Ʒô���
        {
            //Debug.Log("�Ʒô����̶� �����");
>>>>>>> Stashed changes
            Dial_changyj.SetActive(false);
            isbasicdial_yj = false;
            choiceUI1_yj.SetActive(true);

        }
<<<<<<< Updated upstream
    }
    // �⺻Ȱ��1 : "�Ʒ��Ѵ�" �������� ��
=======
        else if (npc_yjyj == npc3_yj) // �ܼ�
        {
            Dial_changyj.SetActive(false);
            isbasicdial_yj = false;
            choiceUI3_yj.SetActive(true);
        }
    }
    // �⺻Ȱ��1 : "�Ʒ��Ѵ�" �������� ��
>>>>>>> Stashed changes
    public void OntrainButtonClick()
    {
        choiceUI1_yj.SetActive(false);

<<<<<<< Updated upstream
        // ���� ���
        playermanager_yj.IncreaseTrainingCount();// �Ϸ� �Ʒ� Ȱ�� Ƚ�� 1 ����
        playermanager_yj.IncreaseTiredness(30);// �Ƿε� 10 ����
        timemanager_yj.CompleteActivity(); // �Ϸ� �⺻ Ȱ�� ���� Ƚ�� 1 ����


        resuedit_yj.text = $"�⺻Ȱ�� Ƚ�� : {timemanager_yj.activityCount / 2} / 3"; // �⺻ Ȱ�� �ؽ�Ʈ ������Ʈ
=======
        // ���� ���
        playermanager_yj.IncreaseTrainingCount();// �Ϸ� �Ʒ� Ȱ�� Ƚ�� 1 ����
        playermanager_yj.IncreaseTiredness(30);// �Ƿε� 10 ����
        timemanager_yj.CompleteActivity(); // �Ϸ� �⺻ Ȱ�� ���� Ƚ�� 1 ����


        resuedit_yj.text = $"�⺻Ȱ�� Ƚ�� : {timemanager_yj.activityCount / 2} / 3"; // �⺻ Ȱ�� �ؽ�Ʈ ������Ʈ
>>>>>>> Stashed changes


        if (timemanager_yj.activityCount >= 5)
        {
<<<<<<< Updated upstream
            resuedit2_yj.text = "�Ϸ�ġ �⺻ Ȱ�� 3���� ��� �ϼ��ϼ̽��ϴ�!\n[���ΰ� ��]�� [ħ��]�� ���ư� �޽��� �����ּ���!";
=======
            resuedit2_yj.text = "�Ϸ�ġ �⺻ Ȱ�� 3���� ��� �ϼ��ϼ̽��ϴ�!\n[���ΰ� ��]�� [ħ��]�� ���ư� �޽��� �����ּ���!";
>>>>>>> Stashed changes
            resultUI2_yj.SetActive(true);
        }

        resultUI_yj.SetActive(true);

        SaveData();

    }
<<<<<<< Updated upstream
    // �⺻Ȱ��3 : �ܼ� ���ڴ� ���� ��
    
=======
    // �⺻Ȱ��3 : �ܼ� ���ڴ� ���� ��
    public void OnhintButtonClick()
    {
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

        SceneManager.LoadScene("InventoryMain"); // �κ��丮 ������ �̵�
        // ã�� �ܼ� ������ �� �� �ø�. �̰� �κ��丮�� ���� �Ŀ� �����ؾ� �ҵ�
    }
>>>>>>> Stashed changes
    public void OngobedButtonClick()
    {
        SceneManager.LoadScene("main_house");
    }
    public void OnNo1ButtonClick()
    {
<<<<<<< Updated upstream
        choiceUI1_yj.SetActive(false); // �Ʒ� UI ����â ��Ȱ��ȭ
        isbasicdial_yj = false;
    }
   
    public void OnNo5ButtonClick()
    {
        resultUI_yj.SetActive(false); // ��� UI ����â ��Ȱ��ȭ
        resultUI2_yj.SetActive(false); // ���2 UI ����â ��Ȱ��ȭ
=======
        choiceUI1_yj.SetActive(false); // �Ʒ� UI ����â ��Ȱ��ȭ
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
>>>>>>> Stashed changes
        isbasicdial_yj = false;
    }


    public void HideUI()
    {
        choiceUI1_yj.SetActive(false);
<<<<<<< Updated upstream
=======
        choiceUI3_yj.SetActive(false);
>>>>>>> Stashed changes
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
