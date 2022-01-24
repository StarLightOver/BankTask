using System;
using System.Collections.Generic;
using System.Linq;
using BankTask.Dtos;
using BankTask.Models;

namespace BankTask.Data
{
    public class FounderRepository : Repository<FounderDto, Founder>
    {
        public FounderRepository(AppDbContext context) : base(context)
        {
        }
        
        public override void CreateOrUpdate(FounderDto founderDto)
        {
            if (founderDto == null)
                throw new NullReferenceException(nameof(founderDto));
            
            if (founderDto.Id.HasValue)
            {
                var founder = Context.Founders.Find(founderDto.Id);

                if (founder == null)
                    throw new NullReferenceException($"Клиент с {founder.Id} не найден!");
                
                if(Context.Founders
                    .FirstOrDefault(x => x.Id != founder.Id && x.INN == founderDto.INN) != null) 
                    throw new NullReferenceException($"Учредитель с ИНН = {founderDto.INN} уже существует!");

                founder.INN = founderDto.INN;
                founder.Name = founderDto.Name;
                founder.DateUpdate = DateTime.Now;
            }
            else
            {
                if(Context.Founders
                    .FirstOrDefault(x => x.INN == founderDto.INN) != null) 
                    throw new NullReferenceException($"Учредитель с ИНН = {founderDto.INN} уже существует!");
                
                var founder = new Founder()
                {
                    INN = founderDto.INN,
                    Name = founderDto.Name,
                    DateCreate = DateTime.Now,
                    DateUpdate = DateTime.Now,
                };
                
                Context.Founders.Add(founder);
            }
        }

        public override IEnumerable<Founder> Get()
        {
            return Context.Founders.ToList();
        }

        public override Founder Get(int id)
        {
            var founder = Context.Founders.Find(id);

            if (founder == null)
                throw new ArgumentNullException(nameof(founder));
            
            return founder;
        }
        
        public override void Delete(int id)
        {
            var founder = Context.Founders.Find(id);

            if (founder == null)
                throw new ArgumentNullException(nameof(founder));

            Context.Founders.Remove(founder);
        }
    }
}