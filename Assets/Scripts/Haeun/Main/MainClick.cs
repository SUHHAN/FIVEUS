using UnityEngine;
using UnityEngine.SceneManagement;

public class MainClick : MonoBehaviour
{
    private string previousSceneName;

    void Start()
    {

        // 현재 씬 이름 가져오기
        string currentSceneName = SceneManager.GetActiveScene().name;

        // 이전 씬 이름 가져오기
        previousSceneName = PlayerPrefs.GetString("PreviousScene", "");


        
        
    }

    public void ChangeIngame() {
        SceneManager.LoadScene("IngameEx");
    }
    public void ChangeIngame222() {
        SceneManager.LoadScene("IngameEx2");
    }
    public void ChangeDataSlot() {
        SceneManager.LoadScene("DataSlotScene");
    }
    public void ChangeLogin() {
        SceneManager.LoadScene("LoginScene");
    }
    public void ReturnMain() {
        SceneManager.LoadScene("MainScene");
    }
    public void ReturnMain222() {
        SceneManager.LoadScene("Prototype22");
    }
    public void changeStore() {
        SceneManager.LoadScene("StoreMain");
    }
    public void changeInventory() {
        SceneManager.LoadScene("InventoryMain");
    }
    
    public void OnExitButtonClick()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
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
