using System;
using System.Collections.Generic;

namespace HackKU2019.Models
{
    public class ResultsModel
    {
        public List<Tweets> Tweets { get; set; }
        public MainUser User { get; set; }
        public string Error { get; set; }
        public string Sorting { get; set; }
    }
}