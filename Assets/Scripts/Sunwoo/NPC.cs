using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public string npcName; // NPC 이름
    public int affection = 0; // NPC 호감도
    public string[] dialogues; // 호감도에 따른 대사 배열
    public GameObject dialogueUI; // 대화 UI
    public GameObject choiceUI; // 선택지 UI
    public GameObject giftUI; // 선물 UI
    public GameObject persuadeUI; // 설득 UI
    public Text dialogueText; // 대화 텍스트

    private bool isNearPlayer = false; // 플레이어가 가까이 있는지 여부
    private ToastManager toastManager; // 토스트 매니저 인스턴스

    void Start()
    {
        toastManager = FindObjectOfType<ToastManager>(); // ToastManager 인스턴스 찾기
    }

    void Update()
    {
        // 플레이어가 가까이 있고 E 키를 눌렀을 때 선택지 표시
        if (isNearPlayer && Input.GetKeyDown(KeyCode.E))
        {
            ShowChoices();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어가 트리거 범위에 들어왔을 때
        if (other.CompareTag("Player"))
        {
            isNearPlayer = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // 플레이어가 트리거 범위에서 나갔을 때
        if (other.CompareTag("Player"))
        {
            isNearPlayer = false;
            HideChoices();
        }
    }

    public void ShowChoices()
    {
        choiceUI.SetActive(true); // 선택지 UI 표시
    }

    public void HideChoices()
    {
        choiceUI.SetActive(false); // 선택지 UI 숨기기
    }

    public void Talk()
    {
        HideChoices();
        dialogueUI.SetActive(true); // 대화 UI 표시
        UpdateDialogue(); // 대화 업데이트
    }

    public void UpdateDialogue()
    {
        string dialogue = GetDialogueBasedOnAffection(); // 호감도에 따른 대사 가져오기
        dialogueText.text = dialogue; // 대사 텍스트 설정
        if (affection == 100)
        {
            choiceUI.SetActive(false); // 호감도가 100이면 선택지 UI 숨기기
        }
        else
        {
            ShowChoices(); // 그렇지 않으면 선택지 UI 표시
        }
        EndDialogue();
    }

    public void EndDialogue()
    {
        // 대화가 끝난 후 호감도 변화 메시지 표시
        string affectionChangeMessage = "호감도가 " + affection + "만큼 되었습니다.";
        toastManager.ShowToast(affectionChangeMessage);
    }

    public string GetDialogueBasedOnAffection()
    {
        // 호감도에 따른 대사 반환
        if (affection >= 100) return "너가 너무 좋아!";
        else if (affection >= 90) return dialogues[6];
        else if (affection >= 80) return dialogues[5];
        else if (affection >= 70) return dialogues[4];
        else if (affection >= 60) return dialogues[3];
        else if (affection >= 50) return dialogues[2];
        else return dialogues[1];
    }

    public void Gift()
    {
        HideChoices();
        giftUI.SetActive(true); // 선물 UI 표시
    }

    public void ReceiveGift(string giftName)
    {
        int affectionChange = GetGiftAffectionChange(giftName); // 선물에 따른 호감도 변화 가져오기
        if (affectionChange > 0)
        {
            affection += affectionChange; // 호감도 증가
            toastManager.ShowToast("호감도가 " + affectionChange + "만큼 증가했습니다."); // 호감도 변화 메시지 표시
        }
        giftUI.SetActive(false); // 선물 UI 숨기기
    }

    public int GetGiftAffectionChange(string giftName)
    {
        // 특정 선물에 따른 호감도 변화 구현
        // 예: if (giftName == "특정 선물") return 10;
        return 0; // 기본적으로 호감도 변화 없음
    }

    public void Persuade()
    {
        HideChoices();
        persuadeUI.SetActive(true); // 설득 UI 표시
    }

    public void AttemptPersuasion()
    {
        int successChance = affection; // 호감도에 비례한 성공 확률
        int randomValue = Random.Range(0, 100); // 0에서 100 사이의 랜덤 값 생성
        if (randomValue < successChance)
        {
            // 설득 성공
            toastManager.ShowToast("설득에 성공했습니다!");
            // 팀에 영입하는 로직 추가
        }
        else
        {
            // 설득 실패
            toastManager.ShowToast("설득에 실패했습니다.");
        }
        persuadeUI.SetActive(false); // 설득 UI 숨기기
    }
}
