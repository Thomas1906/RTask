using System.Collections.Generic;
using RTask.Models;

namespace RTask.Services
{
    public interface IInputMaterialsService
    {
        List<Material> CreateMaterialList(string[] inputLines);
        string CreateOutputList(List<Material> materialList);
    }
}
