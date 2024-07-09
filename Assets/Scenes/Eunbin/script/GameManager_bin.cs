using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_bin : MonoBehaviour
{
    public GameObject talkPanel_bin;
    public TextMeshProUGUI talkText_bin;
    public GameObject scanObject_bin;
    public bool isAction_bin;

    private static GameManager_bin instance;


    public void Action(GameObject scanObj_yy)
    {
        if (isAction_bin)
        {
            isAction_bin = false;
        }
        else
        {
            isAction_bin = true;
            scanObject_bin = scanObj_yy;
            talkText_bin.text = "Hello!" + scanObject_bin.name;
        }
        talkPanel_bin.SetActive(isAction_bin);
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this )
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // 초기화 코드
    }

    void Update()
    {
        // 업데이트 로직
    }
}
