using UnityEngine;
using System.Collections;

public class LevelHUD : MonoBehaviour {
    
    public GameObject contestant1FollowUI;
    public UILabel passOrFailLabel;

	// Use this for initialization
	void Start ()
    {
        passOrFailLabel.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Vector3 screenPos = GameObject.Find("Main Camera").camera.WorldToViewportPoint(GameManager.Instance.contestant1Object.transform.position);
        //screenPos.z = 0f;
        //contestant1FollowUI.transform.position = GameObject.Find("HUDCamera").camera.ViewportToWorldPoint(screenPos);
	}

    public void DisplayLevelOutcome(bool didWePass)
    {
        if (didWePass)
        {
            passOrFailLabel.text = "Mission Passed!";
        }
        else
        {
            passOrFailLabel.text = "Mission Failed!";
        }

        passOrFailLabel.gameObject.SetActive(true);
    }
}
