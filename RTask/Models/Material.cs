using System.Collections.Generic;

namespace RTask.Models
{
    public class Material
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public List<Warehouse> Warehouses { get; set; }
    }
}
