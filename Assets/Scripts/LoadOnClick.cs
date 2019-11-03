using GameAnalyticsSDK;
using UnityEngine;

public class LoadOnClick : MonoBehaviour {

    void Awake()
    {
        GameAnalytics.Initialize();
    }

    public void LoadScene(int level)
    {
        Application.LoadLevel(level);
    }
}