using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    [SerializeField]
    private Transform critterList;

    [SerializeField]
    private RawImage imagePrefab;

    private Dictionary<int, RawImage> images;

    private void Start()
    {
        images = new Dictionary<int, RawImage>();
    }
    public void AddCritterImage(int id, RenderTexture renderTexture)
    {
        images.Add(id, Instantiate(imagePrefab, critterList));
        images[id].texture = renderTexture;
    }

    public void RemoveCritterImage(int id)
    {
        //Destroy(images[id].gameObject);
        //images.Remove(id);
        images[id].color = Color.red;
    }
}
