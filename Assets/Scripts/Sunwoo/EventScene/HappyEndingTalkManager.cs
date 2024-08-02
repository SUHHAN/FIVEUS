using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // SceneManager
using TMPro; // TextMeshPro

public class HappyEndingTalkManager : MonoBehaviour
{
    public GameObject dialogue;
    public GameObject nameObj; // 이름
    public TextMeshProUGUI nameText; // TextMeshPro UI 텍스트
    public TextMeshProUGUI descriptionText; // TextMeshPro UI 텍스트
    public GameObject ending;
    public GameObject hell;
    public GameObject narration;
    public TextMeshProUGUI narrationText; // TextMeshPro UI 텍스트

    private int dialogueState = 0; // 대사 진행 상태

    // 추가된 변수들
    public GameObject image1; // 주인공 이미지
    public GameObject princessImage; // 공주 이미지
    public GameObject yesButton; // yes 버튼
    public GameObject noButton; // no 버튼

    // Start is called before the first frame update
    void Start()
    {
        dialogue.SetActive(true); // 대화 시작 시 활성화
        nameText.text = ""; // 이름 텍스트 초기화
        descriptionText.text = ""; // 설명 텍스트 초기화

        // 추가된 변수 초기화
        image1.SetActive(false); // 주인공 이미지 비활성화
        princessImage.SetActive(false); // 공주 이미지 비활성화
        yesButton.SetActive(false); // yes 버튼 비활성화
        noButton.SetActive(false); // no 버튼 비활성화
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
                image1.SetActive(true); // 주인공 이미지 활성화
                dialogueState++;
                break;
            case 1:
                image1.SetActive(false); // 주인공 이미지 비활성화
                dialogue.SetActive(false);
                narration.SetActive(true);
                narrationText.text = "주인공 일행은 단서를 활용해 마계로 향하는 길을 찾았다.";
                dialogueState++;
                break;
            case 2:
                narrationText.text = "마왕성으로 가는 동안 많은 몬스터들을 마주쳤지만";
                dialogueState++;
                break;
            case 3:
                narrationText.text = "그들은 높은 단합력으로 몬스터들을 무찌르고 마왕성에 도착한다.";
                dialogueState++;
                break;
            case 4:
                hell.SetActive(true);
                narration.SetActive(false);
                dialogue.SetActive(true);
                image1.SetActive(true); // 주인공 이미지 활성화
                nameText.text = "주인공";
                descriptionText.text = "구하러 왔습니다, 공주님!";
                dialogueState++;
                break;
            case 5:
                // 공주일 경우 공주 이미지로 변경
                image1.SetActive(false); // 주인공 이미지 비활성화
                princessImage.SetActive(true); // 공주 이미지 활성화
                nameText.text = "공주";
                descriptionText.text = "......";
                dialogueState++;
                break;
            case 6:
                princessImage.SetActive(false); // 공주 이미지 비활성화
                dialogue.SetActive(false);
                narration.SetActive(true);
                narrationText.text = "그들은 공주님을 만나 마왕과 진심으로 사랑하는 사이라는 비밀을 들었다.";
                dialogueState++;
                break;
            case 7:
                // yes버튼과 no버튼 활성화
                narrationText.text = "공주를 왕국으로 데려가시겠습니까?";
                yesButton.SetActive(true); // yes 버튼 활성화
                noButton.SetActive(true); // no 버튼 활성화
                dialogueState++;
                break;
            case 8:
                SceneManager.LoadScene("MainScene"); // MainScene으로 씬 전환
                break;
        }
    }

    // 추가된 메서드
    public void OnYesButtonClicked()
    {
        // Yes 버튼을 누를 경우의 로직 구현
        ending.SetActive(true);
        narrationText.text = "주인공은 공주를 왕국으로 데려갔고, 결국 전쟁이 발발하고 말았다.";
        dialogueState = 8;
    }

    public void OnNoButtonClicked()
    {
        // No 버튼을 누를 경우의 로직 구현
        ending.SetActive(true);
        narrationText.text = "주인공은 공주의 편지를 왕에게 전했고, 왕은 그들의 선택을 존중했다.";
        dialogueState = 8;
    }
}
