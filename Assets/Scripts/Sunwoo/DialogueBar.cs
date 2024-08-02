using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueBar : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameTxt; // �̸� ǥ���� ������Ʈ
    [SerializeField] TypeEffect talk; // ��� ǥ���� ������Ʈ
    string[] str = {}; // ���� ��ȭ �迭

    void Start()
    {
        // '���ΰ�'�̶�� �̸��� str �迭�� ��ȭ�� ����Ͽ� ��ȭâ Ȱ��ȭ
        ActiveDialogue(0, "���ΰ�", ref str);
    }

    void Update()
    {
        // Update �Լ��� �ʿ����� �ʴٸ� ����ΰų� ���� ����
    }

    // ��ȭâ Ȱ��ȭ �޼���
    void ActiveDialogue(int idx, string nameData, ref string[] talkData)
    {
        if (nameTxt == null || talk == null || talkData == null || talkData.Length == 0)
        {
            Debug.LogError("Missing components or data.");
            return; // �ʿ��� �����Ͱ� ���� ���, �Լ� ���� ����
        }

        nameTxt.text = nameData; // �̸� �ؽ�Ʈ ����

        // ��ȭ ������ ���
        StartCoroutine(DisplayTalk(talkData)); // �ڷ�ƾ���� ��� ���
    }

    IEnumerator DisplayTalk(string[] talkData)
    {
        for (int i = 0; i < talkData.Length; i++)
        {
            talk.SetMsg(talkData[i]);
            // TypeEffect�� ��� ����� �Ϸ�� ������ ���
            while (!talk.IsComplete())
            {
                yield return null;
            }
            yield return new WaitForSeconds(1f); // ��� �� ��� �ð�
        }
    }
}
