using _2_DOMAIN.Interfaces;
using _2_DOMAIN.Models;
using HtmlAgilityPack;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace _2_DOMAIN.Services
{
    public class BaseServices : IBaseServices
    {
        public HttpCookie _cookieJar;
        public static RestClient _client;
        public static HtmlDocument htmlDoc = new HtmlDocument();



        public MinerResult ScrapyImageShutterStock(ParametersResult parameter)
        {
            MinerResult result = new MinerResult();
            _client = new RestClient();
            _client.Timeout = -1;
            _client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.198 Safari/537.36";          
            try
            {
                RestRequest request = new RestRequest(Method.GET);
                request.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                request.AddHeader("Accept-Language", "pt-BR,pt;q=0.9,en-US;q=0.8,en;q=0.7");
                request.AddHeader("Accept-Encoding", "gzip, deflate, br");
                request.AddHeader("Connection", "keep-alive");
                string header = parameter.UrlBase.Replace("https://", string.Empty);
                request.AddHeader("Host", header);    //------>   request.AddHeader("Host", "www.shutterstock.com");


                int pageCount = parameter.PageCount;
                for (int i = 1; i < pageCount; i++)
                {

                    _client.BaseUrl = new Uri(parameter.UrlBase + "/" + parameter.UrlSearch + parameter.PaginatorParameter + i * parameter.PageMultiplier);
                    // ---------------> new Uri($"https://www.shutterstock.com/pt/search/people+with+phone?kw=%2Bbanco+%2Bimagens&image_type=photo&page={i*100}");

                    if (i == 1 && parameter.FirstPage)
                        _client.BaseUrl = new Uri(parameter.UrlBase + "/" + parameter.UrlSearch);


                    IRestResponse response = _client.Execute(request);
                    if (response.StatusCode == HttpStatusCode.OK)
                        DownloadImageShutterStock(response.Content, i, parameter);
                }

            }
            catch (Exception Ex)
            {

            }
           
            return result;
        }



        private MinerResult DownloadImageShutterStock(string Content, int i, ParametersResult parameter)
        {
            MinerResult result = new MinerResult();
            try
            {
                
                htmlDoc.LoadHtml(Content);
                HtmlNodeCollection ImageList = htmlDoc.DocumentNode.SelectNodes("//img/@" + parameter.HtmlSrcName);

                int count = 1;
                foreach (var item in ImageList)
                {
                    var tempFile = parameter.PathName + "/" + parameter.FileNameBase + i.ToString()+"_"+count.ToString() + parameter.Extension;  //--------------------> $"C:/Users/Jonathan/Documents/Fotos_projeto/FotosMineradas/fotoComCelular{i}_{count}.jpg";

                    using (FileStream writer = File.OpenWrite(tempFile))
                    {
                        RestRequest requestImg = new RestRequest(Method.GET);
                        requestImg.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                        requestImg.AddHeader("Accept-Language", "pt-BR,pt;q=0.9,en-US;q=0.8,en;q=0.7");
                        requestImg.AddHeader("Accept-Encoding", "gzip, deflate, br");
                        requestImg.AddHeader("Connection", "keep-alive");
                        //requestImg.AddHeader("Host", "https://www.image.shutterstock.com");


                        string linkDL = item.Attributes[parameter.HtmlSrcName].Value;
                        _client.BaseUrl = new Uri(linkDL);
                        requestImg.ResponseWriter = responseStream =>
                        {
                            using (responseStream)
                            {
                                responseStream.CopyTo(writer);
                            }
                        };
                        var responseImg = _client.DownloadData(requestImg);
                    }
                    count++;
                }
            }
            catch (Exception Ex)
            {
            }
            return result;
        }
    }




}
