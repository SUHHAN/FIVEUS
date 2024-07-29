using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting; // TextMeshPro 네임스페이스 추가

//
public class talkwithjjang_yj : MonoBehaviour
{
    public GameObject choiceUI_yj; // 선택 UI 패널
    public GameObject dialogueUI_yj; // 대화 UI 패널
    public GameObject resultUI_yj; // 결과 UI 패널
    public TextMeshProUGUI dialogueText; // 대사 텍스트 UI 연결
    public TextMeshProUGUI resultText; // 결과 텍스트 UI 연결
    public Button yesButton; // 예 버튼 연결
    public Button noButton; // 아니오 버튼 연결

    // 기본 활동 버튼들
    public Button trainingButton_yj; // 1.훈련 시도 버튼 연결
    public Button togetherButton_yj; // 2.단합 시도 버튼 연결
    public Button findhintButton_yj; // 3.힌트 확인 버튼 연결

    public float interactionRange = 3.0f; // 상호작용 거리
    private GameObject player; // 플레이어 오브젝트
    private bool isTalking = false; // 대화 중인지 여부

    private ProDialogue_yj whatdial_yj;
    // 기본 활동 정도는 스트립트 내에서 전부 대사 처리(어차피 5개밖에 없음)
    // 아이디 설정 설명 : 6000번대부터 시작함
    // 6001 : 훈련대장, 6002 : 기사단장, 6003 : 단서

    // 기본활동1 : 훈련대장 기본 대사1
    ProDialogue_yj serif1_1 = new ProDialogue_yj(6001, "훈련대장", "여어-말라깽이! 훈련할 준비는 됐나?");
    // 기본활동1 : 훈련대장 기본 대사2
    ProDialogue_yj serif1_2 = new ProDialogue_yj(6001, "훈련대장", "안되면 될때까지! 훈련 시작이다!");

    // 기본활동2 : 기사단장 기본 대사1
    ProDialogue_yj serif2_1 = new ProDialogue_yj(6002, "기사단장", "뭉치면 살고 흩어지면 죽는다! \n단합훈련 시작이다!!");
    // 기본활동2 : 기사단장 기본 대사2
    ProDialogue_yj serif2_2 = new ProDialogue_yj(6002, "기사단장", "3 -1 = 0! 우리는 하나다!  \n단합훈련 시작이다!!");

    // 기본활동3 : 단서 칮았을 때 기본 대사
    ProDialogue_yj serif3 = new ProDialogue_yj(6003, "단서", "단서를 찾았다. 내용을 살펴보자.");


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // 태그가 "Player"인 오브젝트 찾기
        choiceUI_yj.SetActive(false); // 시작할 때 선택 UI 비활성화       
        dialogueUI_yj.SetActive(false); // 시작할 때 대화 UI 비활성화
        resultUI_yj.SetActive(false); // 시작할 때 결과 UI 비활성화

        //talkButton.onClick.AddListener(OnTalkButtonClick); // 대화하기 버튼 클릭 이벤트 연결
        yesButton.onClick.AddListener(OnYesButtonClick); // 예 버튼 클릭 이벤트 연결
        noButton.onClick.AddListener(OnNoButtonClick); // 아니오 버튼 클릭 이벤트 연결
    }



    // Update is called once per frame
    void Update()
    {
        if (isTalking)
        {
            if ((isTalking && Input.GetKeyDown(KeyCode.Return))) // 엔터 키 입력 감지
            {
                EndDialogue(); // 대화 종료
            }
            //return;
        }

        float distance = Vector3.Distance(player.transform.position, transform.position); // 플레이어와 NPC 간 거리 계산
        if (distance <= interactionRange) // 상호작용 거리 내에 있는지 확인
        { 
            if (Input.GetKeyDown(KeyCode.Return)) // 엔터 키 입력 감지
            {
                // 상대 누구를 만났는지 먼저 기본대사가 뜸(TalkManager_yj 참고)
                switch (whatdial_yj.id)
                {
                    // 기본활동1 : 훈련단장일 떄(훈련)
                    case 6001:
                        dialogueText.text = serif1_1.line; // 랜덤하게 훈련대장 기본 대사 출력
                        dialogueText.text = ""; // 대사 텍스트 초기화
                        break;
                    // 기본활동2 : 기사단장일 때(단합)
                    case 6002:
                        dialogueText.text = serif2_1.line; // 랜덤하게 기사단장 기본 대사 출력
                        dialogueText.text = ""; // 대사 텍스트 초기화
                        break;
                    // 기본활동3 : 단서일 때(단서)
                    case 6003:
                        dialogueText.text = serif3.line; // 힌트 대사 출력
                        dialogueText.text = ""; // 대사 텍스트 초기화
                        break;
                }

                ShowChoiceUI_yj(); // 만나는 상대에 따라 다른 선택 UI 표시
                // 같은 선택 UI를 사용하는데, 문구만 다름. yes,no도 같음.
                // 훈련단장(6001)을 만났을 떄는 "훈련하시겠습니까?" 선택지 뜸
                // 기사단장(6002)을 만났을 떄는 "단합 훈련을 진행하시겠습니까?" 선택지 뜸
                // 훈련단장(6001)을 만났을 떄는 "단서를 조사하시겠겠습니까?" 선택지 뜸
            }
        }
        else
        {
            choiceUI_yj.SetActive(false); // 선택 UI 숨기기
        }
    }

    void ShowChoiceUI_yj()
    {
        choiceUI_yj.SetActive(true); // 선택 UI 활성화

        switch (whatdial_yj.id)
        {
            // 기본활동1 : 훈련단장일떄(훈련)
            case 6001:
                choiceUI_yj.SetActive(true); // 선택 UI 활성화
                break;

            // 기본활동2 : 기사단장일때(단합)
            case 6002:
                dialogueText.text = ""; // 대사 텍스트 초기화
                break;

            case 6003:
                dialogueText.text = ""; // 대사 텍스트 초기화
                break;
        }        
    }

    public void OnTalkButtonClick()
    {
        choiceUI_yj.SetActive(true); // 선택 UI 활성화
        //choiceUI.SetActive(false); // 선택 UI 숨기기
        //dialogueUI.SetActive(true); // 대화 UI 활성화
        dialogueText.text = "단합하시겠습니까?"; // 대사 표시
        isTalking = true; // 대화 상태 설정
    }


    public void OnYesButtonClick()
    {
        // TalkManager_yj 클래스의 IncreaseTeamPower 호출
        TalkManager_yj talkManager = FindObjectOfType<TalkManager_yj>();
        if (talkManager != null)
        {
            //talkManager.IncreaseTeamPower(10); // 예시로 10만큼 팀 파워 증가
        }
        else
        {
            Debug.LogError("TalkManager_yj not found in the scene.");
        }


        //AttemptPersuasion(); // 설득 시도
        dialogueText.text = "단합을 진행했다"; // 선택지 처리
        // TalkManager_yj에서 처리할 내용으로 연결
        isTalking = false; // 대화 상태 해제
        choiceUI_yj.SetActive(false); // 선택 UI 숨기기
    }

    public void OnNoButtonClick()
     {
        // persuadeUI.SetActive(false); // 설득 UI 숨기기
        dialogueText.text = "단합을 진행하지 않습니다"; // 선택지 처리
        isTalking = false; // 대화 상태 해제
        choiceUI_yj.SetActive(false); // 선택 UI 숨기기
    }


    /*void HideChoices()
    {
        choiceUI.SetActive(false); // 선택 UI 숨기기
        dialogueUI.SetActive(false); // 대화 UI 숨기기
        persuadeUI.SetActive(false); // 설득 UI 숨기기
    }*/

    void EndDialogue()
    {
        //dialogueUI.SetActive(false); // 대화 UI 숨기기
        isTalking = false; // 대화 상태 해제
        choiceUI_yj.SetActive(false); // 선택 UI 숨기
    }

}