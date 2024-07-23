using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IngameManager : MonoBehaviour
{
    void Start()
    {
        // Additively load the GUI scene
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }
}
