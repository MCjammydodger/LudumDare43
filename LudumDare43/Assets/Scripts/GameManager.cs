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
    private HUD hud;

    [SerializeField]
    private Player playerPrefab;
    [SerializeField]
    private CritterImage critterImagePrefab;

    private List<Critter> critters;

    private List<RenderTexture> critterImages;

    private Color[] critterColours = new Color[] { Color.red, Color.blue, Color.green, Color.yellow, Color.black, Color.white, Color.cyan, Color.grey};

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
        critterImages = new List<RenderTexture>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void RegisterCritter(Critter critter)
    {
        critters.Add(critter);
        if(critters.Count - 1 < critterColours.Length)
        {
            critter.SetColour(critterColours[critters.Count - 1]);
        }
        critter.id = critters.Count - 1;
        critterImages.Add(new RenderTexture(256, 256, 16));
        CritterImage newImage = Instantiate(critterImagePrefab, new Vector3((critter.id + 1) * 1000, 0, 0), Quaternion.identity);
        newImage.SetupImage(critterColours[critter.id], critterImages[critter.id]);
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
        SaveLoad.LevelData data = new SaveLoad.LevelData();
        data.levelNumber = SceneManager.GetActiveScene().buildIndex;
        data.completed = true;
        data.totalCritters = critters.Count;
        data.foundCritters = critters.Count - trapped;
        data.savedCritters = saved;
        SaveLoad.SaveLevel(data);
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

    public void CollectedCritter(Critter critter)
    {
        hud.AddCritterImage(critter.id, critterImages[critter.id], critter.GetHealth());
    }

    public void UpdateCritterHealth(Critter critter)
    {
        hud.SetHealth(critter.id, critter.GetHealth());
    }

    public Transform GetPlayerSpawnPoint()
    {
        return playerSpawnPoint;
    }

    public void ReturnToMenu()
    {
        ResumeGame();
        SceneManager.LoadScene(0);
    }
}
