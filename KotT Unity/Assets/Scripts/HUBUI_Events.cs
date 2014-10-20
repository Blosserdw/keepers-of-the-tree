using UnityEngine;
using System.Collections;

public class HUBUI_Events : MonoBehaviour {

    public GameObject player1HUD, player2HUD, player3HUD, player4HUD;

    public UILabel level1Status, level2Status, level3Status, level4Status, level5Status;

    public GameObject chooseCharacterUI;
    public UILabel choosingText;
	public UIButton characterButton1, characterButton2, characterButton3, characterButton4;
	private int firstCharacterNum, secondCharacterNum, thirdCharacterNum;
	private int rosterCount, numChosen;
	private bool contestant1Set = false, contestant2Set = false;

    public GameObject missionUI;
    public GameObject mission1Button, mission2Button, mission3Button, mission4Button, mission5Button;
	

	// Use this for initialization
	void Start ()
    {
        // Make the choose character UI invisible by default
        chooseCharacterUI.SetActive(false);

        // Display the mission UI
        missionUI.SetActive(true);

		// Reset the contestant Set flags
		contestant1Set = false;
		contestant2Set = false;

        // Decide which mission button to make active
        if (GameManager.Instance.currentLevel == Levels.HUB)
        {
            mission1Button.SetActive(true);
            mission2Button.SetActive(false);
            mission3Button.SetActive(false);
            mission4Button.SetActive(false);
            mission5Button.SetActive(false);
        }
        else if (GameManager.Instance.currentLevel == Levels.Level1)
        {
            mission1Button.SetActive(false);
            mission2Button.SetActive(true);
            mission3Button.SetActive(false);
            mission4Button.SetActive(false);
            mission5Button.SetActive(false);
        }
        else if (GameManager.Instance.currentLevel == Levels.Level2)
        {
            mission1Button.SetActive(false);
            mission2Button.SetActive(false);
            mission3Button.SetActive(true);
            mission4Button.SetActive(false);
            mission5Button.SetActive(false);
        }
        else if (GameManager.Instance.currentLevel == Levels.Level3)
        {
            mission1Button.SetActive(false);
            mission2Button.SetActive(false);
            mission3Button.SetActive(false);
            mission4Button.SetActive(true);
            mission5Button.SetActive(false);
        }
        else if (GameManager.Instance.currentLevel == Levels.Level4)
        {
            mission1Button.SetActive(false);
            mission2Button.SetActive(false);
            mission3Button.SetActive(false);
            mission4Button.SetActive(false);
            mission5Button.SetActive(true);
        }
        else
        {
            mission1Button.SetActive(false);
            mission2Button.SetActive(false);
            mission3Button.SetActive(false);
            mission4Button.SetActive(false);
            mission5Button.SetActive(false);
        }

	    // Load up the level statuses from GameManager
        if (GameManager.Instance.level1Status == -1)
        {
            level1Status.text = "FAIL";
        }
        else if (GameManager.Instance.level1Status == 1)
        {
            level1Status.text = "PASS";
        }

        if (GameManager.Instance.level2Status == -1)
        {
            level2Status.text = "FAIL";
        }
        else if (GameManager.Instance.level2Status == 1)
        {
            level2Status.text = "PASS";
        }

        if (GameManager.Instance.level3Status == -1)
        {
            level3Status.text = "FAIL";
        }
        else if (GameManager.Instance.level3Status == 1)
        {
            level3Status.text = "PASS";
        }

        if (GameManager.Instance.level4Status == -1)
        {
            level4Status.text = "FAIL";
        }
        else if (GameManager.Instance.level4Status == 1)
        {
            level4Status.text = "PASS";
        }

        if (GameManager.Instance.level5Status == -1)
        {
            level5Status.text = "FAIL";
        }
        else if (GameManager.Instance.level5Status == 1)
        {
            level5Status.text = "PASS";
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public void OnMission1Start()
    {
		// Update how many the roster is for this mission
		rosterCount = 2;
		numChosen = 0;

		// Hide Mission UI
		missionUI.SetActive(false);

        DisplayChooseCharacterUI(1);
    }

    public void OnMission2Start()
    {
		// Update how many the roster is for this mission
		rosterCount = 3;
		numChosen = 0;
		
		// Hide Mission UI
		missionUI.SetActive(false);
		
		DisplayChooseCharacterUI(2);
    }

    public void OnMission3Start()
    {
		// Update how many the roster is for this mission
		rosterCount = 2;
		numChosen = 0;
		
		// Hide Mission UI
		missionUI.SetActive(false);
		
		DisplayChooseCharacterUI(3);

    }

    public void OnMission4Start()
    {
		// Update how many the roster is for this mission
		rosterCount = 3;
		numChosen = 0;
		
		// Hide Mission UI
		missionUI.SetActive(false);
		
		DisplayChooseCharacterUI(4);

    }

    public void OnMission5Start()
    {
		// Update how many the roster is for this mission
		rosterCount = 2;
		numChosen = 0;
		
		// Hide Mission UI
		missionUI.SetActive(false);
		
		DisplayChooseCharacterUI(Random.Range(1, 5));

    }

    public void DisplayChooseCharacterUI(int playerNum)
    {
        // Change the character number
        choosingText.text = "Player " + playerNum + " choose " + rosterCount + " candidates!";

        // Display the choose character UI
        chooseCharacterUI.gameObject.SetActive(true);
    }

    public void OnCharacter1()
    {
        characterButton1.isEnabled = false;
		AddCharacterToMission(1);
    }

	public void OnCharacter2()
	{
		characterButton2.isEnabled = false;
		AddCharacterToMission(2);
	}

	public void OnCharacter3()
	{
		characterButton3.isEnabled = false;
		AddCharacterToMission(3);
	}

	public void OnCharacter4()
	{
		characterButton4.isEnabled = false;
		AddCharacterToMission(4);
	}

	public void AddCharacterToMission(int characterNum)
	{
		numChosen++;

		// Set up this character as one to go on the misson
		if (!contestant1Set)
		{
			contestant1Set = true;
			GameManager.Instance.contestant1 = characterNum;
		}
		else if (!contestant2Set)
		{
			contestant2Set = true;
			GameManager.Instance.contestant2 = characterNum;
		}
		else
		{
			GameManager.Instance.contestant3 = characterNum;
		}

		// See if we continue or if we're done choosing
		if (numChosen < rosterCount)
		{
			// Keep choosing

		}
		else
		{
			// Choosing is over, start mission!
			DisableCharacterButtons();
			LoadLevel();
		}
	}

	public void DisableCharacterButtons()
	{
		characterButton1.gameObject.SetActive(false);
		characterButton2.gameObject.SetActive(false);
		characterButton3.gameObject.SetActive(false);
		characterButton4.gameObject.SetActive(false);
	}

    public void LoadLevel()
    {
        if (GameManager.Instance.level1Status == 0)
        {
            GameManager.Instance.currentLevel = Levels.Level1;

            Application.LoadLevel("level1");
        }
        else if (GameManager.Instance.level2Status == 0)
        {
            GameManager.Instance.currentLevel = Levels.Level2;

            Application.LoadLevel("level2");
        }
        else if (GameManager.Instance.level3Status == 0)
        {
            GameManager.Instance.currentLevel = Levels.Level3;

            Application.LoadLevel("level3");
        }
        else if (GameManager.Instance.level4Status == 0)
        {
            GameManager.Instance.currentLevel = Levels.Level4;

            Application.LoadLevel("level4");
        }
        else if (GameManager.Instance.level5Status == 0)
        {
            GameManager.Instance.currentLevel = Levels.Level5;

            Application.LoadLevel("level5");
        }
    }
}
