using Managers;
using Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMinimap : MonoBehaviour {
    public Collider minimapBoundingBox;
    public Image minimap;
    public Image arrow;
    public Text mapName;

    private Transform playerTransform;
	void Start () {
        Debug.Log("---Start UIMinimap.cs");
        MinimapManager.Instance.minimap = this;
        this.UpdateMap();
    }
    public void UpdateMap()
    {
        this.mapName.text = User.Instance.CurrentMapData.Name;
        Debug.Log("---before if.cs");
        this.minimap.overrideSprite = MinimapManager.Instance.LoadCurrentMinimap();

        this.minimap.SetNativeSize();
        Debug.Log("---Minimap transform " + this.minimap.transform.localPosition.ToString());
        this.minimap.transform.localPosition = Vector3.zero;
        Debug.Log("---Minimap transform " + this.minimap.transform.localPosition.ToString());
        this.minimapBoundingBox = MinimapManager.Instance.MinimapBoundingBox;
        this.playerTransform = null;
    }

    // Update is called once per frame
    void Update () {
        if (playerTransform == null)
        {
            playerTransform = MinimapManager.Instance.PlayerTransform;
        }
        if (minimapBoundingBox == null || playerTransform == null) return;
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
