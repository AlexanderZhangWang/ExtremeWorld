﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWorldElement : MonoBehaviour {

    public Transform owner;
    public float height = 2.5f;
    void Start () {
		
	}
	
	void Update () {
		if (owner != null)
        {
            this.transform.position = owner.position + Vector3.up * height;
        }
	}
}
