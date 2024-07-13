using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeEffect : MonoBehaviour
{
    public GameObject endCursor; // ���� Ŀ��
    public int charPerSeconds; // �ʴ� ����� ���� ��
    string targetMsg; // ��ǥ ���ڿ�
    [SerializeField] TextMeshProUGUI msgText; // �޽��� �ٷ��� ������Ʈ
    int idx; // ���� ����� ���� �ε���
    float interval; // ���� ��� ����

    private void Awake()
    {
        //msgText = GetComponent<TextMeshProUGUI>();
    }

    // ȿ���� �����ϴ� �޼���
    public void SetMsg(string msg)
    {
        targetMsg = msg;
        EffectStart();
    }

    // ȿ���� �����ϴ� �޼���
    void EffectStart()
    {
        msgText.text = ""; // �ؽ�Ʈ �ʱ�ȭ
        idx = 0; // �ε��� 0���� �ʱ�ȭ
        interval = 1.0f / charPerSeconds; // ���� ��� ���� ���
        endCursor.SetActive(false); // ���� Ŀ�� ��Ȱ��ȭ

        Invoke("Effecting", 1 / charPerSeconds); // ������ �������� Effecting �޼��� ȣ��
    }

    // ���� ��� �޼���
    void Effecting()
    {
        if (msgText.text == targetMsg) // ��� ���� ������� ���
        {
            EffectEnd(); // ȿ�� ����
            return;
        }
        msgText.text += targetMsg[idx]; // ���� �ε����� ���� �߰�
        idx++; // �ε��� ����

        Invoke("Effecting", interval); // ���� ���ڸ� ����ϱ� ���� �ٽ� ȣ��
    }

    // ȿ���� �����ϴ� �޼���
    void EffectEnd()
    {
        endCursor.SetActive(true); // ���� Ŀ�� Ȱ��ȭ
    }
}
