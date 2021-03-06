﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edi.Models.Shared;

namespace Edi.Models.PurchaseOrderModels
{
    public class PoItemName : Name
    {
        public PoItemName()
        {
            Refs = new List<PoItemNameRef>();           
        }

        public int PoItemID { get; set; }
        public virtual PoItem Item { get; set; }
        public virtual List<PoItemNameRef> Refs { get; set; }
    }
}
