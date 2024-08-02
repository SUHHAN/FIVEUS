using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro 네임스페이스 추가
using UnityEngine.SceneManagement; // SceneManager

public class FailEndingTalkManager : MonoBehaviour
{
    public GameObject opening;
    public TextMeshProUGUI openingText; // TextMeshPro UI 텍스트 요소

    public GameObject narration;
    public TextMeshProUGUI narrationText; // TextMeshPro UI 텍스트 요소

    public GameObject dialogue;
    public GameObject nameObj; // 이름 요소
    public TextMeshProUGUI nameText; // TextMeshPro UI 텍스트 요소
    public TextMeshProUGUI descriptionText; // TextMeshPro UI 텍스트 요소

    private int currentDialogueIndex = 0; // 현재 대사 인덱스
    private bool isActivated = false; // TalkManager가 활성화되었는지 여부

    private List<Dialogue> dialogues = new List<Dialogue>(); // 대화 리스트

    // Start is called before the first frame update
    void Start()
    {
        // 초기 대사 설정
        dialogues.Add(new Dialogue("주인공", "이제 마계로 떠나야 될 시간이야. 가서 공주님을 모셔오자!"));
        dialogues.Add(new Dialogue("나레이션", "주인공 파티는 마계로 가는 길에서 헤매다가 다른 기사단처럼 공주님의 흔적조차 찾지 못하고 돌아오게 되었다."));
        dialogues.Add(new Dialogue("나레이션", "왕은 주인공에게 왕국에서의 추방 명령을 내렸다."));
        dialogues.Add(new Dialogue("나레이션", "모집했던 용병들은 그동안 고생했던 것에 대한 보상을 주인공에게 요구했다."));
        dialogues.Add(new Dialogue("나레이션", "결국 주인공은 그들에게서 도망치며 떠돌아다니는 삶을 살게 된다."));

        ActivateTalk(); // 오브젝트 활성화
        PrintProDialogue(); // 첫 번째 대사 출력
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated && Input.GetKeyDown(KeyCode.Space))
        {
            currentDialogueIndex++;
            if (currentDialogueIndex < dialogues.Count)
            {
                PrintProDialogue();
            }
            else
            {
                DeactivateTalk();
            }
        }
    }

    void PrintProDialogue()
    {
        Dialogue currentDialogue = dialogues[currentDialogueIndex];

        if (currentDialogue.name == "주인공")
        {
            narration.SetActive(false);
            dialogue.SetActive(true);
            opening.SetActive(true);
            descriptionText.text = currentDialogue.line;
        }
        else if (currentDialogue.name == "나레이션")
        {
            narration.SetActive(true);
            dialogue.SetActive(false);
            opening.SetActive(true);
            narrationText.text = currentDialogue.line;
        }
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

    // 대사 구조체 정의
    public struct Dialogue
    {
        public string name; // 대사 주체의 이름
        public string line; // 대사 내용

        public Dialogue(string name, string line)
        {
            this.name = name;
            this.line = line;
        }
    }
}
