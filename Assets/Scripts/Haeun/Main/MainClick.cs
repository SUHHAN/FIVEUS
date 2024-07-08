using UnityEngine;
using UnityEngine.SceneManagement;

public class MainClick : MonoBehaviour
{
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
    public void OnExitButtonClick()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

}
