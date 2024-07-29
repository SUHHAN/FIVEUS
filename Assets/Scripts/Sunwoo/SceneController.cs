using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    void Update()
    {
        string sceneName = DetermineScene();
        LoadScene(sceneName);
    }

    // �ð��뿡 ���� �� �ٲٱ�
    string DetermineScene()
    {
        if (TimeManager.Instance.IsMorning() || TimeManager.Instance.IsAfternoon())
        {
            if (CheckForNPCInScene("sub2_house"))
                return "sub2_house";
            else if (CheckForNPCInScene("training"))
                return "training";
            else
                return "main_map";
        }
        else if (TimeManager.Instance.IsEvening())
        {
            if (CheckForNPCInScene("big_house"))
                return "big_house";
            else if (CheckForNPCInScene("bar"))
                return "bar";
            else if (CheckForNPCInScene("training"))
                return "training";
        }

        return "main_map"; // �⺻ ��
    }

    // Ư�� ���� �����ϴ� NPC�� �ִ��� Ȯ��
    bool CheckForNPCInScene(string sceneName)
    {
        NPCController[] npcs = FindObjectsOfType<NPCController>();
        foreach (var npc in npcs)
        {
            if (npc.IsInScene(sceneName))
                return true;
        }
        return false;
    }

    // ������ �� �ε�
    void LoadScene(string sceneName)
    {
        if (SceneManager.GetActiveScene().name != sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
