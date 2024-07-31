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
    //private eightbuttons_yj eightbuttons_yjyj;
    private bool isbasicdial_yj = false; // ��� ġ�� �ִ��� ����

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
        if (isbasicdial_yj)
        {
            if (Input.GetKeyDown(KeyCode.Space)) // �����̽� Ű �Է� ����
            {
                EndDialogue(); // ��ȭ ����
            }
            return;
        }

        CheckNPCInteraction(); // NPC���� ��ȣ�ۿ� üũ
        // ����Ű �Է� ����
        if (currentNPC != null && Input.GetKeyDown(KeyCode.Return))
        {
            HandleNPCDialogue_yj(currentNPC);
        }
        if (!isbasicdial_yj && Input.GetKeyDown(KeyCode.Space))
        {
            if (currentNPC == npc1_yj) {
                //  ShowChoice1UI_yj();
                choiceUI1_yj.SetActive(true);
            }
            else if (currentNPC == npc2_yj) {
                // ShowChoice2UI_yj();
                choiceUI2_yj.SetActive(true);
            }
                
            else if (currentNPC == npc3_yj) {
                // ShowChoice3UI_yj();
                choiceUI3_yj.SetActive(true);
            }

            else if (currentNPC == npc4_yj) {
                // ShowChoice4UI_yj();
                choiceUI4_yj.SetActive(true);
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

        //if (currentNPC != null && nowplayer_yj.howtoday_py<3)
        if (currentNPC != null) // ��ȣ�ۿ� �Ÿ� ���� �ִ���, �⺻ Ȱ�� Ƚ�� Ȯ��
        {
            if (Input.GetKeyDown(KeyCode.Return)) // ���� Ű �Է� ����
            {
                HandleNPCDialogue_yj(currentNPC); // npc���� �ٸ��� �˷��ֱ�
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
            dialogueText_yj.text = "����-������! \n�Ʒ��� �غ�� �Ƴ�? \n [�����̽��ٸ� ���� �Ʒ��� �����ϼ���]"; // �Ʒô��� �⺻ ��� ���
            isbasicdial_yj = true; // �⺻��� ġ�� �ִ� ��
            
        }
        else if (npc_yjyj == npc2_yj) // ķ����
        {            
            dialoguename_yj.text = "ķ���� ����"; // ķ���� �̸� ��� 
            dialogueText_yj.text = "�������~ķ�����Դϴ�. \nķ���� ���Ͽ� ������ �����Ͻ� �ǰ���? \n [�����̽��ٸ� ���� ������ �����ϼ���]"; // ķ���� �⺻ ��� ���
            isbasicdial_yj = true; // �⺻��� ġ�� �ִ� ��
            //ShowChoice2UI_yj(); // ķ�� ���� UI ǥ��
        }
        else if (npc_yjyj == npc3_yj) // �ܼ�
        {
            dialoguename_yj.text = "�ܼ�"; // �ܼ� �̸� ��� 
            dialogueText_yj.text = "�ܼ��� ã�Ҵ�.\n�κ��丮���� ������ Ȯ���� ����. \n [�����̽��ٸ� ���� �κ��丮 ������ �̵��ϼ���]"; // �ܼ� �⺻ ��� ���                                      
            isbasicdial_yj = true; // �⺻��� ġ�� �ִ� ��
            //ShowChoice3UI_yj(); // ķ�� ���� UI ǥ��
        }
        else if (npc_yjyj == npc4_yj) // ħ��
        {
            dialoguename_yj.text = "ħ��"; // �Ʒô��� �̸� ��� 
            dialogueText_yj.text = "�ƴ��� �� ���� ħ���.\n����� �޽��� ���� ����. \n [�����̽��ٸ� ���� �޽��� �����ϼ���]"; // �Ʒô��� �⺻ ��� ���                                      
            isbasicdial_yj = true; // �⺻��� ġ�� �ִ� ��
            //ShowChoice4UI_yj(); // ķ�� ���� UI ǥ��
        }
    }

void HideUI()
{
    choiceUI1_yj.SetActive(false);
    choiceUI2_yj.SetActive(false);
    choiceUI3_yj.SetActive(false);
    choiceUI4_yj.SetActive(false);
    Dial_changyj.SetActive(false);
}
    void EndDialogue()
    {
        Dial_changyj.SetActive(false); // ��ȭ UI �����
        isbasicdial_yj = false; // ��ȭ ���� ����
    }

}