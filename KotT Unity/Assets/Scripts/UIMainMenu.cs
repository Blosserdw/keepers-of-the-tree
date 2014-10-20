using UnityEngine;
using System.Collections;

public class UIMainMenu : UIMenu
{
    //public static bool FirstBoot = true;

    public UIPage AltStartingPage = null;

	//**************************************************
	// INITIALIZATION / DESTROY
	//**************************************************
	protected override void OpenMenu()
	{
		TitleBar = GameObject.Find("_TitleBar");
//		PageTitle = GameObject.Find("_PageTitle").GetComponent<UILabel>();
		BackButton = GameObject.Find("_Btn_Back");
	}

    protected override void GotoStartingPage()
    {
        GotoPage(StartingPage);
    }

    void OnCloseApp()
    {
        Debug.Log( "CLOSING APPLICATION..." );
        Application.Quit();
    }

	//**************************************************
	// TITLE BAR
	//**************************************************
	private GameObject 	TitleBar 	= null;
	//private UILabel	PageTitle	= null;
	private GameObject	BackButton	= null;

	public void SetPageTitle( string Title )
	{
		NGUITools.SetActive( TitleBar, (Title != "") );
		//PageTitle.text = Title;
	}

	private void EnableBack( bool DoEnable )
	{
		if(BackButton != null)
			NGUITools.SetActive( BackButton, DoEnable );
	}

	//**************************************************
	// PAGE FLOW
	//**************************************************
	protected override void PageChanged()
	{
		EnableBack( (PageStack.Count > 2) );
	}

	//**************************************************
	// GAME
	//**************************************************
	public void StartSinglePlayerGame()
	{
        
	}
}