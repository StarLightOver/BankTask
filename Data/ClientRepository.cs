using System;
using System.Collections.Generic;
using System.Linq;
using BankTask.Dtos;
using BankTask.Extensions;
using BankTask.Models;
using Microsoft.EntityFrameworkCore;

namespace BankTask.Data
{
    public class ClientRepository : Repository<ClientDto, Client>
    {
        public ClientRepository(AppDbContext context) : base(context)
        {
        }
        
        public override void CreateOrUpdate(ClientDto clientDto)
        {
            if (clientDto == null)
                throw new NullReferenceException(nameof(clientDto));
            
            if (clientDto.Id.HasValue)
            {
                var client = Context.Clients
                    .Include(x=> x.Founders)
                    .FirstOrDefault(x => x.Id == clientDto.Id);

                if (client == null)
                    throw new NullReferenceException($"Клиент с Id={clientDto.Id} не найден!");
                
                if(Context.Clients
                    .FirstOrDefault(x => x.Id != client.Id && x.INN == clientDto.INN) != null) 
                    throw new NullReferenceException($"Клиент с ИНН = {clientDto.INN} уже существует!");

                client.INN = clientDto.INN;
                client.Name = clientDto.Name;
                client.Type = clientDto.Type;
                client.DateUpdate = DateTime.Now;

                client.Founders = clientDto.Founders.IsNullOrEmpty() 
                    ? null 
                    : Context.Founders.Where(x => clientDto.Founders.Contains(x.Id)).ToList();
            }
            else
            {
                if(Context.Clients
                    .FirstOrDefault(x => x.INN == clientDto.INN) != null) 
                    throw new NullReferenceException($"Клиент с ИНН = {clientDto.INN} уже существует!");
                
                var client = new Client()
                {
                    INN = clientDto.INN,
                    Name = clientDto.Name,
                    Type = clientDto.Type,
                    DateCreate = DateTime.Now,
                    DateUpdate = DateTime.Now,
                    Founders = Context.Founders.Where(x => clientDto.Founders.Contains(x.Id)).ToList(),
                };
                
                Context.Clients.Add(client);
            }
        }

        public override IEnumerable<Client> Get()
        {
            return Context.Clients
                .Include(x=>x.Founders)
                .ToList();
        }

        public override Client Get(int id)
        {
            var client = Context.Clients
                .Include(x=>x.Founders)
                .FirstOrDefault(cl => cl.Id == id);

            if (client == null)
                throw new ArgumentNullException(nameof(client));
            
            return client;
        }

        public override void Delete(int id)
        {
            var client = Context.Clients.Find(id);

            if (client == null)
                throw new ArgumentNullException(nameof(client));

            Context.Clients.Remove(client);
        }
    }
}