using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting; // TextMeshPro 네임스페이스 추가
using UnityEngine.SceneManagement;
using System.Xml.Serialization;

// 의뢰 + 단서
// 기본활동 5번째 의뢰 구현(5인데 5가 있으니까 7번으로 설정)
public class QuestData_yj : MonoBehaviour
{
    // random gold 
    private bool isbasicdial_yj = false; // 대사 치고 있는지 여부

    public GameObject choiceUI3_yj; // 기본활동3 UI 패널
    public GameObject choiceUI7_yj; // 기본활동7 UI 패널

    public GameObject Dial_changyj; // 기본대사 띄울 대화창
    public TextMeshProUGUI dialoguename_yj; // name text
    public TextMeshProUGUI dialogueText_yj; // line text
    public float interactionRange = 1f; // 상호작용 거리

    private GameObject player; // 플레이어 오브젝트
    private GameObject currentNPC; // 현재 상호작용하는 NPC 저장 변수

    public GameObject npc3_yj; // 힌트
    public GameObject npc7_yj; // 우체통
    public TextMeshProUGUI QuestEditText_yj; // result edit text

    public Button noButton3; // 아니오 버튼 연결3
    public Button noButton7; // 아니오 버튼 연결7
    public Button noButton5; // 결과창 닫기 버튼 연결5

    public Button findhintButton_yj; // 3. 단서 보기 버튼 연결
    public Button myreasonButton_yj; //7. 의뢰 하기 버튼 연결
    public Button gotobedButton_yj; //5. 침대 이동 버튼 연결

    public GameObject resultUI_yj; // 결과 UI 패널
    public GameObject resultUI2_yj;
    public TextMeshProUGUI resuedit_yj; // result edit text
    public TextMeshProUGUI resuedit2_yj; // result edit text

    // 기본활동 몇번 진행했는지 세야 하니..플레이어를 부르자
    // public PlayerNow_yj nowplayer_yj;
    public PlayerManager_yj playermanager_yj; // 체력관리용
    public TimeManager timemanager_yj; // 날짜 관리 + 기본활동 덧뺄셈용

    private int questmoneyy_yj;// (여기서만 사용됨) : 의뢰 가격

    private static QuestData_yj _instance;

    public static QuestData_yj Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<QuestData_yj>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("talkwithjjang_yj");
                    _instance = obj.AddComponent<QuestData_yj>();
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

       /* findhintButton_yj.onClick.AddListener(OnhintButtonClick);
        myreasonButton_yj.onClick.AddListener(OnQuestButtonClick);
        gotobedButton_yj.onClick.AddListener(OngobedButtonClick);

        noButton3.onClick.AddListener(OnNo3ButtonClick);
        noButton5.onClick.AddListener(OnNo5ButtonClick);
        noButton7.onClick.AddListener(OnNo7ButtonClick);*/

        isbasicdial_yj = false;
        questmoneyy_yj = 0;
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
                {
                    HandleNPCchoice_yj(currentNPC);
                }

            }
        }
    }

    void CheckNPCInteraction()
    {
        if (npc3_yj != null)
        {
            float distanceNPC3 = Vector3.Distance(player.transform.position, npc3_yj.transform.position);

            if (distanceNPC3 <= interactionRange)
            {
                currentNPC = npc3_yj;
            }
            else
            {
                currentNPC = null;
            }
        }
        if (npc7_yj != null)
        {
            float distanceNPC7 = Vector3.Distance(player.transform.position, npc7_yj.transform.position);
            //Debug.Log("distanceNPC7 : " + distanceNPC7);

            if (distanceNPC7 <= interactionRange)
            {
                currentNPC = npc7_yj;
            }
            else
            {
                currentNPC = null;
            }
        }

        
    }
    // 의뢰 가격 결정하는 메소드(500-1000골드)
    int QuestMomey_yj()
    {
        // 이곳에 랜덤하게 questmoneyy_yj를 조작하는 코드 작성
        System.Random random = new System.Random();
        int randomValue = random.Next(5, 11);
        int goldcal_yj = randomValue * 100;
        return goldcal_yj;
    }

    void HandleNPCDialogue_yj(GameObject npc_yjyj)
    {
        if (isbasicdial_yj == false)
            Dial_changyj.SetActive(true);
        else if (resultUI_yj.activeSelf || choiceUI7_yj.activeSelf || choiceUI3_yj.activeSelf)
            Dial_changyj.SetActive(false);
        // 현재 NPC에 따라 대화 처리
        if (npc_yjyj == npc7_yj) // 의뢰
        {
            dialoguename_yj.text = "새로운 의뢰"; // 의뢰 이름 출력 

            // 퀘스트 금액을 함수 호출하여 가져오고, 문자열 보간 사용
            if (questmoneyy_yj == 0)
            {
                questmoneyy_yj = QuestMomey_yj();
            }
            dialogueText_yj.text = $"새로운 의뢰가 들어왔다. \n{questmoneyy_yj}골드?! 엄청나잖아! \n[스페이스바를 눌러 진행하세요]";
            isbasicdial_yj = true; // 기본대사 치고 있는 중
        }
        else if (npc_yjyj == npc3_yj) // 단서
        {
            dialoguename_yj.text = "단서"; // 단서 이름 출력 
            dialogueText_yj.text = "단서를 찾았다.\n인벤토리에서 내용을 확인해 보자.\n[스페이스바를 누르세요]"; // 단서 기본 대사 출력                                      
            isbasicdial_yj = true; // 기본대사 치고 있는 중
        }
    }

    void HandleNPCchoice_yj(GameObject npc_yjyj)
    {
        // 현재 NPC에 따라 선택지처리
        if (npc_yjyj == npc7_yj) // 의뢰
        {
            Dial_changyj.SetActive(false);
            isbasicdial_yj = false;
            QuestEditText_yj.text = $"보상 : {questmoneyy_yj}G";
            choiceUI7_yj.SetActive(true);
        }
        else if (npc_yjyj == npc3_yj) // 단서
        {
            Dial_changyj.SetActive(false);
            isbasicdial_yj = false;
            choiceUI3_yj.SetActive(true);
        }

    }
    // 기본활동3 : 단서 보겠다 했을 때
    public void OnhintButtonClick()
    {
        Debug.Log("단서 클릭");
        choiceUI3_yj.SetActive(false);
        timemanager_yj.CompleteActivity(); // 하루 기본 활동 수행 횟수 1 증가
        DataManager.instance.nowPlayer.Player_hint += 1;

        resuedit_yj.text = $"기본활동 횟수 : {DataManager.instance.nowPlayer.Player_howtoday}";
        if (timemanager_yj.activityCount == 2)
        {
            resuedit2_yj.text = "하루치 기본 활동 3개를 모두 완수하셨습니다!\n[주인공 집]의 [침대]로 돌아가 휴식을 취해주세요!";
            resultUI2_yj.SetActive(true);
        }

        resultUI_yj.SetActive(true);

        Invoke("HideResultPanel()", 2f);

        SaveData();
        // 인벤토리에 힌트 랜덤 추가
        ItemManager.instance.GetHint_inv();

        SceneManager.LoadScene("InventoryMain"); // 인벤토리 씬으로 이동
        // 찾은 단서 개수를 한 개 늘림. 이건 인벤토리랑 연관 후에 생각해야 할듯
    }

    // 기본활동7 : 의뢰한다 했을 때
    public void OnQuestButtonClick()
    {
        choiceUI7_yj.SetActive(false); // 선택 UI 비활성화

        // 변수 계산
        playermanager_yj.DecreaseHealth(10);// 하루 체력 10 감소
        playermanager_yj.IncreaseMoney(questmoneyy_yj);// 재화 증가
        timemanager_yj.CompleteActivity(); // 하루 기본 활동 수행 횟수 1 증가

        // 결과창 업데이트
        resuedit_yj.text = $"기본활동 횟수 : {DataManager.instance.nowPlayer.Player_howtoday}\n총 골드 : {DataManager.instance.nowPlayer.Player_money} G"; // 기본 활동 텍스트 업데이트

        if (timemanager_yj.activityCount == 5)
        {
            resuedit2_yj.text = "하루치 기본 활동 3개를 모두 완수하셨습니다!\n[주인공 집]의 [침대]로 돌아가 휴식을 취해주세요!";
            resultUI2_yj.SetActive(true);
        }

        resultUI_yj.SetActive(true);
        SaveData();
    }

    public void OngobedButtonClick()
    {
        SceneManager.LoadScene("main_house");
    }
    public void OnNo7ButtonClick()
    {
        choiceUI7_yj.SetActive(false); // 의뢰 UI 선택창 비활성화
        isbasicdial_yj = false;
    }
    public void OnNo3ButtonClick()
    {
        choiceUI3_yj.SetActive(false); // 단서 UI 선택창 비활성화
        isbasicdial_yj = false;
    }
    public void OnNo5ButtonClick()
    {
        resultUI_yj.SetActive(false); // 결과 UI 선택창 비활성화
        resultUI2_yj.SetActive(false); // 결과2 UI 선택창 비활성화
        isbasicdial_yj = false;
    }


    public void HideUI()
    {
        choiceUI3_yj.SetActive(false);
        choiceUI7_yj.SetActive(false);
        Dial_changyj.SetActive(false);
        resultUI_yj.SetActive(false);
    }

    void HideResultPanel()
    {
        resultUI_yj.SetActive(false);
    }

    void SaveData()
    {
        DataManager.instance.nowPlayer.Player_hp = playermanager_yj.playerNow.hp_py;
        DataManager.instance.nowPlayer.Player_tired = playermanager_yj.playerNow.tired_py;
        DataManager.instance.nowPlayer.Player_money = playermanager_yj.playerNow.money_py;
        DataManager.instance.nowPlayer.Player_hint = playermanager_yj.playerNow.hint_py;
        DataManager.instance.nowPlayer.Player_team = playermanager_yj.playerNow.team_py;
        DataManager.instance.nowPlayer.Player_day = playermanager_yj.playerNow.day_py;
        DataManager.instance.nowPlayer.Player_howtoday = playermanager_yj.playerNow.howtoday_py;
        DataManager.instance.nowPlayer.Player_howtrain = playermanager_yj.playerNow.howtrain_py;

        DataManager.instance.SaveData();
    }
}
