using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // SceneManager
using TMPro; // TextMeshPro

public class MonsterDeadEndingManager : MonoBehaviour
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
                descriptionText.text = "우리 이제 공주님을 찾으러 가보자!";
                dialogueState++;
                break;
            case 1:
                dialogue.SetActive(false);
                narration.SetActive(true);
                narrationText.text = "주인공 일행은 단서를 활용해 마계로 가는 길을 찾았다.";
                dialogueState++;
                break;
            case 2:
                narrationText.text = "그러나 파티의 단합력이 부족해 마왕성으로 가는 길에 마주친 몬스터들과 싸우는데 어려움을 겪었고...";
                dialogueState++;
                break;
            case 3:
                narrationText.text = "결국 그들은 몬스터와 싸우다 사망하고 말았다.";
                dialogueState++;
                break;
            case 4:
                narrationText.text = "~Monster Dead Ending~";
                dialogueState++;
                break;
            case 5:
                SceneManager.LoadScene("MainScene"); // MainScene으로 씬 전환
                break;
        }
    }
}
