using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpPage : MonoBehaviour {

    [SerializeField]
    private Transform listParent;

    [SerializeField]
    private UnityEngine.UI.Button buttonPrefab;

    [SerializeField]
    private HelpPanel helpPanel;

    private void Start()
    {
        foreach(HelpPanel.Help help in helpPanel.helpList)
        {
            UnityEngine.UI.Button newButton = Instantiate(buttonPrefab, listParent);
            Text text = newButton.GetComponentInChildren<Text>();
            text.text = help.title;
            newButton.onClick.AddListener(() => { helpPanel.SetupHelpPanel(help.type, () => gameObject.SetActive(true)); gameObject.SetActive(false); }); 
        }
    }
}
