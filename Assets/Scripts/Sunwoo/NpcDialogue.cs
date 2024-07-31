using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NpcDialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // 대사 텍스트 UI 연결
    public TextMeshProUGUI npcNameText; // NPC 이름 텍스트 UI 연결
    public Image npcImageView; // NPC 이미지 UI 연결
    private Queue<string> dialogueQueue; // 대사 큐
    private NpcScript npcScript; // NpcScript 참조

    void Start()
    {
        dialogueQueue = new Queue<string>();
        npcScript = GetComponent<NpcScript>();

        // NPC 이름과 이미지를 초기화
        npcNameText.text = "";
        npcImageView.sprite = null;
    }

    // 대화를 시작하고 대사, 이름, 이미지를 받아오는 함수
    public void StartDialogue(string[] dialogues, string npcName, Sprite npcImage)
    {
        dialogueQueue.Clear();
        foreach (string dialogue in dialogues)
        {
            dialogueQueue.Enqueue(dialogue);
        }

        // 대화가 시작되면서 NPC 이름과 이미지 설정
        npcNameText.text = npcName; // NPC 이름 표시
        npcImageView.sprite = npcImage; // NPC 이미지 표시

        DisplayNextSentence();
    }

    // 암살자 대화를 시작하는 함수
    public void StartAssassinDialogue()
    {
        dialogueQueue.Clear();
        dialogueQueue.Enqueue("흠흠흠~");
        DisplayNextSentence();
        npcScript.choice1Button.gameObject.SetActive(true);
        npcScript.choice2Button.gameObject.SetActive(true);
        npcScript.choice1Button.GetComponentInChildren<TextMeshProUGUI>().text = "심심해요?";
        npcScript.choice2Button.GetComponentInChildren<TextMeshProUGUI>().text = "(가만히 지켜본다)";
    }

    // 다음 대사를 표시하는 함수
    public void DisplayNextSentence()
    {
        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = dialogueQueue.Dequeue();
        dialogueText.text = sentence;
    }

    // 대화를 종료하는 함수
    private void EndDialogue()
    {
        npcScript.EndDialogue(); // NpcScript의 EndDialogue 함수 호출
    }

    void Update()
    {
        if (npcScript.isTalking && Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextSentence();
        }
    }

    // 특정 대사를 설정하는 함수
    public void SetDialogue(string dialogue)
    {
        dialogueQueue.Clear();
        dialogueQueue.Enqueue(dialogue);
        DisplayNextSentence();
    }
}
