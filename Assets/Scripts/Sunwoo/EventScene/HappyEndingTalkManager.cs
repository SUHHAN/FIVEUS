using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // SceneManager
using TMPro; // TextMeshPro

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

    // �߰��� ������
    public GameObject image1; // ���ΰ� �̹���
    public GameObject princessImage; // ���� �̹���
    public GameObject yesButton; // yes ��ư
    public GameObject noButton; // no ��ư

    // Start is called before the first frame update
    void Start()
    {
        dialogue.SetActive(true); // ��ȭ ���� �� Ȱ��ȭ
        nameText.text = ""; // �̸� �ؽ�Ʈ �ʱ�ȭ
        descriptionText.text = ""; // ���� �ؽ�Ʈ �ʱ�ȭ

        // �߰��� ���� �ʱ�ȭ
        image1.SetActive(false); // ���ΰ� �̹��� ��Ȱ��ȭ
        princessImage.SetActive(false); // ���� �̹��� ��Ȱ��ȭ
        yesButton.SetActive(false); // yes ��ư ��Ȱ��ȭ
        noButton.SetActive(false); // no ��ư ��Ȱ��ȭ
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
                yesButton.SetActive(true); // yes ��ư Ȱ��ȭ
                noButton.SetActive(true); // no ��ư Ȱ��ȭ
                dialogueState++;
                break;
            case 8:
                SceneManager.LoadScene("MainScene"); // MainScene���� �� ��ȯ
                break;
        }
    }

    // �߰��� �޼���
    public void OnYesButtonClicked()
    {
        // Yes ��ư�� ���� ����� ���� ����
        ending.SetActive(true);
        narrationText.text = "���ΰ��� ���ָ� �ձ����� ��������, �ᱹ ������ �߹��ϰ� ���Ҵ�.";
        dialogueState = 8;
    }

    public void OnNoButtonClicked()
    {
        // No ��ư�� ���� ����� ���� ����
        ending.SetActive(true);
        narrationText.text = "���ΰ��� ������ ������ �տ��� ���߰�, ���� �׵��� ������ �����ߴ�.";
        dialogueState = 8;
    }
}
