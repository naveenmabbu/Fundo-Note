using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Model
{
    public class ResetPass
    {
        public string Password { get; set; }
        public string ConformPassword { get; set; }
    }
}
