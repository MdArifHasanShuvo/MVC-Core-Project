using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Evidance_Works.Models
{
    public class Customer
    {
        public Customer()
        {
            this.Solutions = new List<Solution>();
        }
        public int CustomerId { get; set; }
        [Required, StringLength(50), Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        [Required, StringLength(50)]
        public string Phone { get; set; }

        public virtual ICollection<Solution> Solutions { get; set; }
    }
    public class Solution
    {
        public int SolutionId { get; set; }
        [Required, StringLength(50), Display(Name = "Servive Center Name")]
        public string servicePointName { get; set; }
        [Required, DataType(DataType.Date), Display(Name = "Solution Date")]
        public DateTime SolutionDate { get; set; }
        [Required, StringLength(50), Display(Name = "Servive Categoty")]
        public string serviceCategory { get; set; }
        [Required, StringLength(150)]
        public string Picture { get; set; }
        [Required, ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

    }
    public class SolutionDbContext : DbContext
    {
        public SolutionDbContext(DbContextOptions<SolutionDbContext> options) : base(options) { }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Solution> Solutions { get; set; }

    }
}
