using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad : MonoBehaviour {

    public struct LevelData
    {
        public int levelNumber;
        public bool completed;
        public int totalCritters;
        public int savedCritters;
        public int foundCritters;
    }

	public static LevelData LoadLevel(int number)
    {
        LevelData data = new LevelData();
        data.levelNumber = number;
        data.completed = PlayerPrefs.GetInt("Level" + number + "completed", 0) == 1 ? true : false;
        data.totalCritters = PlayerPrefs.GetInt("Level" + number + "totalCritters", -1);
        data.savedCritters = PlayerPrefs.GetInt("Level" + number + "savedCritters", 0);
        data.foundCritters = PlayerPrefs.GetInt("Level" + number + "foundCritters", 0);
        return data;
    }

    public static void SaveLevel(LevelData data)
    {
        SaveHighestValue("Level" + data.levelNumber + "completed", data.completed ? 1 : 0);
        SaveHighestValue("Level" + data.levelNumber + "totalCritters", data.totalCritters);
        SaveHighestValue("Level" + data.levelNumber + "savedCritters", data.savedCritters);
        SaveHighestValue("Level" + data.levelNumber + "foundCritters", data.foundCritters);
        PlayerPrefs.Save();
    }

    private static void SaveHighestValue(string key, int value)
    {
        PlayerPrefs.SetInt(key, Mathf.Max(value, PlayerPrefs.GetInt(key, -1)));
    }
}
