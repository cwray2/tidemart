using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Database;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        IDepartmentDatahandler handlerOfData = new DepartmentDataHandler();

        // GET: api/Department
        [EnableCors("OpenPolicy")]
        [HttpGet]
        public List<Department> Get()
        {
            return handlerOfData.Select();
        }

        // GET: api/Department/5
        [EnableCors("OpenPolicy")]
        [HttpGet("{id}", Name = "Get")]
        public List<Department> Get(int id)
        {
            return handlerOfData.SelectById(id);
        }

        // POST: api/Department
        [EnableCors("OpenPolicy")]
        [HttpPost]
        public void Post([FromBody] Department department)
        {
            handlerOfData.Insert(department);
        }

        // PUT: api/Department/5
        [EnableCors("OpenPolicy")]
        [HttpPut("{id}")]
        public void Put([FromBody] Department department)
        {
            handlerOfData.Update(department);
        }

        // DELETE: api/Department/5
        [EnableCors("OpenPolicy")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            handlerOfData.Delete(id);
        }
    }
}
