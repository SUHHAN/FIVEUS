using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintInteraction : MonoBehaviour
{
    public string hintKey;
    private bool isHintVisible=true;


    void Start()
    {
        // PlayerPrefs 초기화(이 코드로 게임을 한 번 실행해서 저장해야 게임 재시작할 때 단서가 초기화됩니다)
       //DeleteHintKey();
        LoadHintState();

        gameObject.SetActive(isHintVisible);
       
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleHint();
        }
    }
    void ToggleHint()
    {
        isHintVisible = !isHintVisible;
        gameObject.SetActive(isHintVisible);

        PlayerPrefs.SetInt(hintKey, isHintVisible ? 1 : 0);
        PlayerPrefs.Save();
    }
    void LoadHintState()
    {
        if (PlayerPrefs.HasKey(hintKey))
        {
            isHintVisible = PlayerPrefs.GetInt(hintKey) == 1;
        }
        else isHintVisible = true;
    }

void DeleteHintKey()
{
    if (PlayerPrefs.HasKey(hintKey))
    {
        PlayerPrefs.DeleteKey(hintKey);
        PlayerPrefs.Save();
    }
}
}