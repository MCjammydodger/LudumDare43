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
    public void AddCritterImage(int id, RenderTexture renderTexture, int maxHealth)
    {
        images.Add(id, Instantiate(imagePrefab, critterList));
        images[id].texture = renderTexture;
        images[id].GetComponent<Slider>().maxValue = maxHealth;
        images[id].GetComponent<Slider>().value = 0;
    }

    public void RemoveCritterImage(int id)
    {
        Destroy(images[id].gameObject);
        images.Remove(id);
    }

    public void SetHealth(int id, int health)
    {
        Slider slider = images[id].GetComponent<Slider>();
        slider.value = slider.maxValue-health;
    }
}
