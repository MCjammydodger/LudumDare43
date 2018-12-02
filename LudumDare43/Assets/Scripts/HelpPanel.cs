using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HelpPanel : MonoBehaviour {

    [SerializeField]
    private RawImage image;
    [SerializeField]
    private Text title;
    [SerializeField]
    private Text info;

    public enum HelpType { GOAL, CRITTERS, CAGE, PICK_UP, LEVERS, BUTTONS, SACRIFICE, WATER, TRAPS, CONTROLS}

    [Serializable]
    public class Help
    {
        public HelpType type;
        public Texture image;
        public string title;
        [TextArea]
        public string info;
    }

    public List<Help> helpList;

    private Action callback;

	public void SetupHelpPanel(HelpType helpType, Action clbk)
    {
        gameObject.SetActive(true);
        callback = clbk;
        foreach(var help in helpList) {
            if (help.type == helpType)
            {
                if (help.image != null)
                {
                    image.gameObject.SetActive(true);
                    image.texture = help.image;
                }
                else
                {
                    image.gameObject.SetActive(false);
                }
                title.text = help.title;
                info.text = help.info;
                return;
            }
        }
        Debug.LogError("Haven't set up help for: " + helpType);
        ClosePanel();     
    }

    public void ClosePanel()
    {
        if (callback != null)
        {
            callback();
        }
        gameObject.SetActive(false);
    }
}
