using System;
using System.Collections.Generic;
using System.Text;

namespace _2_DOMAIN.Models
{
    public class ImageResult
    {
        public string SourcePage { get; set; }
        public string DisplayName { get; set; }
        public string ParentalText { get; set; }
        public byte[] bytes { get; set; }
    }
}
