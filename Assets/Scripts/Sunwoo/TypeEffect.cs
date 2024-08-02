using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeEffect : MonoBehaviour
{
    public GameObject endCursor;
    public int charPerSeconds;
    string targetMsg;
    [SerializeField] TextMeshProUGUI msgText;
    int idx;
    float interval;



    private void Awake()
    {
        //msgText = GetComponent<TextMeshProUGUI>();
    }

    public void SetMsg(string msg)
    {
        targetMsg = msg;
        EffectStart();
    }

    void EffectStart()
    {
        msgText.text = "";
        idx = 0;
        interval = 1.0f / charPerSeconds;
        endCursor.SetActive(false);

        Invoke("Effecting", 1 / charPerSeconds);
    }

    void Effecting()
    {
        if (msgText.text == targetMsg)
        {
            EffectEnd();
            return;
        }
        msgText.text += targetMsg[idx];
        idx++;

        Invoke("Effecting", interval);
    }

    void EffectEnd()
    {
        endCursor.SetActive(true);
    }
}
