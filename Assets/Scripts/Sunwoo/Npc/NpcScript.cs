using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement; // �� ��ȯ

public class NpcScript : MonoBehaviour
{
    public GameObject choiceUI; // ���� UI �г�
    public GameObject dialogueUI; // ��ȭ UI �г�
    public GameObject npcAffectionUI; // ȣ���� UI �г�
    public TextMeshProUGUI dialogueText; // ��� �ؽ�Ʈ UI ����
    public TextMeshProUGUI affectionText; // ȣ���� �ؽ�Ʈ UI ����
    public TextMeshProUGUI npcNameText; // NPC �̸� �ؽ�Ʈ UI ����
    public Button talkButton; // ��ȭ�ϱ� ��ư ����
    public Button persuadeButton; // �����ϱ� ��ư ����
    public Button giftButton; // �����ϱ� ��ư ����
    public Button choice1Button; // ������ 1 ��ư
    public Button choice2Button; // ������ 2 ��ư
    public TextMeshProUGUI choice1Text; // ������ 1 �ؽ�Ʈ
    public TextMeshProUGUI choice2Text; // ������ 2 �ؽ�Ʈ
    public float interactionRange = 3.0f; // ��ȣ�ۿ� �Ÿ�
    public GameObject player; // �÷��̾� ������Ʈ
    public bool isTalking = false; // ��ȭ ������ ����
    public string npcType; // NPC Ÿ��
    public double affection = 0; // NPC ȣ����

    private int currentDialogueIndex = 0; // ���� ��ȭ �ε���
    private List<string> dialogues = new List<string>(); // ��� ���
    private List<string> choice1Dialogues = new List<string>(); // ���̽� 1 ��� ���
    private int choice1DialogueIndex = 0; // ���̽� 1 ��� �ε���

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // �±װ� "Player"�� ������Ʈ ã��
        choiceUI.SetActive(false); // ������ �� ���� UI ��Ȱ��ȭ
        dialogueUI.SetActive(false); // ������ �� ��ȭ UI ��Ȱ��ȭ
        persuadeButton.onClick.AddListener(OnPersuadeButtonClick); // �����ϱ� ��ư Ŭ�� �̺�Ʈ ����
        choice1Button.gameObject.SetActive(false); // ������ 1 ��ư ��Ȱ��ȭ
        choice2Button.gameObject.SetActive(false); // ������ 2 ��ư ��Ȱ��ȭ
        giftButton.onClick.AddListener(OnGiftButtonClick); // �����ϱ� ��ư Ŭ�� �̺�Ʈ ����

        choice1Button.onClick.AddListener(OnChoice1ButtonClick); // ������ 1 ��ư Ŭ�� �̺�Ʈ ����
        choice2Button.onClick.AddListener(OnChoice2ButtonClick); // ������ 2 ��ư Ŭ�� �̺�Ʈ ����

        // ������ ��ư �ؽ�Ʈ ����
        if (npcType == "�˻�")
        {
            choice1Button.GetComponentInChildren<TextMeshProUGUI>().text = "����� �� ������ �뺴 Į���� ����?";
            choice2Button.GetComponentInChildren<TextMeshProUGUI>().text = "�ƴ�, ������ ���� ���µ�...";
        }
        else if (npcType == "�ü�")
        {
            choice1Button.GetComponentInChildren<TextMeshProUGUI>().text = "�ʹ� �Ƹ��ٿ�ż���.";
            choice2Button.GetComponentInChildren<TextMeshProUGUI>().text = "Ȥ�� Ȱ ��� �� �������� �� �����Ű���?";
        }
        else if (npcType == "��Ŀ")
        {
            choice1Button.GetComponentInChildren<TextMeshProUGUI>().text = "�ȳ��ϼ���. ���� ������ �� ���׿�!";
            choice2Button.GetComponentInChildren<TextMeshProUGUI>().text = "�ȳ��ϼ���. ��Ŀ��.";
        }
        else if (npcType == "������")
        {
            choice1Button.GetComponentInChildren<TextMeshProUGUI>().text = "������ �� ������ �� ���ƿ�.";
            choice2Button.GetComponentInChildren<TextMeshProUGUI>().text = "�ȳ��ϼ���. ������ �� ���׿�!";
        }
        else if (npcType == "����")
        {
            choice1Button.GetComponentInChildren<TextMeshProUGUI>().text = "���� ��ȣ��?";
            choice2Button.GetComponentInChildren<TextMeshProUGUI>().text = "�����մϴ�, ������.";
        }
        else if (npcType == "�ϻ���")
        {
            choice1Button.GetComponentInChildren<TextMeshProUGUI>().text = "�ɽ��ؿ�?";
            choice2Button.GetComponentInChildren<TextMeshProUGUI>().text = "(������ ���Ѻ���)";
        }
    }


    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position); // �÷��̾�� NPC �� �Ÿ� ���
        if (distance <= interactionRange) // ��ȣ�ۿ� �Ÿ� ���� �ִ��� Ȯ��
        {
            if (Input.GetKeyDown(KeyCode.Return) && !isTalking) // ���� Ű �Է� ���� �� ��ȭ ���� �ƴ� ���
            {
                ShowChoiceUI(); // ���� UI ǥ��
            }
            else if (Input.GetKeyDown(KeyCode.Space) && isTalking) // ��ȭ ���� �� �����̽��� �Է� ����
            {
                if (choice1Dialogues.Count > 0 && choice1DialogueIndex < choice1Dialogues.Count)
                {
                    ShowNextChoice1Dialogue(); // ���� ���� �Ѿ��
                }
                else
                {
                    ShowNextDialogue(); // �⺻ ���� �Ѿ��
                }
            }
        }
        else
        {
            choiceUI.SetActive(false); // ���� UI�� �ڽ� ������Ʈ�� �����
        }
    }

    void ShowChoiceUI()
    {
        choiceUI.SetActive(true); // ���� UI Ȱ��ȭ
        dialogueText.text = ""; // ��� �ؽ�Ʈ �ʱ�ȭ
    }

    public void OnTalkButtonClick()
    {
        choiceUI.SetActive(false); // ���� UI �����
        dialogueUI.SetActive(true); // ��ȭ UI Ȱ��ȭ
        isTalking = true; // ��ȭ ���� ����
        currentDialogueIndex = 0; // ��ȭ �ε��� �ʱ�ȭ
        SetDialogue(npcType); // NPC Ÿ�Կ� ���� ��� ����
        ShowNextDialogue(); // ù ��° ��� ǥ��
    }

    public void OnPersuadeButtonClick()
    {
        choiceUI.SetActive(false); // ���� UI �����
        GetComponent<NpcPersuade>().ShowPersuadeUI(); // ���� UI ǥ��
    }

    public void OnGiftButtonClick()
    {
        PlayerPrefs.SetString("NpcType", npcType);
        PlayerPrefs.Save();

        // �ٸ� ������ curType�� ����
        PlayerPrefs.SetString("CurType", "��Ÿ"); // "���" ��� ���ϴ� �� �̸� ���
        PlayerPrefs.Save();

        print(PlayerPrefs.GetString("NpcType"));
        SceneManager.LoadScene("InventoryMain"); // InventoryMain ������ �̵�
    }

    public void HidePersuadeAndGiftButtons()
    {
        persuadeButton.gameObject.SetActive(false); // �����ϱ� ��ư �����
        giftButton.gameObject.SetActive(false); // �����ϱ� ��ư �����
    }

    public void EndDialogue()
    {
        isTalking = false; // ��ȭ ���� ����
        dialogueUI.SetActive(false); // ��ȭ UI ��Ȱ��ȭ
        choiceUI.SetActive(false); // ���� UI�� �ڽ� ������Ʈ�� ��Ȱ��ȭ
        choice1Button.gameObject.SetActive(false); // ������ 1 ��ư ��Ȱ��ȭ
        choice2Button.gameObject.SetActive(false); // ������ 2 ��ư ��Ȱ��ȭ
    }

    void SetDialogue(string type)
    {
        dialogues.Clear(); // ���� ��� �ʱ�ȭ
        if (type == "�˻�")
        {
            npcNameText.text = "???";
            dialogues.Add("��, ó�� ���� �� ������.");
            dialogues.Add("������ ���� �����̶�?");
        }
        else if (type == "�ü�")
        {
            npcNameText.text = "???";
            dialogues.Add("...����. ������ ���� �־�?");
        }
        else if (type == "��Ŀ")
        {
            npcNameText.text = "???";
            dialogues.Add("......");
        }
        else if (type == "������")
        {
            npcNameText.text = "???";
            dialogues.Add(".......");
        }
        else if (type == "����")
        {
            npcNameText.text = "???";
            dialogues.Add("�ȳ��ϼ���, �뺴��!");
            dialogues.Add("������ �ݰ����ϴ�. ���� ��ȣ�� �Բ� �Ͻñ�..");
        }
        else if (type == "�ϻ���")
        {
            npcNameText.text = "???";
            dialogues.Add("������~");
        }
    }

    void ShowNextDialogue()
    {
        if (currentDialogueIndex < dialogues.Count)
        {
            dialogueText.text = dialogues[currentDialogueIndex]; // ���� ��� ǥ��

            // Ư�� ����� �� ������ ��ư Ȱ��ȭ
            if (dialogueText.text == "������ ���� �����̶�?")
            {
                choice1Button.gameObject.SetActive(true); // ������ 1 ��ư Ȱ��ȭ
                choice2Button.gameObject.SetActive(true); // ������ 2 ��ư Ȱ��ȭ
            }
            else if (dialogueText.text == "...����. ������ ���� �־�?")
            {
                choice1Button.gameObject.SetActive(true); // ������ 1 ��ư Ȱ��ȭ
                choice2Button.gameObject.SetActive(true); // ������ 2 ��ư Ȱ��ȭ
            }
            else if (dialogueText.text == "......")
            {
                choice1Button.gameObject.SetActive(true); // ������ 1 ��ư Ȱ��ȭ
                choice2Button.gameObject.SetActive(true); // ������ 2 ��ư Ȱ��ȭ
            }
            else if (dialogueText.text == ".......")
            {
                choice1Button.gameObject.SetActive(true); // ������ 1 ��ư Ȱ��ȭ
                choice2Button.gameObject.SetActive(true); // ������ 2 ��ư Ȱ��ȭ
            }
            else if (dialogueText.text == "������ �ݰ����ϴ�. ���� ��ȣ�� �Բ� �Ͻñ�..")
            {
                choice1Button.gameObject.SetActive(true); // ������ 1 ��ư Ȱ��ȭ
                choice2Button.gameObject.SetActive(true); // ������ 2 ��ư Ȱ��ȭ
            }
            else if (dialogueText.text == "������~")
            {
                choice1Button.gameObject.SetActive(true); // ������ 1 ��ư Ȱ��ȭ
                choice2Button.gameObject.SetActive(true); // ������ 2 ��ư Ȱ��ȭ
            }
            currentDialogueIndex++; // �ε��� ����
        }
        else
        {
            EndDialogue(); // ��� ������ ��ȭ ����
        }
    }

    public void OnChoice1ButtonClick()
    {
        if (npcType == "�˻�")
        {
            choice1Dialogues = new List<string>
            {
                "Į����,����, ���� Į���� ����.",
                "Į����,�� �ƴ� ����̾�����, �ݰ���.",
                "Į����,�����ϴ� ��ŵ� �뺴 ������... �� ������ ���� �뺴�ΰ�?",
                "Į����,���� �� ������ ���� �Ƿڰ� �� ���´ٱ淡 ��� �̰��� �ӹ����� �־�",
                "Į����,������ �� �������ڰ�!"
            };
            ChangeAffection(2.5); // ȣ���� +5
        }
        else if (npcType == "�ü�")
        {
            choice1Dialogues = new List<string>
            {
                "������,��... ����."
            };
            ChangeAffection(-5); // ȣ���� -10
        }
        else if (npcType == "��Ŀ")
        {
            choice1Dialogues = new List<string>
            {
                "�港,��.�ȳ��Ͻÿ�.",
                "�港,...�׷���. ������ ���� �ٵ� �𸣰� ������ �� �߼�.",
                "�港,������ �λ縦 �ǳ��༭ ����. û��."
            };
            ChangeAffection(2.5); // ȣ���� +5
        }
        else if (npcType == "������")
        {
            choice1Dialogues = new List<string>
            {
                "ũ���̱�,��. �ȳ��ϼ���.",
                "ũ���̱�, ���� �׷��� �����ؿ�! ������ �� ��������!",
                "ũ���̱�, �׸��� �� ������ ������ ������ ���� �پ��� ������ ������ �Ұ����ؿ�.",
                "ũ���̱�, �׷��� �ʳ���?"
            };
            ChangeAffection(2.5); // ȣ���� +5
        }
        else if (npcType == "����")
        {
            choice1Dialogues = new List<string>
            {
                "��������, ��.. ���� ���� �����ó���?",
                "��������, �׷��ٸ� �� �ƽ��׿�.."
            };
            ChangeAffection(-2.5); // ȣ���� -5
        }
        else if (npcType == "�ϻ���")
        {
            choice1Dialogues = new List<string>
            {
                "����, ��! ��� �˾���~?",
                "����, ���� ��մ� ���� �������� ���ھ��~",
                "����, ����� �� ��վ� ���̱� �ϳ׿�!"
            };
            ChangeAffection(+2.5); // ȣ���� +5
        }
        choice1DialogueIndex = 0;
        isTalking = true; // ��ȭ ���� ����
        choice1Button.gameObject.SetActive(false); // ������ 1 ��ư ��Ȱ��ȭ
        choice2Button.gameObject.SetActive(false); // ������ 2 ��ư ��Ȱ��ȭ
        ShowNextChoice1Dialogue(); // ù ��° ��� ���
    }

    public void OnChoice2ButtonClick()
    {
        // choice1Dialogues ����Ʈ �ʱ�ȭ
        choice1Dialogues.Clear();
        choice1DialogueIndex = 0;
        if (npcType == "�˻�")
        {
            dialogueText.text = "...�׷� �� ���� �� ����?"; // ������ 2�� ���� ���
            npcNameText.text = "Į����";
            ChangeAffection(-2.5); // ȣ���� -5
        }
        else if (npcType == "�ü�")
        {
            choice1Dialogues = new List<string>
            {
                "������,Ȱ�̶��?",
                "������,��... ��� �뺴�̱���.",
                "������,...Ȱ�� ���߷��� �߿�����.",
                "������,�������ִ� �� �𸣰����� ���� ���� �� �־�."
            };
            ChangeAffection(2.5); // ȣ���� +5
        }
        else if (npcType == "��Ŀ")
        {
            choice1Dialogues = new List<string>
            {
                "�港,...�� �˰� �ִ°�?",
                "�港,�̾������� ��Ŀ ������ ����ϰ� �� �Ŷ�� ���ư���.",
                "�港, ���� �ٸ� ����� �Բ� ������ �ʾ�."
            };
            ChangeAffection(-2.5); // ȣ���� -5
        }
        else if (npcType == "������")
        {
            choice1Dialogues = new List<string>
            {
                "ũ���̱�, �� ��. �׷��׿�.",
                "ũ���̱�, ...",
                "ũ���̱�, ��... �� �Ͻ� �����̶�?"
            };
            ChangeAffection(-2.5); // ȣ���� -5
        }
        else if (npcType == "����")
        {
            choice1Dialogues = new List<string>
            {
                "��������, ������. �������� ��� Īȣ�׿�.",
                "��������, ��� ������ ������ �ƴ�����...",
                "��������, �׷��� ������ ���� ����� �ִ�ϴ�~"
            };
            ChangeAffection(2.5); // ȣ���� +5
        }
        else if (npcType == "�ϻ���")
        {
            choice1Dialogues = new List<string>
            {
                "����, ������~",
                "����, (����ؼ� �뷡�� ���Ÿ���)"
            };
            ChangeAffection(-2.5); // ȣ���� -5
        }
        choice1DialogueIndex = 0;
        isTalking = true; // ��ȭ ���� ����
        choice1Button.gameObject.SetActive(false); // ������ 1 ��ư ��Ȱ��ȭ
        choice2Button.gameObject.SetActive(false); // ������ 2 ��ư ��Ȱ��ȭ
        ShowNextChoice1Dialogue(); // ù ��° ��� ���
    }

    void ShowNextChoice1Dialogue()
    {
        if (choice1DialogueIndex < choice1Dialogues.Count)
        {
            // ���� �̸��� ','�� �����Ͽ� ǥ��
            var splitDialogue = choice1Dialogues[choice1DialogueIndex].Split(new string[] { "," }, StringSplitOptions.None);
            if (splitDialogue.Length > 1)
            {
                npcNameText.text = splitDialogue[0];
                dialogueText.text = splitDialogue[1];
            }
            else
            {
                dialogueText.text = splitDialogue[0];
            }

            choice1DialogueIndex++; // �ε��� ����
        }
        else
        {
            EndDialogue(); // ��� ������ ��ȭ ����
        }
    }

    // NPC ȣ���� ����
    void ChangeAffection(double amount)
    {
        Character character = DataManager.instance.nowPlayer.characters.Find(x => x.Type == npcType);
        affection += amount;
        character.Love = affection.ToString();

        DataManager.instance.SaveData();

        affectionText.text = $"ȣ����: {affection}";
    }

    // NPC ��ȭ ���� ǥ��
    void DisplayDialogue(string npcName, string description)
    {
        dialogueText.text = description; // ��ȭ ���� ����
        npcNameText.text = npcName; // NPC �̸� ����
    }

    void UpdatePosition(string timeOfDay)
    {
        Vector3 newPosition = Vector3.zero;
        string currentScene = SceneManager.GetActiveScene().name;
        bool shouldBeActive = false;

        switch (npcType)
        {
            case "�˻�":
                if (currentScene == "main_map")
                {
                    if (timeOfDay == "Morning" || timeOfDay == "Afternoon")
                    {
                        newPosition = new Vector3(-6, -0.5f, 0);
                        shouldBeActive = true; // main_map�� ���� ���� Ȱ��ȭ
                    }
                    else
                    {
                        shouldBeActive = false; // ���ῡ�� main_map���� ��Ȱ��ȭ
                    }
                }
                else if (currentScene == "hotel")
                {
                    if (timeOfDay == "Evening")
                    {
                        newPosition = new Vector3(-2, 0, 0);
                        shouldBeActive = true; // hotel�� ���� ���� Ȱ��ȭ
                    }
                    else
                    {
                        shouldBeActive = false; // ��ħ�� ���ɿ��� hotel���� ��Ȱ��ȭ
                    }
                }
                break;

            case "����":
                if (currentScene == "main_map")
                {
                    if (timeOfDay == "Morning" || timeOfDay == "Afternoon")
                    {
                        newPosition = new Vector3(14.68f, 7.29f, 0);
                        shouldBeActive = true; // main_map�� ���� ���� Ȱ��ȭ
                    }
                    else
                    {
                        shouldBeActive = false; // ���ῡ�� main_map���� ��Ȱ��ȭ
                    }
                }
                else if (currentScene == "hotel_hall")
                {
                    if (timeOfDay == "Evening")
                    {
                        newPosition = new Vector3(2, 0.85f, 0);
                        shouldBeActive = true; // hotel_hall�� ���� ���� Ȱ��ȭ
                    }
                    else
                    {
                        shouldBeActive = false; // ��ħ�� ���ɿ��� hotel_hall���� ��Ȱ��ȭ
                    }
                }
                break;

            case "��Ŀ":
                if (currentScene == "training")
                {
                    if (timeOfDay == "Morning" || timeOfDay == "Afternoon")
                    {
                        newPosition = new Vector3(-4, 3.36f, 0);
                        shouldBeActive = true; // training�� ���� ���� Ȱ��ȭ
                    }
                    else
                    {
                        shouldBeActive = false; // ���ῡ�� training���� ��Ȱ��ȭ
                    }
                }
                else if (currentScene == "hotel_room1")
                {
                    if (timeOfDay == "Evening")
                    {
                        newPosition = new Vector3(0.4f, 3.5f, 0);
                        shouldBeActive = true; // hotel_room1�� ���� ���� Ȱ��ȭ
                    }
                    else
                    {
                        shouldBeActive = false; // ��ħ�� ���ɿ��� hotel_room1���� ��Ȱ��ȭ
                    }
                }
                break;

            case "������":
                if (currentScene == "magic_house")
                {
                    newPosition = new Vector3(1.73f, 0.63f, 0);
                    shouldBeActive = true; // magic_house���� �׻� Ȱ��ȭ
                }
                break;

            case "�ϻ���":
                if (currentScene == "bar")
                {
                    if (timeOfDay == "Evening")
                    {
                        newPosition = new Vector3(-2.23f, -0.59f, 0);
                        shouldBeActive = true; // bar�� ���� ���� Ȱ��ȭ
                    }
                    else
                    {
                        shouldBeActive = false; // ���� �̿��� �ð����� bar���� ��Ȱ��ȭ
                    }
                }
                break;

            case "�ü�":
                if (currentScene == "training")
                {
                    if (timeOfDay == "Evening")
                    {
                        newPosition = new Vector3(4.2f, -1.74f, 0);
                        shouldBeActive = true; // training�� ���� ���� Ȱ��ȭ
                    }
                    else
                    {
                        shouldBeActive = false; // ���� �̿��� �ð����� training���� ��Ȱ��ȭ
                    }
                }
                break;
        }

        // NPC Ȱ��ȭ ���ο� ���� ���� ������Ʈ Ȱ��ȭ/��Ȱ��ȭ
        gameObject.SetActive(shouldBeActive);
        if (shouldBeActive)
        {
            transform.position = newPosition; // NPC ��ġ ����
        }
    }
}