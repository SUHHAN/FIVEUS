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

    // 시간대에 따라 씬 바꾸기
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

        return "main_map"; // 기본 씬
    }

    // 특정 씬에 등장하는 NPC가 있는지 확인
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

    // 지정된 씬 로드
    void LoadScene(string sceneName)
    {
        if (SceneManager.GetActiveScene().name != sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
