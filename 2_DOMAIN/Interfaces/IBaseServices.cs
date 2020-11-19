using _2_DOMAIN.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace _2_DOMAIN.Interfaces
{
    public interface IBaseServices
    {
        MinerResult ScrapyImageShutterStock(ParametersResult parameter);
    }
}
