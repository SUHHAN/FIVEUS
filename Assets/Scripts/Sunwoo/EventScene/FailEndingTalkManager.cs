using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro ���ӽ����̽� �߰�
using UnityEngine.SceneManagement; // SceneManager

public class FailEndingTalkManager : MonoBehaviour
{
    public GameObject opening;
    public TextMeshProUGUI openingText; // TextMeshPro UI �ؽ�Ʈ ���

    public GameObject narration;
    public TextMeshProUGUI narrationText; // TextMeshPro UI �ؽ�Ʈ ���

    public GameObject dialogue;
    public GameObject nameObj; // �̸� ���
    public TextMeshProUGUI nameText; // TextMeshPro UI �ؽ�Ʈ ���
    public TextMeshProUGUI descriptionText; // TextMeshPro UI �ؽ�Ʈ ���

    private int currentDialogueIndex = 0; // ���� ��� �ε���
    private bool isActivated = false; // TalkManager�� Ȱ��ȭ�Ǿ����� ����

    private List<Dialogue> dialogues = new List<Dialogue>(); // ��ȭ ����Ʈ

    // Start is called before the first frame update
    void Start()
    {
        // �ʱ� ��� ����
        dialogues.Add(new Dialogue("���ΰ�", "���� ����� ������ �� �ð��̾�. ���� ���ִ��� ��ſ���!"));
        dialogues.Add(new Dialogue("�����̼�", "���ΰ� ��Ƽ�� ����� ���� �濡�� ��Ŵٰ� �ٸ� ����ó�� ���ִ��� �������� ã�� ���ϰ� ���ƿ��� �Ǿ���."));
        dialogues.Add(new Dialogue("�����̼�", "���� ���ΰ����� �ձ������� �߹� ����� ���ȴ�."));
        dialogues.Add(new Dialogue("�����̼�", "�����ߴ� �뺴���� �׵��� ����ߴ� �Ϳ� ���� ������ ���ΰ����� �䱸�ߴ�."));
        dialogues.Add(new Dialogue("�����̼�", "�ᱹ ���ΰ��� �׵鿡�Լ� ����ġ�� �����ƴٴϴ� ���� ��� �ȴ�."));

        ActivateTalk(); // ������Ʈ Ȱ��ȭ
        PrintProDialogue(); // ù ��° ��� ���
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated && Input.GetKeyDown(KeyCode.Space))
        {
            currentDialogueIndex++;
            if (currentDialogueIndex < dialogues.Count)
            {
                PrintProDialogue();
            }
            else
            {
                DeactivateTalk();
            }
        }
    }

    void PrintProDialogue()
    {
        Dialogue currentDialogue = dialogues[currentDialogueIndex];

        if (currentDialogue.name == "���ΰ�")
        {
            narration.SetActive(false);
            dialogue.SetActive(true);
            opening.SetActive(true);
            descriptionText.text = currentDialogue.line;
        }
        else if (currentDialogue.name == "�����̼�")
        {
            narration.SetActive(true);
            dialogue.SetActive(false);
            opening.SetActive(true);
            narrationText.text = currentDialogue.line;
        }
    }

    public void ActivateTalk()
    {
        this.gameObject.SetActive(true);
        isActivated = true;
    }

    void DeactivateTalk()
    {
        this.gameObject.SetActive(false);
        isActivated = false;
    }

    // ��� ����ü ����
    public struct Dialogue
    {
        public string name; // ��� ��ü�� �̸�
        public string line; // ��� ����

        public Dialogue(string name, string line)
        {
            this.name = name;
            this.line = line;
        }
    }
}
