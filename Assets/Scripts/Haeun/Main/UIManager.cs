using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Canvas canvas; // Canvas 오브젝트를 참조

    public GameObject TempDataCheckWindow;

    private string previousSceneName;
    private string previousIngameSceneName;

    [Header("#상단바 버튼 모음")]    
    public Button StoreButton;
    public Button InventoryButton;
    public Button SettingButton;
    public Button PartyButton;
    public Button Load_Button;
    public Button Ingame_Button;
    public GameObject LoadButton; 
    public GameObject IngameButton;

    [Header("#TextChange")]
    public TextMeshProUGUI HPLevelText;      // 체력 수치 관련 텍스트
    public TextMeshProUGUI FatigueLevelText; // 피로도 수치 관련 텍스트
    public TextMeshProUGUI GoldLevelText;    // 재화 수치 관련 텍스트
    public TextMeshProUGUI DayLevelText;    // 날짜 수치 관련 텍스트

    [Header("#SpriteChange")]
    public Image HPLevelImage;
    public Image FatigueLevelImage;
    public Sprite[] HPLevelSprites;      // 체력 수치 관련 이미지
    public Sprite[] FatigueLevelSprites; // 피로도 수치 관련 이미지

    [Header("#인게임 씬 목록")]
    public string[] ingameScenes = {"bar","camping","hotel","hotel_hall","hotel_room1",
                                    "hotel_room2", "inf_guild", "inf_guild_in", "magic_house",
                                    "main_house", "main_map", "main_map2", "store",
                                    "sub1_house","training"}; // 인게임 씬으로 간주할 씬들의 이름

    void Awake()
    {
        //DontDestroyOnLoad(canvas.gameObject); // Canvas 오브젝트 파괴되지 않도록 설정
    }

    void Start()
    {
        // 데이터 로드
        DataManager.instance.LoadData();

        // 현재 씬 이름 가져오기
        string currentSceneName = SceneManager.GetActiveScene().name;

        // 이전 씬 이름 및 인게임 씬 이름 가져오기
        previousSceneName = PlayerPrefs.GetString("PreviousScene", "");
        previousIngameSceneName = PlayerPrefs.GetString("PreviousIngameScene", "");

        // 씬 이름에 따라 버튼 활성화 설정
        if (currentSceneName == "InventoryMain" 
            || currentSceneName == "StoreMain" 
            || currentSceneName == "PartyMain")
        {
            LoadButton.SetActive(false);
            IngameButton.SetActive(true);
        }
        else
        {
            LoadButton.SetActive(true);
            IngameButton.SetActive(false);

            // 인게임 씬인지 확인하고 저장
            if (Array.Exists(ingameScenes, scene => scene == currentSceneName))
            {
                PlayerPrefs.SetString("PreviousIngameScene", currentSceneName);
                PlayerPrefs.Save();
            }
        }

        // 데이터 저장 알림창 비활성화(기본)
        TempDataCheckWindow.SetActive(false);

        // 버튼 클릭 이벤트 연결
        Button OkButton = TempDataCheckWindow.transform.Find("OkButton").GetComponent<Button>();
        OkButton.onClick.AddListener(ClickOkButton);
        Button NoButton = TempDataCheckWindow.transform.Find("NoButton").GetComponent<Button>();
        NoButton.onClick.AddListener(ClickNoButton);

        // UI 초기화
        UpdateUI();

        // 상단바 버튼 클릭 이벤트 연결
        StoreButton.onClick.AddListener(() => changeStore());
        InventoryButton.onClick.AddListener(() => changeInventory());
        SettingButton.onClick.AddListener(() => LoadSettingsScene(previousSceneName));
        PartyButton.onClick.AddListener(() => changeParty());
        Load_Button.onClick.AddListener(() => OnTempDataSaveButton());
        Ingame_Button.onClick.AddListener(() => ChangeIngame());
    }

    private void OnEnable() {
        // 데이터 변경 이벤트 구독
        DataManager.instance.OnDataChanged += UpdateUI;
    }

    private void OnDisable() {
        // 데이터 변경 이벤트 구독 해제
        DataManager.instance.OnDataChanged -= UpdateUI;
    }

    void UpdateUI()
    {
        int HPnum = DataManager.instance.nowPlayer.Player_hp;
        int Fatiguenum = DataManager.instance.nowPlayer.Player_tired;
        int Gold = DataManager.instance.nowPlayer.Player_money;
        int Day = DataManager.instance.nowPlayer.Player_day;

        ChangeHPLevel(HPnum);
        ChangeFatigueLevel(Fatiguenum);
        ChangeGoldLevel(Gold);
        ChangeDayLevel(Day);
    }

    void ChangeHPLevel(int HPnum) {
        string HPLevel = HPnum.ToString();  
        HPLevelText.text = HPLevel + "/100";

        if (80 <= HPnum && HPnum <= 100) {
            HPLevelImage.sprite = HPLevelSprites[0];
        } else if(60 <= HPnum && HPnum < 80) {
            HPLevelImage.sprite = HPLevelSprites[1];
        } else if(40 <= HPnum && HPnum < 60) {
            HPLevelImage.sprite = HPLevelSprites[2];
        } else if(20 <= HPnum && HPnum < 40) {
            HPLevelImage.sprite = HPLevelSprites[3];
        } else if(0 <= HPnum && HPnum < 20) {
            HPLevelImage.sprite = HPLevelSprites[4];
        }
    }

    void ChangeFatigueLevel(int Fatiguenum) {
        string FatigueLevel = Fatiguenum.ToString(); 
        FatigueLevelText.text = FatigueLevel + "/100";

        if (80 <= Fatiguenum && Fatiguenum <= 100) {
            FatigueLevelImage.sprite = FatigueLevelSprites[0];
        } else if(60 <= Fatiguenum && Fatiguenum < 80) {
            FatigueLevelImage.sprite = FatigueLevelSprites[1];
        } else if(40 <= Fatiguenum && Fatiguenum < 60) {
            FatigueLevelImage.sprite = FatigueLevelSprites[2];
        } else if(20 <= Fatiguenum && Fatiguenum < 40) {
            FatigueLevelImage.sprite = FatigueLevelSprites[3];
        } else if(0 <= Fatiguenum && Fatiguenum < 20) {
            FatigueLevelImage.sprite = FatigueLevelSprites[4];
        }
    }

    void ChangeGoldLevel(int Gold) {
        int GoldLevel = Gold;
        GoldLevelText.text = GoldLevel.ToString("N0");
    }

    void ChangeDayLevel(int Day) {
        int DayLevel = Day;
        DayLevelText.text = $"{Day}일차" + DayLevel.ToString();
    }

    public void changeStore() {
        SceneManager.LoadScene("StoreMain");
    }

    public void changeInventory() {
        SceneManager.LoadScene("InventoryMain");
    }

    public void changeParty() {
        SceneManager.LoadScene("PartyMain");
    }

    public void ChangeIngame() {
        if (!string.IsNullOrEmpty(previousIngameSceneName))
        {
            SceneManager.LoadScene(previousIngameSceneName);
        }
        else
        {
            SceneManager.LoadScene("IngameEx"); // 기본 인게임 씬으로 이동
        }
    }
    
    void OnTempDataSaveButton() {
        TempDataCheckWindow.SetActive(!TempDataCheckWindow.activeSelf);
    }

    public void ClickOkButton() {
        DataManager.instance.SaveData();
        TempDataCheckWindow.SetActive(false);
    }

    public void ClickNoButton() {
        TempDataCheckWindow.SetActive(false);
    }

    public static void LoadSettingsScene(string settingsSceneName)
    {
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();
        SceneManager.LoadScene("SettingMain");
    }
}
