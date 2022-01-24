using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankTask.Models
{
    public class Founder
    {
        [Key]
        public int Id { set; get; }
        
        [Required]
        public long INN { set; get; }
        
        [Required]
        public string Name { set; get; }
        
        [Required]
        public DateTime DateCreate { set; get; }
        
        [Required]
        public DateTime DateUpdate { set; get; }
        
        public IEnumerable<Client> Clients { get; set; }
    }
}