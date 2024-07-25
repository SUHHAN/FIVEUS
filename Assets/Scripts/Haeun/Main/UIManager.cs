using System;
using TMPro;
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

    [Header("#TextChange")]
    public int HPnum_ex = 79;
    public int Fatiguenum_ex = 79;
    public int Gold_ex = 10000000;
    public TextMeshProUGUI HPLevelText;      // 체력 수치 관련 텍스트
    public TextMeshProUGUI FatigueLevelText; // 피로도 수치 관련 텍스트
    public TextMeshProUGUI GoldLevelText;    // 재화 수치 관련 텍스트

    [Header("#SpriteChange")]
    public Image HPLevelImage;
    public Image FatigueLevelImage;
    public Sprite[] HPLevelSprites;      // 체력 수치 관련 이미지
    public Sprite[] FatigueLevelSprites; // 피로도 수치 관련 이미지


    // Start is called before the first frame update
    void Start()
    {
        // 0. 씬 설정의 경우
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


        // 1. UI 바꾸기의 경우
        ChangeHPLevel();
        ChangeFatigueLevel();
        ChangeGoldLevel();


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

    
    void ChangeHPLevel() {
        // 0. 수치 가지고 오기 / 이건 예지 언니 변수 가지고 오는걸로 생각하면 될 것 같아요.
        string HPLevel = HPnum_ex.ToString();  // 예지언니 체력 계산 후 체력
        
        // 1. 체력바 텍스트 바꾸기
        HPLevelText.text = "100/" + HPLevel;

        // 2. 체력바 이미지 바꾸기
        if (80 <= HPnum_ex && HPnum_ex <= 100) {
            if (HPLevelImage != null)
            {
                HPLevelImage.sprite = HPLevelSprites[0];
            }
            else{Debug.LogError("HPLevelImage가 연결되지 않았습니다.");}
        }
        else if(60 <= HPnum_ex && HPnum_ex < 80) {
            if (HPLevelImage != null)
            {
                HPLevelImage.sprite = HPLevelSprites[1];
            }
            else{Debug.LogError("HPLevelImage가 연결되지 않았습니다.");}
        }
        else if(40 <= HPnum_ex && HPnum_ex < 80) {
            if (HPLevelImage != null)
            {
                HPLevelImage.sprite = HPLevelSprites[2];
            }
            else{Debug.LogError("HPLevelImage가 연결되지 않았습니다.");}
        }
        else if(20 <= HPnum_ex && HPnum_ex < 40) {
            if (HPLevelImage != null)
            {
                HPLevelImage.sprite = HPLevelSprites[3];
            }
            else{Debug.LogError("HPLevelImage가 연결되지 않았습니다.");}
        }
        else if(0 <= HPnum_ex && HPnum_ex < 20) {
            if (HPLevelImage != null)
            {
                HPLevelImage.sprite = HPLevelSprites[4];
            }
            else{Debug.LogError("HPLevelImage가 연결되지 않았습니다.");}
        }
    }

    void ChangeFatigueLevel() {
        // 0. 수치 가지고 오기 / 이건 예지 언니 변수 가지고 오는걸로 생각하면 될 것 같아요.
        string FatigueLevel = Fatiguenum_ex.ToString();  // 예지언니 체력 계산 후 체력
        
        // 1. 체력바 텍스트 바꾸기
        FatigueLevelText.text = "100/" + FatigueLevel;

        // 2. 체력바 이미지 바꾸기
        if (80 <= Fatiguenum_ex && Fatiguenum_ex <= 100) {
            if (FatigueLevelImage != null)
            {
                FatigueLevelImage.sprite = FatigueLevelSprites[0];
            }
            else{Debug.LogError("FatigueLevelImage 연결되지 않았습니다.");}
        }
        else if(60 <= Fatiguenum_ex && Fatiguenum_ex < 80) {
            if (FatigueLevelImage != null)
            {
                FatigueLevelImage.sprite = FatigueLevelSprites[1];
            }
            else{Debug.LogError("FatigueLevelImage 연결되지 않았습니다.");}
        }
        else if(40 <= Fatiguenum_ex && Fatiguenum_ex < 80) {
            if (FatigueLevelImage != null)
            {
                FatigueLevelImage.sprite = FatigueLevelSprites[2];
            }
            else{Debug.LogError("FatigueLevelImage 연결되지 않았습니다.");}
        }
        else if(20 <= Fatiguenum_ex && Fatiguenum_ex < 40) {
            if (FatigueLevelImage != null)
            {
                FatigueLevelImage.sprite = FatigueLevelSprites[3];
            }
            else{Debug.LogError("FatigueLevelImage 연결되지 않았습니다.");}
        }
        else if(0 <= Fatiguenum_ex && Fatiguenum_ex < 20) {
            if (FatigueLevelImage != null)
            {
                FatigueLevelImage.sprite = FatigueLevelSprites[4];
            }
            else{Debug.LogError("FatigueLevelImage 연결되지 않았습니다.");}
        }
    }

    void ChangeGoldLevel() 
    {
        // 0. 수치 가지고 오기 / 이건 예지 언니 변수 가지고 오는걸로 생각하면 될 것 같아요.
        int GoldLevel = Gold_ex;  // 예지언니 재화 수치

        // 1. 재화 텍스트 바꾸기 (천 단위 마다 콤마 찍어주는 기능 추가)
        GoldLevelText.text = GoldLevel.ToString("N0"); // "N0"은 천 단위 구분 기호와 소수점 0자리 형식을 의미합니다.
    }


    // 씬 전환 코드
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
