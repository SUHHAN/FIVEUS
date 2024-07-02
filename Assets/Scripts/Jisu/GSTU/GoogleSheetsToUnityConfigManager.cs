using UnityEngine;
using UnityEditor;

public class GoogleSheetsToUnityConfigManager : MonoBehaviour
{
    void Start()
    {
        ConfigManager configManager = FindObjectOfType<ConfigManager>();
        if (configManager != null && configManager.config != null)
        {
            GoogleSheetsToUnity.GoogleSheetsToUnityConfig configAsset = Resources.Load<GoogleSheetsToUnity.GoogleSheetsToUnityConfig>("GSTU_Config");
            if (configAsset != null)
            {
                configAsset.CLIENT_ID = configManager.config.clientId;
                configAsset.CLIENT_SECRET = configManager.config.clientSecret;
                Debug.Log("GSTU_Config updated with CLIENT_ID and CLIENT_SECRET.");
            }
            else
            {
                Debug.LogError("GSTU_Config asset not found.");
            }
        }
        else
        {
            Debug.LogError("ConfigManager or config not found in the scene.");
        }
    }
}
