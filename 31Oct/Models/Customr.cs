using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace _31Oct.Models
{
    public class Customr
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int custId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int addId { get; set; }
    }
}