using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public string curType = "게임"; // 현재 고른 탭의 타입이 무엇인지
    public Image[] TabImage;
    public Color TabSelectColor = new Color32(186, 227, 255, 255);
    public Color TabIdleColor = new Color32(255, 255, 255, 255);

    // 0. 탭 클릭 시, 활성화 되는 윈도우 설정하기
    public GameObject gameWindow; 
    public GameObject AlarmWindow; 
    public GameObject LangWindow; 
    public Button GameTapButton;
    public Button AlarmTapButton;
    public Button LangTapButton;
    public Button BackButton; // Back 버튼 추가

    // 1. 이전 씬 이름 기억하여, 뒤로가기 누르면 저장된 이전 씬으로 이동하기
    private string previousSceneName;

    // 2. 언어 탭 - 영어 버튼이나 한국어 버튼 클릭
    public GameObject korWindow;
    public GameObject engWindow;
    public Button korButton;
    public Button engButton;
    
    // 3. 알림 탭 - 각 버튼 클릭 시, 체크 버튼 활성화 및 비활성화 설정
    public GameObject[] AlarmCheckButton;

    void Start()
    {
        // 0. 탭 클릭 시, 활성화 되는 윈도우 설정하기
        TabImage[0].color = TabSelectColor; // 기본적으로 게임 탭의 색상 설정
        TabImage[1].color = TabIdleColor;
        TabImage[2].color = TabIdleColor;

        GameTapButton.onClick.AddListener(() => TapClick("게임")); // 탭 버튼 클릭 이벤트 연결
        AlarmTapButton.onClick.AddListener(() => TapClick("알림"));
        LangTapButton.onClick.AddListener(() => TapClick("언어"));
        BackButton.onClick.AddListener(BackButtonClick);

        gameWindow.SetActive(true); // 기본적으로 게임 탭 윈도우가 눌러져 있는 상태로 설정
        AlarmWindow.SetActive(false);
        LangWindow.SetActive(false);
        
        // 1. 이전 씬 저장
        previousSceneName = PlayerPrefs.GetString("PreviousScene", ""); // 이전 씬 이름 가져오기

        // 2. 두 언어 window 설정 
        korWindow.SetActive(false); // 일단 두 언어 윈도우 비활성화
        engWindow.SetActive(false); 

        korButton.onClick.AddListener(OnKorWindow); // 두 언어 윈도우버튼 클릭 이벤트 연결
        engButton.onClick.AddListener(OnEngWindow);

        Button OkButton1 = korWindow.GetComponentInChildren<Button>();  // 두 윈도우 속 ok 버튼 클릭시 창 비활성화 매소드 연결
        OkButton1.onClick.AddListener(ClickOkButton);
        Button OkButton2 = engWindow.GetComponentInChildren<Button>();
        OkButton2.onClick.AddListener(ClickOkButton);

        // 3. 알림탭 버튼 이미지 활성화 비활성화 기능 추가 및 상태 로드
        for (int i = 0; i < AlarmCheckButton.Length; i++)
        {
            GameObject button = AlarmCheckButton[i];
            Button checkButton = button.GetComponent<Button>();
            Transform checkImageTransform = button.transform.Find("CheckImage");
            

            // 일단 지금은 무조건 true로 두지만, 저장 상태를 불러오는 기능은 추가해뒀음.
            if(PlayerPrefs.HasKey("AlarmCheck_1")) {
                // 저장 정보가 있을 시에는 저장된 상태를 불러오기
                bool AlarmCheck_isActive = PlayerPrefs.GetInt("AlarmCheck_" + i, 0) == 1; // 저장된 상태 불러오기
                checkImageTransform.gameObject.SetActive(AlarmCheck_isActive); // 저장된 상태로 설정
            }else {
                // 어떤 저장 정보도 없을 시에는 무조건 true로 시작하기
                checkImageTransform.gameObject.SetActive(true);
            }

            if (checkButton != null)
            {
                int index = i; // Lambda 캡처 문제를 해결하기 위해 로컬 변수 사용
                checkButton.onClick.AddListener(() => AlarmCheckBox(checkImageTransform, index));
            }
            else
            {
                Debug.LogError("GameObject에 Button 컴포넌트가 없습니다: " + button.name);
            }
        }
    }

    // 탭 버튼 클릭 시 이벤트 매소드
    public void TapClick(string tabName)
    {
        int tabNum = 0;

        switch (tabName)
        {
            case "게임": 
                tabNum = 0; 
                gameWindow.SetActive(true); 
                AlarmWindow.SetActive(false);
                LangWindow.SetActive(false);
                break;
            case "알림": 
                tabNum = 1; 
                AlarmWindow.SetActive(true); 
                gameWindow.SetActive(false);
                LangWindow.SetActive(false);
                break;
                
            case "언어": 
                tabNum = 2; 
                LangWindow.SetActive(true); 
                gameWindow.SetActive(false);
                AlarmWindow.SetActive(false);
                break;
        }
        for (int i = 0; i < TabImage.Length; i++)
        {
            TabImage[i].color = i == tabNum ? TabSelectColor : TabIdleColor;
        }
    }

    // 2
    public void OnKorWindow() {
        korWindow.SetActive(!korWindow.activeSelf);
    }

    public void OnEngWindow() {
        engWindow.SetActive(!engWindow.activeSelf);
    }

    public void ClickOkButton() {
        engWindow.SetActive(false);
        korWindow.SetActive(false);
    }

    // 3
    // 알람 탭 속 체크 박스 버튼를 이벤트 설정 매소드
    public void AlarmCheckBox(Transform checkImage, int index) {
        bool isActive = !checkImage.gameObject.activeSelf;
        checkImage.gameObject.SetActive(isActive);

        // 상태를 PlayerPrefs에 저장
        PlayerPrefs.SetInt("AlarmCheck_" + index, isActive ? 1 : 0);
        PlayerPrefs.Save();
    }

    // 뒤로가기 버튼 클릭 시 이벤트 매소드
    public void BackButtonClick()
    {
        if (!string.IsNullOrEmpty(previousSceneName))
        {
            SceneManager.LoadScene(previousSceneName);
        }
        else
        {
            Debug.LogWarning("이전 씬 정보가 없습니다.");
        }
    }

    public static void LoadSettingsScene(string settingsSceneName)
    {
        // 현재 씬 이름 저장
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();

        // 환경 설정 씬 로드
        SceneManager.LoadScene("SettingMain");
    }
}
