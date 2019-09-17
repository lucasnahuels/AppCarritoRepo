using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Model;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiServices.Controllers
{
    [Route("api/v{version:apiVersion}/bill")]
    [ApiVersion("1.0")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly DataBaseContext _dbContext; // convencion private _

        public BillController(DataBaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Bill>> Get()
        {
            var billList = _dbContext.Bills.ToList();

            var billListMapped = _mapper.Map<List<BillViewModel>>(billList);

            return Ok(billListMapped);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Bill> Get(int id)
        {
            var bill = _dbContext.Set<Bill>().Find(id);

            if (bill == null) return NotFound();

            var billMapped = _mapper.Map<BillViewModel>(bill);

            return Ok(billMapped); 
        } 

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] BillViewModel vm)
        {
            var billMapped = _mapper.Map<Bill>(vm);

            _dbContext.Bills.Add(billMapped);

            _dbContext.SaveChanges(); //0, 1

            return Created("Get", vm);
        }
        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult Put([FromBody] BillViewModel vm, int id) //debe llamarsre igual que la ruta
        {
            var billMapped = _mapper.Map<Bill>(vm);

            billMapped.BillId = id;

            if (_dbContext.Entry(billMapped).State == EntityState.Detached)
            {
                _dbContext.Bills.Attach(billMapped);

                _dbContext.Entry(billMapped).State = EntityState.Modified;
            }

            _dbContext.SaveChanges();

            return Ok(billMapped);
        }


        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Bill bill = _dbContext.Set<Bill>().Find(id);

            if (bill == null) return NotFound();

            _dbContext.Bills.Remove(bill);

            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}
