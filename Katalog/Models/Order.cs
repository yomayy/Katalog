using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katalog.Models
{
    class Order
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int GoodsId { get; set; }
        public DateTime OrdData { get; set; }
        public int StatusId { get; set; }
    }
}
