using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro; // UI 관련 라이브러리 추가

public class MainManager : MonoBehaviour
{   
    private PlayerManager_yj PlayerManager_yj;
    private string previousSceneName;

    public Button continueButton; // 이어하기 버튼
    public Button newGameButton; // 새로하기 버튼
    public Button OutGameButton; // 나가기 버튼
    public Button SettingButton; // 설정 버튼
    public GameObject SettingButtonWarningText;

    public TMP_InputField newPlayername;    // 새로 입력된 플레이어의 닉네임.
    public bool[] savefile = new bool[3];  // 세이브 파일의 존재 유무 저장

    [Header("#POPUP")]
    public GameObject warningPopup;         // 경고 팝업 프리팹
    public GameObject PlayerNamePopup;      // 이름 입력 팝업 프리팹


    void Start()
    {
        // 현재 씬 이름 가져오기
        string currentSceneName = SceneManager.GetActiveScene().name;
        // 이전 씬 이름 가져오기
        previousSceneName = PlayerPrefs.GetString("PreviousScene", "");


        // 배경음 시작
        AudioManager.Instance.PlayBgm(true);

        // PlayerPrefs 안에 데이터가 있는지 확인하여 이어하기 버튼 활성화 -> 파일 안에 데이터가 있는지 확인
        CheckData();
        CheckDataAlreadyExists();

        warningPopup.SetActive(false);


        // 버튼에 이벤트 리스너 추가
        newGameButton.onClick.AddListener(OnNewGameButtonClick_new);
        OutGameButton.onClick.AddListener(OnExitButtonClick);
        continueButton.onClick.AddListener(OnContinueButtonClick);
        SettingButton.onClick.AddListener(() => LoadSettingsScene(previousSceneName));
    }

    // 이어하기 버튼
    public void OnContinueButtonClick() {
        SceneManager.LoadScene("DataSlotScene");
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.ButtonClick);
    }
    
    // 나가기 버튼
    public void OnExitButtonClick()
    {
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.ButtonClick);
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    
    // PlayerPrefs 안에 새게임을 누른적이 있는지 한번 확인하고, 있으면 이어하기 버튼 활성화
    public void CheckDataAlreadyExists() {
    bool dataExists;

    // savefile 배열의 모든 값이 false인 경우
    if (savefile[0] == false && savefile[1] == false && savefile[2] == false) {
        dataExists = false;
    } else {
        dataExists = true;
    }

    // 이어하기 및 설정 버튼 상태 설정
    continueButton.interactable = dataExists; 
    SettingButton.interactable = dataExists;

    // 경고 텍스트 상태 설정
    SettingButtonWarningText.SetActive(!dataExists);
    }


    public void ResetGamedata() {
        PlayerPrefs.DeleteKey("isDataExisting");
        PlayerPrefs.DeleteKey("PreviousScene");
        PlayerPrefs.SetInt("isDataExisting", 0);
        PlayerPrefs.Save();

        continueButton.interactable = false;
        SettingButton.interactable = false; // 설정 버튼 상태 설정
        SettingButtonWarningText.SetActive(true);
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

        AudioManager.Instance.PlaySfx(AudioManager.Sfx.ButtonClick);

        // 환경 설정 씬 로드
        SceneManager.LoadScene("SettingMain");
    }



    // 여기서 부터는 삭제될 가능성 있음
    // 슬롯에 데이터가 존재하는지 확인하는 매소드
    public void CheckData() {
        for (int i = 0; i < 3; i++) {
            // 슬롯 별로 저장된 데이터가 존재하는지 판단.
            if (File.Exists(DataManager.instance.path + $"{i}")) {
                savefile[i] = true; // 해당 슬롯 번호의 bool 배열을 true로 변환
            }
            else {
                savefile[i] = false; // 해당 슬롯 번호의 bool 배열을 true로 변환
            }
            // 불러왔던 게임 데이터는 변수 초기화 시킴. -> 단지 버튼에 닉네임을 표현하기 위해서 가지고 온 것이므로, 변수 초기화를 해줘야 게임 데이터가 섞이지 않음.
            DataManager.instance.ClearData();
        }
    }


    public void OnNewGameButtonClick_new() 
    {
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.ButtonClick);

        CheckData(); // savefile 배열 업데이트
        bool dataExists = PlayerPrefs.GetInt("isDataExisting", 0) == 1;

        if (dataExists) {
            if (savefile[0] && savefile[1] && savefile[2]) {
                // 모든 슬롯이 차있을 경우 경고 팝업
                warningPopup.SetActive(true);
                Button confirmButton = warningPopup.transform.Find("OkButton").GetComponent<Button>();
                Button cancelButton = warningPopup.transform.Find("NoButton").GetComponent<Button>();

                confirmButton.onClick.AddListener(() => {
                    AudioManager.Instance.PlaySfx(AudioManager.Sfx.ButtonClick);
                    warningPopup.SetActive(false);
                    ResetGamedata_new();
                });

                cancelButton.onClick.AddListener(() => {
                    AudioManager.Instance.PlaySfx(AudioManager.Sfx.ButtonClick);
                    warningPopup.SetActive(false);
                });
            } 
            else {
                // 데이터가 없는 슬롯이 있을 때
                if (!savefile[0]) {
                    StartNewGame(0);
                } 
                else if (!savefile[1]) {
                    StartNewGame(1);
                } 
                else if (!savefile[2]) {
                    StartNewGame(2);
                }
            }
        } else {
            // isDataExisting이 0인 경우, 새 게임 시작
            StartNewGame(0);
        }
    }

    private void StartNewGame(int slot) {
        PlayerPrefs.SetInt("isDataExisting", 1);
        DataManager.instance.nowSlot = slot;

        if (savefile[slot]) {
            DataManager.instance.LoadData();
            GoIngame();
        } else {
            NewPlayerCreate();
        }
    }


    void ResetGamedata_new() {
        bool dataExists = PlayerPrefs.GetInt("isDataExisting", 0) == 1;
        
        for (int i = 0; i < 3; i++) {
            File.Delete(DataManager.instance.path + $"{i}");
        }
        continueButton.interactable = false;
        SettingButton.interactable = false;
        SettingButtonWarningText.SetActive(true);

    }

    // 플레이어 이름을 입력하는 팝업창을 활성화 시킴
    public void NewPlayerCreate() { 
        PlayerNamePopup.gameObject.SetActive(true);
    }


    // 새 플레이어 이름 저장 및 게임 시작 버튼을 누를 때 호출
    public void OnNewPlayerNameEntered() {
        if (string.IsNullOrEmpty(newPlayername.text)) {
            Debug.LogError("플레이어 이름이 입력되지 않았습니다.");
            return;
        }

        if (!savefile[DataManager.instance.nowSlot]) {
            DataManager.instance.nowPlayer.Player_name = newPlayername.text;
            DataManager.instance.LoadCharactersFromCSV("Character", newPlayername.text);
            DataManager.instance.LoadItemsFromCSV("Item");
            DataManager.instance.SaveData(); // 현재의 정보를 저장함.
        }

        SceneManager.LoadScene("main_map");
    }

    // 원하는 파일의 정보를 가지고 게임을 시작하기.
    public void GoIngame() {
        if (!savefile[DataManager.instance.nowSlot]) {
            DataManager.instance.nowPlayer.Player_name = newPlayername.text;
            DataManager.instance.LoadCharactersFromCSV("Character", newPlayername.text);
            DataManager.instance.LoadItemsFromCSV("Item");
            DataManager.instance.SaveData(); // 현재의 정보를 저장함.
        }
        SceneManager.LoadScene("main_map");
    }
}

