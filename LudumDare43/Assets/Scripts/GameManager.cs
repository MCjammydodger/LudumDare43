using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public Player Player { private set; get; }

    private List<Critter> critters;

	// Use this for initialization
	void Awake () {
        instance = this;
        Player = FindObjectOfType<Player>();
	}

    private void Start()
    {
        critters = new List<Critter>();
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
        Debug.Log("You finished! You saved " + Player.GetFollowingCrittersCount() + " critters out of " + critters.Count);
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
}
