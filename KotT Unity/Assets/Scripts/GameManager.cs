using UnityEngine;
using System.Collections;

public struct PlayerObject
{
    public Texture2D playerTexture;
    public GameObject playerPrefab;
}

public enum Levels
{
    HUB,
    Level1,
    Level2,
    Level3,
    Level4,
    Level5
}

public class GameManager : MonoBehaviour {

    private static GameManager instance = null;
    public static GameManager Instance
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

        gameObject.AddComponent<UIManager>();
    }
    
	public Sprite pamSprite;
	public Sprite carlosSprite;

	// Use this for initialization
	void Start ()
    {
	    // Load the title Screen UI
        UIManager.Instance.LoadTitleScreen();

		// Load sprites for characters
        pamSprite = Resources.Load("pam", typeof(Sprite)) as Sprite;
        carlosSprite = Resources.Load("carlos", typeof(Sprite)) as Sprite;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public GameObject playerInstance1 = null;
    public GameObject playerInstance2 = null;
    public GameObject playerInstance3 = null;
    public GameObject playerInstance4 = null;

    public int contestant1, contestant2, contestant3;
    public bool firstContestantChosen = false;
    public GameObject contestant1Object, contestant2Object, contestant3Object;

    public void LoadPlayer(int playerNum)
    {
        GameObject PlayerPrefab = (GameObject)Resources.Load("Prefabs/Player");

        if (playerNum == 1)
        {
            playerInstance1 = GameObject.Instantiate(PlayerPrefab) as GameObject;
			playerInstance1.GetComponent<SpriteRenderer>().sprite = pamSprite;

            if (!firstContestantChosen)
            {
                firstContestantChosen = true;
                contestant1Object = playerInstance1;
            }
            else
            {
                contestant2Object = playerInstance1;
            }
        }
        else if (playerNum == 2)
        {
            playerInstance2 = GameObject.Instantiate(PlayerPrefab) as GameObject;
			playerInstance2.GetComponent<SpriteRenderer>().sprite = carlosSprite;
            
            if (!firstContestantChosen)
            {
                firstContestantChosen = true;
                contestant1Object = playerInstance2;
            }
            else
            {
                contestant2Object = playerInstance2;
            }
        }
        else if (playerNum == 3)
        {
            playerInstance3 = GameObject.Instantiate(PlayerPrefab) as GameObject;

            if (!firstContestantChosen)
            {
                firstContestantChosen = true;
                contestant1Object = playerInstance3;
            }
            else
            {
                contestant2Object = playerInstance3;
            }
        }
        else if (playerNum == 4)
        {
            playerInstance4 = GameObject.Instantiate(PlayerPrefab) as GameObject;
            
            if (!firstContestantChosen)
            {
                firstContestantChosen = true;
                contestant1Object = playerInstance4;
            }
            else
            {
                contestant2Object = playerInstance4;
            }
        }
    }

    public void DestroyPlayer(int playerNum)
    {
        if (playerNum == 1)
        {
            if (playerInstance1 != null)
            {
                Destroy(playerInstance1);
                playerInstance1 = null;
            }
        }
        else if (playerNum == 2)
        {
            if (playerInstance2 != null)
            {
                Destroy(playerInstance2);
                playerInstance2 = null;
            }
        }
        else if (playerNum == 3)
        {
            if (playerInstance3 != null)
            {
                Destroy(playerInstance3);
                playerInstance3 = null;
            }
        }
        else if (playerNum == 4)
        {
            if (playerInstance4 != null)
            {
                Destroy(playerInstance4);
                playerInstance4 = null;
            }
        }
    }

    // 0 means not played, 1 means passed, -1 means failed
    public int level1Status = 0;
    public int level2Status = 0;
    public int level3Status = 0;
    public int level4Status = 0;
    public int level5Status = 0;

    public int level1Passes = 0;
    public int level2Passes = 0;
    public int level3Passes = 0;
    public int level4Passes = 0;
    public int level5Passes = 0;

    public Levels currentLevel = Levels.HUB;

    public int voteCount = 0;

    public void LevelVoteEvent(bool myVote)
    {
        voteCount++;

        if (currentLevel == Levels.Level1)
        {
            if (myVote)
            {
                level1Passes++;
            }

            // Once voting is over
            if (voteCount == 2)
            {
                if (level1Passes == 2)
                {
                    // Set level status to complete
                    level1Status = 1;
                    UIManager.Instance.DisplayLevelOutcome(true);
                }
                else
                {
                    level1Status = -1;
                    UIManager.Instance.DisplayLevelOutcome(false);
                }
            }
        }
        else if (currentLevel == Levels.Level2)
        {
            level2Passes++;

            if (level2Passes == 2)
            {
                // We beat level 2!
                level2Status = 1;
            }
        }
        else if (currentLevel == Levels.Level3)
        {
            level3Passes++;

            if (level3Passes == 2)
            {
                // We beat level 3!
                level3Status = 1;
            }
        }
        else if (currentLevel == Levels.Level4)
        {
            level4Passes++;

            if (level4Passes == 2)
            {
                // We beat level 4!
                level4Status = 1;
            }
        }
        else if (currentLevel == Levels.Level5)
        {
            level5Passes++;

            if (level5Passes == 2)
            {
                // We beat level 5!
                level5Status = 1;
            }
        }
    }

    public IEnumerator DelayedLevelComplete()
    {
        yield return new WaitForSeconds(1.5f);

        Application.LoadLevel("HUB");
        StartCoroutine(DelayedLoadHUD());
    }

    public IEnumerator DelayedLoadHUD()
    {
        yield return new WaitForSeconds(0.1f);

        UIManager.Instance.LoadHUBUI();
    }
}
