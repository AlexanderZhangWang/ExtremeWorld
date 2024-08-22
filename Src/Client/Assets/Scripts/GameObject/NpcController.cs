using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.Data;
using Managers;

public class NpcController : MonoBehaviour {

    public int npcID;
    Animator anim;

    NpcDefine npc;

    void Start () {
        anim = this.gameObject.GetComponent<Animator>();
        npc = NPCManager.Instance.GetNpcDefine(npcID);

    }


    void Update () {
		
	}

    void OnMouseDown()
    {
        Debug.LogError(this.name);
    }
}
