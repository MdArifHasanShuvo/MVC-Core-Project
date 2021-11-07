using Evidance_Works.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Evidance_Works.ViewModels
{
    public class SolutionViewVM
    {
        public int SolutionId { get; set; }
        [Required, StringLength(50), Display(Name = "Servive Center Name")]
        public string servicePointName { get; set; }
        [Required, DataType(DataType.Date), Display(Name = "Solution Date")]
        public DateTime SolutionDate { get; set; }
        [Required, StringLength(50), Display(Name = "Servive Categoty")]
        public string serviceCategory { get; set; }
        public IFormFile Picture { get; set; }
        [Required, ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
