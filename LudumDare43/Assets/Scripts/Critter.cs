using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Critter : MonoBehaviour {

    private Player player;
    private NavMeshAgent navAgent;

    private bool trapped;

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Use this for initialization
    void Start () {
        trapped = true;
        player = GameManager.instance.Player;
        GameManager.instance.RegisterCritter(this);
	}
	
	// Update is called once per frame
	void Update () {
        if (!trapped)
        {
            navAgent.SetDestination(player.transform.position);
        }
	}

    public void Free()
    {
        trapped = false;
        player.AddNewCritter(this);
    }
}
