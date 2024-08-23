using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITest : UIWindow
{
    public string title;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    internal void SetTitle(string name)
    {
        title = name;
    }
}
