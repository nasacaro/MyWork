using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWork.Models.DBModels
{
    public class Backlog : Entity
    {
        public string BacklogTitle { get; set; }

        public string BacklogProject { get; set; }

        public int Status { get; set; }

        public int Priority { get; set; }

        public int EstimateHour { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public int Sprint { get; set; }
    }
}
