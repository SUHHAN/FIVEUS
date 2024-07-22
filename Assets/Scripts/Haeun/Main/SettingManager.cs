using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public string curType = "게임"; // 현재 고른 탭의 타입이 무엇인지
    public GameObject[] slot;
    public Image[] TabImage;
    public Sprite[] itemSprites;
    public Color TabSelectColor = new Color32(186, 227, 255, 255);
    public Color TabIdleColor = new Color32(255, 255, 255, 255);

    public GameObject gameWindow; 
    public GameObject AlarmWindow; 
    public GameObject LangWindow; 
    public Button GameTapButton;
    public Button AlarmTapButton;
    public Button LangTapButton;
    public Button BackButton; // Back 버튼 추가

    private string previousSceneName;

    // Start is called before the first frame update
    void Start()
    {
        // Null 체크를 통해 연결이 되었는지 확인
        if (gameWindow == null || AlarmWindow == null || LangWindow == null || GameTapButton == null || AlarmTapButton == null || LangTapButton == null || BackButton == null)
        {
            Debug.LogError("UI 요소가 연결되지 않았습니다.");
            return;
        }
        
        // 기본적으로 게임 탭이 눌러져 있는 상태로 설정
        gameWindow.SetActive(true); 
        AlarmWindow.SetActive(false);
        LangWindow.SetActive(false);

        // 기본적으로 게임 탭의 색상 설정
        TabImage[0].color = TabSelectColor;
        TabImage[1].color = TabIdleColor;
        TabImage[2].color = TabIdleColor;

        // 버튼 클릭 이벤트 연결
        GameTapButton.onClick.AddListener(() => TapClick("게임"));
        AlarmTapButton.onClick.AddListener(() => TapClick("알림"));
        LangTapButton.onClick.AddListener(() => TapClick("언어"));
        BackButton.onClick.AddListener(BackButtonClick); // Back 버튼 클릭 이벤트 연결

        // 이전 씬 이름 가져오기
        previousSceneName = PlayerPrefs.GetString("PreviousScene", "");
    }

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
