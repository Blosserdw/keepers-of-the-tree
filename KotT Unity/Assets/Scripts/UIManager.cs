using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{

    private static UIManager instance = null;
    public static UIManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    /// =======================================
    /// TITLE SCREEN UI
    /// =======================================
    public GameObject TitleScreenInstance = null;
    public UIPage_TitleScreen TitleScreenScript = null;

    public void LoadTitleScreen()
    {
        //LoadBackgroundUI();

        GameObject TitleScreenPrefab = (GameObject)Resources.Load("Prefabs/TitleScreenUI");
        TitleScreenInstance = GameObject.Instantiate(TitleScreenPrefab) as GameObject;
        TitleScreenScript = TitleScreenInstance.GetComponent<UIPage_TitleScreen>() as UIPage_TitleScreen;
    }

    public void DestroyTitleScreen()
    {
        DestroyBackgroundUI();

        if (TitleScreenInstance != null)
        {
            Destroy(TitleScreenInstance);
            TitleScreenInstance = null;
        }
    }


    /// =======================================
    /// BACKGROUND UI
    /// =======================================
    public GameObject BackgroundInstance = null;

    public void LoadBackgroundUI()
    {
        GameObject BackgroundPrefab = (GameObject)Resources.Load("Prefabs/BackgroundUI");
        BackgroundInstance = GameObject.Instantiate(BackgroundPrefab) as GameObject;
    }

    public void DestroyBackgroundUI()
    {
        if (BackgroundInstance != null)
        {
            Destroy(BackgroundInstance);
            BackgroundInstance = null;
        }
    }


    /// =======================================
    /// HUB UI
    /// =======================================
    public GameObject HUBUIInstance = null;
    public HUBUI_Events HUBUIScript = null;

    public void LoadHUBUI()
    {
        GameObject HUBUIPrefab = (GameObject)Resources.Load("Prefabs/HUBUI");
        HUBUIInstance = GameObject.Instantiate(HUBUIPrefab) as GameObject;
        HUBUIScript = HUBUIInstance.GetComponent<HUBUI_Events>() as HUBUI_Events;

        // might need to fill out the HUB UI with relevant information
    }

    public void DestroyHUBUI()
    {
        if (TitleScreenInstance != null)
        {
            Destroy(TitleScreenInstance);
            TitleScreenInstance = null;
        }
    }


    /// =======================================
    /// LEVEL HUD
    /// =======================================
    public GameObject LevelHUDInstance = null;
    public LevelHUD LevelHUDScript = null;

    public void LoadLevelHUD()
    {
        GameObject LevelHUDPrefab = (GameObject)Resources.Load("Prefabs/LevelHUD");
        LevelHUDInstance = GameObject.Instantiate(LevelHUDPrefab) as GameObject;
        LevelHUDScript = LevelHUDInstance.GetComponent<LevelHUD>() as LevelHUD;
    }

    public void DestroyLevelHUD()
    {
        if (LevelHUDInstance != null)
        {
            Destroy(LevelHUDInstance);
            LevelHUDInstance = null;
        }
    }

    public void DisplayLevelOutcome(bool didWePass)
    {
        LevelHUDScript.DisplayLevelOutcome(didWePass);
        StartCoroutine(GameManager.Instance.DelayedLevelComplete());
    }
}
