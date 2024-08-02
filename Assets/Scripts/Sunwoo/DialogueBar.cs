using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueBar : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameTxt;
    [SerializeField] TypeEffect talk;
    string[] str = {};

    void Start()
    {
        ActiveDialogue(0, "", ref str);
    }

    void Update()
    {

    }

    void ActiveDialogue(int idx, string nameData, ref string[] talkData)
    {
        if (nameData == null && talkData == null)
        {
            
        }

        nameTxt.text = nameData;

        for (int i = 0; i < talkData.Length; i++)
        {
            talk.SetMsg(talkData[i]);
        }

    }
}
