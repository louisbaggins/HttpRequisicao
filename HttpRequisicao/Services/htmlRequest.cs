using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;

namespace HttpRequisicao.Services
{
    public class RequisicaoHTML
    {
       static WebClient htmlRequest = new WebClient();

        public string InfoHolder { get; set; }

        public RequisicaoHTML()
        {
            InfoHolder = htmlRequest.DownloadString("https://take.net/vaga/estagiario-de-desenvolvimento/");
        }

        public RequisicaoHTML(string parameter)
        {
            InfoHolder = parameter;
        }

        public string ExtractText()
        {
            if (this.InfoHolder == null)
            {
                throw new ArgumentNullException("html");
            }

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(this.InfoHolder);

            var chunks = new List<string>();

#pragma warning disable CS0618 // Type or member is obsolete
            foreach (var item in doc.DocumentNode.DescendantNodesAndSelf())
#pragma warning restore CS0618 // Type or member is obsolete
            {
                if (item.NodeType == HtmlNodeType.Text)
                {
                    if (item.InnerText.Trim() != "")
                    {
                        chunks.Add(item.InnerText.Trim());
                    }
                }
            }
            return RxMaker(String.Join(" ", chunks));
        }

        public string RxMaker(string parameter)
        {
            Regex melodyMaker = new Regex(@"O que esperamos dessa pessoa(.*?)Perfil", RegexOptions.Singleline);

            //string test = melodyMaker.Value ;

            if (melodyMaker.IsMatch(parameter))
            {
                var matches = melodyMaker.Matches(parameter);
                var matchesNumber = matches.Count;
                for(int count = 0; count< matchesNumber; count++)
                {
                    this.InfoHolder = matches[count].Value;
                    this.InfoHolder = InfoHolder.Replace("Perfil", string.Empty);
                    var resultPlace = new RequisicaoHTML(this.InfoHolder);
                    return resultPlace.InfoHolder;
                }
            }



            return this.InfoHolder;
            
        }

    }

  
}
 