using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IngameR_Manager : MonoBehaviour
{    
    void Start()
    {
        // Additively load the GUI scene
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }
}
