using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPTCOM.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public decimal Price { get; set; }
    }
}