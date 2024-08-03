using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueBar : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameTxt;
    [SerializeField] TypeEffect talk;
    string[] str = { }; // �ʱ� �� ��� �迭

    void Start()
    {
        string initialName = ""; // �ʱ� �̸� ���� (��: ???)
        ActiveDialogue(0, initialName, ref str); // �ʱ� ��ȭ ����
    }

    void Update()
    {
        // �ʿ�� Update �޼��忡�� �߰� ���� �ۼ�
    }

    void ActiveDialogue(int idx, string nameData, ref string[] talkData)
    {
        // nameData�� talkData�� null�� ��� �⺻�� ����
        if (nameData == null) nameData = ""; // �⺻ �̸� ����
        if (talkData == null) talkData = new string[] { "No dialogue available." }; // �⺻ ��� ����

        nameTxt.text = nameData;

        for (int i = 0; i < talkData.Length; i++)
        {
            talk.SetMsg(talkData[i]);
        }
    }
}
