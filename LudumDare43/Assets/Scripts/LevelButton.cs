using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour {

    [SerializeField]
    private Text levelText;

    private UnityEngine.UI.Button button;
    private MainMenu mainMenu;
    private int levelNumber;

    private void Awake()
    {
        button = GetComponent<UnityEngine.UI.Button>();
    }

    public void SetupButton(int number, string levelName, bool completed, int percentComplete, MainMenu menu)
    {
        ColorBlock colours = button.colors;
        colours.normalColor = completed ? Color.green : Color.red;
        button.colors = colours;
        levelText.text = levelName + "\n" + percentComplete.ToString() + "%";
        mainMenu = menu;
        levelNumber = number;
    }

    public void OnButtonPressed()
    {
        mainMenu.ShowLevelInfo(levelNumber);
    }
}
