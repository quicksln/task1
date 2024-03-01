using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QS.Task2.Infrastructure.ViewModels
{
    public class ChartItem
    {
        public string? Location { get; set; }
        public double? Value { get; set; }

        public DateTime? LastUpdateTime { get; set; }
    }
}
