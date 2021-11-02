using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace vericekme.Models
{
    public class ProductAddress
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Path { get; set; }
        public bool State { get; set; } = true;
        public int Source { get; set; } = 0;
    }
}
