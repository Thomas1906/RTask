using System.Text;
using System.Collections.Generic;
using System.Linq;
using RTask.Models;

namespace RTask.Services
{
    public class InputMaterialsService : IInputMaterialsService
    {
        public List<Material> CreateMaterialList(string[] inputLines)
        {
            var materialList = new List<Material>();

            foreach (var inputLine in inputLines)
            {
                if (inputLine.TrimStart()[0].Equals('#')) continue;

                var splitLine = inputLine.Split(";");
                var splitWarehouses = splitLine[2].Split("|");

                var warehouses = new List<Warehouse>();
                foreach (var splitWarehouse in splitWarehouses)
                {
                    var splittedWarehouse = splitWarehouse.Split(",");

                    var warehouse = new Warehouse
                    {
                        Name = splittedWarehouse[0],
                        Count = int.Parse(splittedWarehouse[1])
                    };
                    warehouses.Add(warehouse);
                }

                var material = new Material
                {
                    Name = splitLine[0],
                    Id = splitLine[1],
                    Warehouses = warehouses
                };

                materialList.Add(material);
            }

            return materialList;
        }

        public string CreateOutputList(List<Material> materialList)
        {
            var stringBuilder = new StringBuilder();
            var warehouses = new List<Warehouse>();
            
            foreach (var material in materialList)
            {
                var materialWarehouses = from mw in material.Warehouses select new { mw.Name, mw.Count };
                warehouses.AddRange(materialWarehouses.Select(materialWarehouse => new Warehouse { Name = materialWarehouse.Name, Count = materialWarehouse.Count }));
            }

            var sumWarehouses = from o in (from w in warehouses group w by new {w.Name} into g select new {g.Key.Name, Count = g.Sum(s => s.Count)}) orderby o.Count descending, o.Name descending select new { o.Name, o.Count };

            foreach (var sumWarehouse in sumWarehouses)
            {
                stringBuilder.AppendFormat("{0} (total {1})", sumWarehouse.Name, sumWarehouse.Count);
                stringBuilder.AppendLine();

                foreach (var material in materialList.OrderBy(ob => ob.Id))
                {
                    foreach (var warehouse in material.Warehouses.Where(warehouse => warehouse.Name.Equals(sumWarehouse.Name)))
                    {
                        stringBuilder.AppendFormat("{0}: {1}", material.Id, warehouse.Count);
                        stringBuilder.AppendLine();
                    }
                }

                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }
    }
}
