using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Entities;
using Managers;
using Models;
using System;

public class GameObjectManager : MonoSingleton<GameObjectManager>
{

    Dictionary<int, GameObject> Characters = new Dictionary<int, GameObject>();
    // Use this for initialization
    protected override void OnStart()
    {
        StartCoroutine(InitGameObjects());
        CharacterManager.Instance.OnCharacterEnter += OnCharacterEnter;
        CharacterManager.Instance.OnCharacterLeave += OnCharacterLeave;
    }

    private void OnDestroy()
    {
        CharacterManager.Instance.OnCharacterEnter -= OnCharacterEnter;
        CharacterManager.Instance.OnCharacterEnter -= OnCharacterLeave;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCharacterEnter(Character cha)
    {
        CreateCharacterObject(cha);
    }
    void OnCharacterLeave(Character cha)
    {
        if (!Characters.ContainsKey(cha.entityId))
        {
            return;
        }
        if (Characters[cha.entityId] != null)
        {
            Destroy(Characters[cha.entityId]);
            this.Characters.Remove(cha.entityId);
        }
    }

    IEnumerator InitGameObjects()
    {
        foreach (var cha in CharacterManager.Instance.Characters.Values)
        {
            CreateCharacterObject(cha);
            yield return null;
        }
    }

    private void CreateCharacterObject(Character character)
    {
        Debug.Log("GameObject Created");
        if (!Characters.ContainsKey(character.entityId) || Characters[character.entityId] == null)
        {
            UnityEngine.Object obj = Resloader.Load<UnityEngine.Object>(character.Define.Resource);
            if(obj == null)
            {
                Debug.LogErrorFormat("Character[{0}] Resource[{1}] not existed.",character.Define.TID, character.Define.Resource);
                return;
            }
            GameObject go = (GameObject)Instantiate(obj, this.transform);
            go.name = "Character_" + character.Info.Id + "_" + character.Info.Name;

            go.transform.position = GameObjectTool.LogicToWorld(character.position);
            go.transform.forward = GameObjectTool.LogicToWorld(character.direction);
            Characters[character.Info.Id] = go;

            EntityController ec = go.GetComponent<EntityController>();
            if (ec != null)
            {
                ec.entity = character;
                ec.isPlayer = character.IsPlayer;
            }
            
            PlayerInputController pc = go.GetComponent<PlayerInputController>();
            if (pc != null)
            {
               
                if (character.Info.Id == Models.User.Instance.CurrentCharacter.Id)
                {
                    User.Instance.CurrentCharacterObject = go;
                    MainPlayerCamera.Instance.player = go;
                    pc.enabled = true;
                    pc.character = character;
                    pc.entityController = ec;
                }
                else
                {
                    pc.enabled = false;
                }
            }

            UIWorldElementManager.Instance.AddCharacterNameBar(go.transform, character);
        }
    }
}

