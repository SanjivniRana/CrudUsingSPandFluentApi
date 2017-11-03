using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _31Oct.Models
{
    public class CState
    {
        [Key]
        public int StateId { get; set; }
        public string StateName { get; set; }
    }
}