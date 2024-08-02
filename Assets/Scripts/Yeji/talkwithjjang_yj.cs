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
    public newbuttoncontrol newbuttoncon_yj;
    private bool isbasicdial_yj = false; // 대사 치고 있는지 여부
    private bool isResultActive_yj = false; // 결과 UI가 활성화 되어 있는지 여부


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

    // 기본활동 몇번 진행했는지 세야 하니..플레이어를 부르자
    public PlayerNow_yj nowplayer_yj;

    private bool iswquestion_yj = true; // 제발 선택지 패널이 뜨지 않게 해주세요

    // Start is called before the first frame update
    void Start()
    {
        //newbuttoncon_yj = GetComponent<newbuttoncontrol>(); 
        //nowplayer_yj = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerNow_yj>();

        nowplayer_yj.howtoday_py = 0; // 하루에 기본활동 몇번했는지 변수 0으로 시작
        nowplayer_yj.howtrain_py = 0; // 전체적으로 훈련 몇번했는지 변수 0으로 시작
        player = GameObject.FindGameObjectWithTag("Player"); // 태그가 "Player"인 오브젝트 찾기
        HideUI(); // 시작할 때 UI 숨기기
    }

    // Update is called once per frame
    void Update()
    {
        CheckNPCInteraction();
        HandleUserInput();
        // 이곳에 코드를 입력하시오. 필요하다면 다른 메소드를 채워도 됩니다. 

    }

    void HandleUserInput()
    {
        if (currentNPC != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!isbasicdial_yj && !Dial_changyj.activeSelf && !isResultActive_yj)
                {
                    // Step 1: No action for space key press
                    // According to step 1 requirements
                    // Do nothing on space key
                }
                else if (Dial_changyj.activeSelf)
                {
                    // According to step 2 requirements
                    Dial_changyj.SetActive(false);
                    choiceUI1_yj.SetActive(true);
                    newbuttoncon_yj.resultUI_yj.SetActive(false);
                    isbasicdial_yj = false;
                    isResultActive_yj = false;
                }
                else if (newbuttoncon_yj.resultUI_yj.activeSelf)
                {
                    // Step 4: Hide result UI on space key press
                    HideUI();
                    isResultActive_yj = false;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                if (isbasicdial_yj)
                {
                    HandleNPCDialogue_yj(currentNPC);
                    // According to step 1 requirements
                    Dial_changyj.SetActive(true);
                    choiceUI1_yj.SetActive(false);
                    newbuttoncon_yj.resultUI_yj.SetActive(false);
                    isResultActive_yj = false;
                }
                else if (Dial_changyj.activeSelf)
                {
                    // According to step 2 requirements
                    Dial_changyj.SetActive(false);
                    choiceUI1_yj.SetActive(true);
                    newbuttoncon_yj.resultUI_yj.SetActive(false);
                    isResultActive_yj = false;
                }
                else if (choiceUI1_yj.activeSelf)
                {
                    // According to step 3 requirements
                    Dial_changyj.SetActive(false);
                    choiceUI1_yj.SetActive(false);
                    newbuttoncon_yj.resultUI_yj.SetActive(true);
                    isResultActive_yj = true;
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

        //if (currentNPC != null && nowplayer_yj.howtoday_py<3)
        /*if (currentNPC != null) // 상호작용 거리 내에 있는지, 기본 활동 횟수 확인
        {
            if (Input.GetKeyDown(KeyCode.Return)) // 엔터 키 입력 감지
            {
                HandleNPCDialogue_yj(currentNPC); // npc별로 다르게 알려주기
            }
        }
        else
        {
            HideUI();
        }*/
    }

    void HandleNPCDialogue_yj(GameObject npc_yjyj)
    {
        Dial_changyj.SetActive(true);
        // 현재 NPC에 따라 대화 처리
        if (npc_yjyj == npc1_yj) // 훈련단장
        {
            dialoguename_yj.text = "훈련대장"; // 훈련대장 이름 출력 
            dialogueText_yj.text = "여어-말라깽이! \n훈련할 준비는 됐나? \n [스페이스바를 두 번 눌러 훈련을 진행하세요]"; // 훈련대장 기본 대사 출력
            //isbasicdial_yj = true; // 기본대사 치고 있는 중
            
        }
        else if (npc_yjyj == npc2_yj) // 캠핑장
        {            
            dialoguename_yj.text = "캠핑장 주인"; // 캠핑장 이름 출력 
            dialogueText_yj.text = "어서오세요~캠핑장입니다. \n캠핑을 통하여 단합을 진행하실 건가요? \n [스페이스바를 두 번 눌러 단합을 진행하세요]"; // 캠핑장 기본 대사 출력
            //isbasicdial_yj = true; // 기본대사 치고 있는 중
        }
        else if (npc_yjyj == npc3_yj) // 단서
        {
            dialoguename_yj.text = "단서"; // 단서 이름 출력 
            dialogueText_yj.text = "단서를 찾았다.\n인벤토리에서 내용을 확인해 보자."; // 단서 기본 대사 출력                                      
            //isbasicdial_yj = true; // 기본대사 치고 있는 중
        }
        else if (npc_yjyj == npc4_yj) // 침대
        {
            dialoguename_yj.text = "침대"; // 훈련대장 이름 출력 
            dialogueText_yj.text = "아늑한 내 방의 침대다.\n편안히 휴식을 취해 보자. \n [스페이스바를 눌러 휴식을 진행하세요]"; // 훈련대장 기본 대사 출력                                      
            //isbasicdial_yj = true; // 기본대사 치고 있는 중
        }
    }

void HideUI()
{
    choiceUI1_yj.SetActive(false);
    choiceUI2_yj.SetActive(false);
    choiceUI3_yj.SetActive(false);
    choiceUI4_yj.SetActive(false);
    Dial_changyj.SetActive(false);
    newbuttoncon_yj.resultUI_yj.SetActive(false);
}
    void EndDialogue()
    {
        Dial_changyj.SetActive(false); // 대화 UI 숨기기
        //isbasicdial_yj = false; // 대화 상태 해제
    }

    public void ActivateChoiceUI1()
    {
        if (Dial_changyj.activeSelf)
        {
            Dial_changyj.SetActive(false);
            choiceUI1_yj.SetActive(true);
            newbuttoncon_yj.resultUI_yj.SetActive(false);
            isbasicdial_yj = false;
            isResultActive_yj = false;
        }
    }
}