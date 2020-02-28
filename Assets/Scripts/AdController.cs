using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdController : MonoBehaviour
{
    [SerializeField] public int countGamesBeforeAd;
    private string storeId = "3480807";
    private bool testMode = default;

    void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Advertisement.Initialize(storeId, testMode);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("enter"))
        {
            ShowInterstitialAd();
        }
    }
    public void ShowInterstitialAd()
    {
            Advertisement.Show();
    }
}
