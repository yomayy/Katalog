﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katalog.Models
{
    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string ProducerName { get; set; }
        public decimal Price { get; set; }
        public int Number { get; set; }
    }
}
