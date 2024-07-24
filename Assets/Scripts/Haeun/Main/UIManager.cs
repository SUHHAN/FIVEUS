using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject LoadButton; 
    public GameObject IngameButton;

    public Button StoreButton;
    public Button InventoryButton;
    public Button SettingButton;
    public Button PartyButton;
    public Button Load_Button;
    public Button Ingame_Button;

    public Canvas canvas; // Canvas 오브젝트를 참조

    private string previousSceneName;

    // Start is called before the first frame update
    void Start()
    {
        // Null 체크를 통해 연결이 되었는지 확인
        if (LoadButton == null || IngameButton == null || canvas == null)
        {
            
            Debug.LogError("UI 오브젝트가 연결되지 않았습니다.");
            
            return;
        }

        // 현재 씬 이름 가져오기
        string currentSceneName = SceneManager.GetActiveScene().name;

        // 씬 이름에 따라 버튼 활성화 설정
        if (currentSceneName == "IngameEx")
        {
            LoadButton.SetActive(true);
            IngameButton.SetActive(false);
        }
        else
        {
            LoadButton.SetActive(false);
            IngameButton.SetActive(true);
        }

        // 이전 씬 이름 가져오기
        previousSceneName = PlayerPrefs.GetString("PreviousScene", "");

        // 버튼 클릭 이벤트 연결
        StoreButton.onClick.AddListener(() => changeStore());
        InventoryButton.onClick.AddListener(() => changeInventory());
        SettingButton.onClick.AddListener(() => LoadSettingsScene(previousSceneName));
        PartyButton.onClick.AddListener(() => changeParty());
        Load_Button.onClick.AddListener(() => changeInventory());
        Ingame_Button.onClick.AddListener(() => ChangeIngame());
        
    }

    void Awake()
    {
        DontDestroyOnLoad(canvas.gameObject); // Canvas 오브젝트 파괴되지 않도록 설정
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
        SceneManager.LoadScene("IngameEx");
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
