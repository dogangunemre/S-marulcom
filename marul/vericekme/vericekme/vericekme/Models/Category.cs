using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace vericekme.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public int? ParentCategoryID { get; set; }
        public Category ParentCategory { get; set; }
        public string Description { get; set; }
        public bool State { get; set; }
        public ICollection<Product> Products { get; set; }
        public string Address { get; set; }
        public int Source { get; set; } = 0;
    }
}
