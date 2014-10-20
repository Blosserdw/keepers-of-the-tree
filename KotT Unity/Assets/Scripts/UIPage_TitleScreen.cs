using UnityEngine;
using System.Collections;

public class UIPage_TitleScreen : UIPage {



	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void OnStart()
    {
        Menu.GotoPage("Pg_Instructions");
        GameObject.Find("Title Screen").SetActive(false);
    }

    public void OnCredits()
    {
        Menu.GotoPage("Pg_Credits");
    }
}
