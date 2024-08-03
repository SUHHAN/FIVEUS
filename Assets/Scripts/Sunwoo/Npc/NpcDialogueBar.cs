using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NpcDialogueBar : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameTxt;
    [SerializeField] TypeEffect talk;
    string[] str = { }; // 초기 빈 대사 배열

    void Start()
    {
        string initialName = "???"; // 초기 이름 설정 (예: ???)
        ActiveDialogue(0, initialName, ref str); // 초기 대화 설정
    }

    void Update()
    {
        // 필요시 Update 메서드에서 추가 로직 작성
    }

    void ActiveDialogue(int idx, string nameData, ref string[] talkData)
    {
        // nameData나 talkData가 null일 경우 기본값 설정
        if (nameData == null) nameData = "???"; // 기본 이름 설정
        if (talkData == null) talkData = new string[] { "No dialogue available." }; // 기본 대사 설정

        nameTxt.text = nameData;

        for (int i = 0; i < talkData.Length; i++)
        {
            talk.SetMsg(talkData[i]);
        }
    }
}
