using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritterImage : MonoBehaviour {

    [SerializeField]
    private Camera cam;
    [SerializeField]
    private Renderer critterRenderer;
   
	// Use this for initialization
	void Start () {
		
	}
	
	public void SetupImage(Color colour, RenderTexture renderTexture)
    {
        critterRenderer.material.color = colour;
        cam.targetTexture = renderTexture;
    }


}
