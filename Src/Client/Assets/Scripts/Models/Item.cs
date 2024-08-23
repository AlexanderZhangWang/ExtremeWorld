﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillBridge.Message;

namespace Models
{
    

    public class Item
    {
        public int Id;
        public int Count;

        public Item(NItemInfo item)
        {
            this.Id = item.Id;
            this.Count = item.Count;
        }

        public override string ToString()
        {
            return string.Format("Id:{0},Count:{1}", this.Id, this.Count);

        }

    }
}