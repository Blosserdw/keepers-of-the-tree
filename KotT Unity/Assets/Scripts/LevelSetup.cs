using UnityEngine;
using System.Collections;

public class LevelSetup : MonoBehaviour {

    public bool cameraFollow = false;

	// Use this for initialization
	void Start ()
    {
        // Pick out the relevant information from GameManager
        GameManager.Instance.LoadPlayer(GameManager.Instance.contestant1);
        GameManager.Instance.LoadPlayer(GameManager.Instance.contestant2);

        if (cameraFollow)
        {
            GameObject.Find("Main Camera").AddComponent<SmoothFollow>();

            // Make the camera follow the new player
            GameObject.Find("Main Camera").camera.GetComponent<SmoothFollow>().target = GameManager.Instance.contestant1Object.transform;
            GameObject.Find("Main Camera").camera.GetComponent<SmoothFollow>().target2 = GameManager.Instance.contestant2Object.transform;
        }

        // Set contestant flags for use later with camera stuff


        // Load the UI
        UIManager.Instance.LoadLevelHUD();

        // Set controls
        GameManager.Instance.contestant2Object.GetComponent<NonPhysicsPlayerTester>().jumpKey = KeyCode.I;
        GameManager.Instance.contestant2Object.GetComponent<NonPhysicsPlayerTester>().leftKey = KeyCode.J;
        GameManager.Instance.contestant2Object.GetComponent<NonPhysicsPlayerTester>().rightKey = KeyCode.L;
        GameManager.Instance.contestant2Object.GetComponent<NonPhysicsPlayerTester>().passKey = KeyCode.U;
        GameManager.Instance.contestant2Object.GetComponent<NonPhysicsPlayerTester>().failKey = KeyCode.O;

        // Reset the first contestant flag in Game Manager
        GameManager.Instance.firstContestantChosen = false;

        // Reset the vote count in GameManager for the new level
        GameManager.Instance.voteCount = 0;

	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}
}
