using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIMenu : MonoBehaviour
{
    public UIPage StartingPage = null;

	private Dictionary<string, UIPage> PageLookup = new Dictionary<string, UIPage>();
	protected List<UIPage> PageStack = new List<UIPage>();

	//**************************************************
	// INITIALIZATION / DESTROY
	//**************************************************
	void Start ()
	{
		OpenMenu();
        InitializePages();
		GotoStartingPage();
	}

	protected virtual void OpenMenu() {}

    void InitializePages()
	{
		UIPage[] Pages = gameObject.GetComponentsInChildren<UIPage>();
		foreach ( UIPage page in Pages )
		{
			PageLookup.Add( page.gameObject.name, page );
			NGUITools.SetActive( page.gameObject, false );
		}
	}

    protected virtual void GotoStartingPage()
    {
        GotoPage(StartingPage);
    }

	//**************************************************
	// PAGE FLOW
	//**************************************************
	public void GotoPage( string PageName )
	{
		UIPage nextPage = null;
		PageLookup.TryGetValue( PageName, out nextPage );
		GotoPage(nextPage);
	}

	public void GotoPage( UIPage Page )
	{
		// Hide the current page if not the first page
		if( PageStack.Count > 0 )
		{
			PageStack[PageStack.Count - 1].ClosePage();
			NGUITools.SetActive( PageStack[PageStack.Count - 1].gameObject, false );
		}

		// Add the page to the stack
		PageStack.Add(Page);

		// Show the new page
		NGUITools.SetActive( Page.gameObject, true );
		Page.OpenPage();

		// Notify menu of page change
		PageChanged();
	}

	public void GoBack()
	{
		// Remove the current page
		PageStack[PageStack.Count - 1].PageBack();
		PageStack[PageStack.Count - 1].ClosePage();
		NGUITools.SetActive( PageStack[PageStack.Count - 1].gameObject, false );
		PageStack.RemoveAt( PageStack.Count - 1 );

		// Go to the previous page
		NGUITools.SetActive( PageStack[PageStack.Count - 1].gameObject, true );
		PageStack[PageStack.Count - 1].OpenPage();
		
		// Notify menu of page change
		PageChanged();
	}

	protected virtual void PageChanged() {}
}
