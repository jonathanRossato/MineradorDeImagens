using System;
using System.Collections.Generic;
using System.Text;

namespace _2_DOMAIN.Models
{
    public class MinerResult
    {
        public MinerResult()
        {
            Sucess = false;
            Erro = string.Empty;
        }
        public int NrImageFound { get; set; }
        public int NrPageFound { get; set; }
        public string Erro { get; set; }
        public bool Sucess { get; set; }
    }
}
