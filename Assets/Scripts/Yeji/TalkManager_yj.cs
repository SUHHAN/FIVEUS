using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro ≥◊¿”Ω∫∆‰¿ÃΩ∫ √ﬂ∞°
using UnityEngine.UI; // UI ø‰º“ ªÁøÎ¿ª ¿ß«— ≥◊¿”Ω∫∆‰¿ÃΩ∫ √ﬂ∞°

// ProDialogue ≈¨∑°Ω∫ ¡§¿« («¡∑—∑Œ±◊ ¥ÎªÁ ¿˙¿Â)
public class ProDialogue_yj
{
    public int id; // π¯»£
    public string name; // ¿Œπ∞
    public string line; // ¥ÎªÁ

    public ProDialogue_yj(int id, string name, string line)
    {
        this.id = id;
        this.name = name;
        this.line = line;
    }
}


public class TalkManager_yj : MonoBehaviour
{
    /*   Dictionary<int, string[]> talkData_yj;
       Dictionary<int, Sprite> portraitData_yj;

       public Sprite[] portraitArr_yj;
       // Start is called before the first frame update
       void Awake()
       {
           talkData_yj = new Dictionary<int, string[]>();
           portraitData_yj = new Dictionary<int, Sprite>();
           GenerateData();
       }

       void GenerateData()
       {
           // »˘∆Æ æ∆¿Ã≈€(æ¯æÓ¡˙ ºˆµµ ¿÷¿Ω. ªÁΩ« ø÷ ∏∏µÈæ˙¥¬¡ˆ ≥™µµ∏Ù∑Á∞⁄¥Ÿ)
           talkData_yj.Add(100, new string[] { "¥‹º≠¥Ÿ. ¡∂ªÁ«ÿ ∫∏¿⁄()" });
           // »∆∑√¥‹¿Â ∏∏≥µ¿ª ∂ß
           talkData_yj.Add(1000, new string[] { "Hello?:0", "It's your first time here, right?:1" });
           // ±‚ªÁ¥‹¿Â ∏∏≥µ¿ª ∂ß
           talkData_yj.Add(2000, new string[] { "Let's exercise!:1", "Muscle! hustle!:2" });

           // quest talk
           talkData_yj.Add(10 + 1000, new string[] { "Welcome!:0", "Talk to KightsJJang! :1" });
           talkData_yj.Add(11 + 2000, new string[] { "Hey!:0", "Give me THAT coin! :1" });
           talkData_yj.Add(20 + 1000, new string[] { "coin?:0", "I found it! :3" });
           talkData_yj.Add(20 + 5000, new string[] { "I found THAT coin! :1" });
           talkData_yj.Add(21 + 2000, new string[] { "Thanks! :2"});

           portraitData_yj.Add(1000 + 0, portraitArr_yj[0]);
           portraitData_yj.Add(1000 + 1, portraitArr_yj[1]);
           portraitData_yj.Add(1000 + 2, portraitArr_yj[2]);
           portraitData_yj.Add(1000 + 3, portraitArr_yj[3]);

           portraitData_yj.Add(2000 + 0, portraitArr_yj[4]);
           portraitData_yj.Add(2000 + 1, portraitArr_yj[5]);
           portraitData_yj.Add(2000 + 2, portraitArr_yj[6]);
           portraitData_yj.Add(2000 + 3, portraitArr_yj[7]);
       }

       public string GetTalk_yj(int id_yj, int talkIndex_yj)
       {
           if (talkIndex_yj == talkData_yj[id_yj].Length)
               return null;
           else
               return talkData_yj[id_yj][talkIndex_yj];
       }

       public Sprite GetPortait_yj(int id_yj, int portraitIndex_yj)
       {
           return portraitData_yj[id_yj + portraitIndex_yj];
       }
       void Start()
       {

       }

       // Update is called once per frame
       void Update()
       {

       }
   }
   */
    // ±‚∫ª »∞µø ¡§µµ¥¬ Ω∫∆Æ∏≥∆Æ ≥ªø°º≠ ¿¸∫Œ ¥ÎªÁ √≥∏Æ(æÓ¬˜«« 5∞≥π€ø° æ¯¿Ω)
    // æ∆¿Ãµ º≥¡§ º≥∏Ì : 6000π¯¥Î∫Œ≈Õ Ω√¿€«‘
    // 6001 : »∆∑√¥Î¿Â, 6002 : ±‚ªÁ¥‹¿Â, 6003 : ¥‹º≠


    // ±‚∫ª»∞µø1 : »∆∑√¥Î¿Â ±‚∫ª ¥ÎªÁ1
    ProDialogue_yj serif1_1 = new ProDialogue_yj(6001, "»∆∑√¥Î¿Â","ø©æÓ-∏ª∂Û≤§¿Ã! »∆∑√«“ ¡ÿ∫Ò¥¬ µ∆≥™?");
    // ±‚∫ª»∞µø1 : »∆∑√¥Î¿Â ±‚∫ª ¥ÎªÁ2
    ProDialogue_yj serif1_2 = new ProDialogue_yj(6001, "»∆∑√¥Î¿Â", "æ»µ«∏È µ…∂ß±Ó¡ˆ! »∆∑√ Ω√¿€¿Ã¥Ÿ!");

    // ±‚∫ª»∞µø2 : ±‚ªÁ¥‹¿Â ±‚∫ª ¥ÎªÁ1
    ProDialogue_yj serif2_1 = new ProDialogue_yj(6002, "±‚ªÁ¥‹¿Â", "π∂ƒ°∏È ªÏ∞Ì »æÓ¡ˆ∏È ¡◊¥¬¥Ÿ! \n¥‹«’»∆∑√ Ω√¿€¿Ã¥Ÿ!!");
    // ±‚∫ª»∞µø2 : ±‚ªÁ¥‹¿Â ±‚∫ª ¥ÎªÁ2
    ProDialogue_yj serif2_2 = new ProDialogue_yj(6002, "±‚ªÁ¥‹¿Â", "3 -1 = 0! øÏ∏Æ¥¬ «œ≥™¥Ÿ!  \n¥‹«’»∆∑√ Ω√¿€¿Ã¥Ÿ!!");

    // ±‚∫ª»∞µø3 : ¥‹º≠ ØÅæ“¿ª ∂ß ±‚∫ª ¥ÎªÁ
    ProDialogue_yj serif3 = new ProDialogue_yj(6003, "¥‹º≠", "¥‹º≠∏¶ √£æ“¥Ÿ. ≥ªøÎ¿ª ªÏ∆Ï∫∏¿⁄.");

    // ¥ÎªÁµÈ¿ª ¿˙¿Â«“ ∏ÆΩ∫∆Æ
    private List<ProDialogue> proDialogue;

    public GameObject opening;
    public TextMeshProUGUI openingText; // TextMeshPro UI ≈ÿΩ∫∆Æ ø‰º“

    public GameObject narration;
    public TextMeshProUGUI narrationText; // TextMeshPro UI ≈ÿΩ∫∆Æ ø‰º“

    public GameObject dialogue;
    public GameObject nameObj; // ¿Ã∏ß ø‰º“
    public TextMeshProUGUI nameText; // TextMeshPro UI ≈ÿΩ∫∆Æ ø‰º“
    public TextMeshProUGUI descriptionText; // TextMeshPro UI ≈ÿΩ∫∆Æ ø‰º“

    //public GameObject resultPanel; // ¥‹«’ ∞·∞˙∏¶ «•Ω√«“ ∆–≥Œ

    //public GameObject home; // ¡˝ πË∞Ê »≠∏È

    private int currentDialogueIndex = 0; // «ˆ¿Á ¥ÎªÁ ¿Œµ¶Ω∫
    private bool isActivated = false; // TalkManager∞° »∞º∫»≠µ«æ˙¥¬¡ˆ ø©∫Œ

    void Awake()
    {
        proDialogue = new List<ProDialogue>();
        // LoadDialogueFromCSV(); // CSVø°º≠ µ•¿Ã≈Õ∏¶ ∑ŒµÂ«œ¥¬ «‘ºˆ »£√‚
        LoadDialogueManually(); // CSV ø¨∞· æ¯¿Ã ¥Î»≠ ºˆµø ¿‘∑¬
    }

    void Start()
    {
        ActivateTalk(); // ø¿∫Í¡ß∆Æ »∞º∫»≠
    }

    void Update()
    {
        /*if (isActivated && currentDialogueIndex == 0)
        {
            PrintProDialogue(currentDialogueIndex);
        }*/
        if (isActivated && Input.GetKeyDown(KeyCode.Space))
        {
            currentDialogueIndex++;
            PrintProDialogue(currentDialogueIndex);
        }
    }

    // ¥Î»≠ ºˆµø ¿‘∑¬
    void LoadDialogueManually()
    {
        // ºˆµø¿∏∑Œ ¥Î»≠ ¿‘∑¬
        proDialogue.Add(new ProDialogue(0, "±‚ªÁ¥‹¿Â", "¥‹«’«œΩ√∞⁄Ω¿¥œ±Ó?"));
    }

    void PrintProDialogue(int index)
    {
        if (index >= proDialogue.Count)
        {
            narration.SetActive(false);
            dialogue.SetActive(false);
            return; // ¥ÎªÁ ∏ÆΩ∫∆Æ∏¶ π˛æÓ≥™∏È ø¿∫Í¡ß∆Æ ∫Ò»∞º∫»≠ »ƒ ∏Æ≈œ
        }

        ProDialogue currentDialogue = proDialogue[index];

        dialogue.SetActive(true);
        nameText.text = currentDialogue.name;
        descriptionText.text = currentDialogue.line;

        // ¿Œπ∞ø° µ˚∂Û ¥ÎªÁ/≥™∑π¿Ãº«/≈ÿΩ∫∆Æ √¢ »∞º∫»≠

        // ±‚∫ª»∞µø 1 : »∆∑√¥‹¿Â -> »∆∑√«œ±‚
        if (currentDialogue.name == "»∆∑√¥‹¿Â")
        {
            narration.SetActive(true);
            dialogue.SetActive(false);
            opening.SetActive(false);
            narrationText.text = currentDialogue.line;
            //ShowConfirmationPanel(); // "øπ" ∂«¥¬ "æ∆¥œø¿" º±≈√ ∆–≥Œ «•Ω√
        }
        // ±‚∫ª»∞µø 2 : ±‚ªÁ¥‹¿Â -> ¥‹«’«œ±‚
        else if (currentDialogue.name == "±‚ªÁ¥‹¿Â")
        {
            narration.SetActive(true);
            dialogue.SetActive(false);
            opening.SetActive(false);
            narrationText.text = currentDialogue.line;
            //ShowConfirmationPanel(); // "øπ" ∂«¥¬ "æ∆¥œø¿" º±≈√ ∆–≥Œ «•Ω√
        }
        // ±‚∫ª»∞µø 3 : ¡§∫∏ºˆ¡˝ -> ±Êø° ¡§∫∏ ∂≥æÓ¡Æ ¿÷¿Ω
        else if (currentDialogue.name == "information")
        {
            narration.SetActive(true);
            dialogue.SetActive(false);
            opening.SetActive(false);
            narrationText.text = currentDialogue.line;
            //ShowConfirmationPanel(); // "øπ" ∂«¥¬ "æ∆¥œø¿" º±≈√ ∆–≥Œ «•Ω√
        }
        // æ∆∏∂ Ω∫≈©∏≥∆Æ ≥ªø°º≠ √≥∏Æ«“ ∞≈¥œ±Ó æ∆¿Ãµ √º≈© æ»«ÿµµ µ… ∞≈ ∞∞±‰ «‘
        // CheckTalk(currentDialogue.id);
    }

    // º±≈√ ∆–≥Œ¿∫ ¥Ÿ∏• ≈¨∑°Ω∫ø°º≠ πﬁæ∆ø√ ∞≈ ∞∞¿∏¥œ ¡ˆøÔµÌ
    void ShowConfirmationPanel()
    {
        // øπ ∂«¥¬ æ∆¥œø¿ º±≈√ ∆–≥Œ¿ª «•Ω√«œ∞Ì πˆ∆∞ ¿Ã∫•∆Æ º≥¡§
        //resultPanel.SetActive(true);

        // øπ πˆ∆∞ ≈¨∏Ø Ω√ √≥∏Æ
        /*resultPanel.transform.Find("YesButton").GetComponent<Button>().onClick.AddListener(() => {
            HandleAffiliation(true); // ¥‹«’«œ±‚ √≥∏Æ
        });

        // æ∆¥œø¿ πˆ∆∞ ≈¨∏Ø Ω√ √≥∏Æ
        resultPanel.transform.Find("NoButton").GetComponent<Button>().onClick.AddListener(() => {
            HandleAffiliation(false); // ¥‹«’ æ»«œ±‚ √≥∏Æ
        });*/
    }

    public void ActivateTalk()
    {
        this.gameObject.SetActive(true);
        isActivated = true;
    }

    void DeactivateTalk()
    {
        this.gameObject.SetActive(false);
        isActivated = false;
    }

    // ¥‹«’∑¬ ¡ı∞° «‘ºˆ
   /*
    public void IncreaseTeamPower(int amount)
    {
        PlayerManager_yj playerManager = FindObjectOfType<PlayerManager_yj>();
        if (playerManager != null)
        {
            playerManager.IncreaseTeamPower(amount);
        }
        else
        {
            Debug.LogError("PlayerManager_yj not found in the scene.");
        }
    }*/
}
