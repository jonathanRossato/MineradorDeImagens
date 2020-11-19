using _2_DOMAIN.Models;
using _2_DOMAIN.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1_IMAGE_MINER
{
    public partial class MinerForm : Form
    {
        public MinerForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void IniciarBtt_Click(object sender, EventArgs e)
        {
            string nomeArquivo = this.nomeArquivoTb.Text;
            string Extensao = this.ExtensaoTb.Text;
            int contador = Convert.ToInt32(this.ContadorUD.Value);
            int multiplicador = Convert.ToInt32(this.MultiplicadorUD.Value);
            string parametroPaginacao = this.ParametroPaginacaoTb.Text;
            string pastaDestino = this.PastaDestinoTb.Text;
            bool primeiraPaginacao = this.contemPagRB.Checked?true:false;
            string urlBase = this.UrlBaseTb.Text;
            string urlPesquisa = this.UrlPesquisaTb.Text;
            string htmlSrcName = this.HtmlSrcTb.Text;

            ParametersResult parameters = new ParametersResult {
                FileNameBase = nomeArquivo,
                FirstPage = primeiraPaginacao,
                PageCount = contador,
                PageMultiplier  = multiplicador,
                PaginatorParameter = parametroPaginacao,
                PathName = pastaDestino,
                Extension = Extensao,
                UrlBase = urlBase,
                UrlSearch = urlPesquisa,
                HtmlSrcName= htmlSrcName
            };         

            BaseServices BaseS = new BaseServices();
            BaseS.ScrapyImageShutterStock(parameters);

        }
    }
}
