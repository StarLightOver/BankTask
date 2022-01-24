using System;
using System.Collections.Generic;
using System.Linq;
using BankTask.Data;
using BankTask.Dtos;
using BankTask.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankTask.Controllers
{
    [ApiController]
    [Route("api/founder")]
    public class FounderController : ControllerBase
    {
        private readonly IRepository<FounderDto, Founder> _repository;

        public FounderController(IRepository<FounderDto, Founder> repository)
        {
            _repository = repository;
        }

        [HttpPost("EditFounder")]
        public ActionResult EditFounder(FounderDto founderDto)
        {
            try
            {
                _repository.CreateOrUpdate(founderDto);
                _repository.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpGet("GetFounders")]
        public ActionResult<IEnumerable<FounderDto>> GetFounders()
        {
            var result = _repository.Get().Select(founder => new FounderDto
            {
                Id = founder.Id,
                Name = founder.Name,
                INN = founder.INN
            });
            return Ok(result);
        }

        [HttpGet("GetFounderById")]
        public ActionResult<FounderDto> GetFounderById(int id)
        {
            try
            {
                var founder = _repository.Get(id);
                return Ok(new FounderDto
                {
                    Id = founder.Id,
                    Name = founder.Name,
                    INN = founder.INN
                });
            }
            catch
            {
                return NotFound($"Учредитель с Id {id} не найден");
            }
            
        }

        [HttpDelete("DeleteFounder")]
        public ActionResult DeleteFounder(int id)
        {
            try
            {
                _repository.Delete(id);
                _repository.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            return Ok();
        }
    }
}