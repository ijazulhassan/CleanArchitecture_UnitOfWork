using Application;
using Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitofwork)
        {
            _unitOfWork = unitofwork;

        }
        // GET: api/<Products>
        [HttpGet]
        public IEnumerable<Products> GetAll()
        {
            var data = _unitOfWork.ProductRepository.GetAll();
            return data;
        }

        // GET api/<Product>/id
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
           var affect =  _unitOfWork.ProductRepository.Get(id);
            return Ok(affect);
        }

        // POST api/<Products>
        [HttpPost]
        public IActionResult Post([FromBody] Products entity)
        {
            var data = _unitOfWork.ProductRepository.Add(entity);
            _unitOfWork.Commit();
            return Ok(data);
        }

        // PUT api/<Products>/id
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Products entity)
        {
            var data = _unitOfWork.ProductRepository.Update(entity, id);
            _unitOfWork.Commit();
                return Ok(data);
         }

        // DELETE api/<Products>/id
        [HttpDelete("{id}")]
       public IActionResult Delete(int id)
        {
            var affect = _unitOfWork.ProductRepository.Delete(id);
            _unitOfWork.Commit();
            return Ok(affect);
                
        }
    }
}
