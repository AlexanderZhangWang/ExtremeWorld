using Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Managers
{
    class NPCManager : Singleton<NPCManager>
    {
        public NpcDefine GetNpcDefine(int npcID)
        {
            return DataManager.Instance.NPCs[npcID];

        }


    }
}
