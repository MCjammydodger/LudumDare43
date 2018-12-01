using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    [SerializeField]
    private int numberOfLevels;

    [SerializeField]
    private Text levelName;
    [SerializeField]
    private Text levelStats;

    [SerializeField]
    private Transform levelList;
    [SerializeField]
    private LevelButton buttonPrefab;

    private int currentLevel;

    private List<SaveLoad.LevelData> levelDatas = new List<SaveLoad.LevelData>(); 

	// Use this for initialization
	void Start () {
		for(int i = 1; i < numberOfLevels+1; i++)
        {
            LevelButton button = Instantiate(buttonPrefab, levelList);
            levelDatas.Add(SaveLoad.LoadLevel(i));
            button.SetupButton(i, "Level " + i, levelDatas[i-1].completed, CalculateLevelPercentage(levelDatas[i-1]), this);
        }
        ShowLevelInfo(1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private int CalculateLevelPercentage(SaveLoad.LevelData data)
    {
        if(data.totalCritters == -1)
        {
            return 0;
        }
        return (int)(((data.savedCritters + data.foundCritters) / (data.totalCritters * 2.0f)) * 100);
        
    }

    public void ShowLevelInfo(int levelNumber)
    {
        currentLevel = levelNumber;
        levelName.text = "Level " + levelNumber;

        string totalCritters = levelDatas[levelNumber - 1].totalCritters == -1 ? "?" : levelDatas[levelNumber - 1].totalCritters.ToString();

        levelStats.text = "Critters Saved: " + levelDatas[levelNumber - 1].savedCritters +"/" + totalCritters + "\n";
        levelStats.text += "Critters Found: " + levelDatas[levelNumber - 1].foundCritters + "/" + totalCritters;
    }

    public void PlayLevel()
    {
        SceneManager.LoadScene(currentLevel);
    }
}
