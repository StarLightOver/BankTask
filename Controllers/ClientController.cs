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
    [Route("api/client")]
    public class ClientController : ControllerBase
    {
        private readonly IRepository<ClientDto, Client> _repository;

        public ClientController(IRepository<ClientDto, Client> repository)
        {
            _repository = repository;
        }

        [HttpPost("EditClient")]
        public ActionResult EditClient(ClientDto clientDto)
        {
            try
            {
                _repository.CreateOrUpdate(clientDto);
                _repository.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpGet("GetClients")]
        public ActionResult<IEnumerable<ClientDto>> GetClients()
        {
            return Ok(_repository.Get().Select(client => new ClientDto
            {
                Id = client.Id,
                Name = client.Name,
                Type = client.Type,
                INN = client.INN,
                Founders = client.Founders?.Select(x => x.Id).ToList()
            }));
        }

        [HttpGet("GetClientById")]
        public ActionResult<ClientDto> GetClientById(int id)
        {
            try
            {
                var client = _repository.Get(id);
                return Ok(new ClientDto
                {
                    Id = client.Id,
                    Name = client.Name,
                    Type = client.Type,
                    INN = client.INN,
                    Founders = client.Founders?.Select(x => x.Id).ToList()
                });
            }
            catch
            {
                return NotFound($"Клиент с Id {id} не найден");
            }
        }

        [HttpDelete("DeleteClient")]
        public ActionResult DeleteClient(int id)
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