using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class newbuttoncontrol : MonoBehaviour
{
    // ��ư �̺�Ʈ ��ũ��Ʈ : ���� �̰� ���
    public talkwithjjang_yj talkScript_yj;
    public Button noButton1; // �ƴϿ� ��ư ����1
    public Button noButton2; // �ƴϿ� ��ư ����2
    public Button noButton3; // �ƴϿ� ��ư ����3
    public Button noButton4; // �ƴϿ� ��ư ����4

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

    private bool isworking_yj = false; // ���� ���������� �˾ƺ��� �Լ�
    private bool isworking_yj2 = false; // ���� ���������� �˾ƺ��� �Լ�

    public void Start()
    {

        //talkScript_yj = GetComponent<talkwithjjang_yj>();

        if (talkScript_yj == null)
        {
            Debug.LogError("talkwithjjang_yj component not found or not initialized properly.");
            return;
        }

        trainingButton_yj.onClick.AddListener(OntrainButtonClick);
        campingButton_yj.onClick.AddListener(OncampButtonClick);
        findhintButton_yj.onClick.AddListener(OnhintButtonClick);
        laybedButton_yj.onClick.AddListener(OnbedButtonClick);

        noButton1.onClick.AddListener(OnNo1ButtonClick);
        noButton2.onClick.AddListener(OnNo2ButtonClick);
        noButton3.onClick.AddListener(OnNo3ButtonClick);
        noButton4.onClick.AddListener(OnNo4ButtonClick);
    }

    // �⺻Ȱ��1 : "�Ʒ��Ѵ�" �������� ��
    public void OntrainButtonClick()
    {
        talkScript_yj.choiceUI1_yj.SetActive(false); // ���� UI ��Ȱ��ȭ
                                                     // �����̽��ٸ� ������ UI ��ȯ
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (trainingUI_yj.activeSelf)
            {
                trainingUI_yj.SetActive(false);
                resultUI_yj.SetActive(true);
                // ��� ó�� ���� (�Ʒ� ���� ���� ��)
                talkScript_yj.nowplayer_yj.howtrain_py++;
                talkScript_yj.nowplayer_yj.howtoday_py++;
            }
            else if (resultUI_yj.activeSelf)
            {
                resultUI_yj.SetActive(false);
            }
        }
        /*
        if (!isworking_yj)
        {
            resultUI_yj.SetActive(false);
            trainingUI_yj.SetActive(true);// �Ʒ� UI ǥ��(3�ʰ� ����)
            Invoke("DisabletrainUI_yj", 3f); // 3�� �Ŀ� �Ʒ� UI�� �ڵ����� ��Ȱ��ȭ ó���ϴ� �޼��� 
            isworking_yj2 = true;
        }
        if (isworking_yj2)
        {
            trainingUI_yj.SetActive(false);// �Ʒ� UI ��ǥ��
            resultUI_yj.SetActive(true);// ��� â ǥ�� (����UI, 1,2 ���� ���)
            talkScript_yj.nowplayer_yj.howtrain_py++;// �Ʒú��� 1 ����
            talkScript_yj.nowplayer_yj.howtoday_py++;// �Ϸ� �⺻ Ȱ�� ���� Ƚ�� 1 ����        
            Invoke("DisableResultUI_yj", 3f); // 3�� �Ŀ� ��� UI�� �ڵ����� ��Ȱ��ȭ ó���ϴ� �޼��� ȣ��
        }
        isworking_yj2  = false;*/
    }
    // �⺻Ȱ��2 : �����Ѵ� ���� ��
    public void OncampButtonClick()
    {
        talkScript_yj.choiceUI2_yj.SetActive(false); // ���� UI ��Ȱ��ȭ
        campingUI_yj.SetActive(true);// ���� UI ǥ��(3�ʰ� ����)
        Invoke("DisablecampUI_yj", 3f); // 3�� �Ŀ� ���� UI�� �ڵ����� ��Ȱ��ȭ ó���ϴ� �޼��� 
        resultUI_yj.SetActive(true);// ��� â ǥ�� (����UI, 1,2 ���� ���)
        talkScript_yj.nowplayer_yj.team_py++; // ���պ��� 1 ����
        talkScript_yj.nowplayer_yj.howtoday_py++; // �Ϸ� �⺻ Ȱ�� ���� Ƚ�� 1 ����
        Invoke("DisableResultUI_yj", 3f); // 3�� �Ŀ� ��� UI�� �ڵ����� ��Ȱ��ȭ ó���ϴ� �޼��� ȣ��
    }
    // �⺻Ȱ��3 : �ܼ� ���ڴ� ���� ��
    public void OnhintButtonClick()
    {
        talkScript_yj.choiceUI3_yj.SetActive(false);
        SceneManager.LoadScene("InventoryMain"); // �κ��丮 ������ �̵�
        // ã�� �ܼ� ������ �� �� �ø�. �̰� �κ��丮�� ���� �Ŀ� �����ؾ� �ҵ�
    }
    // �⺻Ȱ��4 : �޽� ���ϰڴ� ���� ��
    public void OnbedButtonClick()
    {
        talkScript_yj.choiceUI4_yj.SetActive(false);
        iaminbedUI_yj.SetActive(true); // �޽� UI ����

    }

    public void OnNo1ButtonClick()
    {
        talkScript_yj.choiceUI1_yj.SetActive(false); // �Ʒ� UI ����â ��Ȱ��ȭ 
    }
    public void OnNo2ButtonClick()
    {
        talkScript_yj.choiceUI2_yj.SetActive(false); // ���� UI ����â ��Ȱ��ȭ
    }
    public void OnNo3ButtonClick()
    {
        talkScript_yj.choiceUI3_yj.SetActive(false); // �ܼ� UI ����â ��Ȱ��ȭ
    }
    public void OnNo4ButtonClick()
    {
        talkScript_yj.choiceUI4_yj.SetActive(false); // �޽� UI ����â ��Ȱ��ȭ
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
}
