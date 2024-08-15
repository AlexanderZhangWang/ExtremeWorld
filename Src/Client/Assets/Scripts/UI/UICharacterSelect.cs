using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SkillBridge.Message;
using Models;
using Services;

public class UICharacterSelect : MonoBehaviour {

    public GameObject panelCreate;
    public GameObject panelSelect;

    public GameObject btnCreateCancel;

    public InputField charName;
    CharacterClass charClass;

    public Transform uiCharList;
    public GameObject uiCharInfo;

    public List<GameObject> uiChars = new List<GameObject>();
    private int selectCharacterIdx = -1;

    public UICharacterView characterView;

    public Image[] titles;
    public Text desc;
    public Button[] buttons;
    public Sprite[] selectedSprites;
     
	void Start () {
        //DataManager.Instance.Load();
        buttons[0].image.overrideSprite = selectedSprites[0];
        InitCharacterSelect(true);
        UserService.Instance.OnCharacterCreate = OnCharacterCreate;
	}

    public void InitCharacterSelect(bool init)
    {
        panelCreate.SetActive(false);
        panelSelect.SetActive(true);
        if (init)
        {
            foreach (var old in uiChars)
            {
                Destroy(old);
            }
            uiChars.Clear();
            
            for (int i = 0; i < User.Instance.Info.Player.Characters.Count; i++)
            {
                GameObject go = Instantiate(uiCharInfo, this.uiCharList);
                UICharInfo chrInfo = go.GetComponent<UICharInfo>();
                chrInfo.info = User.Instance.Info.Player.Characters[i];


                Button button = go.GetComponent<Button>();
                int idx = i;
                button.onClick.AddListener(() => {
                    OnSelectCharacter(idx);
                });
                uiChars.Add(go);
                go.SetActive(true);
            }
        }
        int ind = 0;
        for (int i = 0; i < User.Instance.Info.Player.Characters.Count; i++)
        {
            UICharInfo ci = this.uiChars[i].GetComponent<UICharInfo>();
            if (ci.Selected)
            {
                ind = i;
            }
        }
        characterView.CurrectCharacter = ind;

    }
	public void OnSelectClass(int charClass)
    {
        this.charClass = (CharacterClass)charClass;
        characterView.CurrectCharacter = charClass - 1;
        for (int i = 0; i < 3; i++)
        {
            titles[i].gameObject.SetActive(i == charClass - 1);
        }
        desc.text = DataManager.Instance.Characters[charClass].Description;
        UpdateUIs(charClass-1);
    }
    void UpdateUIs(int n)
    {
        for (int i = 0; i < 3; i++)
        {
            buttons[i].image.overrideSprite = i == n ? selectedSprites[i] : null;
        }
    }
    void OnCharacterCreate (Result result, string message)
    {
        if (result == Result.Success)
        {
            InitCharacterSelect(true);
        }
        else
            MessageBox.Show(message, "错误", MessageBoxType.Error);
    }
    public void OnSelectCharacter(int idx)
    {
        this.selectCharacterIdx = idx;
        var cha = User.Instance.Info.Player.Characters[idx];
        Debug.LogFormat("Select Char:[{0}]{1}[{2}]", cha.Id, cha.Name, cha.Class);
        User.Instance.CurrentCharacter = cha;
        characterView.CurrectCharacter = (int)User.Instance.Info.Player.Characters[idx].Class-1;

        for (int i = 0; i < User.Instance.Info.Player.Characters.Count; i++)
        {
            UICharInfo ci = this.uiChars[i].GetComponent<UICharInfo>();
            ci.Selected = idx == i;
        }

    }
    public void OnClickPlay()
    {
        if (this.selectCharacterIdx >= 0)
        {
            UserService.Instance.SendGameEnter(selectCharacterIdx);
        }
    }
    public void OnClickCreate()
    {
        if (string.IsNullOrEmpty(this.charName.text))
        {
            MessageBox.Show("请输入角色名称");
            return;
        }
        UserService.Instance.SendCharacterCreate(this.charName.text, this.charClass);
    }
	void Update () {
		
	}
    
}
