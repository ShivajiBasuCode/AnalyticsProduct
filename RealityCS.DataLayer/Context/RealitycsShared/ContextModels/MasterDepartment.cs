﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RealityCS.DataLayer.Context.RealitycsShared.ContextModels
{
    public class MasterDepartment:RealitycsSharedBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}