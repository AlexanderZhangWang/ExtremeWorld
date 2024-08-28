using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class UIManager : Singleton<UIManager>
{
    class UIElement
    {
        public string Resources;
        public bool Cache;
        public GameObject Instance;
    }

    private Dictionary<Type, UIElement> UIResource = new Dictionary<Type, UIElement>();

    public UIManager()
    {
        isUIOpen = false;
        this.UIResource.Add(typeof(UITest), new UIElement() { Resources = "UI/UITest", Cache = true });
        this.UIResource.Add(typeof(UIBag), new UIElement() { Resources = "UI/UIBag", Cache = false });
        this.UIResource.Add(typeof(UIShop), new UIElement() { Resources = "UI/UIShop", Cache = false });
    }

    //UIManager()
    //{
    //}

    public bool isUIOpen { get; set; }

    public T Show<T>()
    {
        isUIOpen = true;
        //SoundManager.Instance.PlaySound("ui_open");
        Type type = typeof(T);
        if (this.UIResource.ContainsKey(type))
        {
            UIElement info = this.UIResource[type];
            if (info.Instance != null)
            {
                info.Instance.SetActive(true);
            }
            else
            {
                UnityEngine.Object prefab = Resources.Load(info.Resources);
                if (prefab == null)
                {
                    return default(T);
                }
                info.Instance = (GameObject)GameObject.Instantiate(prefab);
            }
            return info.Instance.GetComponent<T>();
        }
        return default(T);
    }

    public void Close(Type type)
    {
        isUIOpen = false;
        //SoundManager.Instance.PlaySound("ui_close");
        if (this.UIResource.ContainsKey(type))
        {
            UIElement info = this.UIResource[type];
            if (info.Cache)
            {
                info.Instance.SetActive(false);
            }
            else
            {
                GameObject.Destroy(info.Instance);
                info.Instance = null;
            }
        }
    }
}

