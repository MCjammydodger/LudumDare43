using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour {

    [SerializeField]
    private Text foundCritters;
    [SerializeField]
    private Text savedCritters;
    [SerializeField]
    private Text trappedCritters;
    [SerializeField]
    private Text sacrificedCritters;

	public void SetStats(int total, int saved, int trapped, int sacrificed, int found)
    {
        foundCritters.text = found + "/" + total;
        savedCritters.text = saved + "/" + total;
        trappedCritters.text = trapped + "/" + total;
        sacrificedCritters.text = sacrificed + "/" + total;
    }
}
