using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Managers
{
    class MinimapManager : Singleton<MinimapManager>
    {
        public Sprite LoadCurrentMinimap()
        {
            Debug.Log("--- load res" + User.Instance.CurrentMapData.MiniMap);
            return Resloader.Load<Sprite>("UI/Minimap/" + User.Instance.CurrentMapData.MiniMap);

        }
    }
}
