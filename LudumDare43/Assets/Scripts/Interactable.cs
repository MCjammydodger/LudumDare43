using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour {

    private static Dictionary<string, string> inputToText = new Dictionary<string, string>()
    {
        {"Interact", "E" }
    };

    [SerializeField]
    private float distance = 2f;

    [SerializeField]
    private string input;

    [SerializeField]
    private UnityEvent OnInteract;

    [SerializeField]
    private WorldSpaceCanvas buttonPromptCanvasPrefab;

    private WorldSpaceCanvas buttonPromptCanvas;

    private Player player;
    
    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    // Use this for initialization
    void Start () {
        SpawnCanvas();
	}
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(player.transform.position, transform.position) <= distance)
        {
            buttonPromptCanvas.gameObject.SetActive(true);
            if(Input.GetButtonDown(input))
            {
                OnInteract.Invoke();
            }
        }
        else
        {
            buttonPromptCanvas.gameObject.SetActive(false);
        }
	}

    void SpawnCanvas()
    {
        buttonPromptCanvas = Instantiate(buttonPromptCanvasPrefab, transform, false);
        buttonPromptCanvas.SetText(inputToText[input]);
    }

    private void OnDisable()
    {
        buttonPromptCanvas.gameObject.SetActive(false);
    }
}
