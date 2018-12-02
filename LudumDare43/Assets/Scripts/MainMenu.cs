using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    [SerializeField]
    private int numberOfLevels;

    [SerializeField]
    private GameObject deleteButton;

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
        RefreshMenu();
	}
	
    private void RefreshMenu()
    {
        if(SaveLoad.HasSaveData())
        {
            deleteButton.SetActive(true);
        }
        else
        {
            deleteButton.SetActive(false);
        }
        for(int i = 0; i < levelList.childCount; i++)
        {
            Destroy(levelList.GetChild(i).gameObject);
        }
        for (int i = 1; i < numberOfLevels + 1; i++)
        {
            LevelButton button = Instantiate(buttonPrefab, levelList);
            levelDatas.Add(SaveLoad.LoadLevel(i));
            bool prevCompleted = i == 1 ? true : SaveLoad.LoadLevel(i-1).completed;
            
            button.SetupButton(i, "Level " + i, levelDatas[i - 1].completed, CalculateLevelPercentage(levelDatas[i - 1]), this, prevCompleted);
        }
        ShowLevelInfo(1);
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

    public void Delete()
    {
        SaveLoad.DeleteData();
        RefreshMenu();
    }
}
