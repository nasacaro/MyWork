﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWork.Models.DBModels
{
    public class Sprint : Entity
    {
        public int SprintNum { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set; }
    }
}
