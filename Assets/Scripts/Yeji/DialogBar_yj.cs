using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueBar_yj : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameTxt; // �̸� ǥ���� ������Ʈ
    [SerializeField] TypeEffect talk; // ��� ǥ���� ������Ʈ
    string[] str = { }; // ���� ��ȭ �迭

    void Start()
    {
        ActiveDialogue(0, "������", ref str); // ��ȭâ Ȱ��ȭ �޼��� ȣ��
    }

    void Update()
    {

    }

    // ��ȭâ Ȱ��ȭ �޼���
    void ActiveDialogue(int idx, string nameData, ref string[] talkData)
    {
        if (nameData == null && talkData == null)
        {
            // �����͸� �������� ������ ���, �ʿ��� �����͸� �����ϴ� ������ �����ɴϴ�.
        }

        nameTxt.text = nameData; // �̸� �ؽ�Ʈ ����

        // ��ȭ ������ ���
        for (int i = 0; i < talkData.Length; i++)
        {
            talk.SetMsg(talkData[i]);
            //Ư�� Ű ������ ���� ��ȭ ���� ����.
            //��ȭ ��ŵ ���δ� ���� �� ���� (InputSystem�� �׷��� ���� �߰��ؾ� ��)
        }
    }

    //��ȣ�ۿ��ϸ� ��ȭâ Ȱ��ȭ
    /*
     * UI Manager���� �÷��̾�� �ٸ� ������Ʈ�� �浹 / Ư�� ��ȭ �̺�Ʈ �߻��� Ȱ��ȭ
     dialogueBar.SetAcvite(true);
     */
}
