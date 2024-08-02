using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting; // TextMeshPro ���ӽ����̽� �߰�
using UnityEngine.SceneManagement;
using System.Xml.Serialization;

// �⺻ Ȱ�� �����ϴ� ��ũ��Ʈ
public class talkwithjjang_yj : MonoBehaviour
{
    private bool isbasicdial_yj = false; // ��� ġ�� �ִ��� ����
    private bool isResultActive_yj = false; // ��� UI�� Ȱ��ȭ �Ǿ� �ִ��� ����


    // �⺻ Ȱ�� �гε�
    public GameObject choiceUI1_yj; // �⺻Ȱ��1 UI �г�
    public GameObject choiceUI2_yj; // �⺻Ȱ��2 UI �г�
    public GameObject choiceUI3_yj; // �⺻Ȱ��3 UI �г�
    public GameObject choiceUI4_yj; // �⺻Ȱ��4 UI �г�

    public GameObject Dial_changyj; // �⺻��� ��� ��ȭâ
    public TextMeshProUGUI dialoguename_yj; // name text
    public TextMeshProUGUI dialogueText_yj; // line text
    public float interactionRange = 1.5f; // ��ȣ�ۿ� �Ÿ�

    public GameObject player; // �÷��̾� ������Ʈ
    private GameObject currentNPC; // ���� ��ȣ�ۿ��ϴ� NPC ���� ����
    public GameObject npc1_yj; // �Ʒô���
    public GameObject npc2_yj; // ķ����
    public GameObject npc3_yj; // ��Ʈ
    public GameObject npc4_yj; // �޽�

    public Button noButton1; // �ƴϿ� ��ư ����1
    public Button noButton2; // �ƴϿ� ��ư ����2
    public Button noButton3; // �ƴϿ� ��ư ����3
    public Button noButton4; // �ƴϿ� ��ư ����4

    public Button noButton5; // ���â �ݱ� ��ư ����5

    // �⺻ Ȱ�� ��ư��
    public Button trainingButton_yj; // 1. �Ʒ� �õ� ��ư ����
    public Button campingButton_yj; // 2. ���� �õ� ��ư ����
    public Button findhintButton_yj; // 3. �ܼ� ���� ��ư ����
    public Button laybedButton_yj; // 4. �޽��ϱ� ��ư ����4

    // ��ư ������ ���� ������ �гε� 
    public GameObject trainingUI_yj; // �Ʒ� �� �г�
    public GameObject campingUI_yj; // ���� �� �г�
    public GameObject iaminbedUI_yj; // �޽� UI �г�

    public GameObject resultUI_yj; // ��� UI �г�
    public TextMeshProUGUI resuedit_yj; // result edit text

    // �⺻Ȱ�� ��� �����ߴ��� ���� �ϴ�..�÷��̾ �θ���
   // public PlayerNow_yj nowplayer_yj;
    public PlayerManager_yj playermanager_yj; // �⺻Ȱ�� ��������


    // Start is called before the first frame update
    void Start()
    {
        // ��ư ����
        trainingButton_yj.onClick.AddListener(OntrainButtonClick);
        campingButton_yj.onClick.AddListener(OncampButtonClick);
        findhintButton_yj.onClick.AddListener(OnhintButtonClick);
        laybedButton_yj.onClick.AddListener(OnbedButtonClick);

        noButton1.onClick.AddListener(OnNo1ButtonClick);
        noButton2.onClick.AddListener(OnNo2ButtonClick);
        noButton3.onClick.AddListener(OnNo3ButtonClick);
        noButton4.onClick.AddListener(OnNo4ButtonClick);
        noButton5.onClick.AddListener(OnNo5ButtonClick);
        //newbuttoncon_yj = GetComponent<newbuttoncontrol>(); 
        //nowplayer_yj = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerNow_yj>();

        isbasicdial_yj = false;
        playermanager_yj.playerNow.howtoday_py = 0;
        //nowplayer_yj.howtoday_py = 0; // �Ϸ翡 �⺻Ȱ�� ����ߴ��� ���� 0���� ����
        playermanager_yj.playerNow.howtrain_py = 0;
        //nowplayer_yj.howtrain_py = 0; // ��ü������ �Ʒ� ����ߴ��� ���� 0���� ����
        player = GameObject.FindGameObjectWithTag("Player"); // �±װ� "Player"�� ������Ʈ ã��
        HideUI(); // ������ �� UI �����
    }

    // Update is called once per frame
    void Update()
    {
        CheckNPCInteraction();
        HandleUserInput();
        // �̰��� �ڵ带 �Է��Ͻÿ�. �ʿ��ϴٸ� �ٸ� �޼ҵ带 ä���� �˴ϴ�. 
    }

    void HandleUserInput()
    {
        if (currentNPC != null)
        {
            HandleNPCDialogue_yj(currentNPC); // npc���� ������ ���� ��ȭâ�� ���

            if (Input.GetKeyDown(KeyCode.Space))
            {
                
                //Dial_changyj.SetActive(false);
                if (isbasicdial_yj)
                {
                    //Debug.Log("isbasic true");
                    HandleNPCchoice_yj(currentNPC);
                }
                else
                {
                    //Debug.Log("isbasic false");
                }
            }
        }
    }

    void CheckNPCInteraction()
    {
        float distanceNPC1 = Vector3.Distance(player.transform.position, npc1_yj.transform.position);
        float distanceNPC2 = Vector3.Distance(player.transform.position, npc2_yj.transform.position);
        float distanceNPC3 = Vector3.Distance(player.transform.position, npc3_yj.transform.position);
        float distanceNPC4 = Vector3.Distance(player.transform.position, npc4_yj.transform.position);


        if (distanceNPC1 <= interactionRange)
        {
            currentNPC = npc1_yj;
        }
        else if (distanceNPC2 <= interactionRange)
        {
            currentNPC = npc2_yj;
        }
        else if (distanceNPC3 <= interactionRange)
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
        if(isbasicdial_yj == false)
            Dial_changyj.SetActive(true);
        else if(choiceUI1_yj.activeSelf || choiceUI2_yj.activeSelf || choiceUI3_yj.activeSelf || choiceUI4_yj.activeSelf)
            Dial_changyj.SetActive(false);
        // ���� NPC�� ���� ��ȭ ó��
        if (npc_yjyj == npc1_yj) // �Ʒô���
        {
            dialoguename_yj.text = "�Ʒô���"; // �Ʒô��� �̸� ��� 
            dialogueText_yj.text = "����-������! \n�Ʒ��� �غ�� �Ƴ�? \n[�����̽��ٸ� ���� �Ʒ��� �����ϼ���]"; // �Ʒô��� �⺻ ��� ���
            isbasicdial_yj = true; // �⺻��� ġ�� �ִ� ��
            
        }
        else if (npc_yjyj == npc2_yj) // ķ����
        {            
            dialoguename_yj.text = "ķ���� ����"; // ķ���� �̸� ��� 
            dialogueText_yj.text = "�������~ķ�����Դϴ�. \nķ���� ���Ͽ� ������ �����Ͻ� �ǰ���? \n[�����̽��ٸ� ���� ������ �����ϼ���]"; // ķ���� �⺻ ��� ���
            isbasicdial_yj = true; // �⺻��� ġ�� �ִ� ��
        }
        else if (npc_yjyj == npc3_yj) // �ܼ�
        {
            dialoguename_yj.text = "�ܼ�"; // �ܼ� �̸� ��� 
            dialogueText_yj.text = "�ܼ��� ã�Ҵ�.\n�κ��丮���� ������ Ȯ���� ����.\n\n[�����̽��ٸ� ��������]"; // �ܼ� �⺻ ��� ���                                      
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
        if (npc_yjyj == npc1_yj) // �Ʒô���
        {           
            //Debug.Log("�Ʒô����̶� �����");
            Dial_changyj.SetActive(false);
            isbasicdial_yj = false;
            choiceUI1_yj.SetActive(true);

        }
        else if (npc_yjyj == npc2_yj) // ķ����
        {
            Dial_changyj.SetActive(false);
            isbasicdial_yj = false;
            choiceUI2_yj.SetActive(true);
        }
        else if (npc_yjyj == npc3_yj) // �ܼ�
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
    // ��ư �̺�Ʈ ����
    // �⺻Ȱ��1 : "�Ʒ��Ѵ�" �������� ��
    public void OntrainButtonClick()
    {
        //Debug.Log("1�⺻Ȱ�� Ƚ�� ���� : " + nowplayer_yj.howtoday_py);
        Debug.Log("1�⺻Ȱ�� Ƚ�� ���� : " + playermanager_yj.playerNow.howtoday_py);

        Dial_changyj.SetActive(false);
        choiceUI1_yj.SetActive(false);
        
        // ���� ���
        playermanager_yj.IncreaseTrainingCount();// �Ϸ� �Ʒ� Ȱ�� Ƚ�� 1 ����
        //nowplayer_yj.howtrain_py ++; // �Ʒ� Ƚ�� ���� 1 ����

        playermanager_yj.IncreaseTiredness(10);
        playermanager_yj.playerNow.tired_py += 10; // �Ƿε� 10 ����

        //nowplayer_yj.tired_py +=10; // �Ƿε� 10 ����

        playermanager_yj.IncreaseDailyActivityCount();
        // nowplayer_yj.howtoday_py++; // �Ϸ� �⺻ Ȱ�� ���� Ƚ�� 1 ����

        resuedit_yj.text = $"�⺻Ȱ�� Ƚ��: {playermanager_yj.playerNow.howtoday_py} / 3"; // �⺻ Ȱ�� �ؽ�Ʈ ������Ʈ
        //resuedit_yj.text = $"�⺻Ȱ�� Ƚ��: {nowplayer_yj.howtoday_py} / 3"; // �⺻ Ȱ�� �ؽ�Ʈ ������Ʈ
        //Debug.Log("�Ʒ� Ƚ�� ���� : " + nowplayer_yj.howtrain_py);
        resultUI_yj.SetActive(true);
    }

    // �⺻Ȱ��2 : �����Ѵ� ���� ��
    public void OncampButtonClick()
    {
        //Debug.Log("2�⺻Ȱ�� Ƚ�� ���� : " + nowplayer_yj.howtoday_py);
        Debug.Log("2�⺻Ȱ�� Ƚ�� ���� : " + playermanager_yj.playerNow.howtoday_py);
        Dial_changyj.SetActive(false);
        choiceUI2_yj.SetActive(false); // ���� UI ��Ȱ��ȭ

        // ���� ���
        playermanager_yj.IncreaseTeamPower(1);// �Ϸ� ���� Ȱ�� Ƚ�� 1 ����
        //nowplayer_yj.team_py++; // ���պ��� 1 ����
        playermanager_yj.IncreaseDailyActivityCount();
        //nowplayer_yj.howtoday_py++; // �Ϸ� �⺻ Ȱ�� ���� Ƚ�� 1 ����

        // ���â ������Ʈ
        resuedit_yj.text = $"�⺻Ȱ�� Ƚ��: {playermanager_yj.playerNow.howtoday_py} / 3"; // �⺻ Ȱ�� �ؽ�Ʈ ������Ʈ
        //resuedit_yj.text = $"�⺻Ȱ�� Ƚ��: {nowplayer_yj.howtoday_py} / 3"; // �⺻ Ȱ�� �ؽ�Ʈ ������Ʈ
        resultUI_yj.SetActive(true);
   
       
    }
    // �⺻Ȱ��3 : �ܼ� ���ڴ� ���� ��
    public void OnhintButtonClick()
    {
        //Debug.Log("3�⺻Ȱ�� Ƚ�� ���� : " + nowplayer_yj.howtoday_py);
        Debug.Log("3�⺻Ȱ�� Ƚ�� ���� : " + playermanager_yj.playerNow.howtoday_py);
        choiceUI3_yj.SetActive(false);
        SceneManager.LoadScene("InventoryMain"); // �κ��丮 ������ �̵�
        // ã�� �ܼ� ������ �� �� �ø�. �̰� �κ��丮�� ���� �Ŀ� �����ؾ� �ҵ�
    }
    // �⺻Ȱ��4 : �޽� ���ϰڴ� ���� ��
    public void OnbedButtonClick()
    {
        Debug.Log("4�⺻Ȱ�� Ƚ�� ���� : " + playermanager_yj.playerNow.howtoday_py);
        //Debug.Log("4�⺻Ȱ�� Ƚ�� ���� : " + nowplayer_yj.howtoday_py);
        Dial_changyj.SetActive(false);
        choiceUI4_yj.SetActive(false);
        resultUI_yj.SetActive(true);

        playermanager_yj.IncreaseDailyActivityCount();
        //nowplayer_yj.howtoday_py++; // �Ϸ� �⺻ Ȱ�� ���� Ƚ�� 1 ����
        // ���â ������Ʈ
        resuedit_yj.text = $"�⺻Ȱ�� Ƚ��: {playermanager_yj.playerNow.howtoday_py} / 3"; // �⺻ Ȱ�� �ؽ�Ʈ ������Ʈ

        //resuedit_yj.text = $"�⺻Ȱ�� Ƚ��: {nowplayer_yj.howtoday_py} / 3"; // �⺻ Ȱ�� �ؽ�Ʈ ������Ʈ
        resultUI_yj.SetActive(true);
    }

    public void OnNo1ButtonClick()
    {
        choiceUI1_yj.SetActive(false); // �Ʒ� UI ����â ��Ȱ��ȭ
        isbasicdial_yj = false;
    }
    public void OnNo2ButtonClick()
    {
        choiceUI2_yj.SetActive(false); // ���� UI ����â ��Ȱ��ȭ
        isbasicdial_yj = false;
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
        isbasicdial_yj = false;
    }


    void HideUI()
{
    choiceUI1_yj.SetActive(false);
    choiceUI2_yj.SetActive(false);
    choiceUI3_yj.SetActive(false);
    choiceUI4_yj.SetActive(false);
    Dial_changyj.SetActive(false);
    resultUI_yj.SetActive(false);
}
    void EndDialogue()
    {
        Dial_changyj.SetActive(false); // ��ȭ UI �����
        //isbasicdial_yj = false; // ��ȭ ���� ����
    }

    public void ActivateChoiceUI1()
    {
        if (Dial_changyj.activeSelf)
        {
            Dial_changyj.SetActive(false);
            choiceUI1_yj.SetActive(true);
            choiceUI2_yj.SetActive(true);
            choiceUI3_yj.SetActive(true);
            choiceUI4_yj.SetActive(true);
            resultUI_yj.SetActive(false);
            isbasicdial_yj = false;
            isResultActive_yj = false;
        }
    }
}