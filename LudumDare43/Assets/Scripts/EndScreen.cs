using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour {

    [SerializeField]
    private Text totalCritters;
    [SerializeField]
    private Text savedCritters;
    [SerializeField]
    private Text trappedCritters;
    [SerializeField]
    private Text sacrificedCritters;

	public void SetStats(int total, int saved, int trapped, int sacrificed)
    {
        totalCritters.text = total.ToString();
        savedCritters.text = saved.ToString();
        trappedCritters.text = trapped.ToString();
        sacrificedCritters.text = sacrificed.ToString();
    }
}
