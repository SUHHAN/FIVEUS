using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting; // TextMeshPro ���ӽ����̽� �߰�

//
public class talkwithjjang_yj : MonoBehaviour
{
    public GameObject choiceUI_yj; // ���� UI �г�
    public GameObject dialogueUI_yj; // ��ȭ UI �г�
    public GameObject resultUI_yj; // ��� UI �г�
    public TextMeshProUGUI dialogueText; // ��� �ؽ�Ʈ UI ����
    public TextMeshProUGUI resultText; // ��� �ؽ�Ʈ UI ����
    public Button yesButton; // �� ��ư ����
    public Button noButton; // �ƴϿ� ��ư ����

    // �⺻ Ȱ�� ��ư��
    public Button trainingButton_yj; // 1.�Ʒ� �õ� ��ư ����
    public Button togetherButton_yj; // 2.���� �õ� ��ư ����
    public Button findhintButton_yj; // 3.��Ʈ Ȯ�� ��ư ����

    public float interactionRange = 3.0f; // ��ȣ�ۿ� �Ÿ�
    private GameObject player; // �÷��̾� ������Ʈ
    private bool isTalking = false; // ��ȭ ������ ����

    private ProDialogue_yj whatdial_yj;
    // �⺻ Ȱ�� ������ ��Ʈ��Ʈ ������ ���� ��� ó��(������ 5���ۿ� ����)
    // ���̵� ���� ���� : 6000������� ������
    // 6001 : �Ʒô���, 6002 : ������, 6003 : �ܼ�

    // �⺻Ȱ��1 : �Ʒô��� �⺻ ���1
    ProDialogue_yj serif1_1 = new ProDialogue_yj(6001, "�Ʒô���", "����-������! �Ʒ��� �غ�� �Ƴ�?");
    // �⺻Ȱ��1 : �Ʒô��� �⺻ ���2
    ProDialogue_yj serif1_2 = new ProDialogue_yj(6001, "�Ʒô���", "�ȵǸ� �ɶ�����! �Ʒ� �����̴�!");

    // �⺻Ȱ��2 : ������ �⺻ ���1
    ProDialogue_yj serif2_1 = new ProDialogue_yj(6002, "������", "��ġ�� ��� ������� �״´�! \n�����Ʒ� �����̴�!!");
    // �⺻Ȱ��2 : ������ �⺻ ���2
    ProDialogue_yj serif2_2 = new ProDialogue_yj(6002, "������", "3 -1 = 0! �츮�� �ϳ���!  \n�����Ʒ� �����̴�!!");

    // �⺻Ȱ��3 : �ܼ� ������ �� �⺻ ���
    ProDialogue_yj serif3 = new ProDialogue_yj(6003, "�ܼ�", "�ܼ��� ã�Ҵ�. ������ ���캸��.");


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // �±װ� "Player"�� ������Ʈ ã��
        choiceUI_yj.SetActive(false); // ������ �� ���� UI ��Ȱ��ȭ       
        dialogueUI_yj.SetActive(false); // ������ �� ��ȭ UI ��Ȱ��ȭ
        resultUI_yj.SetActive(false); // ������ �� ��� UI ��Ȱ��ȭ

        //talkButton.onClick.AddListener(OnTalkButtonClick); // ��ȭ�ϱ� ��ư Ŭ�� �̺�Ʈ ����
        yesButton.onClick.AddListener(OnYesButtonClick); // �� ��ư Ŭ�� �̺�Ʈ ����
        noButton.onClick.AddListener(OnNoButtonClick); // �ƴϿ� ��ư Ŭ�� �̺�Ʈ ����
    }



    // Update is called once per frame
    void Update()
    {
        if (isTalking)
        {
            if ((isTalking && Input.GetKeyDown(KeyCode.Return))) // ���� Ű �Է� ����
            {
                EndDialogue(); // ��ȭ ����
            }
            //return;
        }

        float distance = Vector3.Distance(player.transform.position, transform.position); // �÷��̾�� NPC �� �Ÿ� ���
        if (distance <= interactionRange) // ��ȣ�ۿ� �Ÿ� ���� �ִ��� Ȯ��
        { 
            if (Input.GetKeyDown(KeyCode.Return)) // ���� Ű �Է� ����
            {
                // ��� ������ �������� ���� �⺻��簡 ��(TalkManager_yj ����)
                switch (whatdial_yj.id)
                {
                    // �⺻Ȱ��1 : �Ʒô����� ��(�Ʒ�)
                    case 6001:
                        dialogueText.text = serif1_1.line; // �����ϰ� �Ʒô��� �⺻ ��� ���
                        dialogueText.text = ""; // ��� �ؽ�Ʈ �ʱ�ȭ
                        break;
                    // �⺻Ȱ��2 : �������� ��(����)
                    case 6002:
                        dialogueText.text = serif2_1.line; // �����ϰ� ������ �⺻ ��� ���
                        dialogueText.text = ""; // ��� �ؽ�Ʈ �ʱ�ȭ
                        break;
                    // �⺻Ȱ��3 : �ܼ��� ��(�ܼ�)
                    case 6003:
                        dialogueText.text = serif3.line; // ��Ʈ ��� ���
                        dialogueText.text = ""; // ��� �ؽ�Ʈ �ʱ�ȭ
                        break;
                }

                ShowChoiceUI_yj(); // ������ ��뿡 ���� �ٸ� ���� UI ǥ��
                // ���� ���� UI�� ����ϴµ�, ������ �ٸ�. yes,no�� ����.
                // �Ʒô���(6001)�� ������ ���� "�Ʒ��Ͻðڽ��ϱ�?" ������ ��
                // ������(6002)�� ������ ���� "���� �Ʒ��� �����Ͻðڽ��ϱ�?" ������ ��
                // �Ʒô���(6001)�� ������ ���� "�ܼ��� �����Ͻðڰڽ��ϱ�?" ������ ��
            }
        }
        else
        {
            choiceUI_yj.SetActive(false); // ���� UI �����
        }
    }

    void ShowChoiceUI_yj()
    {
        choiceUI_yj.SetActive(true); // ���� UI Ȱ��ȭ

        switch (whatdial_yj.id)
        {
            // �⺻Ȱ��1 : �Ʒô����ϋ�(�Ʒ�)
            case 6001:
                choiceUI_yj.SetActive(true); // ���� UI Ȱ��ȭ
                break;

            // �⺻Ȱ��2 : �������϶�(����)
            case 6002:
                dialogueText.text = ""; // ��� �ؽ�Ʈ �ʱ�ȭ
                break;

            case 6003:
                dialogueText.text = ""; // ��� �ؽ�Ʈ �ʱ�ȭ
                break;
        }        
    }

    public void OnTalkButtonClick()
    {
        choiceUI_yj.SetActive(true); // ���� UI Ȱ��ȭ
        //choiceUI.SetActive(false); // ���� UI �����
        //dialogueUI.SetActive(true); // ��ȭ UI Ȱ��ȭ
        dialogueText.text = "�����Ͻðڽ��ϱ�?"; // ��� ǥ��
        isTalking = true; // ��ȭ ���� ����
    }


    public void OnYesButtonClick()
    {
        // TalkManager_yj Ŭ������ IncreaseTeamPower ȣ��
        TalkManager_yj talkManager = FindObjectOfType<TalkManager_yj>();
        if (talkManager != null)
        {
            //talkManager.IncreaseTeamPower(10); // ���÷� 10��ŭ �� �Ŀ� ����
        }
        else
        {
            Debug.LogError("TalkManager_yj not found in the scene.");
        }


        //AttemptPersuasion(); // ���� �õ�
        dialogueText.text = "������ �����ߴ�"; // ������ ó��
        // TalkManager_yj���� ó���� �������� ����
        isTalking = false; // ��ȭ ���� ����
        choiceUI_yj.SetActive(false); // ���� UI �����
    }

    public void OnNoButtonClick()
     {
        // persuadeUI.SetActive(false); // ���� UI �����
        dialogueText.text = "������ �������� �ʽ��ϴ�"; // ������ ó��
        isTalking = false; // ��ȭ ���� ����
        choiceUI_yj.SetActive(false); // ���� UI �����
    }


    /*void HideChoices()
    {
        choiceUI.SetActive(false); // ���� UI �����
        dialogueUI.SetActive(false); // ��ȭ UI �����
        persuadeUI.SetActive(false); // ���� UI �����
    }*/

    void EndDialogue()
    {
        //dialogueUI.SetActive(false); // ��ȭ UI �����
        isTalking = false; // ��ȭ ���� ����
        choiceUI_yj.SetActive(false); // ���� UI ����
    }

}