using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWork.Models.DBModels
{
    public class Backlog : Entity
    {
        public string BacklogName { get; set; }

        public int Status { get; set; }

        public int Priority { get; set; }

        public int EstimateHour { get; set; }

    }
}
