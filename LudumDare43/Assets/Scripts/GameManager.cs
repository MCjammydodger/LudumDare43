using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public Player Player { private set; get; }

    [SerializeField]
    private Transform playerSpawnPoint;
    [SerializeField]
    private CameraFollow cameraFollow;
    [SerializeField]
    private EndScreen endScreen;

    [SerializeField]
    private Player playerPrefab;

    private List<Critter> critters;

	// Use this for initialization
	void Awake () {
        instance = this;
        Player = Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);
        cameraFollow.SetTarget(Player.transform);
	}

    private void Start()
    {
        critters = new List<Critter>();
        endScreen.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void RegisterCritter(Critter critter)
    {
        critters.Add(critter);
    }

    public void Finished()
    {
        PauseGame();
        int saved = 0, trapped = 0, sacrificed = 0;
        foreach(Critter critter in critters)
        {
            switch(critter.GetCurrentState())
            {
                case Critter.State.TRAPPED:
                    trapped++;
                    break;
                case Critter.State.FOLLOWING:
                case Critter.State.HELD:
                    saved++;
                    break;
                case Critter.State.DEAD:
                    sacrificed++;
                    break;
            }
        }
        endScreen.SetStats(critters.Count, saved, trapped, sacrificed);
        endScreen.gameObject.SetActive(true);
    }

    public void PauseGame()
    {
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0;
        }
    }

    public void ResumeGame()
    {
        if(Time.timeScale != 1)
        {
            Time.timeScale = 1;
        }
    }

    public bool IsGamePaused()
    {
        return Time.timeScale == 0;
    }

    public void RestartLevel()
    {
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
