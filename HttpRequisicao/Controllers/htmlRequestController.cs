using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HttpRequisicao.Services;
using HtmlAgilityPack;

namespace HttpRequisicao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class htmlRequestController : ControllerBase
    {

        RequisicaoHTML takeBack = new RequisicaoHTML();
        [HttpGet]
        
        public string getHTML()
        {
            
            return takeBack.ExtractText();
        }
        
    }
} 