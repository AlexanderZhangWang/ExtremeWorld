﻿using Managers;
using Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Managers;

public class UIMinimap : MonoBehaviour {
    public Collider minimapBoundingBox;
    public Image minimap;
    public Image arrow;
    public Text mapName;

    private Transform playerTransform;
	void Start () {
        Debug.Log("---Start UIMinimap.cs");
        this.InitMap();
    }
    void InitMap()
    {
        this.mapName.text = User.Instance.CurrentMapData.Name;
        Debug.Log("---before if.cs");
        if (this.minimap.overrideSprite == null)
            this.minimap.overrideSprite = MinimapManager.Instance.LoadCurrentMinimap();

        this.minimap.SetNativeSize();
        Debug.Log("---Minimap transform " + this.minimap.transform.localPosition.ToString());
        this.minimap.transform.localPosition = Vector3.zero;
        Debug.Log("---Minimap transform " + this.minimap.transform.localPosition.ToString());
        this.playerTransform = User.Instance.CurrentCharacterObject.transform;
    }

    // Update is called once per frame
    void Update () {
        float realWidth = minimapBoundingBox.bounds.size.x;
        float realHeight = minimapBoundingBox.bounds.size.z;
        float relaX = playerTransform.position.x - minimapBoundingBox.bounds.min.x;
        float relaY = playerTransform.position.z - minimapBoundingBox.bounds.min.z;

        float pivotX = relaX / realWidth;
        float pivotY = relaY / realHeight;

        this.minimap.rectTransform.pivot = new Vector2(pivotX, pivotY);
        this.minimap.rectTransform.localPosition = Vector2.zero;
        this.arrow.transform.eulerAngles = new Vector3(0, 0, -playerTransform.eulerAngles.y);
	}
}