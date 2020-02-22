using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMdb_Film_Searcher
{
    public class SearchResult
    {
        public Search[] Search { get; set; }
        public string totalResults { get; set; }
        public string Response { get; set; }
    }
}
