using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting; // TextMeshPro 네임스페이스 추가
using UnityEngine.SceneManagement;

// 기본 활동 진행하는 스크립트
public class talkwithjjang_yj : MonoBehaviour
{
    // 기본 활동 패널들
    public GameObject choiceUI1_yj; // 기본활동1 UI 패널
    public GameObject choiceUI2_yj; // 기본활동2 UI 패널
    public GameObject choiceUI3_yj; // 기본활동3 UI 패널
    public GameObject choiceUI4_yj; // 기본활동4 UI 패널

    public GameObject trainingUI_yj; // 훈련 중 패널
    public GameObject campingUI_yj; // 단합 중 패널
    public GameObject iaminbedUI_yj; // 휴식 UI 패널
    public GameObject resultUI_yj; // 결과 UI 패널

    public GameObject Dial_changyj; // 기본대사 띄울 대화창
    public TextMeshProUGUI dialoguename_yj; // name text
    public TextMeshProUGUI dialogueText_yj; // line text

    public Button noButton1; // 아니오 버튼 연결1
    public Button noButton2; // 아니오 버튼 연결2
    public Button noButton3; // 아니오 버튼 연결3
    public Button noButton4; // 아니오 버튼 연결4

    // 기본 활동 버튼들
    public Button trainingButton_yj; // 1. 훈련 시도 버튼 연결
    public Button campingButton_yj; // 2. 단합 시도 버튼 연결
    public Button findhintButton_yj; // 3. 단서 보기 버튼 연결
    public Button laybedButton_yj; // 4. 휴식하기 버튼 연결
    public float interactionRange = 1.5f; // 상호작용 거리

    public GameObject player; // 플레이어 오브젝트
    private GameObject currentNPC; // 현재 상호작용하는 NPC 저장 변수
    public GameObject npc1_yj; // 훈련단장
    public GameObject npc2_yj; // 캠핑장
    public GameObject npc3_yj; // 힌트
    public GameObject npc4_yj; // 휴식

    // 기본활동 몇번 진행했는지 세야 하니..인물을 부르자
    public PlayerNow_yj nowplayer_yj;

    // Start is called before the first frame update
    void Start()
    {
        nowplayer_yj = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerNow_yj>();

        nowplayer_yj.howtoday_py = 0; // 하루에 기본활동 몇번했는지 변수 0으로 시작
        nowplayer_yj.howtrain_py = 0; // 전체적으로 훈련 몇번했는지 변수 0으로 시작
        player = GameObject.FindGameObjectWithTag("Player"); // 태그가 "Player"인 오브젝트 찾기
        HideUI(); // 시작할 때 UI 숨기기
    }

    // Update is called once per frame
    void Update()
    {
        CheckNPCInteraction(); // NPC와의 상호작용 체크
        // 엔터키 입력 감지
        if (currentNPC != null && Input.GetKeyDown(KeyCode.Return))
        {
            HandleNPCDialogue_yj(currentNPC);
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

        //if (currentNPC != null && nowplayer_yj.howtoday_py<3)
        if (currentNPC != null) // 상호작용 거리 내에 있는지, 기본 활동 횟수 확인
        {
            if (Input.GetKeyDown(KeyCode.Return)) // 엔터 키 입력 감지
            {
                HandleNPCDialogue_yj(currentNPC);
            }
        }
        else
        {
            HideUI();
        }
    }

    void HandleNPCDialogue_yj(GameObject npc_yjyj)
    {
        Dial_changyj.SetActive(true);
        // 현재 NPC에 따라 대화 처리
        if (npc_yjyj == npc1_yj) // 훈련단장
        {
            dialoguename_yj.text = "훈련대장"; // 훈련대장 이름 출력 
            dialogueText_yj.text = "여어-말라깽이! \n훈련할 준비는 됐나?"; // 훈련대장 기본 대사 출력
            ShowChoice1UI_yj();
            

        }
        else if (npc_yjyj == npc2_yj) // 캠핑장
        {            
            dialoguename_yj.text = "캠핑장"; // 캠핑장 이름 출력 
            dialogueText_yj.text = "캠핑을 통하여 단합을 진행하실 건가요?"; // 캠핑장 기본 대사 출력                                      
            ShowChoice2UI_yj(); // 캠핑 선택 UI 표시
        }
        else if (npc_yjyj == npc3_yj) // 단서
        {
            dialoguename_yj.text = "단서"; // 단서 이름 출력 
            dialogueText_yj.text = "단서를 찾았다.\n인벤토리에서 내용을 확인해 보자."; // 단서 기본 대사 출력                                      
            ShowChoice3UI_yj(); // 캠핑 선택 UI 표시
        }
        else if (npc_yjyj == npc4_yj) // 침대
        {
            dialoguename_yj.text = "침대"; // 훈련대장 이름 출력 
            dialogueText_yj.text = "아늑한 내 방의 침대다.\n편안히 휴식을 취해 보자."; // 훈련대장 기본 대사 출력                                      
            ShowChoice4UI_yj(); // 캠핑 선택 UI 표시
        }

        // 훈련단장(6001)을 만났을 떄는 "훈련하시겠습니까?" 선택지 뜸
        // 기사단장(6002)을 만났을 떄는 "단합을 진행하시겠습니까?" 선택지 뜸
        // 단서(6003)를 만났을 떄는 "단서를 조사하시겠습니까?" 선택지 뜸
        // 침대(6004)를 만났을 떄는 "휴식을 취하시겠습니까?" 선택지 뜸
    }




void ShowChoice1UI_yj()
{        
        choiceUI1_yj.SetActive(true);
}

void ShowChoice2UI_yj()
{
    HideUI();
    choiceUI2_yj.SetActive(true);
}

void ShowChoice3UI_yj()
{
    HideUI();
    choiceUI3_yj.SetActive(true);
}

void ShowChoice4UI_yj()
{
    HideUI();
    choiceUI4_yj.SetActive(true);
}

void HideUI()
{
    choiceUI1_yj.SetActive(false);
    choiceUI2_yj.SetActive(false);
    choiceUI3_yj.SetActive(false);
    choiceUI4_yj.SetActive(false);
    Dial_changyj.SetActive(false);
}
   
    void DisableResultUI_yj()
    {
        resultUI_yj.SetActive(false);
        
    }
    void DisabletrainUI_yj()
    {
        trainingUI_yj.SetActive(false);
    }
    void DisablecampUI_yj()
    {
        campingUI_yj.SetActive(false);
    }
    void DisablebedUI_yj()
    {
        iaminbedUI_yj.SetActive(false);
    }
    // 기본활동1 : "훈련한다" 선택했을 때
    public void OntrainButtonClick()
    {
        choiceUI1_yj.SetActive(false); // 선택 UI 비활성화

        trainingUI_yj.SetActive(true);// 훈련 UI 표시(3초간 지속)
        Invoke("DisabletrainUI_yj", 3f); // 3초 후에 훈련 UI를 자동으로 비활성화 처리하는 메서드 
        resultUI_yj.SetActive(true);// 결과 창 표시 (성공UI, 1,2 동시 사용)
        nowplayer_yj.howtrain_py++;// 훈련변수 1 증가
        nowplayer_yj.howtoday_py++;// 하루 기본 활동 수행 횟수 1 증가        
        Invoke("DisableResultUI_yj", 3f); // 3초 후에 결과 UI를 자동으로 비활성화 처리하는 메서드 호출
    }
    // 기본활동2 : 단합한다 했을 때
    public void OncampButtonClick()
    {
        choiceUI2_yj.SetActive(false); // 선택 UI 비활성화
        campingUI_yj.SetActive(true);// 단합 UI 표시(3초간 지속)
        Invoke("DisablecampUI_yj", 3f); // 3초 후에 단합 UI를 자동으로 비활성화 처리하는 메서드 
        resultUI_yj.SetActive(true);// 결과 창 표시 (성공UI, 1,2 동시 사용)
        nowplayer_yj.team_py++; // 단합변수 1 증가
        nowplayer_yj.howtoday_py++; // 하루 기본 활동 수행 횟수 1 증가
        Invoke("DisableResultUI_yj", 3f); // 3초 후에 결과 UI를 자동으로 비활성화 처리하는 메서드 호출
    }
    // 기본활동3 : 단서 보겠다 했을 때
    public void OnhintButtonClick()
    { 
        choiceUI3_yj.SetActive(false);
        SceneManager.LoadScene("InventoryMain"); // 인벤토리 씬으로 이동
        // 찾은 단서 개수를 한 개 늘림. 이건 인벤토리랑 연관 후에 생각해야 할듯
    }
    // 기본활동4 : 휴식 취하겠다 했을 때
    public void OnbedButtonClick()
    {
        choiceUI4_yj.SetActive(false);
        iaminbedUI_yj.SetActive(true); // 휴식 UI 띄우기
        
    }
    public void OnNoButtonClick()
     {
        // 이거 뭐 켜져있는지 따져봐서 비활성화 따로 해야 하나? 
        choiceUI1_yj.SetActive(false); // 훈련 UI 선택창 비활성화
        choiceUI2_yj.SetActive(false); // 단합 UI 선택창 비활성화
        choiceUI3_yj.SetActive(false); // 단서 UI 선택창 비활성화
        choiceUI4_yj.SetActive(false); // 휴식 UI 선택창 비활성화
    }

}