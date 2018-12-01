using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Critter : MonoBehaviour {

    [SerializeField]
    private Renderer renderer;

    public enum State { TRAPPED, FOLLOWING, DEAD, HELD};

    private State currentState;

    private Player player;
    private NavMeshAgent navAgent;
    private Interactable interactable;
    private Rigidbody rb;

    private float timeSinceThrown = 0;

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        interactable = GetComponent<Interactable>();
        rb = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start () {
        currentState = State.TRAPPED;
        player = GameManager.instance.Player;
        GameManager.instance.RegisterCritter(this);
        interactable.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (currentState == State.FOLLOWING)
        {
            navAgent.SetDestination(player.transform.position);
        }

        if(currentState == State.HELD)
        {
            if (!rb.isKinematic)
            {
                timeSinceThrown += Time.deltaTime;
                // Wait a second before checking the velocity so this block doesn't execute straight after a throw.
                if (timeSinceThrown > 1 && rb.velocity == Vector3.zero)
                {
                    rb.isKinematic = true;
                    currentState = State.FOLLOWING;
                    navAgent.enabled = true;
                    navAgent.isStopped = false;
                    interactable.enabled = true;
                }
            }
        }
	}

    public void Free()
    {
        currentState = State.FOLLOWING;
        interactable.enabled = true;
    }

    public State GetCurrentState()
    {
        return currentState;
    }

    public void PickUp()
    {
        navAgent.isStopped = true;
        navAgent.enabled = false;
        currentState = State.HELD;
        interactable.enabled = false;
        player.PickUpCritter(this);
    }

    public void Throw(Vector3 force)
    {
        rb.isKinematic = false;
        rb.AddForce(force, ForceMode.VelocityChange);
        timeSinceThrown = 0;
    }

    public void SetColour(Color colour)
    {
        renderer.material.color = colour;
    }
}
