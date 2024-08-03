using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting; // TextMeshPro ���ӽ����̽� �߰�
using UnityEngine.SceneManagement;
using System.Xml.Serialization;

// �� ���� �����°�(�޽�4)
// �� �ڴ� �ڵ� �߰��ؾ� �� 
public class bedin4_yj : MonoBehaviour
{
    private bool isbasicdial_yj = false; // ��� ġ�� �ִ��� ����

    public GameObject choiceUI4_yj; // �⺻Ȱ��4 UI �г�
    public GameObject iaminbedUI_yj;// �޽��� UI �г�

    public GameObject Dial_changyj; // �⺻��� ��� ��ȭâ
    public TextMeshProUGUI dialoguename_yj; // name text
    public TextMeshProUGUI dialogueText_yj; // line text
    public float interactionRange = 3f; // ��ȣ�ۿ� �Ÿ�

    private GameObject player; // �÷��̾� ������Ʈ
    private GameObject currentNPC; // ���� ��ȣ�ۿ��ϴ� NPC ���� ����

    public GameObject npc4_yj; // �޽�

    public Button noButton4; // �ƴϿ� ��ư ����4
    public Button noButton5; // ���â �ݱ� ��ư ����5
    public Button noButton6; // �޽�â �ݱ� ��ư ����

    public Button laybedButton_yj; // 4. �޽��ϱ� ��ư ����
    public Button gotobedButton_yj; //5. ħ�� �̵� ��ư ����

    public GameObject resultUI_yj; // ��� UI �г�
    public GameObject resultUI2_yj;
    public TextMeshProUGUI resuedit_yj; // result edit text
    public TextMeshProUGUI resuedit2_yj; // result edit text

    // �⺻Ȱ�� ��� �����ߴ��� ���� �ϴ�..�÷��̾ �θ���
    // public PlayerNow_yj nowplayer_yj;
    public PlayerManager_yj playermanager_yj; // ü�°�����
    public TimeManager timemanager_yj; // ��¥ ���� + �⺻Ȱ�� ��������

    private static bedin4_yj _instance;

    public static bedin4_yj Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<bedin4_yj>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("talkwithjjang_yj");
                    _instance = obj.AddComponent<bedin4_yj>();
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
        laybedButton_yj.onClick.AddListener(OnbedButtonClick);
        gotobedButton_yj.onClick.AddListener(OngobedButtonClick);
       
        noButton4.onClick.AddListener(OnNo4ButtonClick);
        noButton5.onClick.AddListener(OnNo5ButtonClick);
        noButton6.onClick.AddListener(OnNo6ButtonClick);

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
        float distanceNPC4 = Vector3.Distance(player.transform.position, npc4_yj.transform.position);

        if (distanceNPC4 <= interactionRange)
        {
            currentNPC = npc4_yj;
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
        else if (resultUI_yj.activeSelf || choiceUI4_yj.activeSelf)
            Dial_changyj.SetActive(false);
        // ���� NPC�� ���� ��ȭ ó��
        if (npc_yjyj == npc4_yj) // ħ��
        {
            dialoguename_yj.text = "ħ��"; // ħ�� �̸� ��� 
            dialogueText_yj.text = "�ƴ��� �� ���� ħ���.\n����� �޽��� ���� ����. \n[�����̽��ٸ� ���� �޽��� �����ϼ���]"; // ħ�� Ư�� ��� ���                                      
            isbasicdial_yj = true; // �⺻��� ġ�� �ִ� ��

            if (timemanager_yj.activityCount >= 5)
            {
                // ���ΰ��� �ھ߸� �� ��
                dialoguename_yj.text = "ħ��"; // ħ�� �̸� ��� 
                dialogueText_yj.text = "��� �Ϸ縦 ��ġ�� �ڴ� ���� ��������! .\n���� �Ϸ絵 �����߾�~. \n[�����̽��ٸ� ���� ������ ���ϼ���]"; // ħ�� Ư�� ��� ���                                      
                isbasicdial_yj = true; // �⺻��� ġ�� �ִ� ��
            }
        }
    }

    void HandleNPCchoice_yj(GameObject npc_yjyj)
    {
        if (npc_yjyj == npc4_yj) // ħ��
        {

            Dial_changyj.SetActive(false);
            isbasicdial_yj = false;
            choiceUI4_yj.SetActive(true);

            if(timemanager_yj.activityCount >= 5)
            {
                // ������ ���� �ڰ��ִٴ� â�� 2�ʵ��� �ߵ���
                Dial_changyj.SetActive(false);
                isbasicdial_yj = false;
                iaminbedUI_yj.SetActive(true);
            }
        }
    }
    
    // �⺻Ȱ��4 : �޽� ���ϰڴ� ���� ��
    public void OnbedButtonClick()
    {
        choiceUI4_yj.SetActive(false);
        resultUI_yj.SetActive(true);

        timemanager_yj.CompleteActivity();// �Ϸ� �⺻ Ȱ�� ���� Ƚ�� 1 ����

        // ���â ������Ʈ
        resuedit_yj.text = $"�⺻Ȱ�� Ƚ�� : {timemanager_yj.activityCount / 2} / 3"; // �⺻ Ȱ�� �ؽ�Ʈ ������Ʈ

        if (timemanager_yj.activityCount >= 5)
        {
            resuedit2_yj.text = "�Ϸ�ġ �⺻ Ȱ�� 3���� ��� �ϼ��ϼ̽��ϴ�!\n[���ΰ� ��]�� [ħ��]�� ���ư� �޽��� �����ּ���!";
            resultUI2_yj.SetActive(true);
        }

        resultUI_yj.SetActive(true);
    }
    public void OngobedButtonClick()
    {
        //SceneManager.LoadScene("main_house"); // �̰� ���� �� ������? ���� ���� ��������.

        // ��� OnNo5ButtonClick()��ư�̶� ���� ���..�̹� ���� �ִµ� �� �̵���Ű�� ��������� 
        resultUI_yj.SetActive(false); // ��� UI ����â ��Ȱ��ȭ
        resultUI2_yj.SetActive(false); // ���2 UI ����â ��Ȱ��ȭ
        isbasicdial_yj = false;
    }

    
    public void OnNo4ButtonClick()
    {
        choiceUI4_yj.SetActive(false); // �޽� UI ����â ��Ȱ��ȭ
        isbasicdial_yj = false;
    }
    public void OnNo5ButtonClick()
    {
        resultUI_yj.SetActive(false); // ��� UI ����â ��Ȱ��ȭ
        resultUI2_yj.SetActive(false); // ���2 UI ����â ��Ȱ��ȭ
        isbasicdial_yj = false;
    }
    public void OnNo6ButtonClick()
    {
        iaminbedUI_yj.SetActive(false); // �޽ĿϷ� UI â ��Ȱ��ȭ
        isbasicdial_yj = false;
        timemanager_yj.UpdateDateAndTimeDisplay();// ������ ��¥ ��
    }

    public void HideUI()
    {
        choiceUI4_yj.SetActive(false);
        Dial_changyj.SetActive(false);
        resultUI_yj.SetActive(false);
    }
    void HideResultPanel()
    {
        resultUI_yj.SetActive(false);
    }
}
