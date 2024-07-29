using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public enum NPCType { 검사, 힐러, 탱커, 마법사, 암살자, 궁수 }
    public NPCType npcType;

    // 각 씬의 위치 지정
    public Transform mainMapPosition;
    public Transform bigHousePosition;
    public Transform trainingPosition;
    public Transform sub2HousePosition;
    public Transform barPosition;

    void Start()
    {
        UpdateNPCPosition();
    }

    void Update()
    {
        UpdateNPCPosition();
    }

    // 시간대에 따라 NPC의 위치 바꾸기
    void UpdateNPCPosition()
    {
        switch (npcType)
        {
            case NPCType.검사:
            case NPCType.힐러:
                if (TimeManager.Instance.IsMorning() || TimeManager.Instance.IsAfternoon())
                    transform.position = mainMapPosition.position;
                else
                    transform.position = bigHousePosition.position;
                break;

            case NPCType.탱커:
                if (TimeManager.Instance.IsMorning() || TimeManager.Instance.IsAfternoon())
                    transform.position = trainingPosition.position;
                else
                    transform.position = bigHousePosition.position;
                break;

            case NPCType.마법사:
                transform.position = sub2HousePosition.position;
                break;

            case NPCType.암살자:
                if (TimeManager.Instance.IsEvening())
                    transform.position = barPosition.position;
                else
                    gameObject.SetActive(false); // 아침과 점심에는 등장하지 않음
                break;

            case NPCType.궁수:
                if (TimeManager.Instance.IsEvening())
                    transform.position = trainingPosition.position;
                else
                    gameObject.SetActive(false); // 아침과 점심에는 등장하지 않음
                break;
        }
    }

    // NPC가 특정 씬에 있는지 확인
    public bool IsInScene(string sceneName)
    {
        switch (npcType)
        {
            case NPCType.검사:
            case NPCType.힐러:
                return sceneName == "main_map" || sceneName == "big_house";
            case NPCType.탱커:
                return sceneName == "training" || sceneName == "big_house";
            case NPCType.마법사:
                return sceneName == "sub2_house";
            case NPCType.암살자:
                return sceneName == "bar" && TimeManager.Instance.IsEvening();
            case NPCType.궁수:
                return sceneName == "training" && TimeManager.Instance.IsEvening();
            default:
                return false;
        }
    }
}
