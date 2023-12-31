﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface IBLEntity
    {
        public int Id { get; set; }

        public bool CheckConsistency();

        public bool CheckConsistencyOnDelete();
    }
}
