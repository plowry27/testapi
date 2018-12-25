using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Contacts;


namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {       
            Contact con = new Contact() {firstname = "Elizabeth", lastname="Lowry"};
            var info = con.GetContactInfo();
            return new string[] { info.firstname, info.lastname };
        }

        [HttpGet("GetContactInfo/{fn}/{ln}")]
        public ActionResult<IEnumerable<string>> GetContactInfo(string fn, string ln)
        {       
            Contact con = new Contact() {firstname = fn, lastname=ln};
            var info = con.GetContactInfo();
            return new string[] { info.firstname, info.lastname, info.address, info.city };
        }

        // GET api/values/5
        [HttpGet("{fn}/{ln}")]
        public ActionResult<Contact> Get(string fn, string ln)
        {
            Contact con = new Contact() {firstname = fn, lastname=ln};
            var info = con.GetContactInfo();
            return info;
        }

        // POST api/values
        [HttpPost]
        public string Post([FromBody] Contact contact)
        {
            var retcode = contact.AddContactInfo();
            return retcode;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
