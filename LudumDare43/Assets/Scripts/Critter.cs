using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Critter : MonoBehaviour {

    public enum State { TRAPPED, FOLLOWING, DEAD};

    private State currentState;

    private Player player;
    private NavMeshAgent navAgent;

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Use this for initialization
    void Start () {
        currentState = State.TRAPPED;
        player = GameManager.instance.Player;
        GameManager.instance.RegisterCritter(this);
	}
	
	// Update is called once per frame
	void Update () {
        if (currentState == State.FOLLOWING)
        {
            navAgent.SetDestination(player.transform.position);
        }
	}

    public void Free()
    {
        currentState = State.FOLLOWING;
        player.AddNewCritter(this);
    }

    public State GetCurrentState()
    {
        return currentState;
    }
}
