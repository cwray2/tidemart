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
    public class TimesheetController : ControllerBase
    {
        ITimesheetDataHandler handlerOfData = new TimesheetDataHandler();
        // GET: api/Timesheet
        [EnableCors("OpenPolicy")]
        [HttpGet]
        public List<Timesheet> Get()
        {
            return handlerOfData.Select();
        }

        // GET: api/Timesheet/5
        [EnableCors("OpenPolicy")]
        [HttpGet("{id}", Name = "GetTimesheet")]
        public List<Timesheet> Get(int id)
        {
            return handlerOfData.SelectById(id);
        }

        // POST: api/Timesheet
        [EnableCors("OpenPolicy")]
        [HttpPost]
        public void Post([FromBody] Timesheet timesheet)
        {
            handlerOfData.Insert(timesheet);
        }

        // PUT: api/Timesheet/5
        [EnableCors("OpenPolicy")]
        [HttpPut("{id}")]
        public void Put([FromBody] Timesheet timesheet)
        {
            handlerOfData.Update(timesheet);
        }

        // DELETE: api/Timesheet/5
        [EnableCors("OpenPolicy")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            handlerOfData.Delete(id);
        }
    }
}
