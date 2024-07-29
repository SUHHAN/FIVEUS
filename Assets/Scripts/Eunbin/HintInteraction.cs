using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintInteraction : MonoBehaviour
{
    public string hintKey;
    private bool isHintVisible=true;


    void Start()
    {
       // PlayerPrefs √ ±‚»≠
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
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
}