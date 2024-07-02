using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ConfigManager : MonoBehaviour
{
    public Config config;

    void Awake()
    {
        LoadConfig();
    }

    void LoadConfig()
    {
        TextAsset configFile = Resources.Load<TextAsset>("config");
        if (configFile != null)
        {
            config = JsonUtility.FromJson<Config>(configFile.text);
            Debug.Log("Client ID: " + config.clientId);
            Debug.Log("Client Secret: " + config.clientSecret);
        }
        else
        {
            Debug.LogError("Config file not found!");
        }
    }
}