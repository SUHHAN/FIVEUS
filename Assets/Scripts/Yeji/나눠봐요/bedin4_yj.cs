using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting; // TextMeshPro ���ӽ����̽� �߰�
using UnityEngine.SceneManagement;
using System.Xml.Serialization;

// �� ���� �����°�(�޽�4 + �ܼ�3)
public class bedin4_yj : MonoBehaviour
{
    private bool isbasicdial_yj = false; // ��� ġ�� �ִ��� ����

    public GameObject choiceUI3_yj; // �⺻Ȱ��3 UI �г�
    public GameObject choiceUI4_yj; // �⺻Ȱ��4 UI �г�

    public GameObject Dial_changyj; // �⺻��� ��� ��ȭâ
    public TextMeshProUGUI dialoguename_yj; // name text
    public TextMeshProUGUI dialogueText_yj; // line text
    public float interactionRange = 3f; // ��ȣ�ۿ� �Ÿ�

    public GameObject player; // �÷��̾� ������Ʈ
    private GameObject currentNPC; // ���� ��ȣ�ۿ��ϴ� NPC ���� ����

    public GameObject npc3_yj; // ��Ʈ
    public GameObject npc4_yj; // �޽�

    public Button noButton3; // �ƴϿ� ��ư ����3
    public Button noButton4; // �ƴϿ� ��ư ����4
    public Button noButton5; // ���â �ݱ� ��ư ����5

    public Button findhintButton_yj; // 3. �ܼ� ���� ��ư ����
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
        findhintButton_yj.onClick.AddListener(OnhintButtonClick);
        laybedButton_yj.onClick.AddListener(OnbedButtonClick);
        gotobedButton_yj.onClick.AddListener(OngobedButtonClick);

        noButton3.onClick.AddListener(OnNo3ButtonClick);
        noButton4.onClick.AddListener(OnNo4ButtonClick);
        noButton5.onClick.AddListener(OnNo5ButtonClick);

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
        float distanceNPC3 = Vector3.Distance(player.transform.position, npc3_yj.transform.position);
        float distanceNPC4 = Vector3.Distance(player.transform.position, npc4_yj.transform.position);

        if (distanceNPC3 <= interactionRange)
        {
            currentNPC = npc3_yj;
        }
        else if (distanceNPC4 <= interactionRange)
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
        else if (resultUI_yj.activeSelf || choiceUI4_yj.activeSelf || choiceUI3_yj.activeSelf)
            Dial_changyj.SetActive(false);
        // ���� NPC�� ���� ��ȭ ó��
        if (npc_yjyj == npc3_yj) // �ܼ�
        {
            dialoguename_yj.text = "�ܼ�"; // �ܼ� �̸� ��� 
            dialogueText_yj.text = "�ܼ��� ã�Ҵ�.\n�κ��丮���� ������ Ȯ���� ����.\n[�����̽��ٸ� ��������]"; // �ܼ� �⺻ ��� ���                                      
            isbasicdial_yj = true; // �⺻��� ġ�� �ִ� ��
        }
        else if (npc_yjyj == npc4_yj) // ħ��
        {
            dialoguename_yj.text = "ħ��"; // �Ʒô��� �̸� ��� 
            dialogueText_yj.text = "�ƴ��� �� ���� ħ���.\n����� �޽��� ���� ����. \n[�����̽��ٸ� ���� �޽��� �����ϼ���]"; // �Ʒô��� �⺻ ��� ���                                      
            isbasicdial_yj = true; // �⺻��� ġ�� �ִ� ��
        }
    }

    void HandleNPCchoice_yj(GameObject npc_yjyj)
    {
        // ���� NPC�� ���� ������ó��
        if (npc_yjyj == npc3_yj) // �ܼ�
        {
            Dial_changyj.SetActive(false);
            isbasicdial_yj = false;
            choiceUI3_yj.SetActive(true);
        }
        else if (npc_yjyj == npc4_yj) // ħ��
        {

            Dial_changyj.SetActive(false);
            isbasicdial_yj = false;
            choiceUI4_yj.SetActive(true);
        }
    }
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

        SceneManager.LoadScene("InventoryMain"); // �κ��丮 ������ �̵�
        // ã�� �ܼ� ������ �� �� �ø�. �̰� �κ��丮�� ���� �Ŀ� �����ؾ� �ҵ�
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
        SceneManager.LoadScene("main_house");
    }
    public void OnNo3ButtonClick()
    {
        choiceUI3_yj.SetActive(false); // �ܼ� UI ����â ��Ȱ��ȭ
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


    public void HideUI()
    {
        choiceUI4_yj.SetActive(false);
        choiceUI3_yj.SetActive(false);
        Dial_changyj.SetActive(false);
        resultUI_yj.SetActive(false);
    }
    void HideResultPanel()
    {
        resultUI_yj.SetActive(false);
    }
}
