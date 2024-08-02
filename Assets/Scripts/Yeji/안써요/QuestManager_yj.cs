using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class QuestManager_yj : MonoBehaviour
{
    public int questid_yj;
    public int questActionIndex_yj;
    public GameObject[] questObject_yj;
    Dictionary<int, QuestData_yj> questlist_yj;
    // Start is called before the first frame update

    void Awake()
    {
        questlist_yj = new Dictionary<int, QuestData_yj> ();
        GenerateData_yj();
    }

    private void GenerateData_yj()
    {
        questlist_yj.Add(10,new QuestData_yj("talk to NPC!",new int[] {1000,2000}));
        questlist_yj.Add(20, new QuestData_yj("Find Coin", new int[] {5000, 2000}));

    }

    public int GetQuestTalkIndex(int id_yj)
    {
        return questid_yj + questActionIndex_yj;
    }
    public string CheckQuest_yj(int id_yj) {

        // next talk target
        if (id_yj == questlist_yj[questid_yj].npcid_yj[questActionIndex_yj])
            questActionIndex_yj++;

        // control quest object
        ControlObject_yj();
      
        // talk complete & next quest
        if (questActionIndex_yj == questlist_yj[questid_yj].npcid_yj.Length)
            NextQuest_yj();

        //quest name
        return questlist_yj[questid_yj].questname_yj;
    }

    void NextQuest_yj()
    {
        questid_yj += 10;
        questActionIndex_yj = 0;
    }

    void ControlObject_yj()
    {
        switch (questid_yj)
        {
            case 10:
                if(questActionIndex_yj == 2)
                    questObject_yj[0].SetActive(true);
                break;
            case 20:
                if (questActionIndex_yj == 1)
                    questObject_yj[0].SetActive(false);
                break;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
