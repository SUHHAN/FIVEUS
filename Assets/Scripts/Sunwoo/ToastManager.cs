using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToastManager : MonoBehaviour
{
    public Text toastText;
    public GameObject toastUI;

    public void ShowToast(string message)
    {
        toastText.text = message;
        toastUI.SetActive(true);
        Invoke("HideToast", 2f); // 2�� �Ŀ� �佺Ʈ �޽��� �����
    }

    private void HideToast()
    {
        toastUI.SetActive(false);
    }
}
