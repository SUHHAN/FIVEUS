using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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
    public GameObject resultUI2_yj;
    public TextMeshProUGUI resuedit_yj; // result edit text
    public TextMeshProUGUI resuedit2_yj; // result edit text

    // �⺻Ȱ�� ��� �����ߴ��� ���� �ϴ�..�÷��̾ �θ���
    // public PlayerNow_yj nowplayer_yj;
    public PlayerManager_yj playermanager_yj; // ü�°�����
    public TimeManager timemanager_yj; // ��¥ ���� + �⺻Ȱ�� ��������

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
        /*
        trainingButton_yj.onClick.AddListener(OntrainButtonClick);
        gotobedButton_yj.onClick.AddListener(OngobedButtonClick);

        noButton1.onClick.AddListener(OnNo1ButtonClick);
        noButton5.onClick.AddListener(OnNo5ButtonClick);
        */
        isbasicdial_yj = false;
        playermanager_yj.playerNow.howtoday_py = 0;
        playermanager_yj.playerNow.howtrain_py = 0;
        
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
                if (isbasicdial_yj)
                    HandleNPCchoice_yj(currentNPC);
            }
        }
    }

    void CheckNPCInteraction()
    {
        float distanceNPC1 = Vector3.Distance(player.transform.position, npc1_yj.transform.position);
        
        if (distanceNPC1 <= interactionRange)
        {
            currentNPC = npc1_yj;
        }
        else
        {
            currentNPC = null;
        }
    }

    void HandleNPCDialogue_yj(GameObject npc_yjyj)
    {
        if (isbasicdial_yj == false)
            Dial_changyj.SetActive(true);
        else if (resultUI_yj.activeSelf || choiceUI1_yj.activeSelf)
            Dial_changyj.SetActive(false);
        // ���� NPC�� ���� ��ȭ ó��
        if (npc_yjyj == npc1_yj) // �Ʒô���
        {
            dialoguename_yj.text = "훈련대장"; // �Ʒô��� �̸� ��� 
            dialogueText_yj.text = "여어-말라깽이! \n훈련할 준비는 됐나? \n[스페이스바를 눌러 훈련을 진행하세요]"; // �Ʒô��� �⺻ ��� ���
            isbasicdial_yj = true; // �⺻��� ġ�� �ִ� ��

        }
    }

    void HandleNPCchoice_yj(GameObject npc_yjyj)
    {
        // ���� NPC�� ���� ������ó��
        if (npc_yjyj == npc1_yj) // �Ʒô���
        {
           
            Dial_changyj.SetActive(false);
            isbasicdial_yj = false;
            choiceUI1_yj.SetActive(true);

        }
    }
    public void OntrainButtonClick()
    {
        choiceUI1_yj.SetActive(false);

        playermanager_yj.IncreaseTrainingCount();// �Ϸ� �Ʒ� Ȱ�� Ƚ�� 1 ����
        playermanager_yj.IncreaseTiredness(30);// �Ƿε� 10 ����
        timemanager_yj.CompleteActivity(); // �Ϸ� �⺻ Ȱ�� ���� Ƚ�� 1 ����


        resuedit_yj.text = $"기본 활동 횟수: {DataManager.instance.nowPlayer.Player_howtoday}"; // �⺻ Ȱ�� �ؽ�Ʈ ������Ʈ


        if (DataManager.instance.nowPlayer.Player_howtoday >= 3)
        {
            resuedit2_yj.text = "하루치 기본 활동 3개를 모두 완수하셨습니다!\n[주인공 집]의 [침대]로 돌아가 휴식을 취해주세요!";
            resultUI2_yj.SetActive(true);
        }

        resultUI_yj.SetActive(true);

        SaveData();

    }
    // �⺻Ȱ��3 : �ܼ� ���ڴ� ���� ��
    
    public void OngobedButtonClick()
    {
        SceneManager.LoadScene("main_house");
    }
    public void OnNo1ButtonClick()
    {
        choiceUI1_yj.SetActive(false); // �Ʒ� UI ����â ��Ȱ��ȭ
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
        choiceUI1_yj.SetActive(false);
        Dial_changyj.SetActive(false);
        resultUI_yj.SetActive(false);
    }
    void HideResultPanel()
    {
        resultUI_yj.SetActive(false);
    }


    void SaveData()
    {
        // DataManager.instance.nowPlayer.Player_hp = playermanager_yj.playerNow.hp_py;
        // DataManager.instance.nowPlayer.Player_tired = playermanager_yj.playerNow.tired_py;
        // DataManager.instance.nowPlayer.Player_money = playermanager_yj.playerNow.money_py;
        // DataManager.instance.nowPlayer.Player_hint = playermanager_yj.playerNow.hint_py;
        // DataManager.instance.nowPlayer.Player_team = playermanager_yj.playerNow.team_py;
        // DataManager.instance.nowPlayer.Player_day = playermanager_yj.playerNow.day_py;
        // DataManager.instance.nowPlayer.Player_howtoday = playermanager_yj.playerNow.howtoday_py;
        // DataManager.instance.nowPlayer.Player_howtrain = playermanager_yj.playerNow.howtrain_py;

        DataManager.instance.SaveData();
    }
}
