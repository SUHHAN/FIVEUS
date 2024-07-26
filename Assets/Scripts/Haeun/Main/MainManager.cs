using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // UI 관련 라이브러리 추가

public class MainManager : MonoBehaviour
{
    private string previousSceneName;

    public Button continueButton; // 이어하기 버튼
    public Button newGameButton; // 새로하기 버튼
    public Button SettingButton; // 설정 버튼
    public GameObject SettingButtonWarningText;
    public GameObject warningPopup; // 경고 팝업 프리팹

    void Start()
    {
        // 현재 씬 이름 가져오기
        string currentSceneName = SceneManager.GetActiveScene().name;
        // 이전 씬 이름 가져오기
        previousSceneName = PlayerPrefs.GetString("PreviousScene", "");

        // 배경음 시작
        AudioManager.Instance.PlayBgm(true);

        // PlayerPrefs 안에 데이터가 있는지 확인하여 이어하기 버튼 활성화
        CheckDataAlreadyExists();
        warningPopup.SetActive(false);


        // 새로하기 버튼에 이벤트 리스너 추가
        newGameButton.onClick.AddListener(OnNewGameButtonClick);
        continueButton.onClick.AddListener(OnContinueButtonClick);
        SettingButton.onClick.AddListener(() => LoadSettingsScene(previousSceneName));
    }

    // 이어하기 버튼
    public void OnContinueButtonClick() {
        SceneManager.LoadScene("DataSlotScene");
    }
    
    // 나가기 버튼
    public void OnExitButtonClick()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    
    // PlayerPrefs 안에 새게임을 누른적이 있는지 한번 확인하고, 있으면 이어하기 버튼 활성화
    public void CheckDataAlreadyExists() {
        bool dataExists = PlayerPrefs.GetInt("isDataExisting", 0) == 1;

        continueButton.interactable = dataExists; // 이어하기 버튼 상태 설정
        SettingButton.interactable = dataExists; // 설정 버튼 상태 설정
        SettingButtonWarningText.SetActive(!dataExists);
    }

    // 새 게임 버튼
    public void OnNewGameButtonClick()
    {
        bool dataExists = PlayerPrefs.GetInt("isDataExisting", 0) == 1;

        if (dataExists) {
            // 경고 팝업 생성
            warningPopup.SetActive(true);
            // 경고 팝업에서 확인 버튼과 취소 버튼 가져오기
            Button confirmButton = warningPopup.transform.Find("OkButton").GetComponent<Button>();
            Button cancelButton = warningPopup.transform.Find("NoButton").GetComponent<Button>();

            // 확인 버튼에 이벤트 리스너 추가
            confirmButton.onClick.AddListener(() => {
                warningPopup.SetActive(false);
                ResetGamedata(); // 새 게임 시작
            });

            // 취소 버튼에 이벤트 리스너 추가
            cancelButton.onClick.AddListener(() => {
               warningPopup.SetActive(false);
            });
        } else {
            StartNewGame(); // 저장된 데이터가 없으면 바로 새 게임 시작
        }
    }

    public void ResetGamedata() {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("isDataExisting", 0);
        PlayerPrefs.Save();
        continueButton.interactable = false;
        SettingButton.interactable = false; // 설정 버튼 상태 설정
        SettingButtonWarningText.SetActive(true);

    }

    public void StartNewGame()
    {
        // 새 게임 시작 시 PlayerPrefs 초기화 및 isDataExisting 플래그 설정
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("isDataExisting", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("IngameEx");
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
