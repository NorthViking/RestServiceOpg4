using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BogLibrary.Model;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace RestServiceOpg4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BogController : ControllerBase
    {
        private static readonly List<Bog> bøger = new List<Bog>()
        {
            new Bog("Mester Jakob","Paul",323,"1234567890123"),
            new Bog("Robin Hood","Karl", 423,"2345678901234"),
            new Bog("David","Caspar", 163,"3456789012345"),
            new Bog("Lord of the Rings","Talia", 247,"4567890123456"),

        };
        // GET: api/Bog
        [HttpGet]
        public IEnumerable<Bog> Get()
        {
            return bøger;
        }

        // GET: api/Bog/5
        [HttpGet]
        [Route("isbn13/{substring}")]
        public Bog GetBog(string substring)
        {
            return bøger.Find(i=>i.Isbn13.Equals(substring));
        }

        [HttpGet]
        [Route("forfatter/{substring}")]
        public IEnumerable<Bog> GetForfatter(string substring)
        {
            return bøger.FindAll(i => i.Isbn13.Contains(substring));
        }

        [HttpGet]
        [Route("titel/{substring}")]
        public IEnumerable<Bog> GetTitel(string substring)
        {
            return bøger.FindAll(i => i.Titel.Contains(substring));
        }
        // POST: api/Bog
        [HttpPost]
        public void Post([FromBody] Bog value)
        {
            bøger.Add(value);
        }

        // PUT: api/Bog/5
        [HttpPost]
        [Route("{isbn13}")]
        public void Put(string isbn13, [FromBody] Bog value)
        {
            Bog bog = GetBog(isbn13);
            if (bog != null)
            {
                bog.Titel = value.Titel;
                bog.Forfatter = value.Forfatter;
                bog.Sidetal = value.Sidetal;
                bog.Isbn13 = value.Isbn13;
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{isbn13}")]
        public void Delete(string isbn13)
        {
            Bog bog = GetBog(isbn13);
            bøger.Remove(bog);

        }
    }
}
