using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainManager : MonoBehaviour
{

    void Start()
    {
        // Additively load the GUI scene
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }


}
