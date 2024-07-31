using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_yj : MonoBehaviour
{
    public TalkManager_yj talkManager_yj;
    public QuestManager_yj questManager_yj;
    public GameObject talkPanel_yj;
    public Image portrait_yj;
    public TextMeshProUGUI talkText_yj;
    //public GameObject scanObject_yj;
    public bool isAction_yj;
    public int talkIndex_yj;
    public void Action(GameObject scanObj_yy)
    {
            isAction_yj = true;           
            //scanObject_yj = scanObj_yy;
            //ObjData_yjj objData_yj= scanObject_yj.GetComponent<ObjData_yjj>();
            // talkText_yj.text = "Hello!" + scanObject_yj.name;
            //Talkyj(objData_yj.id_yj, objData_yj.isNpc_yjj);
           
        talkPanel_yj.SetActive(isAction_yj);
    }

    void Talkyj(int id_yj, bool isNpc_yjj)
    {
        // Set Talk Data
        int questTalkIndex_yj = questManager_yj.GetQuestTalkIndex(id_yj);
        //string talkData_yj = talkManager_yj.GetTalk_yj(id_yj+ questTalkIndex_yj, talkIndex_yj);      

        /*if(talkData_yj == null)
        {
            isAction_yj = false;
            talkIndex_yj = 0;
            Debug.Log(questManager_yj.CheckQuest_yj(id_yj));
            return;
        }

        if (isNpc_yjj) { 
            talkText_yj.text = talkData_yj.Split(':')[0];
            //portrait_yj.sprite = talkManager_yj.GetPortait_yj(id_yj, int.Parse(talkData_yj.Split(':')[1]));
            //portrait_yj.color = new Color(1, 1, 1, 1);
        }
        else
        {
            talkText_yj.text = talkData_yj;
            //portrait_yj.color = new Color(1, 1, 1, 0);
        }
        isAction_yj = true;
        talkIndex_yj++;*/
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
