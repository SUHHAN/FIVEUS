using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // SceneManager
using TMPro; // TextMeshPro

public class DeadEndingTalkManager : MonoBehaviour
{
    public GameObject dialogue;
    public GameObject nameObj; // 이름
    public TextMeshProUGUI nameText; // TextMeshPro UI 텍스트
    public TextMeshProUGUI descriptionText; // TextMeshPro UI 텍스트
    public GameObject ending;
    public GameObject narration;
    public TextMeshProUGUI narrationText; // TextMeshPro UI 텍스트

    private int dialogueState = 0; // 대사 진행 상태

    // Start is called before the first frame update
    void Start()
    {
        dialogue.SetActive(true); // 대화 시작 시 활성화
        nameText.text = ""; // 이름 텍스트 초기화
        descriptionText.text = ""; // 설명 텍스트 초기화
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
                nameText.text = "주인공";
                descriptionText.text = "어라?";
                dialogueState++;
                break;
            case 1:
                descriptionText.text = "갑자기 눈 앞이 깜깜해졌다.";
                dialogueState++;
                break;
            case 2:
                descriptionText.text = "너무 무리했나..";
                dialogueState++;
                break;
            case 3:
                dialogue.SetActive(false);
                narration.SetActive(true);
                narrationText.text = "체력이 0이 되어 사망하셨습니다.";
                dialogueState++;
                break;
            case 4:
                narrationText.text = "~Dead Ending~";
                dialogueState++;
                break;
            case 5:
                SceneManager.LoadScene("MainScene"); // MainScene으로 씬 전환
                break;
        }
    }
}
