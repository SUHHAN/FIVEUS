using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting; // TextMeshPro 네임스페이스 추가
using UnityEngine.SceneManagement;
using System.Xml.Serialization;

// 집 가면 나오는거(휴식4)
// 잠 자는 코드 추가해야 함 
public class bedin4_yj : MonoBehaviour
{
    private bool isbasicdial_yj = false; // 대사 치고 있는지 여부

    public GameObject choiceUI4_yj; // 기본활동4 UI 패널
    public GameObject iaminbedUI_yj;// 휴식중 UI 패널

    public GameObject Dial_changyj; // 기본대사 띄울 대화창
    public TextMeshProUGUI dialoguename_yj; // name text
    public TextMeshProUGUI dialogueText_yj; // line text
    public float interactionRange = 3f; // 상호작용 거리

    private GameObject player; // 플레이어 오브젝트
    private GameObject currentNPC; // 현재 상호작용하는 NPC 저장 변수

    public GameObject npc4_yj; // 휴식

    public Button noButton4; // 아니오 버튼 연결4
    public Button noButton5; // 결과창 닫기 버튼 연결5
    public Button noButton6; // 휴식창 닫기 버튼 연결

    public Button laybedButton_yj; // 4. 휴식하기 버튼 연결
    public Button gotobedButton_yj; //5. 침대 이동 버튼 연결

    public GameObject resultUI_yj; // 결과 UI 패널
    public GameObject resultUI2_yj;
    public TextMeshProUGUI resuedit_yj; // result edit text
    public TextMeshProUGUI resuedit2_yj; // result edit text

    // 기본활동 몇번 진행했는지 세야 하니..플레이어를 부르자
    // public PlayerNow_yj nowplayer_yj;
    public PlayerManager_yj playermanager_yj; // 체력관리용
    public TimeManager timemanager_yj; // 날짜 관리 + 기본활동 덧뺄셈용

    private static bedin4_yj _instance;

    public static bedin4_yj Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<bedin4_yj>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("talkwithjjang_yj");
                    _instance = obj.AddComponent<bedin4_yj>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        /*
        laybedButton_yj.onClick.AddListener(OnbedButtonClick);
        gotobedButton_yj.onClick.AddListener(OngobedButtonClick);
       
        noButton4.onClick.AddListener(OnNo4ButtonClick);
        noButton5.onClick.AddListener(OnNo5ButtonClick);
        noButton6.onClick.AddListener(OnNo6ButtonClick);
        */
        isbasicdial_yj = false;
        player = GameObject.FindGameObjectWithTag("Player"); // 태그가 "Player"인 오브젝트 찾기
        HideUI(); // 시작할 때 UI 숨기기
    }

    // Update is called once per frame
    void Update()
    {
        CheckNPCInteraction();
        HandleUserInput();
        //timeManager_yj.UpdateDateAndTimeDisplay(); 
    }

    void HandleUserInput()
    {
        if (currentNPC != null)
        {
            HandleNPCDialogue_yj(currentNPC); // npc한테 가까이 가면 대화창이 뜬다

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isbasicdial_yj)
                    HandleNPCchoice_yj(currentNPC);
            }
        }
    }

    void CheckNPCInteraction()
    {
        float distanceNPC4 = Vector3.Distance(player.transform.position, npc4_yj.transform.position);

        if (distanceNPC4 <= interactionRange)
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
        if (isbasicdial_yj == false)
            Dial_changyj.SetActive(true);
        else if (resultUI_yj.activeSelf || choiceUI4_yj.activeSelf)
            Dial_changyj.SetActive(false);
        // 현재 NPC에 따라 대화 처리
        if (npc_yjyj == npc4_yj) // 침대
        {
            dialoguename_yj.text = "침대"; // 침대 이름 출력 
            dialogueText_yj.text = "아늑한 내 방의 침대다.\n편안히 휴식을 취해 보자. \n[스페이스바를 눌러 휴식을 진행하세요]"; // 침대 특수 대사 출력                                      
            isbasicdial_yj = true; // 기본대사 치고 있는 중

            if (DataManager.instance.nowPlayer.Player_howtoday >= 2)
            {
                // 주인공이 자야만 할 때
                dialoguename_yj.text = "침대"; // 침대 이름 출력 
                dialogueText_yj.text = "고된 하루를 마치고 자는 잠은 꿀잠이지! .\n오늘 하루도 수고했어~. \n[스페이스바를 눌러 수면을 취하세요]"; // 침대 특수 대사 출력                                      
                isbasicdial_yj = true; // 기본대사 치고 있는 중

                PlayerPrefs.DeleteKey("bedGood");
                PlayerPrefs.Save();

            }
        }
    }

    void HandleNPCchoice_yj(GameObject npc_yjyj)
    {
        if (npc_yjyj == npc4_yj) // 침대
        {

            Dial_changyj.SetActive(false);
            isbasicdial_yj = false;
            choiceUI4_yj.SetActive(true);

            if(DataManager.instance.nowPlayer.Player_howtoday >= 2)
            {
                // 선택지 말고 자고있다는 창이 2초동안 뜨도록
                Dial_changyj.SetActive(false);
                isbasicdial_yj = false;
                iaminbedUI_yj.SetActive(true);
            }
        }
    }
    
    // 기본활동4 : 휴식 취하겠다 했을 때
    public void OnbedButtonClick()
    {
        choiceUI4_yj.SetActive(false);
        resultUI_yj.SetActive(true);
        // 피로도 또는 체력 수정
        timemanager_yj.CompleteActivity();// 하루 기본 활동 수행 횟수 1 증가

        // 결과창 업데이트
        resuedit_yj.text = $"기본활동 횟수 : {DataManager.instance.nowPlayer.Player_howtoday} / 3"; // 기본 활동 텍스트 업데이트

        if (DataManager.instance.nowPlayer.Player_howtoday >= 2)
        {
            resuedit2_yj.text = "하루치 기본 활동 3개를 모두 완수하셨습니다!\n[주인공 집]의 [침대]로 돌아가 휴식을 취해주세요!";
            resultUI2_yj.SetActive(true);
        }

        resultUI_yj.SetActive(true);
        SaveData();
    }
    public void OngobedButtonClick()
    {
     
        // 사실 OnNo5ButtonClick()버튼이랑 같은 기능..이미 씬에 있는데 또 이동시키면 오류날까봐 
        resultUI_yj.SetActive(false); // 결과 UI 선택창 비활성화
        resultUI2_yj.SetActive(false); // 결과2 UI 선택창 비활성화
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
        resultUI2_yj.SetActive(false); // 결과2 UI 선택창 비활성화
        isbasicdial_yj = false;
    }
    public void OnNo6ButtonClick()
    {
        iaminbedUI_yj.SetActive(false); // 휴식완료 UI 창 비활성화
        isbasicdial_yj = false;
        timemanager_yj.UpdateDateAndTimeDisplay();// 다음날 날짜 뜸
    }

    public void HideUI()
    {
        choiceUI4_yj.SetActive(false);
        Dial_changyj.SetActive(false);
        resultUI_yj.SetActive(false);
    }
    void HideResultPanel()
    {
        resultUI_yj.SetActive(false);
    }

    void SaveData()
    {
        // DataManager.instance.nowPlayer.Player_hp = playermanager_yj.playerNow.hp_py;
        // DataManager.instance.nowPlayer.Player_tired = playermanager_yj.playerNow.tired_py;
        // DataManager.instance.nowPlayer.Player_money = playermanager_yj.playerNow.money_py;
        // DataManager.instance.nowPlayer.Player_hint = playermanager_yj.playerNow.hint_py;
        // DataManager.instance.nowPlayer.Player_team = playermanager_yj.playerNow.team_py;
        // DataManager.instance.nowPlayer.Player_day = playermanager_yj.playerNow.day_py;
        // DataManager.instance.nowPlayer.Player_howtoday = playermanager_yj.playerNow.howtoday_py;
        // DataManager.instance.nowPlayer.Player_howtrain = playermanager_yj.playerNow.howtrain_py;

        DataManager.instance.SaveData();
    }
}
