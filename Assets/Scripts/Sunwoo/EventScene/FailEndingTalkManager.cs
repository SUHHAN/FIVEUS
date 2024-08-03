using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // SceneManager
using TMPro; // TextMeshPro

public class FailEndingTalkManager : MonoBehaviour
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
                narrationText.text = "수상한 기운이 느껴지는 길이다.";
                dialogueState++;
                break;
            case 2:
                narrationText.text = "마계로 가는 길로 한참을 걸었지만, 다른 기사단처럼 공주님의 흔적조차 찾지 못했다.";
                dialogueState++;
                break;
            case 3:
                narrationText.text = "왕은 임무에 실패한 그들에게 왕국에서의 추방 명령을 내린다.";
                dialogueState++;
                break;
            case 4:
                narrationText.text = "모집했던 용병들은 그동안 고생했던 것에 대한 보상을 주인공에게 요구한다.";
                dialogueState++;
                break;
            case 5:
                narrationText.text = "결국 주인공은 그들에게서 도망치며 떠돌아다니는 삶을 살게 된다.";
                dialogueState++;
                break;
            case 6:
                narrationText.text = "~Fail Ending~";
                dialogueState++;
                break;
            case 7:
                SceneManager.LoadScene("MainScene"); // MainScene으로 씬 전환
                break;
        }
    }
}
