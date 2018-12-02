using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Critter : MonoBehaviour {

    [SerializeField]
    private Renderer critterRenderer;

    public enum State { TRAPPED, FOLLOWING, DEAD, HELD};

    public int id;

    private State currentState;

    private Player player;
    private NavMeshAgent navAgent;
    private Interactable interactable;
    private Rigidbody rb;

    private int health = 4;

    private float timeSinceThrown = 0;
    private float timeSinceLastCollision = 0;

    private bool sinking;
    private float sinkBottom;
    private float sinkSpeed = 2;

    private bool found;

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        interactable = GetComponent<Interactable>();
        rb = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start () {
        player = GameManager.instance.Player;
        GameManager.instance.RegisterCritter(this);
        Trap();
	}
	
	// Update is called once per frame
	void Update () {

        if (currentState == State.FOLLOWING)
        {
            navAgent.SetDestination(player.transform.position);
            if(Vector3.Distance(player.transform.position, transform.position) > 10)
            {
                // If the critter is far from the player and there is no path, it has probably got stuck
                // and so should be teleported to the player.
                NavMeshPath path = new NavMeshPath();
                navAgent.CalculatePath(player.transform.position, path);
                if(path.status != NavMeshPathStatus.PathComplete)
                {
                    navAgent.Warp(player.transform.position);
                }
            }
            if(player.IsHoldingCritter())
            {
                interactable.enabled = false;
            }
            else
            {
                interactable.enabled = true;
            }
        }

        if(currentState == State.HELD)
        {
            if(sinking)
            {
                transform.position -= Vector3.up * sinkSpeed * Time.deltaTime;
                if(transform.position.y < sinkBottom)
                {
                    SetHealth(0);
                }
                return;
            }
            if (!rb.isKinematic)
            {
                timeSinceThrown += Time.deltaTime;
                // Wait a second before checking the velocity so this block doesn't execute straight after a throw.
                if (timeSinceThrown > 1 && rb.velocity == Vector3.zero)
                {
                    HeldToFollow();
                }
                // May have been thrown out of the map.
                if(timeSinceThrown > 10)
                {
                    transform.position = player.transform.position;
                    HeldToFollow();
                }
            }
        }

        timeSinceLastCollision += Time.deltaTime;
	}

    private void HeldToFollow()
    {
        rb.isKinematic = true;
        currentState = State.FOLLOWING;
        navAgent.enabled = true;
        navAgent.isStopped = false;
        interactable.enabled = true;
    }

    public void Free()
    {
        transform.parent = null;
        currentState = State.FOLLOWING;
        interactable.enabled = true;
        if (!found)
        {
            GameManager.instance.CollectedCritter(this);
        }
        navAgent.enabled = true;
        found = true;
    }

    public State GetCurrentState()
    {
        return currentState;
    }

    public void PickUp()
    {
        if (!player.IsHoldingCritter())
        {
            navAgent.isStopped = true;
            navAgent.enabled = false;
            currentState = State.HELD;
            interactable.enabled = false;
            player.PickUpCritter(this);
        }
    }

    public void Throw(Vector3 force)
    {
        rb.isKinematic = false;
        rb.AddForce(force, ForceMode.VelocityChange);
        timeSinceThrown = 0;
    }

    public void SetColour(Color colour)
    {
        critterRenderer.material.color = colour;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Don't deal damage if damage was just done, since it's probably just colliding with the same object again.
        if (currentState == State.HELD && timeSinceLastCollision > 1 && !sinking)
        {
            timeSinceLastCollision = 0;
            SetHealth(health - 1);           
        }
    }

    public int GetHealth()
    {
        return health;
    }

    public void Sink()
    {
        if (IsBeingThrown() && !sinking)
        {
            sinking = true;
            sinkBottom = transform.position.y - 2;
            GetComponent<Collider>().enabled = false;
            rb.isKinematic = true;
        }
    }

    private void SetHealth(int h)
    {
        health = h;
        GameManager.instance.UpdateCritterHealth(this);
        if (health <= 0)
        {
            currentState = State.DEAD;
            gameObject.SetActive(false);
        }
    }

    public void Trap()
    {
        interactable.enabled = false;
        navAgent.enabled = false;
        rb.isKinematic = true;
        currentState = State.TRAPPED;
    }

    public bool IsFound()
    {
        return found;
    }

    public bool IsBeingThrown()
    {
        return currentState == State.HELD && !rb.isKinematic;
    }
}
