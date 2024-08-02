using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public int day = 1; // ���� day ������(1~15)
    public int activityCount = 0; // �Ϸ� Ȱ�� ��(3ȸ���� ����)
    public string timeOfDay = "Morning"; // ���� �ð�(����, ����, ����)

    void Update()
    {
        if (activityCount >= 3)
        {
            AdvanceDay(); // Ȱ�� ���� 3�� �̻��̸� ���� ���� �Ѿ
        }
    }

    public void CompleteActivity()
    {
        activityCount++;
        if (activityCount == 1)
        {
            timeOfDay = "Afternoon"; // ù ��° Ȱ�� �� ���ķ� ����
        }
        else if (activityCount == 2)
        {
            timeOfDay = "Evening"; // �� ��° Ȱ�� �� �������� ����
        }
        else if (activityCount == 3)
        {
            timeOfDay = "Morning"; // �� ��° Ȱ�� �� �ٽ� ��ħ���� ����
        }
        //UpdateNPCPositions(); // NPC ��ġ ������Ʈ
    }

    public void AdvanceDay()
    {
        day++;
        if (day > 15)
        {
            day = 1; // 15�� ���Ŀ��� �ٽ� 1�Ϸ� ���ư�(�ӽ�)
                     // ���߿��� ���������� ����ǰ� �ڵ� �߰�
        }
        activityCount = 0;
        timeOfDay = "Morning"; // ���ο� ���� ������ ��ħ
        //UpdateNPCPositions(); // NPC ��ġ ������Ʈ
    }

    /*public void UpdateNPCPositions()
    {
        // ���� �ð��뿡 ���� ��� NPC�� ��ġ�� ������Ʈ
        foreach (var npc in FindObjectsOfType<NpcScript>())
        {
            npc.UpdatePosition(timeOfDay);
        }
    }*/

    public string GetTimeOfDay()
    {
        return timeOfDay; // ���� �ð��� ��ȯ
    }
}
