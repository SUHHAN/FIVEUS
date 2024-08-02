using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public int day = 1; // ���� day ������(1~15)
    public int activityCount =0; // �Ϸ� Ȱ�� ��(3ȸ���� ����)
    private string timeOfDay = "��ħ"; // ���� �ð�(��ħ, ����, ����)
    public talkwithjjang_yj talkwithjjang;

    // ��¥ ǥ���ϴ� �г�
    public GameObject whatisdate_yj; // ��¥ ������ �� ��ο����� ȭ��(������)(����Ե巡��)
    public TextMeshProUGUI todayiswhat_yj; // ���� ��ĥ���� �ؽ�Ʈ ���� �ٲ�
    /*
    public GameObject whatistime1_yj; // ���� �ð�(����) ȭ��
    public GameObject whatistime2_yj; // ���� �ð�(����) ȭ��
    public GameObject whatistime3_yj; // ���� �ð�(����) ȭ��
    // public TextMeshProUGUI mornluneve_yj; //  ���� �ð�(����, ����, ����) �ؽ�Ʈ
    */
    public void Start()
    {
        activityCount = 0; // �Ϸ� Ȱ�� ��(3ȸ���� ����)
        Getday();
        GetTimeOfDay();
        todayiswhat_yj.text = $"{day.ToString()}���� {timeOfDay}";
        whatisdate_yj.SetActive(true);// ������ �� ��ĥ���� � ȭ�� �������
        // Invoke the method to hide the whatisdate_yj panel after 2 seconds
        Invoke("HideWhatIsDatePanel", 2f);
    }
    void Update()
    {
        if (activityCount >= 6)
        {
            AdvanceDay(); // Ȱ�� ���� 3�� �̻��̸� ���� ���� �Ѿ
        }
        
    }


    public void CompleteActivity()
    {
        activityCount++;
        if(activityCount == 0){
            timeOfDay = "��ħ "; // 0 ��ħ
           // Debug.Log(timeOfDay);
        }
        else if (activityCount>0 && activityCount <= 2)
        {
            timeOfDay = "���� "; // ù ��° Ȱ�� �� ���ķ� ����
            //Debug.Log(timeOfDay);
        }
        else if (activityCount > 2 && activityCount <= 4)
        {
            timeOfDay = "���� "; // �� ��° Ȱ�� �� �������� ����
           // Debug.Log(timeOfDay);
        }
        else if (activityCount >4 && activityCount <= 6)
        {
            timeOfDay = "��ħ "; // �� ��° Ȱ�� �� �ٽ� ��ħ���� ����
            //Debug.Log(timeOfDay);
        }

    }
    public void UpdateDateAndTimeDisplay()
    {
            Getday();
            GetTimeOfDay();  
            todayiswhat_yj.text = $"{day.ToString()}���� {timeOfDay}";
            talkwithjjang.choiceUI1_yj.SetActive(false);
            talkwithjjang.choiceUI2_yj.SetActive(false);
            talkwithjjang.choiceUI3_yj.SetActive(false);
            talkwithjjang.choiceUI4_yj.SetActive(false);
            talkwithjjang.Dial_changyj.SetActive(false);
            whatisdate_yj.SetActive(true);
            Invoke("HideWhatIsDatePanel", 2f);
        
    }

    public void AdvanceDay()
    {
        day++;

        if (day > 15)
        {
            day = 1; // 15�� ���Ŀ��� �ٽ� 1�Ϸ� ���ư�(�ӽ�)
            SceneManager.LoadScene("FailEndingScene"); // ���߿��� ���������� ����ǰ� �ڵ� �߰�
        }
        activityCount = 0;
        timeOfDay = "��ħ "; // ���ο� ���� ������ ��ħ
        // ���⿡ �ִ°� �±� �ѵ�, �ٽ� ħ�� ���� �ϴϱ� ���� ��ġ �Űܾ� �ϳ�
         //UpdateDateAndTimeDisplay();
    }

    public string GetTimeOfDay()
    {
        return timeOfDay; // ���� �ð��� ��ȯ
    }
    public int Getday()
    {
        return day;
    }
    // Method to hide whatisdate_yj panel
    void HideWhatIsDatePanel()
    {
        whatisdate_yj.SetActive(false);
    }
    public void gotosleep_yj()
    {

    }
}
