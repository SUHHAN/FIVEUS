using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Button_MiniPanel : MonoBehaviour
{
    public GameObject miniWindow; // 미니 창 패널
    public Button homeButton;
    public Button InvButton;
    public Button SetButton;

    void Start()
    {
        // Null 체크를 통해 연결이 되었는지 확인
        if (miniWindow == null || homeButton == null || InvButton == null || SetButton == null)
        {
            Debug.LogError("UI 요소가 연결되지 않았습니다.");
            return;
        }

        miniWindow.SetActive(false); // 미니 창 비활성화

        // 버튼 클릭 이벤트 연결
        homeButton.onClick.AddListener(ToggleMiniWindow);
        InvButton.onClick.AddListener(LoadOption1Scene);
        SetButton.onClick.AddListener(LoadOption2Scene);
    }

    void ShowMiniWindow()
    {
        miniWindow.SetActive(true); // 미니 창 활성화
    }

    void ToggleMiniWindow()
    {
        // 미니 창 활성화/비활성화 상태를 토글
        miniWindow.SetActive(!miniWindow.activeSelf);
    }

    void LoadOption1Scene()
    {
        SceneManager.LoadScene("InventoryMain");
    }

    void LoadOption2Scene()
    {
        SceneManager.LoadScene("SettingMain");
    }
}
