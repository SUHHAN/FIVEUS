using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
<<<<<<< Updated upstream
using Unity.VisualScripting; // TextMeshPro ï¿½ï¿½ï¿½Ó½ï¿½ï¿½ï¿½ï¿½Ì½ï¿½ ï¿½ß°ï¿½
using UnityEngine.SceneManagement;
using System.Xml.Serialization;

// ï¿½Ç·ï¿½ + ï¿½Ü¼ï¿½
// ï¿½âº»È°ï¿½ï¿½ 5ï¿½ï¿½Â° ï¿½Ç·ï¿½ ï¿½ï¿½ï¿½ï¿½(5ï¿½Îµï¿½ 5ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½Ï±ï¿½ 7ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½)
public class QuestData_yj :MonoBehaviour
{
    // random gold 
    private bool isbasicdial_yj = false; // ï¿½ï¿½ï¿½ Ä¡ï¿½ï¿½ ï¿½Ö´ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½

    public GameObject choiceUI3_yj; // ï¿½âº»È°ï¿½ï¿½3 UI ï¿½Ð³ï¿½
    public GameObject choiceUI7_yj; // ï¿½âº»È°ï¿½ï¿½7 UI ï¿½Ð³ï¿½

    public GameObject Dial_changyj; // ï¿½âº»ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ ï¿½ï¿½È­Ã¢
    public TextMeshProUGUI dialoguename_yj; // name text
    public TextMeshProUGUI dialogueText_yj; // line text
    public float interactionRange = 3f; // ï¿½ï¿½È£ï¿½Û¿ï¿½ ï¿½Å¸ï¿½

    private GameObject player; // ï¿½Ã·ï¿½ï¿½Ì¾ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Æ®
    private GameObject currentNPC; // ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½È£ï¿½Û¿ï¿½ï¿½Ï´ï¿½ NPC ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½

    public GameObject npc3_yj; // ï¿½ï¿½Æ®
    public GameObject npc7_yj; // ï¿½ï¿½Ã¼ï¿½ï¿½
    public TextMeshProUGUI QuestEditText_yj; // result edit text

    public Button noButton3; // ï¿½Æ´Ï¿ï¿½ ï¿½ï¿½Æ° ï¿½ï¿½ï¿½ï¿½3
    public Button noButton7; // ï¿½Æ´Ï¿ï¿½ ï¿½ï¿½Æ° ï¿½ï¿½ï¿½ï¿½7
    public Button noButton5; // ï¿½ï¿½ï¿½Ã¢ ï¿½Ý±ï¿½ ï¿½ï¿½Æ° ï¿½ï¿½ï¿½ï¿½5

    public Button findhintButton_yj; // 3. ï¿½Ü¼ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½Æ° ï¿½ï¿½ï¿½ï¿½
    public Button myreasonButton_yj; //7. ï¿½Ç·ï¿½ ï¿½Ï±ï¿½ ï¿½ï¿½Æ° ï¿½ï¿½ï¿½ï¿½
    public Button gotobedButton_yj; //5. Ä§ï¿½ï¿½ ï¿½Ìµï¿½ ï¿½ï¿½Æ° ï¿½ï¿½ï¿½ï¿½

    public GameObject resultUI_yj; // ï¿½ï¿½ï¿½ UI ï¿½Ð³ï¿½
=======
using Unity.VisualScripting; // TextMeshPro ³×ÀÓ½ºÆäÀÌ½º Ãß°¡
using UnityEngine.SceneManagement;
using System.Xml.Serialization;

// ÀÇ·Ú + ´Ü¼­
// ±âº»È°µ¿ 5¹øÂ° ÀÇ·Ú ±¸Çö(5ÀÎµ¥ 5°¡ ÀÖÀ¸´Ï±î 7¹øÀ¸·Î ¼³Á¤)
public class QuestData_yj :MonoBehaviour
{
    // random gold 
    private bool isbasicdial_yj = false; // ´ë»ç Ä¡°í ÀÖ´ÂÁö ¿©ºÎ

    public GameObject choiceUI3_yj; // ±âº»È°µ¿3 UI ÆÐ³Î
    public GameObject choiceUI7_yj; // ±âº»È°µ¿7 UI ÆÐ³Î

    public GameObject Dial_changyj; // ±âº»´ë»ç ¶ç¿ï ´ëÈ­Ã¢
    public TextMeshProUGUI dialoguename_yj; // name text
    public TextMeshProUGUI dialogueText_yj; // line text
    public float interactionRange = 3f; // »óÈ£ÀÛ¿ë °Å¸®

    private GameObject player; // ÇÃ·¹ÀÌ¾î ¿ÀºêÁ§Æ®
    private GameObject currentNPC; // ÇöÀç »óÈ£ÀÛ¿ëÇÏ´Â NPC ÀúÀå º¯¼ö

    public GameObject npc3_yj; // ÈùÆ®
    public GameObject npc7_yj; // ¿ìÃ¼Åë
    public TextMeshProUGUI QuestEditText_yj; // result edit text

    public Button noButton3; // ¾Æ´Ï¿À ¹öÆ° ¿¬°á3
    public Button noButton7; // ¾Æ´Ï¿À ¹öÆ° ¿¬°á7
    public Button noButton5; // °á°úÃ¢ ´Ý±â ¹öÆ° ¿¬°á5

    public Button findhintButton_yj; // 3. ´Ü¼­ º¸±â ¹öÆ° ¿¬°á
    public Button myreasonButton_yj; //7. ÀÇ·Ú ÇÏ±â ¹öÆ° ¿¬°á
    public Button gotobedButton_yj; //5. Ä§´ë ÀÌµ¿ ¹öÆ° ¿¬°á

    public GameObject resultUI_yj; // °á°ú UI ÆÐ³Î
>>>>>>> Stashed changes
    public GameObject resultUI2_yj;
    public TextMeshProUGUI resuedit_yj; // result edit text
    public TextMeshProUGUI resuedit2_yj; // result edit text

<<<<<<< Updated upstream
    // ï¿½âº»È°ï¿½ï¿½ ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ß´ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½Ï´ï¿½..ï¿½Ã·ï¿½ï¿½Ì¾î¸¦ ï¿½Î¸ï¿½ï¿½ï¿½
    // public PlayerNow_yj nowplayer_yj;
    public PlayerManager_yj playermanager_yj; // Ã¼ï¿½Â°ï¿½ï¿½ï¿½ï¿½ï¿½
    public TimeManager timemanager_yj; // ï¿½ï¿½Â¥ ï¿½ï¿½ï¿½ï¿½ + ï¿½âº»È°ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½

    private int questmoneyy_yj;// (ï¿½ï¿½ï¿½â¼­ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½) : ï¿½Ç·ï¿½ ï¿½ï¿½ï¿½ï¿½
=======
    // ±âº»È°µ¿ ¸î¹ø ÁøÇàÇß´ÂÁö ¼¼¾ß ÇÏ´Ï..ÇÃ·¹ÀÌ¾î¸¦ ºÎ¸£ÀÚ
    // public PlayerNow_yj nowplayer_yj;
    public PlayerManager_yj playermanager_yj; // Ã¼·Â°ü¸®¿ë
    public TimeManager timemanager_yj; // ³¯Â¥ °ü¸® + ±âº»È°µ¿ µ¡»¬¼À¿ë

    private int questmoneyy_yj;// (¿©±â¼­¸¸ »ç¿ëµÊ) : ÀÇ·Ú °¡°Ý
>>>>>>> Stashed changes
    
    private static QuestData_yj _instance;

    public static QuestData_yj Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<QuestData_yj>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("talkwithjjang_yj");
                    _instance = obj.AddComponent<QuestData_yj>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);

        }
    }
    // Start is called before the first frame update
    void Start()
    {

        findhintButton_yj.onClick.AddListener(OnhintButtonClick);
        myreasonButton_yj.onClick.AddListener(OnQuestButtonClick);
        gotobedButton_yj.onClick.AddListener(OngobedButtonClick);

        noButton3.onClick.AddListener(OnNo3ButtonClick);
        noButton5.onClick.AddListener(OnNo5ButtonClick);
        noButton7.onClick.AddListener(OnNo7ButtonClick);

        isbasicdial_yj = false;
        playermanager_yj.playerNow.howtoday_py = 0;
        playermanager_yj.playerNow.howtrain_py = 0;
        questmoneyy_yj =0;
<<<<<<< Updated upstream
        player = GameObject.FindGameObjectWithTag("Player"); // ï¿½Â±×°ï¿½ "Player"ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Æ® Ã£ï¿½ï¿½
        HideUI(); // ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ UI ï¿½ï¿½ï¿½ï¿½ï¿½
=======
        player = GameObject.FindGameObjectWithTag("Player"); // ÅÂ±×°¡ "Player"ÀÎ ¿ÀºêÁ§Æ® Ã£±â
        HideUI(); // ½ÃÀÛÇÒ ¶§ UI ¼û±â±â
>>>>>>> Stashed changes
    }

    // Update is called once per frame
    void Update()
    {
        
        CheckNPCInteraction();
        HandleUserInput();
        //timeManager_yj.UpdateDateAndTimeDisplay(); 
    }

    void HandleUserInput()
    {
        if (currentNPC != null)
        {
<<<<<<< Updated upstream
            HandleNPCDialogue_yj(currentNPC); // npcï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½È­Ã¢ï¿½ï¿½ ï¿½ï¿½ï¿½
=======
            HandleNPCDialogue_yj(currentNPC); // npcÇÑÅ× °¡±îÀÌ °¡¸é ´ëÈ­Ã¢ÀÌ ¶á´Ù
>>>>>>> Stashed changes

            if (Input.GetKeyDown(KeyCode.Space))
            {
                //QuestMomey_yj();
                //Debug.Log("space");
                if (isbasicdial_yj)
                {
                   //Debug.Log("HandleNPCchoice");
                    HandleNPCchoice_yj(currentNPC);

                }

            }
        }
    }

    void CheckNPCInteraction()
    {
        if (npc3_yj != null)
        {
            float distanceNPC3 = Vector3.Distance(player.transform.position, npc3_yj.transform.position);
            
            if (distanceNPC3 <= interactionRange)
            {
                currentNPC = npc3_yj;
            }
            else
            {
                currentNPC = null;
            }
        }
        float distanceNPC7 = Vector3.Distance(player.transform.position, npc7_yj.transform.position);
        Debug.Log("distanceNPC7 : " + distanceNPC7);

<<<<<<< Updated upstream
        

=======
>>>>>>> Stashed changes
        if (distanceNPC7 <= interactionRange)
        {
            currentNPC = npc7_yj;
        }
    }
<<<<<<< Updated upstream
    // ï¿½Ç·ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½Ï´ï¿½ ï¿½Þ¼Òµï¿½(500-1000ï¿½ï¿½ï¿½)
    int QuestMomey_yj()
    {
        // ï¿½Ì°ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½Ï°ï¿½ questmoneyy_yjï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½Ï´ï¿½ ï¿½Úµï¿½ ï¿½Û¼ï¿½
=======
    // ÀÇ·Ú °¡°Ý °áÁ¤ÇÏ´Â ¸Þ¼Òµå(500-1000°ñµå)
    int QuestMomey_yj()
    {
        // ÀÌ°÷¿¡ ·£´ýÇÏ°Ô questmoneyy_yj¸¦ Á¶ÀÛÇÏ´Â ÄÚµå ÀÛ¼º
>>>>>>> Stashed changes
        System.Random random = new System.Random();
        int randomValue = random.Next(5, 11);
        int goldcal_yj = randomValue * 100;
        return goldcal_yj;
    }

    void HandleNPCDialogue_yj(GameObject npc_yjyj)
    {
        if (isbasicdial_yj == false)
            Dial_changyj.SetActive(true);
        else if (resultUI_yj.activeSelf || choiceUI7_yj.activeSelf || choiceUI3_yj.activeSelf)
            Dial_changyj.SetActive(false);
<<<<<<< Updated upstream
        // ï¿½ï¿½ï¿½ï¿½ NPCï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½È­ Ã³ï¿½ï¿½
        if (npc_yjyj == npc7_yj) // ï¿½Ç·ï¿½
        {
            dialoguename_yj.text = "ï¿½ï¿½ï¿½Î¿ï¿½ ï¿½Ç·ï¿½"; // ï¿½Ç·ï¿½ ï¿½Ì¸ï¿½ ï¿½ï¿½ï¿½ 

            // ï¿½ï¿½ï¿½ï¿½Æ® ï¿½Ý¾ï¿½ï¿½ï¿½ ï¿½Ô¼ï¿½ È£ï¿½ï¿½ï¿½Ï¿ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½, ï¿½ï¿½ï¿½Ú¿ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½
=======
        // ÇöÀç NPC¿¡ µû¶ó ´ëÈ­ Ã³¸®
        if (npc_yjyj == npc7_yj) // ÀÇ·Ú
        {
            dialoguename_yj.text = "»õ·Î¿î ÀÇ·Ú"; // ÀÇ·Ú ÀÌ¸§ Ãâ·Â 

            // Äù½ºÆ® ±Ý¾×À» ÇÔ¼ö È£ÃâÇÏ¿© °¡Á®¿À°í, ¹®ÀÚ¿­ º¸°£ »ç¿ë
>>>>>>> Stashed changes
            if (questmoneyy_yj == 0)
            {
                questmoneyy_yj = QuestMomey_yj();
            }
<<<<<<< Updated upstream
            dialogueText_yj.text = $"ï¿½ï¿½ï¿½Î¿ï¿½ ï¿½Ç·Ú°ï¿½ ï¿½ï¿½ï¿½Ô´ï¿½. \n{questmoneyy_yj}ï¿½ï¿½ï¿½?! ï¿½ï¿½Ã»ï¿½ï¿½ï¿½Ý¾ï¿½! \n[ï¿½ï¿½ï¿½ï¿½ï¿½Ì½ï¿½ï¿½Ù¸ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½Ï¼ï¿½ï¿½ï¿½]";
            isbasicdial_yj = true; // ï¿½âº»ï¿½ï¿½ï¿½ Ä¡ï¿½ï¿½ ï¿½Ö´ï¿½ ï¿½ï¿½
        }
        else if (npc_yjyj == npc3_yj) // ï¿½Ü¼ï¿½
        {
            dialoguename_yj.text = "ï¿½Ü¼ï¿½"; // ï¿½Ü¼ï¿½ ï¿½Ì¸ï¿½ ï¿½ï¿½ï¿½ 
            dialogueText_yj.text = "ï¿½Ü¼ï¿½ï¿½ï¿½ Ã£ï¿½Ò´ï¿½.\nï¿½Îºï¿½ï¿½ä¸®ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ È®ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½.\n[ï¿½ï¿½ï¿½ï¿½ï¿½Ì½ï¿½ï¿½Ù¸ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½]"; // ï¿½Ü¼ï¿½ ï¿½âº» ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½                                      
            isbasicdial_yj = true; // ï¿½âº»ï¿½ï¿½ï¿½ Ä¡ï¿½ï¿½ ï¿½Ö´ï¿½ ï¿½ï¿½
=======
            dialogueText_yj.text = $"»õ·Î¿î ÀÇ·Ú°¡ µé¾î¿Ô´Ù. \n{questmoneyy_yj}°ñµå?! ¾öÃ»³ªÀÝ¾Æ! \n[½ºÆäÀÌ½º¹Ù¸¦ ´­·¯ ÁøÇàÇÏ¼¼¿ä]";
            isbasicdial_yj = true; // ±âº»´ë»ç Ä¡°í ÀÖ´Â Áß
        }
        else if (npc_yjyj == npc3_yj) // ´Ü¼­
        {
            dialoguename_yj.text = "´Ü¼­"; // ´Ü¼­ ÀÌ¸§ Ãâ·Â 
            dialogueText_yj.text = "´Ü¼­¸¦ Ã£¾Ò´Ù.\nÀÎº¥Åä¸®¿¡¼­ ³»¿ëÀ» È®ÀÎÇØ º¸ÀÚ.\n[½ºÆäÀÌ½º¹Ù¸¦ ´©¸£¼¼¿ä]"; // ´Ü¼­ ±âº» ´ë»ç Ãâ·Â                                      
            isbasicdial_yj = true; // ±âº»´ë»ç Ä¡°í ÀÖ´Â Áß
>>>>>>> Stashed changes
        }
    }

    void HandleNPCchoice_yj(GameObject npc_yjyj)
    {
<<<<<<< Updated upstream
        // ï¿½ï¿½ï¿½ï¿½ NPCï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Ã³ï¿½ï¿½
        if (npc_yjyj == npc7_yj) // ï¿½Ç·ï¿½
        {
            Dial_changyj.SetActive(false);
            isbasicdial_yj = false;
            QuestEditText_yj.text = $"ï¿½ï¿½ï¿½ï¿½ : {questmoneyy_yj}G";
            choiceUI7_yj.SetActive(true);
        }
        else if (npc_yjyj == npc3_yj) // ï¿½Ü¼ï¿½
=======
        // ÇöÀç NPC¿¡ µû¶ó ¼±ÅÃÁöÃ³¸®
        if (npc_yjyj == npc7_yj) // ÀÇ·Ú
        {
            Dial_changyj.SetActive(false);
            isbasicdial_yj = false;
            QuestEditText_yj.text = $"º¸»ó : {questmoneyy_yj}G";
            choiceUI7_yj.SetActive(true);
        }
        else if (npc_yjyj == npc3_yj) // ´Ü¼­
>>>>>>> Stashed changes
        {
            Dial_changyj.SetActive(false);
            isbasicdial_yj = false;
            choiceUI3_yj.SetActive(true);
        }

    }
<<<<<<< Updated upstream
    // ï¿½âº»È°ï¿½ï¿½3 : ï¿½Ü¼ï¿½ ï¿½ï¿½ï¿½Ú´ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½
    public void OnhintButtonClick()
    {
        Debug.Log("ï¿½Ü¼ï¿½ Å¬ï¿½ï¿½");
        choiceUI3_yj.SetActive(false);
        timemanager_yj.CompleteActivity(); // ï¿½Ï·ï¿½ ï¿½âº» È°ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ È½ï¿½ï¿½ 1 ï¿½ï¿½ï¿½ï¿½
        resuedit_yj.text = $"ï¿½âº»È°ï¿½ï¿½ È½ï¿½ï¿½ : {timemanager_yj.activityCount / 2} / 3";
        if (timemanager_yj.activityCount >= 5)
        {
            resuedit2_yj.text = "ï¿½Ï·ï¿½Ä¡ ï¿½âº» È°ï¿½ï¿½ 3ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ ï¿½Ï¼ï¿½ï¿½Ï¼Ì½ï¿½ï¿½Ï´ï¿½!\n[ï¿½ï¿½ï¿½Î°ï¿½ ï¿½ï¿½]ï¿½ï¿½ [Ä§ï¿½ï¿½]ï¿½ï¿½ ï¿½ï¿½ï¿½Æ°ï¿½ ï¿½Þ½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½Ö¼ï¿½ï¿½ï¿½!";
=======
    // ±âº»È°µ¿3 : ´Ü¼­ º¸°Ú´Ù ÇßÀ» ¶§
    public void OnhintButtonClick()
    {
        Debug.Log("´Ü¼­ Å¬¸¯");
        choiceUI3_yj.SetActive(false);
        timemanager_yj.CompleteActivity(); // ÇÏ·ç ±âº» È°µ¿ ¼öÇà È½¼ö 1 Áõ°¡
        resuedit_yj.text = $"±âº»È°µ¿ È½¼ö : {timemanager_yj.activityCount / 2} / 3";
        if (timemanager_yj.activityCount >= 5)
        {
            resuedit2_yj.text = "ÇÏ·çÄ¡ ±âº» È°µ¿ 3°³¸¦ ¸ðµÎ ¿Ï¼öÇÏ¼Ì½À´Ï´Ù!\n[ÁÖÀÎ°ø Áý]ÀÇ [Ä§´ë]·Î µ¹¾Æ°¡ ÈÞ½ÄÀ» ÃëÇØÁÖ¼¼¿ä!";
>>>>>>> Stashed changes
            resultUI2_yj.SetActive(true);
        }

        resultUI_yj.SetActive(true);
        Invoke("HideResultPanel()", 2f);
<<<<<<< Updated upstream
        SaveData();
        // ï¿½Îºï¿½ï¿½ä¸®ï¿½ï¿½ ï¿½ï¿½Æ® ï¿½ï¿½ï¿½ï¿½ ï¿½ß°ï¿½
        ItemManager.instance.GetHint_inv();

        SceneManager.LoadScene("InventoryMain"); // ï¿½Îºï¿½ï¿½ä¸® ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½Ìµï¿½
        // Ã£ï¿½ï¿½ ï¿½Ü¼ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ ï¿½ï¿½ ï¿½Ã¸ï¿½. ï¿½Ì°ï¿½ ï¿½Îºï¿½ï¿½ä¸®ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½Ä¿ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½Ø¾ï¿½ ï¿½Òµï¿½
    }

    // ï¿½âº»È°ï¿½ï¿½7 : ï¿½Ç·ï¿½ï¿½Ñ´ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½
    public void OnQuestButtonClick()
    {
        //Dial_changyj.SetActive(false);
        choiceUI7_yj.SetActive(false); // ï¿½ï¿½ï¿½ï¿½ UI ï¿½ï¿½È°ï¿½ï¿½È­

        // ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½
        
        playermanager_yj.DecreaseHealth(20);// ï¿½Ï·ï¿½ Ã¼ï¿½ï¿½ 20 ï¿½ï¿½ï¿½ï¿½
        playermanager_yj.IncreaseMoney(questmoneyy_yj);// ï¿½ï¿½È­ ï¿½ï¿½ï¿½ï¿½
        timemanager_yj.CompleteActivity(); // ï¿½Ï·ï¿½ ï¿½âº» È°ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ È½ï¿½ï¿½ 1 ï¿½ï¿½ï¿½ï¿½

        // ï¿½ï¿½ï¿½Ã¢ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Æ®
        resuedit_yj.text = $"ï¿½âº»È°ï¿½ï¿½ È½ï¿½ï¿½ : {timemanager_yj.activityCount / 2} / 3\nï¿½ï¿½ ï¿½ï¿½ï¿½ : {playermanager_yj.playerNow.money_py} G"; // ï¿½âº» È°ï¿½ï¿½ ï¿½Ø½ï¿½Æ® ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Æ®

        if (timemanager_yj.activityCount >= 5)
        {
            resuedit2_yj.text = "ï¿½Ï·ï¿½Ä¡ ï¿½âº» È°ï¿½ï¿½ 3ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ ï¿½Ï¼ï¿½ï¿½Ï¼Ì½ï¿½ï¿½Ï´ï¿½!\n[ï¿½ï¿½ï¿½Î°ï¿½ ï¿½ï¿½]ï¿½ï¿½ [Ä§ï¿½ï¿½]ï¿½ï¿½ ï¿½ï¿½ï¿½Æ°ï¿½ ï¿½Þ½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½Ö¼ï¿½ï¿½ï¿½!";
=======

        // ÀÎº¥Åä¸®¿¡ ÈùÆ® ·£´ý Ãß°¡
        ItemManager.instance.GetHint_inv();

        SceneManager.LoadScene("InventoryMain"); // ÀÎº¥Åä¸® ¾ÀÀ¸·Î ÀÌµ¿
        // Ã£Àº ´Ü¼­ °³¼ö¸¦ ÇÑ °³ ´Ã¸². ÀÌ°Ç ÀÎº¥Åä¸®¶û ¿¬°ü ÈÄ¿¡ »ý°¢ÇØ¾ß ÇÒµí
    }

    // ±âº»È°µ¿7 : ÀÇ·ÚÇÑ´Ù ÇßÀ» ¶§
    public void OnQuestButtonClick()
    {
        //Dial_changyj.SetActive(false);
        choiceUI7_yj.SetActive(false); // ¼±ÅÃ UI ºñÈ°¼ºÈ­

        // º¯¼ö °è»ê
        
        playermanager_yj.DecreaseHealth(20);// ÇÏ·ç Ã¼·Â 20 °¨¼Ò
        playermanager_yj.IncreaseMoney(questmoneyy_yj);// ÀçÈ­ Áõ°¡
        timemanager_yj.CompleteActivity(); // ÇÏ·ç ±âº» È°µ¿ ¼öÇà È½¼ö 1 Áõ°¡

        // °á°úÃ¢ ¾÷µ¥ÀÌÆ®
        resuedit_yj.text = $"±âº»È°µ¿ È½¼ö : {timemanager_yj.activityCount / 2} / 3\nÃÑ °ñµå : {playermanager_yj.playerNow.money_py} G"; // ±âº» È°µ¿ ÅØ½ºÆ® ¾÷µ¥ÀÌÆ®

        if (timemanager_yj.activityCount >= 5)
        {
            resuedit2_yj.text = "ÇÏ·çÄ¡ ±âº» È°µ¿ 3°³¸¦ ¸ðµÎ ¿Ï¼öÇÏ¼Ì½À´Ï´Ù!\n[ÁÖÀÎ°ø Áý]ÀÇ [Ä§´ë]·Î µ¹¾Æ°¡ ÈÞ½ÄÀ» ÃëÇØÁÖ¼¼¿ä!";
>>>>>>> Stashed changes
            resultUI2_yj.SetActive(true);
        }

        resultUI_yj.SetActive(true);
<<<<<<< Updated upstream
        SaveData();
=======
>>>>>>> Stashed changes
    }

    public void OngobedButtonClick()
    {
        SceneManager.LoadScene("main_house");
    }
    public void OnNo7ButtonClick()
    {
<<<<<<< Updated upstream
        choiceUI7_yj.SetActive(false); // ï¿½Ç·ï¿½ UI ï¿½ï¿½ï¿½ï¿½Ã¢ ï¿½ï¿½È°ï¿½ï¿½È­
=======
        choiceUI7_yj.SetActive(false); // ÀÇ·Ú UI ¼±ÅÃÃ¢ ºñÈ°¼ºÈ­
>>>>>>> Stashed changes
        isbasicdial_yj = false;
    }
    public void OnNo3ButtonClick()
    {
<<<<<<< Updated upstream
        choiceUI3_yj.SetActive(false); // ï¿½Ü¼ï¿½ UI ï¿½ï¿½ï¿½ï¿½Ã¢ ï¿½ï¿½È°ï¿½ï¿½È­
=======
        choiceUI3_yj.SetActive(false); // ´Ü¼­ UI ¼±ÅÃÃ¢ ºñÈ°¼ºÈ­
>>>>>>> Stashed changes
        isbasicdial_yj = false;
    }
    public void OnNo5ButtonClick()
    {
<<<<<<< Updated upstream
        resultUI_yj.SetActive(false); // ï¿½ï¿½ï¿½ UI ï¿½ï¿½ï¿½ï¿½Ã¢ ï¿½ï¿½È°ï¿½ï¿½È­
        resultUI2_yj.SetActive(false); // ï¿½ï¿½ï¿½2 UI ï¿½ï¿½ï¿½ï¿½Ã¢ ï¿½ï¿½È°ï¿½ï¿½È­
=======
        resultUI_yj.SetActive(false); // °á°ú UI ¼±ÅÃÃ¢ ºñÈ°¼ºÈ­
        resultUI2_yj.SetActive(false); // °á°ú2 UI ¼±ÅÃÃ¢ ºñÈ°¼ºÈ­
>>>>>>> Stashed changes
        isbasicdial_yj = false;
    }


    public void HideUI()
    {
        choiceUI3_yj.SetActive(false);
        choiceUI7_yj.SetActive(false);
        Dial_changyj.SetActive(false);
        resultUI_yj.SetActive(false);
    }
    void HideResultPanel()
    {
        resultUI_yj.SetActive(false);
    }
<<<<<<< Updated upstream
    void SaveData()
    {
        DataManager.instance.nowPlayer.Player_hp = playermanager_yj.playerNow.hp_py;
        DataManager.instance.nowPlayer.Player_tired = playermanager_yj.playerNow.tired_py;
        DataManager.instance.nowPlayer.Player_money = playermanager_yj.playerNow.money_py;
        DataManager.instance.nowPlayer.Player_hint = playermanager_yj.playerNow.hint_py;
        DataManager.instance.nowPlayer.Player_team = playermanager_yj.playerNow.team_py;
        DataManager.instance.nowPlayer.Player_day = playermanager_yj.playerNow.day_py;
        DataManager.instance.nowPlayer.Player_howtoday = playermanager_yj.playerNow.howtoday_py;
        DataManager.instance.nowPlayer.Player_howtrain = playermanager_yj.playerNow.howtrain_py;

        DataManager.instance.SaveData();
    }
=======
>>>>>>> Stashed changes
}
