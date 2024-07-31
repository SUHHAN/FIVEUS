using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // SceneManager
using TMPro; // TextMeshPro

public class DeadEndingTalkManager : MonoBehaviour
{
    public GameObject dialogue;
    public GameObject nameObj; // �̸�
    public TextMeshProUGUI nameText; // TextMeshPro UI �ؽ�Ʈ
    public TextMeshProUGUI descriptionText; // TextMeshPro UI �ؽ�Ʈ
    public GameObject ending;
    public GameObject narration;
    public TextMeshProUGUI narrationText; // TextMeshPro UI �ؽ�Ʈ

    private int dialogueState = 0; // ��� ���� ����

    // Start is called before the first frame update
    void Start()
    {
        dialogue.SetActive(true); // ��ȭ ���� �� Ȱ��ȭ
        nameText.text = ""; // �̸� �ؽ�Ʈ �ʱ�ȭ
        descriptionText.text = ""; // ���� �ؽ�Ʈ �ʱ�ȭ
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ProgressDialogue();
        }
    }

    void ProgressDialogue()
    {
        switch (dialogueState)
        {
            case 0:
                nameText.text = "���ΰ�";
                descriptionText.text = "���?";
                dialogueState++;
                break;
            case 1:
                descriptionText.text = "���ڱ� �� ���� ����������.";
                dialogueState++;
                break;
            case 2:
                descriptionText.text = "�ʹ� �����߳�..";
                dialogueState++;
                break;
            case 3:
                dialogue.SetActive(false);
                narration.SetActive(true);
                narrationText.text = "ü���� 0�� �Ǿ� ����ϼ̽��ϴ�.";
                dialogueState++;
                break;
            case 4:
                narrationText.text = "~Dead Ending~";
                dialogueState++;
                break;
            case 5:
                SceneManager.LoadScene("MainScene"); // MainScene���� �� ��ȯ
                break;
        }
    }
}
