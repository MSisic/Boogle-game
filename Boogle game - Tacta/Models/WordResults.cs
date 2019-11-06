using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Boogle_game___Tacta.Models
{
    public class WordResults
    {
        public class Result
        {
            public string Word { get; set; }
            public string Description { get; set; }
            public int Points { get; set; }
        }
        public string Player { get;set;}
        public int TotalPoints { get; set; }
        public List<Result> Results { get; set; }
       
     
    }
}