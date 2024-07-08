using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_yj : MonoBehaviour
{
    public GameObject talkPanel_yj;
    public TextMeshProUGUI talkText_yj;
    public GameObject scanObject_yj;
    public bool isAction_yj;
    public void Action(GameObject scanObj_yy)
    {
        if (isAction_yj) {
            isAction_yj = false;           
        }
        else
        {
            isAction_yj = true;           
            scanObject_yj = scanObj_yy;
            talkText_yj.text = "Hello!" + scanObject_yj.name;
        }   
        talkPanel_yj.SetActive(isAction_yj);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
