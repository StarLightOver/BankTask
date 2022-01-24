using System.Collections.Generic;
using BankTask.Models;

namespace BankTask.Dtos
{
    public class ClientDto
    {
        public int? Id { set; get; }
        
        public long INN { set; get; }
        
        public string Name { set; get; }
        
        public ClientType Type { set; get; }
        
        public List<int> Founders { set; get; }
    }
}