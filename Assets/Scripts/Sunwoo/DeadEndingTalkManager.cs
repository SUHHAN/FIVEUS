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

    private PlayerNow_yj playerNow; // PlayerNow_yj ����
    private int dialogueState = 0; // ��� ���� ����

    // Start is called before the first frame update
    void Start()
    {
        playerNow = FindObjectOfType<PlayerNow_yj>(); // PlayerNow_yj ��ü ã��
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerHealth();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ProgressDialogue();
        }
    }

    void CheckPlayerHealth()
    {
        if (playerNow.hp_py <= 0)
        {
            dialogue.SetActive(true);
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
