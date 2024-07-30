using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class eightbuttons_yj : MonoBehaviour
{
    /*
    public talkwithjjang_yj talkScript_yj;
    // �⺻ Ȱ�� ��ư��
    public Button trainingButton_yj; // 1. �Ʒ� �õ� ��ư ����
    public Button campingButton_yj; // 2. ���� �õ� ��ư ����
    public Button findhintButton_yj; // 3. �ܼ� ���� ��ư ����
    public Button laybedButton_yj; // 4. �޽��ϱ� ��ư ����
    public Button noButton1; // �ƴϿ� ��ư ����1
    public Button noButton2; // �ƴϿ� ��ư ����2
    public Button noButton3; // �ƴϿ� ��ư ����3
    public Button noButton4; // �ƴϿ� ��ư ����4

    public void Start()
    {
        // talkwithjjang_yj ��ũ��Ʈ�� ����� ���� ������Ʈ���� talkwithjjang_yj ��ũ��Ʈ ��������
        talkScript_yj = GetComponent<talkwithjjang_yj>();

        if (talkScript_yj == null)
        {
            Debug.LogError("talkwithjjang_yj component not found or not initialized properly.");
            return;
        }


        // �� ��ư�� Ŭ�� �̺�Ʈ�� �߰��մϴ�.
        trainingButton_yj.onClick.AddListener(OnTrainButtonClick);
        campingButton_yj.onClick.AddListener(OnCampButtonClick);
        findhintButton_yj.onClick.AddListener(OnHintButtonClick);
        laybedButton_yj.onClick.AddListener(OnBedButtonClick);

        noButton1.onClick.AddListener(OnNoButtonClick); // �ƴϿ�1 ��ư Ŭ�� �̺�Ʈ ����
        noButton2.onClick.AddListener(OnNoButtonClick); // �ƴϿ�2 ��ư Ŭ�� �̺�Ʈ ����
        noButton3.onClick.AddListener(OnNoButtonClick); // �ƴϿ�3 ��ư Ŭ�� �̺�Ʈ ����
        noButton4.onClick.AddListener(OnNoButtonClick); // �ƴϿ�4 ��ư Ŭ�� �̺�Ʈ ����

    }
    // �Ʒ� ��ư Ŭ�� ó��
    public void OnTrainButtonClick()
    {
        // ������ talkwithjjang_yj ��ũ��Ʈ�� OntrainButtonClick �޼��� ������ ���⿡ �ֽ��ϴ�.
        talkScript_yj.OntrainButtonClick();
    }

    // ���� ��ư Ŭ�� ó��
    public void OnCampButtonClick()
    {
        talkScript_yj.OncampButtonClick();
    }

    // �ܼ� ���� ��ư Ŭ�� ó��
    public void OnHintButtonClick()
    {
        talkScript_yj.OnhintButtonClick();
    }

    // �޽��ϱ� ��ư Ŭ�� ó��
    public void OnBedButtonClick()
    {
        talkScript_yj.OnbedButtonClick();
    }
    public void OnNoButtonClick()
    {
        talkScript_yj.OnNoButtonClick();
    }
    */

    public talkwithjjang_yj talkScript_yj;
    public Button trainingButton_yj;
    public Button campingButton_yj;
    public Button findhintButton_yj;
    public Button laybedButton_yj;
    public Button noButton1;
    public Button noButton2;
    public Button noButton3;
    public Button noButton4;

    public void Start()
    {
        talkScript_yj = GetComponent<talkwithjjang_yj>();

        if (talkScript_yj == null)
        {
            Debug.LogError("talkwithjjang_yj component not found or not initialized properly.");
            return;
        }

        trainingButton_yj.onClick.AddListener(OnTrainButtonClick);
        campingButton_yj.onClick.AddListener(OnCampButtonClick);
        findhintButton_yj.onClick.AddListener(OnHintButtonClick);
        laybedButton_yj.onClick.AddListener(OnBedButtonClick);

        noButton1.onClick.AddListener(OnNoButtonClick);
        noButton2.onClick.AddListener(OnNoButtonClick);
        noButton3.onClick.AddListener(OnNoButtonClick);
        noButton4.onClick.AddListener(OnNoButtonClick);
    }

    public void OnTrainButtonClick()
    {
        if (talkScript_yj != null)
        {
            talkScript_yj.OntrainButtonClick();
        }
        else
        {
            Debug.LogError("talkScript_yj is not initialized.");
        }
    }

    public void OnCampButtonClick()
    {
        if (talkScript_yj != null)
        {
            talkScript_yj.OncampButtonClick();
        }
        else
        {
            Debug.LogError("talkScript_yj is not initialized.");
        }
    }

    public void OnHintButtonClick()
    {
        if (talkScript_yj != null)
        {
            talkScript_yj.OnhintButtonClick();
        }
        else
        {
            Debug.LogError("talkScript_yj is not initialized.");
        }
    }

    public void OnBedButtonClick()
    {
        if (talkScript_yj != null)
        {
            talkScript_yj.OnbedButtonClick();
        }
        else
        {
            Debug.LogError("talkScript_yj is not initialized.");
        }
    }

    public void OnNoButtonClick()
    {
        if (talkScript_yj != null)
        {
            talkScript_yj.OnNoButtonClick();
        }
        else
        {
            Debug.LogError("talkScript_yj is not initialized.");
        }
    }
}