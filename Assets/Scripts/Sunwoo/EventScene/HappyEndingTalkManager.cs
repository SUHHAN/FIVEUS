using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // SceneManager
using TMPro; // TextMeshPro
using UnityEngine.UI; // Button

public class HappyEndingTalkManager : MonoBehaviour
{
    public GameObject dialogue;
    public GameObject nameObj; // �̸�
    public TextMeshProUGUI nameText; // TextMeshPro UI �ؽ�Ʈ
    public TextMeshProUGUI descriptionText; // TextMeshPro UI �ؽ�Ʈ
    public GameObject ending;
    public GameObject hell;
    public GameObject narration;
    public TextMeshProUGUI narrationText; // TextMeshPro UI �ؽ�Ʈ

    private int dialogueState = 0; // ��� ���� ����
    private bool isYesEndingActive = false; // Yes ��ư ��� ���� ����
    private bool isNoEndingActive = false;  // No ��ư ��� ���� ����

    // �߰��� ������
    public GameObject image1; // ���ΰ� �̹���
    public GameObject princessImage; // ���� �̹���
    public Button yesButton; // yes ��ư
    public Button noButton; // no ��ư

    void Start()
    {
        dialogue.SetActive(true); // ��ȭ ���� �� Ȱ��ȭ
        nameText.text = ""; // �̸� �ؽ�Ʈ �ʱ�ȭ
        descriptionText.text = ""; // ���� �ؽ�Ʈ �ʱ�ȭ

        // �߰��� ���� �ʱ�ȭ
        image1.SetActive(false); // ���ΰ� �̹��� ��Ȱ��ȭ
        princessImage.SetActive(false); // ���� �̹��� ��Ȱ��ȭ
        yesButton.gameObject.SetActive(false); // yes ��ư ��Ȱ��ȭ
        noButton.gameObject.SetActive(false); // no ��ư ��Ȱ��ȭ

        // ��ư Ŭ�� �̺�Ʈ ����
        yesButton.onClick.AddListener(OnYesButtonClicked);
        noButton.onClick.AddListener(OnNoButtonClicked);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isYesEndingActive)
            {
                ProgressYesEndingDialogue();
            }
            else if (isNoEndingActive)
            {
                ProgressNoEndingDialogue();
            }
            else
            {
                ProgressDialogue();
            }
        }
    }

    void ProgressDialogue()
    {
        switch (dialogueState)
        {
            case 0:
                nameText.text = "���ΰ�";
                descriptionText.text = "�츮 ���� ���ִ��� ã���� ������!";
                image1.SetActive(true); // ���ΰ� �̹��� Ȱ��ȭ
                dialogueState++;
                break;
            case 1:
                image1.SetActive(false); // ���ΰ� �̹��� ��Ȱ��ȭ
                dialogue.SetActive(false);
                narration.SetActive(true);
                narrationText.text = "���ΰ� ������ �ܼ��� Ȱ���� ����� ���ϴ� ���� ã�Ҵ�.";
                dialogueState++;
                break;
            case 2:
                narrationText.text = "���ռ����� ���� ���� ���� ���͵��� ����������";
                dialogueState++;
                break;
            case 3:
                narrationText.text = "�׵��� ���� ���շ����� ���͵��� ����� ���ռ��� �����Ѵ�.";
                dialogueState++;
                break;
            case 4:
                hell.SetActive(true);
                narration.SetActive(false);
                dialogue.SetActive(true);
                image1.SetActive(true); // ���ΰ� �̹��� Ȱ��ȭ
                nameText.text = "���ΰ�";
                descriptionText.text = "���Ϸ� �Խ��ϴ�, ���ִ�!";
                dialogueState++;
                break;
            case 5:
                // ������ ��� ���� �̹����� ����
                image1.SetActive(false); // ���ΰ� �̹��� ��Ȱ��ȭ
                princessImage.SetActive(true); // ���� �̹��� Ȱ��ȭ
                nameText.text = "����";
                descriptionText.text = "......";
                dialogueState++;
                break;
            case 6:
                princessImage.SetActive(false); // ���� �̹��� ��Ȱ��ȭ
                dialogue.SetActive(false);
                narration.SetActive(true);
                narrationText.text = "�׵��� ���ִ��� ���� ���հ� �������� ����ϴ� ���̶�� ����� �����.";
                dialogueState++;
                break;
            case 7:
                // yes��ư�� no��ư Ȱ��ȭ
                narrationText.text = "���ָ� �ձ����� �������ðڽ��ϱ�?";
                yesButton.gameObject.SetActive(true);
                yesButton.interactable = true;

                noButton.gameObject.SetActive(true);
                noButton.interactable = true;
                dialogueState++;
                break;
        }
    }

    public void OnYesButtonClicked()
    {
        // Yes ��ư�� ���� ����� ���� ����
        ending.SetActive(true);
        isYesEndingActive = true; // Yes ��ư ���� ��� Ȱ��ȭ
        dialogueState = 0; // ���� ��� ���� ���� �ʱ�ȭ
        narrationText.text = "���ΰ� ������ ������ ������ �����ϰ� �ձ����� ��������, ȭ���� ������ ������ �����Ѵ�.";
    }

    void ProgressYesEndingDialogue()
    {
        switch (dialogueState)
        {
            case 0:
                narrationText.text = "���ΰ� ������ ������ ������ �����ϰ� �ձ����� ��������, ȭ���� ������ ������ �����Ѵ�.";
                dialogueState++;
                break;
            case 1:
                narrationText.text = "�غ� ���� �ʾҴ� �ձ��� ó���� ������ ���� ������ ����Ѵ�.";
                dialogueState++;
                break;
            case 2:
                narrationText.text = "���ΰ��� ����鵵 ����ϰ�, ���ִ� ���հ� �Բ� ����� ���ư���.";
                dialogueState++;
                break;
            case 3:
                narrationText.text = "~Bad Ending~";
                dialogueState++;
                break;
            case 4:
                SceneManager.LoadScene("MainScene"); // MainScene���� �� ��ȯ
                break;
        }
    }

    public void OnNoButtonClicked()
    {
        // No ��ư�� ���� ����� ���� ����
        ending.SetActive(true);
        isNoEndingActive = true; // No ��ư ���� ��� Ȱ��ȭ
        dialogueState = 0; // ���� ��� ���� ���� �ʱ�ȭ
        narrationText.text = "���ΰ��� ������ ������ �տ��� ���߰�, ���� �׵��� ������ �����ߴ�.";
    }

    void ProgressNoEndingDialogue()
    {
        switch (dialogueState)
        {
            case 0:
                narrationText.text = "���ΰ��� ������ ������ �տ��� ���߰�, ���� �׵��� ������ �����ߴ�.";
                dialogueState++;
                break;
            case 1:
                narrationText.text = "���ΰ� ������ ���� ������ �޾Ұ�, ���ΰ��� �������μ��� �� ��� ������ ���ߴ� ���� �� �� �ְ� �Ǿ���.";
                dialogueState++;
                break;
            case 2:
                narrationText.text = "~Happy Ending~";
                dialogueState++;
                break;
            case 3:
                SceneManager.LoadScene("MainScene"); // MainScene���� �� ��ȯ
                break;
        }
    }
}
