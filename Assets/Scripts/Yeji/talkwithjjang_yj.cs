using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting; // TextMeshPro ³×ÀÓ½ºÆäÀÌ½º Ãß°¡

//
public class talkwithjjang_yj : MonoBehaviour
{
    public GameObject choiceUI_yj; // ¼±ÅÃ UI ÆĞ³Î
    public GameObject dialogueUI_yj; // ´ëÈ­ UI ÆĞ³Î
    public GameObject resultUI_yj; // °á°ú UI ÆĞ³Î
    public TextMeshProUGUI dialogueText; // ´ë»ç ÅØ½ºÆ® UI ¿¬°á
    public TextMeshProUGUI resultText; // °á°ú ÅØ½ºÆ® UI ¿¬°á
    public Button yesButton; // ¿¹ ¹öÆ° ¿¬°á
    public Button noButton; // ¾Æ´Ï¿À ¹öÆ° ¿¬°á

    // ±âº» È°µ¿ ¹öÆ°µé
    public Button trainingButton_yj; // 1.ÈÆ·Ã ½Ãµµ ¹öÆ° ¿¬°á
    public Button togetherButton_yj; // 2.´ÜÇÕ ½Ãµµ ¹öÆ° ¿¬°á
    public Button findhintButton_yj; // 3.ÈùÆ® È®ÀÎ ¹öÆ° ¿¬°á

    public float interactionRange = 3.0f; // »óÈ£ÀÛ¿ë °Å¸®
    private GameObject player; // ÇÃ·¹ÀÌ¾î ¿ÀºêÁ§Æ®
    private bool isTalking = false; // ´ëÈ­ ÁßÀÎÁö ¿©ºÎ

    private ProDialogue_yj whatdial_yj;
    // ±âº» È°µ¿ Á¤µµ´Â ½ºÆ®¸³Æ® ³»¿¡¼­ ÀüºÎ ´ë»ç Ã³¸®(¾îÂ÷ÇÇ 5°³¹Û¿¡ ¾øÀ½)
    // ¾ÆÀÌµğ ¼³Á¤ ¼³¸í : 6000¹ø´ëºÎÅÍ ½ÃÀÛÇÔ
    // 6001 : ÈÆ·Ã´ëÀå, 6002 : ±â»ç´ÜÀå, 6003 : ´Ü¼­

    // ±âº»È°µ¿1 : ÈÆ·Ã´ëÀå ±âº» ´ë»ç1
    ProDialogue_yj serif1_1 = new ProDialogue_yj(6001, "ÈÆ·Ã´ëÀå", "¿©¾î-¸»¶ó²¤ÀÌ! ÈÆ·ÃÇÒ ÁØºñ´Â µÆ³ª?");
    // ±âº»È°µ¿1 : ÈÆ·Ã´ëÀå ±âº» ´ë»ç2
    ProDialogue_yj serif1_2 = new ProDialogue_yj(6001, "ÈÆ·Ã´ëÀå", "¾ÈµÇ¸é µÉ¶§±îÁö! ÈÆ·Ã ½ÃÀÛÀÌ´Ù!");

    // ±âº»È°µ¿2 : ±â»ç´ÜÀå ±âº» ´ë»ç1
    ProDialogue_yj serif2_1 = new ProDialogue_yj(6002, "±â»ç´ÜÀå", "¹¶Ä¡¸é »ì°í Èğ¾îÁö¸é Á×´Â´Ù! \n´ÜÇÕÈÆ·Ã ½ÃÀÛÀÌ´Ù!!");
    // ±âº»È°µ¿2 : ±â»ç´ÜÀå ±âº» ´ë»ç2
    ProDialogue_yj serif2_2 = new ProDialogue_yj(6002, "±â»ç´ÜÀå", "3 -1 = 0! ¿ì¸®´Â ÇÏ³ª´Ù!  \n´ÜÇÕÈÆ·Ã ½ÃÀÛÀÌ´Ù!!");

    // ±âº»È°µ¿3 : ´Ü¼­ ¯¾ÒÀ» ¶§ ±âº» ´ë»ç
    ProDialogue_yj serif3 = new ProDialogue_yj(6003, "´Ü¼­", "´Ü¼­¸¦ Ã£¾Ò´Ù. ³»¿ëÀ» »ìÆìº¸ÀÚ.");


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // ÅÂ±×°¡ "Player"ÀÎ ¿ÀºêÁ§Æ® Ã£±â
        choiceUI_yj.SetActive(false); // ½ÃÀÛÇÒ ¶§ ¼±ÅÃ UI ºñÈ°¼ºÈ­       
        dialogueUI_yj.SetActive(false); // ½ÃÀÛÇÒ ¶§ ´ëÈ­ UI ºñÈ°¼ºÈ­
        resultUI_yj.SetActive(false); // ½ÃÀÛÇÒ ¶§ °á°ú UI ºñÈ°¼ºÈ­

        //talkButton.onClick.AddListener(OnTalkButtonClick); // ´ëÈ­ÇÏ±â ¹öÆ° Å¬¸¯ ÀÌº¥Æ® ¿¬°á
        yesButton.onClick.AddListener(OnYesButtonClick); // ¿¹ ¹öÆ° Å¬¸¯ ÀÌº¥Æ® ¿¬°á
        noButton.onClick.AddListener(OnNoButtonClick); // ¾Æ´Ï¿À ¹öÆ° Å¬¸¯ ÀÌº¥Æ® ¿¬°á
    }



    // Update is called once per frame
    void Update()
    {
        if (isTalking)
        {
            if ((isTalking && Input.GetKeyDown(KeyCode.Return))) // ¿£ÅÍ Å° ÀÔ·Â °¨Áö
            {
                EndDialogue(); // ´ëÈ­ Á¾·á
            }
            //return;
        }

        float distance = Vector3.Distance(player.transform.position, transform.position); // ÇÃ·¹ÀÌ¾î¿Í NPC °£ °Å¸® °è»ê
        if (distance <= interactionRange) // »óÈ£ÀÛ¿ë °Å¸® ³»¿¡ ÀÖ´ÂÁö È®ÀÎ
        { 
            if (Input.GetKeyDown(KeyCode.Return)) // ¿£ÅÍ Å° ÀÔ·Â °¨Áö
            {
                // »ó´ë ´©±¸¸¦ ¸¸³µ´ÂÁö ¸ÕÀú ±âº»´ë»ç°¡ ¶ä(TalkManager_yj Âü°í)
                switch (whatdial_yj.id)
                {
                    // ±âº»È°µ¿1 : ÈÆ·Ã´ÜÀåÀÏ ‹š(ÈÆ·Ã)
                    case 6001:
                        dialogueText.text = serif1_1.line; // ·£´ıÇÏ°Ô ÈÆ·Ã´ëÀå ±âº» ´ë»ç Ãâ·Â
                        dialogueText.text = ""; // ´ë»ç ÅØ½ºÆ® ÃÊ±âÈ­
                        break;
                    // ±âº»È°µ¿2 : ±â»ç´ÜÀåÀÏ ¶§(´ÜÇÕ)
                    case 6002:
                        dialogueText.text = serif2_1.line; // ·£´ıÇÏ°Ô ±â»ç´ÜÀå ±âº» ´ë»ç Ãâ·Â
                        dialogueText.text = ""; // ´ë»ç ÅØ½ºÆ® ÃÊ±âÈ­
                        break;
                    // ±âº»È°µ¿3 : ´Ü¼­ÀÏ ¶§(´Ü¼­)
                    case 6003:
                        dialogueText.text = serif3.line; // ÈùÆ® ´ë»ç Ãâ·Â
                        dialogueText.text = ""; // ´ë»ç ÅØ½ºÆ® ÃÊ±âÈ­
                        break;
                }

                ShowChoiceUI_yj(); // ¸¸³ª´Â »ó´ë¿¡ µû¶ó ´Ù¸¥ ¼±ÅÃ UI Ç¥½Ã
                // °°Àº ¼±ÅÃ UI¸¦ »ç¿ëÇÏ´Âµ¥, ¹®±¸¸¸ ´Ù¸§. yes,noµµ °°À½.
                // ÈÆ·Ã´ÜÀå(6001)À» ¸¸³µÀ» ‹š´Â "ÈÆ·ÃÇÏ½Ã°Ú½À´Ï±î?" ¼±ÅÃÁö ¶ä
                // ±â»ç´ÜÀå(6002)À» ¸¸³µÀ» ‹š´Â "´ÜÇÕ ÈÆ·ÃÀ» ÁøÇàÇÏ½Ã°Ú½À´Ï±î?" ¼±ÅÃÁö ¶ä
                // ÈÆ·Ã´ÜÀå(6001)À» ¸¸³µÀ» ‹š´Â "´Ü¼­¸¦ Á¶»çÇÏ½Ã°Ú°Ú½À´Ï±î?" ¼±ÅÃÁö ¶ä
            }
        }
        else
        {
            choiceUI_yj.SetActive(false); // ¼±ÅÃ UI ¼û±â±â
        }
    }

    void ShowChoiceUI_yj()
    {
        choiceUI_yj.SetActive(true); // ¼±ÅÃ UI È°¼ºÈ­

        switch (whatdial_yj.id)
        {
            // ±âº»È°µ¿1 : ÈÆ·Ã´ÜÀåÀÏ‹š(ÈÆ·Ã)
            case 6001:
                choiceUI_yj.SetActive(true); // ¼±ÅÃ UI È°¼ºÈ­
                break;

            // ±âº»È°µ¿2 : ±â»ç´ÜÀåÀÏ¶§(´ÜÇÕ)
            case 6002:
                dialogueText.text = ""; // ´ë»ç ÅØ½ºÆ® ÃÊ±âÈ­
                break;

            case 6003:
                dialogueText.text = ""; // ´ë»ç ÅØ½ºÆ® ÃÊ±âÈ­
                break;
        }        
    }

    public void OnTalkButtonClick()
    {
        choiceUI_yj.SetActive(true); // ¼±ÅÃ UI È°¼ºÈ­
        //choiceUI.SetActive(false); // ¼±ÅÃ UI ¼û±â±â
        //dialogueUI.SetActive(true); // ´ëÈ­ UI È°¼ºÈ­
        dialogueText.text = "´ÜÇÕÇÏ½Ã°Ú½À´Ï±î?"; // ´ë»ç Ç¥½Ã
        isTalking = true; // ´ëÈ­ »óÅÂ ¼³Á¤
    }


    public void OnYesButtonClick()
    {
        // TalkManager_yj Å¬·¡½ºÀÇ IncreaseTeamPower È£Ãâ
        TalkManager_yj talkManager = FindObjectOfType<TalkManager_yj>();
        if (talkManager != null)
        {
            //talkManager.IncreaseTeamPower(10); // ¿¹½Ã·Î 10¸¸Å­ ÆÀ ÆÄ¿ö Áõ°¡
        }
        else
        {
            Debug.LogError("TalkManager_yj not found in the scene.");
        }


        //AttemptPersuasion(); // ¼³µæ ½Ãµµ
        dialogueText.text = "´ÜÇÕÀ» ÁøÇàÇß´Ù"; // ¼±ÅÃÁö Ã³¸®
        // TalkManager_yj¿¡¼­ Ã³¸®ÇÒ ³»¿ëÀ¸·Î ¿¬°á
        isTalking = false; // ´ëÈ­ »óÅÂ ÇØÁ¦
        choiceUI_yj.SetActive(false); // ¼±ÅÃ UI ¼û±â±â
    }

    public void OnNoButtonClick()
     {
        // persuadeUI.SetActive(false); // ¼³µæ UI ¼û±â±â
        dialogueText.text = "´ÜÇÕÀ» ÁøÇàÇÏÁö ¾Ê½À´Ï´Ù"; // ¼±ÅÃÁö Ã³¸®
        isTalking = false; // ´ëÈ­ »óÅÂ ÇØÁ¦
        choiceUI_yj.SetActive(false); // ¼±ÅÃ UI ¼û±â±â
    }


    /*void HideChoices()
    {
        choiceUI.SetActive(false); // ¼±ÅÃ UI ¼û±â±â
        dialogueUI.SetActive(false); // ´ëÈ­ UI ¼û±â±â
        persuadeUI.SetActive(false); // ¼³µæ UI ¼û±â±â
    }*/

    void EndDialogue()
    {
        //dialogueUI.SetActive(false); // ´ëÈ­ UI ¼û±â±â
        isTalking = false; // ´ëÈ­ »óÅÂ ÇØÁ¦
        choiceUI_yj.SetActive(false); // ¼±ÅÃ UI ¼û±â
    }

}