using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting; // TextMeshPro ���ӽ����̽� �߰�
using UnityEngine.SceneManagement;

// �⺻ Ȱ�� �����ϴ� ��ũ��Ʈ
public class talkwithjjang_yj : MonoBehaviour
{
    // �⺻ Ȱ�� �гε�
    public GameObject choiceUI1_yj; // �⺻Ȱ��1 UI �г�
    public GameObject choiceUI2_yj; // �⺻Ȱ��2 UI �г�
    public GameObject choiceUI3_yj; // �⺻Ȱ��3 UI �г�
    public GameObject choiceUI4_yj; // �⺻Ȱ��4 UI �г�

    public GameObject trainingUI_yj; // �Ʒ� �� �г�
    public GameObject campingUI_yj; // ���� �� �г�
    public GameObject iaminbedUI_yj; // �޽� UI �г�
    public GameObject resultUI_yj; // ��� UI �г�

    public GameObject Dial_changyj; // �⺻��� ��� ��ȭâ
    public TextMeshProUGUI dialoguename_yj; // name text
    public TextMeshProUGUI dialogueText_yj; // line text

    public Button noButton1; // �ƴϿ� ��ư ����1
    public Button noButton2; // �ƴϿ� ��ư ����2
    public Button noButton3; // �ƴϿ� ��ư ����3
    public Button noButton4; // �ƴϿ� ��ư ����4

    // �⺻ Ȱ�� ��ư��
    public Button trainingButton_yj; // 1. �Ʒ� �õ� ��ư ����
    public Button campingButton_yj; // 2. ���� �õ� ��ư ����
    public Button findhintButton_yj; // 3. �ܼ� ���� ��ư ����
    public Button laybedButton_yj; // 4. �޽��ϱ� ��ư ����
    public float interactionRange = 1.5f; // ��ȣ�ۿ� �Ÿ�

    public GameObject player; // �÷��̾� ������Ʈ
    private GameObject currentNPC; // ���� ��ȣ�ۿ��ϴ� NPC ���� ����
    public GameObject npc1_yj; // �Ʒô���
    public GameObject npc2_yj; // ķ����
    public GameObject npc3_yj; // ��Ʈ
    public GameObject npc4_yj; // �޽�

    // �⺻Ȱ�� ��� �����ߴ��� ���� �ϴ�..�ι��� �θ���
    public PlayerNow_yj nowplayer_yj;

    // Start is called before the first frame update
    void Start()
    {
        nowplayer_yj = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerNow_yj>();

        nowplayer_yj.howtoday_py = 0; // �Ϸ翡 �⺻Ȱ�� ����ߴ��� ���� 0���� ����
        nowplayer_yj.howtrain_py = 0; // ��ü������ �Ʒ� ����ߴ��� ���� 0���� ����
        player = GameObject.FindGameObjectWithTag("Player"); // �±װ� "Player"�� ������Ʈ ã��
        HideUI(); // ������ �� UI �����
    }

    // Update is called once per frame
    void Update()
    {
        CheckNPCInteraction(); // NPC���� ��ȣ�ۿ� üũ
        // ����Ű �Է� ����
        if (currentNPC != null && Input.GetKeyDown(KeyCode.Return))
        {
            HandleNPCDialogue_yj(currentNPC);
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

        //if (currentNPC != null && nowplayer_yj.howtoday_py<3)
        if (currentNPC != null) // ��ȣ�ۿ� �Ÿ� ���� �ִ���, �⺻ Ȱ�� Ƚ�� Ȯ��
        {
            if (Input.GetKeyDown(KeyCode.Return)) // ���� Ű �Է� ����
            {
                HandleNPCDialogue_yj(currentNPC);
            }
        }
        else
        {
            HideUI();
        }
    }

    void HandleNPCDialogue_yj(GameObject npc_yjyj)
    {
        Dial_changyj.SetActive(true);
        // ���� NPC�� ���� ��ȭ ó��
        if (npc_yjyj == npc1_yj) // �Ʒô���
        {
            dialoguename_yj.text = "�Ʒô���"; // �Ʒô��� �̸� ��� 
            dialogueText_yj.text = "����-������! \n�Ʒ��� �غ�� �Ƴ�?"; // �Ʒô��� �⺻ ��� ���
            ShowChoice1UI_yj();
            

        }
        else if (npc_yjyj == npc2_yj) // ķ����
        {            
            dialoguename_yj.text = "ķ����"; // ķ���� �̸� ��� 
            dialogueText_yj.text = "ķ���� ���Ͽ� ������ �����Ͻ� �ǰ���?"; // ķ���� �⺻ ��� ���                                      
            ShowChoice2UI_yj(); // ķ�� ���� UI ǥ��
        }
        else if (npc_yjyj == npc3_yj) // �ܼ�
        {
            dialoguename_yj.text = "�ܼ�"; // �ܼ� �̸� ��� 
            dialogueText_yj.text = "�ܼ��� ã�Ҵ�.\n�κ��丮���� ������ Ȯ���� ����."; // �ܼ� �⺻ ��� ���                                      
            ShowChoice3UI_yj(); // ķ�� ���� UI ǥ��
        }
        else if (npc_yjyj == npc4_yj) // ħ��
        {
            dialoguename_yj.text = "ħ��"; // �Ʒô��� �̸� ��� 
            dialogueText_yj.text = "�ƴ��� �� ���� ħ���.\n����� �޽��� ���� ����."; // �Ʒô��� �⺻ ��� ���                                      
            ShowChoice4UI_yj(); // ķ�� ���� UI ǥ��
        }

        // �Ʒô���(6001)�� ������ ���� "�Ʒ��Ͻðڽ��ϱ�?" ������ ��
        // ������(6002)�� ������ ���� "������ �����Ͻðڽ��ϱ�?" ������ ��
        // �ܼ�(6003)�� ������ ���� "�ܼ��� �����Ͻðڽ��ϱ�?" ������ ��
        // ħ��(6004)�� ������ ���� "�޽��� ���Ͻðڽ��ϱ�?" ������ ��
    }




void ShowChoice1UI_yj()
{        
        choiceUI1_yj.SetActive(true);
}

void ShowChoice2UI_yj()
{
    HideUI();
    choiceUI2_yj.SetActive(true);
}

void ShowChoice3UI_yj()
{
    HideUI();
    choiceUI3_yj.SetActive(true);
}

void ShowChoice4UI_yj()
{
    HideUI();
    choiceUI4_yj.SetActive(true);
}

void HideUI()
{
    choiceUI1_yj.SetActive(false);
    choiceUI2_yj.SetActive(false);
    choiceUI3_yj.SetActive(false);
    choiceUI4_yj.SetActive(false);
    Dial_changyj.SetActive(false);
}
   
    void DisableResultUI_yj()
    {
        resultUI_yj.SetActive(false);
        
    }
    void DisabletrainUI_yj()
    {
        trainingUI_yj.SetActive(false);
    }
    void DisablecampUI_yj()
    {
        campingUI_yj.SetActive(false);
    }
    void DisablebedUI_yj()
    {
        iaminbedUI_yj.SetActive(false);
    }
    // �⺻Ȱ��1 : "�Ʒ��Ѵ�" �������� ��
    public void OntrainButtonClick()
    {
        choiceUI1_yj.SetActive(false); // ���� UI ��Ȱ��ȭ

        trainingUI_yj.SetActive(true);// �Ʒ� UI ǥ��(3�ʰ� ����)
        Invoke("DisabletrainUI_yj", 3f); // 3�� �Ŀ� �Ʒ� UI�� �ڵ����� ��Ȱ��ȭ ó���ϴ� �޼��� 
        resultUI_yj.SetActive(true);// ��� â ǥ�� (����UI, 1,2 ���� ���)
        nowplayer_yj.howtrain_py++;// �Ʒú��� 1 ����
        nowplayer_yj.howtoday_py++;// �Ϸ� �⺻ Ȱ�� ���� Ƚ�� 1 ����        
        Invoke("DisableResultUI_yj", 3f); // 3�� �Ŀ� ��� UI�� �ڵ����� ��Ȱ��ȭ ó���ϴ� �޼��� ȣ��
    }
    // �⺻Ȱ��2 : �����Ѵ� ���� ��
    public void OncampButtonClick()
    {
        choiceUI2_yj.SetActive(false); // ���� UI ��Ȱ��ȭ
        campingUI_yj.SetActive(true);// ���� UI ǥ��(3�ʰ� ����)
        Invoke("DisablecampUI_yj", 3f); // 3�� �Ŀ� ���� UI�� �ڵ����� ��Ȱ��ȭ ó���ϴ� �޼��� 
        resultUI_yj.SetActive(true);// ��� â ǥ�� (����UI, 1,2 ���� ���)
        nowplayer_yj.team_py++; // ���պ��� 1 ����
        nowplayer_yj.howtoday_py++; // �Ϸ� �⺻ Ȱ�� ���� Ƚ�� 1 ����
        Invoke("DisableResultUI_yj", 3f); // 3�� �Ŀ� ��� UI�� �ڵ����� ��Ȱ��ȭ ó���ϴ� �޼��� ȣ��
    }
    // �⺻Ȱ��3 : �ܼ� ���ڴ� ���� ��
    public void OnhintButtonClick()
    { 
        choiceUI3_yj.SetActive(false);
        SceneManager.LoadScene("InventoryMain"); // �κ��丮 ������ �̵�
        // ã�� �ܼ� ������ �� �� �ø�. �̰� �κ��丮�� ���� �Ŀ� �����ؾ� �ҵ�
    }
    // �⺻Ȱ��4 : �޽� ���ϰڴ� ���� ��
    public void OnbedButtonClick()
    {
        choiceUI4_yj.SetActive(false);
        iaminbedUI_yj.SetActive(true); // �޽� UI ����
        
    }
    public void OnNoButtonClick()
     {
        // �̰� �� �����ִ��� �������� ��Ȱ��ȭ ���� �ؾ� �ϳ�? 
        choiceUI1_yj.SetActive(false); // �Ʒ� UI ����â ��Ȱ��ȭ
        choiceUI2_yj.SetActive(false); // ���� UI ����â ��Ȱ��ȭ
        choiceUI3_yj.SetActive(false); // �ܼ� UI ����â ��Ȱ��ȭ
        choiceUI4_yj.SetActive(false); // �޽� UI ����â ��Ȱ��ȭ
    }

}