using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting; // TextMeshPro 네임스페이스 추가
using UnityEngine.SceneManagement;
using System.Xml.Serialization;

// 기본 활동 진행하는 스크립트
public class talkwithjjang_yj : MonoBehaviour
{
    private bool isbasicdial_yj = false; // 대사 치고 있는지 여부

    // 기본 활동 패널들
    public GameObject choiceUI1_yj; // 기본활동1 UI 패널
    public GameObject choiceUI2_yj; // 기본활동2 UI 패널
    public GameObject choiceUI3_yj; // 기본활동3 UI 패널
    public GameObject choiceUI4_yj; // 기본활동4 UI 패널

    public GameObject Dial_changyj; // 기본대사 띄울 대화창
    public TextMeshProUGUI dialoguename_yj; // name text
    public TextMeshProUGUI dialogueText_yj; // line text
    public float interactionRange = 1.5f; // 상호작용 거리

    public GameObject player; // 플레이어 오브젝트
    private GameObject currentNPC; // 현재 상호작용하는 NPC 저장 변수
    public GameObject npc1_yj; // 훈련단장
    public GameObject npc2_yj; // 캠핑장
    public GameObject npc3_yj; // 힌트
    public GameObject npc4_yj; // 휴식

    public Button noButton1; // 아니오 버튼 연결1
    public Button noButton2; // 아니오 버튼 연결2
    public Button noButton3; // 아니오 버튼 연결3
    public Button noButton4; // 아니오 버튼 연결4

    public Button noButton5; // 결과창 닫기 버튼 연결5

    // 기본 활동 버튼들
    public Button trainingButton_yj; // 1. 훈련 시도 버튼 연결
    public Button campingButton_yj; // 2. 단합 시도 버튼 연결
    public Button findhintButton_yj; // 3. 단서 보기 버튼 연결
    public Button laybedButton_yj; // 4. 휴식하기 버튼 연결4
    public Button gotobedButton_yj; // 5. 마지막에만 나오는 침대로 이동하는 버튼 

    // 버튼 누르면 각각 나오는 패널들 
    //public GameObject trainingUI_yj; // 훈련 중 패널
    //public GameObject heyGotoBed_yj; // 휴식하세요 UI 패널
    public GameObject iaminbedUI_yj; // 휴식하고 있습니다 UI 패널

    public GameObject resultUI_yj; // 결과 UI 패널
    public GameObject resultUI2_yj; // 결과 UI 패널
    public TextMeshProUGUI resuedit_yj; // result edit text
    public TextMeshProUGUI resuedit2_yj; // 침대가시라구..

    // 기본활동 몇번 진행했는지 세야 하니..플레이어를 부르자
    // public PlayerNow_yj nowplayer_yj;
    public PlayerManager_yj playermanager_yj; // 체력관리용
    public TimeManager timemanager_yj; // 날짜 관리 + 기본활동 덧뺄셈용

    // Start is called before the first frame update
    void Start()
    {
        // 버튼 연결
        trainingButton_yj.onClick.AddListener(OntrainButtonClick);
        campingButton_yj.onClick.AddListener(OncampButtonClick);
        findhintButton_yj.onClick.AddListener(OnhintButtonClick);
        laybedButton_yj.onClick.AddListener(OnbedButtonClick);
        gotobedButton_yj.onClick.AddListener(OngobedButtonClick);

        noButton1.onClick.AddListener(OnNo1ButtonClick);
        noButton2.onClick.AddListener(OnNo2ButtonClick);
        noButton3.onClick.AddListener(OnNo3ButtonClick);
        noButton4.onClick.AddListener(OnNo4ButtonClick);
        noButton5.onClick.AddListener(OnNo5ButtonClick);
        //newbuttoncon_yj = GetComponent<newbuttoncontrol>(); 
        //nowplayer_yj = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerNow_yj>();

        isbasicdial_yj = false;
        playermanager_yj.playerNow.howtoday_py = 0;
        //nowplayer_yj.howtoday_py = 0; // 하루에 기본활동 몇번했는지 변수 0으로 시작
        playermanager_yj.playerNow.howtrain_py = 0;
        //nowplayer_yj.howtrain_py = 0; // 전체적으로 훈련 몇번했는지 변수 0으로 시작
        player = GameObject.FindGameObjectWithTag("Player"); // 태그가 "Player"인 오브젝트 찾기
        HideUI(); // 시작할 때 UI 숨기기
    }

    // Update is called once per frame
    void Update()
    {
        CheckNPCInteraction();
        HandleUserInput();
        //timeManager.UpdateDateAndTimeDisplay(); // 날짜와 시간 디스플레이 업데이트
    }

    void HandleUserInput()
    {
        if (currentNPC != null)
        {
            HandleNPCDialogue_yj(currentNPC); // npc한테 가까이 가면 대화창이 뜬다

            if (Input.GetKeyDown(KeyCode.Space))
            {              
                //Dial_changyj.SetActive(false);
                if (isbasicdial_yj)
                {
                    //Debug.Log("isbasic true");
                    HandleNPCchoice_yj(currentNPC);
                }
                else
                {
                    //Debug.Log("isbasic false");
                }
            }
        }
    }

    void CheckNPCInteraction()
    {
        float distanceNPC1 = Vector3.Distance(player.transform.position, npc1_yj.transform.position);
        float distanceNPC2 = Vector3.Distance(player.transform.position, npc2_yj.transform.position);
        float distanceNPC3 = Vector3.Distance(player.transform.position, npc3_yj.transform.position);
        float distanceNPC4 = Vector3.Distance(player.transform.position, npc4_yj.transform.position);


        if (distanceNPC1 <= interactionRange)
        {
            currentNPC = npc1_yj;
        }
        else if (distanceNPC2 <= interactionRange)
        {
            currentNPC = npc2_yj;
        }
        else if (distanceNPC3 <= interactionRange)
        {
            currentNPC = npc3_yj;
        }
        else if (distanceNPC4 <= interactionRange)
        {
            currentNPC = npc4_yj;
        }
        else
        {
            currentNPC = null;
        }
    }

    void HandleNPCDialogue_yj(GameObject npc_yjyj)
    {   
        if(isbasicdial_yj == false)
            Dial_changyj.SetActive(true);
        else if(choiceUI1_yj.activeSelf || choiceUI2_yj.activeSelf || choiceUI3_yj.activeSelf || choiceUI4_yj.activeSelf)
            Dial_changyj.SetActive(false);
        // 현재 NPC에 따라 대화 처리
        if (npc_yjyj == npc1_yj) // 훈련단장
        {
            dialoguename_yj.text = "훈련대장"; // 훈련대장 이름 출력 
            dialogueText_yj.text = "여어-말라깽이! \n훈련할 준비는 됐나? \n[스페이스바를 눌러 훈련을 진행하세요]"; // 훈련대장 기본 대사 출력
            isbasicdial_yj = true; // 기본대사 치고 있는 중
            
        }
        else if (npc_yjyj == npc2_yj) // 캠핑장
        {            
            dialoguename_yj.text = "캠핑장 주인"; // 캠핑장 이름 출력 
            dialogueText_yj.text = "어서오세요~캠핑장입니다. \n캠핑을 통하여 단합을 진행하실 건가요? \n[스페이스바를 눌러 단합을 진행하세요]"; // 캠핑장 기본 대사 출력
            isbasicdial_yj = true; // 기본대사 치고 있는 중
        }
        else if (npc_yjyj == npc3_yj) // 단서
        {
            dialoguename_yj.text = "단서"; // 단서 이름 출력 
            dialogueText_yj.text = "단서를 찾았다.\n인벤토리에서 내용을 확인해 보자.\n[스페이스바를 누르세요]"; // 단서 기본 대사 출력                                      
            isbasicdial_yj = true; // 기본대사 치고 있는 중
        }
        else if (npc_yjyj == npc4_yj) // 침대
        {
            dialoguename_yj.text = "침대"; // 훈련대장 이름 출력 
            dialogueText_yj.text = "아늑한 내 방의 침대다.\n편안히 휴식을 취해 보자. \n[스페이스바를 눌러 휴식을 진행하세요]"; // 훈련대장 기본 대사 출력                                      
            isbasicdial_yj = true; // 기본대사 치고 있는 중
        }
    }

    void HandleNPCchoice_yj(GameObject npc_yjyj)
    {
        // 현재 NPC에 따라 선택지처리
        if (npc_yjyj == npc1_yj) // 훈련단장
        {           
            //Debug.Log("훈련단장이랑 얘기중");
            Dial_changyj.SetActive(false);
            isbasicdial_yj = false;
            choiceUI1_yj.SetActive(true);

        }
        else if (npc_yjyj == npc2_yj) // 캠핑장
        {
            Dial_changyj.SetActive(false);
            isbasicdial_yj = false;
            choiceUI2_yj.SetActive(true);
        }
        else if (npc_yjyj == npc3_yj) // 단서
        {
            Dial_changyj.SetActive(false);
            isbasicdial_yj = false;
            choiceUI3_yj.SetActive(true);
        }
        else if (npc_yjyj == npc4_yj) // 침대
        {

            Dial_changyj.SetActive(false);
            isbasicdial_yj = false;
            choiceUI4_yj.SetActive(true);
        }
    }
    // 버튼 이벤트 연결
    // 기본활동1 : "훈련한다" 선택했을 때
    public void OntrainButtonClick()
    {
        //Dial_changyj.SetActive(false);
        choiceUI1_yj.SetActive(false);
        
        // 변수 계산
        playermanager_yj.IncreaseTrainingCount();// 하루 훈련 활동 횟수 1 증가
        playermanager_yj.IncreaseTiredness(10);// 피로도 10 증가
        timemanager_yj.CompleteActivity();// 하루 기본 활동 수행 횟수 1 증가
        resuedit_yj.text = $"기본활동 횟수: {timemanager_yj.activityCount/2} / 3"; // 기본 활동 텍스트 업데이트
        if ( timemanager_yj.activityCount >= 5)
        {
            resuedit2_yj.text = "하루치 기본 활동 3개를 모두 완수하셨습니다!\n[주인공 집]의 [침대]로 돌아가 휴식을 취해주세요! ";
            resultUI2_yj.SetActive(true);
        }

            resultUI_yj.SetActive(true);
    }

    // 기본활동2 : 단합한다 했을 때
    public void OncampButtonClick()
    {
        choiceUI2_yj.SetActive(false); // 선택 UI 비활성화

        // 변수 계산
        playermanager_yj.IncreaseTeamPower(1);// 하루 단합 활동 횟수 1 증가
        timemanager_yj.CompleteActivity();// 하루 기본 활동 수행 횟수 1 증가

        // 결과창 업데이트
        resuedit_yj.text = $"기본활동 횟수: {timemanager_yj.activityCount/2} / 3"; // 기본 활동 텍스트 업데이트
        if (timemanager_yj.activityCount >=5)
        {
            resuedit2_yj.text = "하루치 기본 활동 3개를 모두 완수하셨습니다!\n[주인공 집]의 [침대]로 돌아가 휴식을 취해주세요! ";
            resultUI2_yj.SetActive(true);
        }
        resultUI_yj.SetActive(true);
        


    }
    // 기본활동3 : 단서 보겠다 했을 때
    public void OnhintButtonClick()
    {
        choiceUI3_yj.SetActive(false);
        timemanager_yj.CompleteActivity();// 하루 기본 활동 수행 횟수 1 증가
        resuedit_yj.text = $"기본활동 횟수: {timemanager_yj.activityCount / 2} / 3"; // 기본 활동 텍스트 업데이트
        if ( timemanager_yj.activityCount >=5)
        {
            resuedit2_yj.text = "하루치 기본 활동 3개를 모두 완수하셨습니다!\n[주인공 집]의 [침대]로 돌아가 휴식을 취해주세요! ";
            resultUI2_yj.SetActive(true);
        }

        resultUI_yj.SetActive(true);

        Invoke("HideResultPanel()", 2f);
        SceneManager.LoadScene("InventoryMain"); // 인벤토리 씬으로 이동
        // 찾은 단서 개수를 한 개 늘림. 이건 인벤토리랑 연관 후에 생각해야 할듯
    }
    // 기본활동4 : 휴식 취하겠다 했을 때
    public void OnbedButtonClick()
    {
        choiceUI4_yj.SetActive(false);
        resultUI_yj.SetActive(true);

        timemanager_yj.CompleteActivity();// 하루 기본 활동 수행 횟수 1 증가
        // 결과창 업데이트
        resuedit_yj.text = $"기본활동 횟수: {timemanager_yj.activityCount / 2} / 3"; // 기본 활동 텍스트 업데이트
        if ( timemanager_yj.activityCount >= 5)
        {
            resuedit2_yj.text = "하루치 기본 활동 3개를 모두 완수하셨습니다!\n[주인공 집]의 [침대]로 돌아가 휴식을 취해주세요! ";
            resultUI2_yj.SetActive(true);
        }
        resultUI_yj.SetActive(true);
    }
    public void OngobedButtonClick()
    {
        SceneManager.LoadScene("main_house"); // 침대 씬으로 이동
    }

    public void OnNo1ButtonClick()
    {
        choiceUI1_yj.SetActive(false); // 훈련 UI 선택창 비활성화
        isbasicdial_yj = false;
    }
    public void OnNo2ButtonClick()
    {
        choiceUI2_yj.SetActive(false); // 단합 UI 선택창 비활성화
        isbasicdial_yj = false;
    }
    public void OnNo3ButtonClick()
    {
        choiceUI3_yj.SetActive(false); // 단서 UI 선택창 비활성화
        isbasicdial_yj = false;
    }
    public void OnNo4ButtonClick()
    {
        choiceUI4_yj.SetActive(false); // 휴식 UI 선택창 비활성화
        isbasicdial_yj = false;
    }
    public void OnNo5ButtonClick()
    {
        resultUI_yj.SetActive(false); // 결과 UI 선택창 비활성화
        resultUI2_yj.SetActive(false); // 결과 UI 선택창 비활성화
        isbasicdial_yj = false;
    }


    public void HideUI()
{
    choiceUI1_yj.SetActive(false);
    choiceUI2_yj.SetActive(false);
    choiceUI3_yj.SetActive(false);
    choiceUI4_yj.SetActive(false);
    Dial_changyj.SetActive(false);
    resultUI_yj.SetActive(false);
    resultUI2_yj.SetActive(false);
    }

    void HideResultPanel()
    {
        resultUI_yj.SetActive(false);
    }
}