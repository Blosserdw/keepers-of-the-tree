using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIPage_ChooseScreen : UIPage {

    public GameObject goodBadObject;
    public UILabel goodBadText;
    public UISprite goodBadFrame;
    private int traitorNum;
    private int currentCharacter = 0;
    private int characterCount = 0;

    public GameObject nextCharacterButton, nextButton;

    public UIButton characterButton1, characterButton2, characterButton3, characterButton4;
	public bool button1Reenable = true,	button2Reenable = true,	button3Reenable = true, button4Reenable = true;

    public UILabel playerChooseText;
    public UILabel hitNextText;

	// Use this for initialization
	void Start ()
    {
        UIManager.Instance.DestroyBackgroundUI();

        goodBadObject.SetActive(true);
        goodBadText.gameObject.SetActive(false);
        hitNextText.gameObject.SetActive(false);
        nextCharacterButton.SetActive(false);
        nextButton.SetActive(false);

        currentCharacter = 0;
        traitorNum = Random.Range(1, 5);
        //Debug.Log("Traitor Num is: " + traitorNum);
        characterCount = 0;

        playerChooseText.gameObject.SetActive(true);
        playerChooseText.text = "Player1 Choose Character. Everyone else look away!";
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public void OnCharacter1()
    {
        currentCharacter = 1;
        DetermineCharacterStatus();
        characterButton1.isEnabled = false;
		button1Reenable = false;
        playerChooseText.text = "Player2 Choose Character. Everyone else look away!";
    }

    public void OnCharacter2()
    {
        currentCharacter = 2;
        DetermineCharacterStatus();
        characterButton2.isEnabled = false;
		button2Reenable = false;
        playerChooseText.text = "Player3 Choose Character. Everyone else look away!";
    }

    public void OnCharacter3()
    {
        currentCharacter = 3;
        DetermineCharacterStatus();
        characterButton3.isEnabled = false;
		button3Reenable = false;
        playerChooseText.text = "Player4 Choose Character. Everyone else look away!";
    }

    public void OnCharacter4()
    {
        currentCharacter = 4;
        DetermineCharacterStatus();
        characterButton4.isEnabled = false;
		button4Reenable = false;
    }

    public void DetermineCharacterStatus()
    {
        playerChooseText.gameObject.SetActive(false);

		// Disable all buttons so they can't choose more than one character here
		DisableAllButtons();

        // Player is chosen, display whether good or bad
        if (currentCharacter == traitorNum)
        {
            goodBadFrame.color = Color.red;
            goodBadText.text = "YOU'RE THE TRAITOR!!";
            goodBadText.color = Color.red;
            goodBadText.gameObject.SetActive(true);
            hitNextText.gameObject.SetActive(true);
        }
        else
        {
            goodBadFrame.color = Color.green;
            goodBadText.text = "YOU'RE GOOD!";
            goodBadText.color = Color.green;
            goodBadText.gameObject.SetActive(true);
            hitNextText.gameObject.SetActive(true);
        }

        nextCharacterButton.SetActive(true);
    }

    public void OnNextCharacter()
    {
        hitNextText.gameObject.SetActive(false);

        if (characterCount == 0)
        {
            playerChooseText.text = "Player2 Choose Character. Everyone else look away!";
            characterCount++;
        }
        else if (characterCount == 1)
        {
            playerChooseText.text = "Player3 Choose Character. Everyone else look away!";
            characterCount++;
        }
        else if (characterCount == 2)
        {
            playerChooseText.text = "Player4 Choose Character. Everyone else look away!";
            characterCount++;
        }
        else if (characterCount == 3)
        {
            nextCharacterButton.SetActive(false);
            nextButton.SetActive(true);
            playerChooseText.text = "Gather your crewmates and hit next to play!";
        }

        goodBadFrame.color = Color.white;
        goodBadText.gameObject.SetActive(false);
        playerChooseText.gameObject.SetActive(true);
        nextCharacterButton.SetActive(false);

		// Re-enable buttons that haven't been chosen yet
		if (button1Reenable)
			characterButton1.gameObject.SetActive(true);
		if (button2Reenable)
			characterButton2.gameObject.SetActive(true);
		if (button3Reenable)
			characterButton3.gameObject.SetActive(true);
		if (button4Reenable)
			characterButton4.gameObject.SetActive(true);
    }

    public void OnNext()
    {
        UIManager.Instance.LoadHUBUI();
        UIManager.Instance.DestroyTitleScreen();
    }

	public void DisableAllButtons()
	{
		characterButton1.gameObject.SetActive(false);
		characterButton2.gameObject.SetActive(false);
		characterButton3.gameObject.SetActive(false);
		characterButton4.gameObject.SetActive(false);
	}
}
