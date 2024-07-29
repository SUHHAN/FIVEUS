using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public enum NPCType { �˻�, ����, ��Ŀ, ������, �ϻ���, �ü� }
    public NPCType npcType;

    // �� ���� ��ġ ����
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

    // �ð��뿡 ���� NPC�� ��ġ �ٲٱ�
    void UpdateNPCPosition()
    {
        switch (npcType)
        {
            case NPCType.�˻�:
            case NPCType.����:
                if (TimeManager.Instance.IsMorning() || TimeManager.Instance.IsAfternoon())
                    transform.position = mainMapPosition.position;
                else
                    transform.position = bigHousePosition.position;
                break;

            case NPCType.��Ŀ:
                if (TimeManager.Instance.IsMorning() || TimeManager.Instance.IsAfternoon())
                    transform.position = trainingPosition.position;
                else
                    transform.position = bigHousePosition.position;
                break;

            case NPCType.������:
                transform.position = sub2HousePosition.position;
                break;

            case NPCType.�ϻ���:
                if (TimeManager.Instance.IsEvening())
                    transform.position = barPosition.position;
                else
                    gameObject.SetActive(false); // ��ħ�� ���ɿ��� �������� ����
                break;

            case NPCType.�ü�:
                if (TimeManager.Instance.IsEvening())
                    transform.position = trainingPosition.position;
                else
                    gameObject.SetActive(false); // ��ħ�� ���ɿ��� �������� ����
                break;
        }
    }

    // NPC�� Ư�� ���� �ִ��� Ȯ��
    public bool IsInScene(string sceneName)
    {
        switch (npcType)
        {
            case NPCType.�˻�:
            case NPCType.����:
                return sceneName == "main_map" || sceneName == "big_house";
            case NPCType.��Ŀ:
                return sceneName == "training" || sceneName == "big_house";
            case NPCType.������:
                return sceneName == "sub2_house";
            case NPCType.�ϻ���:
                return sceneName == "bar" && TimeManager.Instance.IsEvening();
            case NPCType.�ü�:
                return sceneName == "training" && TimeManager.Instance.IsEvening();
            default:
                return false;
        }
    }
}
