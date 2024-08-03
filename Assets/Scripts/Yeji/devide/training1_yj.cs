using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
<<<<<<< Updated upstream
using Unity.VisualScripting; // TextMeshPro ï¿½ï¿½ï¿½Ó½ï¿½ï¿½ï¿½ï¿½Ì½ï¿½ ï¿½ß°ï¿½
using UnityEngine.SceneManagement;
using System.Xml.Serialization;

// ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½Â°ï¿½(ï¿½Æ·ï¿½1 + ï¿½Ü¼ï¿½3)

public class training1_yj : MonoBehaviour
{
    private bool isbasicdial_yj = false; // ï¿½ï¿½ï¿½ Ä¡ï¿½ï¿½ ï¿½Ö´ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½

    public GameObject choiceUI1_yj; // ï¿½âº»È°ï¿½ï¿½1 UI ï¿½Ð³ï¿½


    public GameObject Dial_changyj; // ï¿½âº»ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ ï¿½ï¿½È­Ã¢
    public TextMeshProUGUI dialoguename_yj; // name text
    public TextMeshProUGUI dialogueText_yj; // line text
    public float interactionRange = 3f; // ï¿½ï¿½È£ï¿½Û¿ï¿½ ï¿½Å¸ï¿½

    public GameObject player; // ï¿½Ã·ï¿½ï¿½Ì¾ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Æ®
    private GameObject currentNPC; // ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½È£ï¿½Û¿ï¿½ï¿½Ï´ï¿½ NPC ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½

    public GameObject npc1_yj; // ï¿½Æ·Ã´ï¿½ï¿½ï¿½

    public Button noButton1; // ï¿½Æ´Ï¿ï¿½ ï¿½ï¿½Æ° ï¿½ï¿½ï¿½ï¿½1
    public Button noButton5; // ï¿½ï¿½ï¿½Ã¢ ï¿½Ý±ï¿½ ï¿½ï¿½Æ° ï¿½ï¿½ï¿½ï¿½5

    public Button trainingButton_yj; // 1. ï¿½Æ·ï¿½ ï¿½Ãµï¿½ ï¿½ï¿½Æ° ï¿½ï¿½ï¿½ï¿½
    public Button gotobedButton_yj; //5. Ä§ï¿½ï¿½ ï¿½Ìµï¿½ ï¿½ï¿½Æ° ï¿½ï¿½ï¿½ï¿½

    public GameObject resultUI_yj; // ï¿½ï¿½ï¿½ UI ï¿½Ð³ï¿½
=======
using Unity.VisualScripting; // TextMeshPro ³×ÀÓ½ºÆäÀÌ½º Ãß°¡
using UnityEngine.SceneManagement;
using System.Xml.Serialization;

// À§·Î °¡¸é ³ª¿À´Â°Å(ÈÆ·Ã1 + ´Ü¼­3)

public class training1_yj : MonoBehaviour
{
    private bool isbasicdial_yj = false; // ´ë»ç Ä¡°í ÀÖ´ÂÁö ¿©ºÎ

    public GameObject choiceUI1_yj; // ±âº»È°µ¿1 UI ÆÐ³Î
    public GameObject choiceUI3_yj; // ±âº»È°µ¿3 UI ÆÐ³Î

    public GameObject Dial_changyj; // ±âº»´ë»ç ¶ç¿ï ´ëÈ­Ã¢
    public TextMeshProUGUI dialoguename_yj; // name text
    public TextMeshProUGUI dialogueText_yj; // line text
    public float interactionRange = 3f; // »óÈ£ÀÛ¿ë °Å¸®

    public GameObject player; // ÇÃ·¹ÀÌ¾î ¿ÀºêÁ§Æ®
    private GameObject currentNPC; // ÇöÀç »óÈ£ÀÛ¿ëÇÏ´Â NPC ÀúÀå º¯¼ö

    public GameObject npc1_yj; // ÈÆ·Ã´ÜÀå
    public GameObject npc3_yj; // ÈùÆ®

    public Button noButton1; // ¾Æ´Ï¿À ¹öÆ° ¿¬°á1
    public Button noButton3; // ¾Æ´Ï¿À ¹öÆ° ¿¬°á3
    public Button noButton5; // °á°úÃ¢ ´Ý±â ¹öÆ° ¿¬°á5

    public Button trainingButton_yj; // 1. ÈÆ·Ã ½Ãµµ ¹öÆ° ¿¬°á
    public Button findhintButton_yj; // 3. ´Ü¼­ º¸±â ¹öÆ° ¿¬°á
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
=======
    // ±âº»È°µ¿ ¸î¹ø ÁøÇàÇß´ÂÁö ¼¼¾ß ÇÏ´Ï..ÇÃ·¹ÀÌ¾î¸¦ ºÎ¸£ÀÚ
    // public PlayerNow_yj nowplayer_yj;
    public PlayerManager_yj playermanager_yj; // Ã¼·Â°ü¸®¿ë
    public TimeManager timemanager_yj; // ³¯Â¥ °ü¸® + ±âº»È°µ¿ µ¡»¬¼À¿ë
>>>>>>> Stashed changes

    private static training1_yj _instance;

    public static training1_yj Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<training1_yj>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("talkwithjjang_yj");
                    _instance = obj.AddComponent<training1_yj>();
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
        trainingButton_yj.onClick.AddListener(OntrainButtonClick);
<<<<<<< Updated upstream
        gotobedButton_yj.onClick.AddListener(OngobedButtonClick);

        noButton1.onClick.AddListener(OnNo1ButtonClick);
=======
        findhintButton_yj.onClick.AddListener(OnhintButtonClick);
        gotobedButton_yj.onClick.AddListener(OngobedButtonClick);

        noButton1.onClick.AddListener(OnNo1ButtonClick);
        noButton3.onClick.AddListener(OnNo3ButtonClick);
>>>>>>> Stashed changes
        noButton5.onClick.AddListener(OnNo5ButtonClick);

        isbasicdial_yj = false;
        playermanager_yj.playerNow.howtoday_py = 0;
        playermanager_yj.playerNow.howtrain_py = 0;
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
                if (isbasicdial_yj)
                    HandleNPCchoice_yj(currentNPC);
            }
        }
    }

    void CheckNPCInteraction()
    {
        float distanceNPC1 = Vector3.Distance(player.transform.position, npc1_yj.transform.position);
<<<<<<< Updated upstream
        
=======
        float distanceNPC3 = Vector3.Distance(player.transform.position, npc3_yj.transform.position);

>>>>>>> Stashed changes
        if (distanceNPC1 <= interactionRange)
        {
            currentNPC = npc1_yj;
        }
<<<<<<< Updated upstream
=======
        else if (distanceNPC3 <= interactionRange)
        {
            currentNPC = npc3_yj;
        }
>>>>>>> Stashed changes
        else
        {
            currentNPC = null;
        }
    }

    void HandleNPCDialogue_yj(GameObject npc_yjyj)
    {
        if (isbasicdial_yj == false)
            Dial_changyj.SetActive(true);
<<<<<<< Updated upstream
        else if (resultUI_yj.activeSelf || choiceUI1_yj.activeSelf)
            Dial_changyj.SetActive(false);
        // ï¿½ï¿½ï¿½ï¿½ NPCï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½È­ Ã³ï¿½ï¿½
        if (npc_yjyj == npc1_yj) // ï¿½Æ·Ã´ï¿½ï¿½ï¿½
        {
            dialoguename_yj.text = "ï¿½Æ·Ã´ï¿½ï¿½ï¿½"; // ï¿½Æ·Ã´ï¿½ï¿½ï¿½ ï¿½Ì¸ï¿½ ï¿½ï¿½ï¿½ 
            dialogueText_yj.text = "ï¿½ï¿½ï¿½ï¿½-ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½! \nï¿½Æ·ï¿½ï¿½ï¿½ ï¿½Øºï¿½ï¿½ ï¿½Æ³ï¿½? \n[ï¿½ï¿½ï¿½ï¿½ï¿½Ì½ï¿½ï¿½Ù¸ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½Æ·ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½Ï¼ï¿½ï¿½ï¿½]"; // ï¿½Æ·Ã´ï¿½ï¿½ï¿½ ï¿½âº» ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½
            isbasicdial_yj = true; // ï¿½âº»ï¿½ï¿½ï¿½ Ä¡ï¿½ï¿½ ï¿½Ö´ï¿½ ï¿½ï¿½

        }
=======
        else if (resultUI_yj.activeSelf || choiceUI1_yj.activeSelf || choiceUI3_yj.activeSelf)
            Dial_changyj.SetActive(false);
        // ÇöÀç NPC¿¡ µû¶ó ´ëÈ­ Ã³¸®
        if (npc_yjyj == npc1_yj) // ÈÆ·Ã´ÜÀå
        {
            dialoguename_yj.text = "ÈÆ·Ã´ëÀå"; // ÈÆ·Ã´ëÀå ÀÌ¸§ Ãâ·Â 
            dialogueText_yj.text = "¿©¾î-¸»¶ó²¤ÀÌ! \nÈÆ·ÃÇÒ ÁØºñ´Â µÆ³ª? \n[½ºÆäÀÌ½º¹Ù¸¦ ´­·¯ ÈÆ·ÃÀ» ÁøÇàÇÏ¼¼¿ä]"; // ÈÆ·Ã´ëÀå ±âº» ´ë»ç Ãâ·Â
            isbasicdial_yj = true; // ±âº»´ë»ç Ä¡°í ÀÖ´Â Áß

        }
        else if (npc_yjyj == npc3_yj) // ´Ü¼­
        {
            dialoguename_yj.text = "´Ü¼­"; // ´Ü¼­ ÀÌ¸§ Ãâ·Â 
            dialogueText_yj.text = "´Ü¼­¸¦ Ã£¾Ò´Ù.\nÀÎº¥Åä¸®¿¡¼­ ³»¿ëÀ» È®ÀÎÇØ º¸ÀÚ.\n[½ºÆäÀÌ½º¹Ù¸¦ ´©¸£¼¼¿ä]"; // ´Ü¼­ ±âº» ´ë»ç Ãâ·Â                                      
            isbasicdial_yj = true; // ±âº»´ë»ç Ä¡°í ÀÖ´Â Áß
        }
>>>>>>> Stashed changes
    }

    void HandleNPCchoice_yj(GameObject npc_yjyj)
    {
<<<<<<< Updated upstream
        // ï¿½ï¿½ï¿½ï¿½ NPCï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Ã³ï¿½ï¿½
        if (npc_yjyj == npc1_yj) // ï¿½Æ·Ã´ï¿½ï¿½ï¿½
        {
            //Debug.Log("ï¿½Æ·Ã´ï¿½ï¿½ï¿½ï¿½Ì¶ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½");
=======
        // ÇöÀç NPC¿¡ µû¶ó ¼±ÅÃÁöÃ³¸®
        if (npc_yjyj == npc1_yj) // ÈÆ·Ã´ÜÀå
        {
            //Debug.Log("ÈÆ·Ã´ÜÀåÀÌ¶û ¾ê±âÁß");
>>>>>>> Stashed changes
            Dial_changyj.SetActive(false);
            isbasicdial_yj = false;
            choiceUI1_yj.SetActive(true);

        }
<<<<<<< Updated upstream
    }
    // ï¿½âº»È°ï¿½ï¿½1 : "ï¿½Æ·ï¿½ï¿½Ñ´ï¿½" ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½
=======
        else if (npc_yjyj == npc3_yj) // ´Ü¼­
        {
            Dial_changyj.SetActive(false);
            isbasicdial_yj = false;
            choiceUI3_yj.SetActive(true);
        }
    }
    // ±âº»È°µ¿1 : "ÈÆ·ÃÇÑ´Ù" ¼±ÅÃÇßÀ» ¶§
>>>>>>> Stashed changes
    public void OntrainButtonClick()
    {
        choiceUI1_yj.SetActive(false);

<<<<<<< Updated upstream
        // ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½
        playermanager_yj.IncreaseTrainingCount();// ï¿½Ï·ï¿½ ï¿½Æ·ï¿½ È°ï¿½ï¿½ È½ï¿½ï¿½ 1 ï¿½ï¿½ï¿½ï¿½
        playermanager_yj.IncreaseTiredness(30);// ï¿½Ç·Îµï¿½ 10 ï¿½ï¿½ï¿½ï¿½
        timemanager_yj.CompleteActivity(); // ï¿½Ï·ï¿½ ï¿½âº» È°ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ È½ï¿½ï¿½ 1 ï¿½ï¿½ï¿½ï¿½


        resuedit_yj.text = $"ï¿½âº»È°ï¿½ï¿½ È½ï¿½ï¿½ : {timemanager_yj.activityCount / 2} / 3"; // ï¿½âº» È°ï¿½ï¿½ ï¿½Ø½ï¿½Æ® ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Æ®
=======
        // º¯¼ö °è»ê
        playermanager_yj.IncreaseTrainingCount();// ÇÏ·ç ÈÆ·Ã È°µ¿ È½¼ö 1 Áõ°¡
        playermanager_yj.IncreaseTiredness(30);// ÇÇ·Îµµ 10 Áõ°¡
        timemanager_yj.CompleteActivity(); // ÇÏ·ç ±âº» È°µ¿ ¼öÇà È½¼ö 1 Áõ°¡


        resuedit_yj.text = $"±âº»È°µ¿ È½¼ö : {timemanager_yj.activityCount / 2} / 3"; // ±âº» È°µ¿ ÅØ½ºÆ® ¾÷µ¥ÀÌÆ®
>>>>>>> Stashed changes


        if (timemanager_yj.activityCount >= 5)
        {
<<<<<<< Updated upstream
            resuedit2_yj.text = "ï¿½Ï·ï¿½Ä¡ ï¿½âº» È°ï¿½ï¿½ 3ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ ï¿½Ï¼ï¿½ï¿½Ï¼Ì½ï¿½ï¿½Ï´ï¿½!\n[ï¿½ï¿½ï¿½Î°ï¿½ ï¿½ï¿½]ï¿½ï¿½ [Ä§ï¿½ï¿½]ï¿½ï¿½ ï¿½ï¿½ï¿½Æ°ï¿½ ï¿½Þ½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½Ö¼ï¿½ï¿½ï¿½!";
=======
            resuedit2_yj.text = "ÇÏ·çÄ¡ ±âº» È°µ¿ 3°³¸¦ ¸ðµÎ ¿Ï¼öÇÏ¼Ì½À´Ï´Ù!\n[ÁÖÀÎ°ø Áý]ÀÇ [Ä§´ë]·Î µ¹¾Æ°¡ ÈÞ½ÄÀ» ÃëÇØÁÖ¼¼¿ä!";
>>>>>>> Stashed changes
            resultUI2_yj.SetActive(true);
        }

        resultUI_yj.SetActive(true);

        SaveData();

    }
<<<<<<< Updated upstream
    // ï¿½âº»È°ï¿½ï¿½3 : ï¿½Ü¼ï¿½ ï¿½ï¿½ï¿½Ú´ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½
    
=======
    // ±âº»È°µ¿3 : ´Ü¼­ º¸°Ú´Ù ÇßÀ» ¶§
    public void OnhintButtonClick()
    {
        choiceUI3_yj.SetActive(false);
        timemanager_yj.CompleteActivity(); // ÇÏ·ç ±âº» È°µ¿ ¼öÇà È½¼ö 1 Áõ°¡
        resuedit_yj.text = $"±âº»È°µ¿ È½¼ö : {timemanager_yj.activityCount / 2} / 3";
        if (timemanager_yj.activityCount >= 5)
        {
            resuedit2_yj.text = "ÇÏ·çÄ¡ ±âº» È°µ¿ 3°³¸¦ ¸ðµÎ ¿Ï¼öÇÏ¼Ì½À´Ï´Ù!\n[ÁÖÀÎ°ø Áý]ÀÇ [Ä§´ë]·Î µ¹¾Æ°¡ ÈÞ½ÄÀ» ÃëÇØÁÖ¼¼¿ä!";
            resultUI2_yj.SetActive(true);
        }

        resultUI_yj.SetActive(true);
        Invoke("HideResultPanel()", 2f);

        SaveData();

        SceneManager.LoadScene("InventoryMain"); // ÀÎº¥Åä¸® ¾ÀÀ¸·Î ÀÌµ¿
        // Ã£Àº ´Ü¼­ °³¼ö¸¦ ÇÑ °³ ´Ã¸². ÀÌ°Ç ÀÎº¥Åä¸®¶û ¿¬°ü ÈÄ¿¡ »ý°¢ÇØ¾ß ÇÒµí
    }
>>>>>>> Stashed changes
    public void OngobedButtonClick()
    {
        SceneManager.LoadScene("main_house");
    }
    public void OnNo1ButtonClick()
    {
<<<<<<< Updated upstream
        choiceUI1_yj.SetActive(false); // ï¿½Æ·ï¿½ UI ï¿½ï¿½ï¿½ï¿½Ã¢ ï¿½ï¿½È°ï¿½ï¿½È­
        isbasicdial_yj = false;
    }
   
    public void OnNo5ButtonClick()
    {
        resultUI_yj.SetActive(false); // ï¿½ï¿½ï¿½ UI ï¿½ï¿½ï¿½ï¿½Ã¢ ï¿½ï¿½È°ï¿½ï¿½È­
        resultUI2_yj.SetActive(false); // ï¿½ï¿½ï¿½2 UI ï¿½ï¿½ï¿½ï¿½Ã¢ ï¿½ï¿½È°ï¿½ï¿½È­
=======
        choiceUI1_yj.SetActive(false); // ÈÆ·Ã UI ¼±ÅÃÃ¢ ºñÈ°¼ºÈ­
        isbasicdial_yj = false;
    }
    public void OnNo3ButtonClick()
    {
        choiceUI3_yj.SetActive(false); // ´Ü¼­ UI ¼±ÅÃÃ¢ ºñÈ°¼ºÈ­
        isbasicdial_yj = false;
    }
    public void OnNo5ButtonClick()
    {
        resultUI_yj.SetActive(false); // °á°ú UI ¼±ÅÃÃ¢ ºñÈ°¼ºÈ­
        resultUI2_yj.SetActive(false); // °á°ú2 UI ¼±ÅÃÃ¢ ºñÈ°¼ºÈ­
>>>>>>> Stashed changes
        isbasicdial_yj = false;
    }


    public void HideUI()
    {
        choiceUI1_yj.SetActive(false);
<<<<<<< Updated upstream
=======
        choiceUI3_yj.SetActive(false);
>>>>>>> Stashed changes
        Dial_changyj.SetActive(false);
        resultUI_yj.SetActive(false);
    }
    void HideResultPanel()
    {
        resultUI_yj.SetActive(false);
    }


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
}
