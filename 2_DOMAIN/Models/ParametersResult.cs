using System;
using System.Collections.Generic;
using System.Text;

namespace _2_DOMAIN.Models
{
    public class ParametersResult
    {
        public string PathName { get; set; }
        public string FileNameParameter { get; set; }
        public int PageCount { get; set; }
        public int PageMultiplier { get; set; }
        public string FileNameBase { get; set; }
        public bool FirstPage { get; set; }
        public string UrlSearch { get; set; }
        public string UrlBase { get; set; }
        public string PaginatorParameter { get; set; }

        public string  Extension { get; set; }
        public string HtmlSrcName { get; set; }

    }
}
