using UnityEngine;
using System.Collections;

public class UIPage_Instructions : UIPage {



	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void OnNext()
    {
        Menu.GotoPage("Pg_ChooseCharacters");
        GameObject.Find("Instructions Screen").SetActive(false);
    }
}
