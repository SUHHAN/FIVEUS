using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // SceneManager
using TMPro; // TextMeshPro
using UnityEngine.UI; // Button

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
    private bool isYesEndingActive = false; // Yes 버튼 대사 진행 여부
    private bool isNoEndingActive = false;  // No 버튼 대사 진행 여부

    // 추가된 변수들
    public GameObject image1; // 주인공 이미지
    public GameObject princessImage; // 공주 이미지
    public Button yesButton; // yes 버튼
    public Button noButton; // no 버튼

    void Start()
    {
        dialogue.SetActive(true); // 대화 시작 시 활성화
        nameText.text = ""; // 이름 텍스트 초기화
        descriptionText.text = ""; // 설명 텍스트 초기화

        // 추가된 변수 초기화
        image1.SetActive(false); // 주인공 이미지 비활성화
        princessImage.SetActive(false); // 공주 이미지 비활성화
        yesButton.gameObject.SetActive(false); // yes 버튼 비활성화
        noButton.gameObject.SetActive(false); // no 버튼 비활성화

        // 버튼 클릭 이벤트 연결
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
        // Yes 버튼을 누를 경우의 로직 구현
        ending.SetActive(true);
        isYesEndingActive = true; // Yes 버튼 엔딩 대사 활성화
        dialogueState = 0; // 엔딩 대사 진행 상태 초기화
        narrationText.text = "주인공 일행은 공주의 사정을 무시하고 왕국으로 데려가고, 화가난 마왕은 전쟁을 선포한다.";
    }

    void ProgressYesEndingDialogue()
    {
        switch (dialogueState)
        {
            case 0:
                narrationText.text = "주인공 일행은 공주의 사정을 무시하고 왕국으로 데려가고, 화가난 마왕은 전쟁을 선포한다.";
                dialogueState++;
                break;
            case 1:
                narrationText.text = "준비가 되지 않았던 왕국은 처참히 무너져 많은 국민이 사망한다.";
                dialogueState++;
                break;
            case 2:
                narrationText.text = "주인공과 동료들도 사망하고, 공주는 마왕과 함께 마계로 돌아갔다.";
                dialogueState++;
                break;
            case 3:
                narrationText.text = "~Bad Ending~";
                dialogueState++;
                break;
            case 4:
                SceneManager.LoadScene("MainScene"); // MainScene으로 씬 전환
                break;
        }
    }

    public void OnNoButtonClicked()
    {
        // No 버튼을 누를 경우의 로직 구현
        ending.SetActive(true);
        isNoEndingActive = true; // No 버튼 엔딩 대사 활성화
        dialogueState = 0; // 엔딩 대사 진행 상태 초기화
        narrationText.text = "주인공은 공주의 편지를 왕에게 전했고, 왕은 그들의 선택을 존중했다.";
    }

    void ProgressNoEndingDialogue()
    {
        switch (dialogueState)
        {
            case 0:
                narrationText.text = "주인공은 공주의 편지를 왕에게 전했고, 왕은 그들의 선택을 존중했다.";
                dialogueState++;
                break;
            case 1:
                narrationText.text = "주인공 일행은 후한 보상을 받았고, 주인공은 사기꾼으로서의 삶 대신 본인이 원했던 삶을 살 수 있게 되었다.";
                dialogueState++;
                break;
            case 2:
                narrationText.text = "~Happy Ending~";
                dialogueState++;
                break;
            case 3:
                SceneManager.LoadScene("MainScene"); // MainScene으로 씬 전환
                break;
        }
    }
}
