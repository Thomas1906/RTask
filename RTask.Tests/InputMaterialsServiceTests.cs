using System.Collections.Generic;
using NUnit.Framework;
using RTask.Models;
using RTask.Services;

namespace RTask.Tests
{
    public class InputMaterialsServiceTests
    {
        private IInputMaterialsService _inputMaterialsService;
        private readonly string[] _inputLines = {
            "# Material inventory initial state as of Jan 01 2018",
            "# New materials",
            "Cherry Hardwood Arched Door - PS;COM-100001;WH-A,5|WH-B,10",
            "Maple Dovetail Drawerbox;COM-124047;WH-A,15",
            "Generic Wire Pull;COM-123906c;WH-A,10|WH-B,6|WH-C,2",
            "Yankee Hardware 110 Deg. Hinge;COM-123908;WH-A,10|WH-B,11",
            "# Existing materials, restocked",
            "Hdw Accuride CB0115-CASSRC - Locking Handle Kit - Black;CB0115-CASSRC;WH-C,13|WH-B,5",
            "Veneer - Charter Industries - 3M Adhesive Backed - Cherry 10mm - Paper Back;3M-Cherry-10mm;WH-A,10|WH-B,1",
            "Veneer - Cherry Rotary 1 FSC;COM-123823;WH-C,10",
            "MDF, CARB2, 1 1/8\";COM-101734;WH-C,8"
        };
        private readonly List<Material> _materialList = new List<Material>
        {
            new Material { Id = "COM-100001", Name = "Cherry Hardwood Arched Door - PS", Warehouses = new List<Warehouse> { new Warehouse { Name = "WH-A", Count = 5 }, new Warehouse { Name = "WH-B", Count = 10 } } },
            new Material { Id = "COM-124047", Name = "Maple Dovetail Drawerbox", Warehouses = new List<Warehouse> { new Warehouse { Name = "WH-A", Count = 15 } } },
            new Material { Id = "COM-123906c", Name = "Generic Wire Pull", Warehouses = new List<Warehouse> { new Warehouse { Name = "WH-A", Count = 10 }, new Warehouse { Name = "WH-B", Count = 6 }, new Warehouse { Name = "WH-C", Count = 2 } } },
            new Material { Id = "COM-123908", Name = "Yankee Hardware 110 Deg. Hinge", Warehouses = new List<Warehouse> { new Warehouse { Name = "WH-A", Count = 10 }, new Warehouse { Name = "WH-B", Count = 11 } } },
            new Material { Id = "CB0115-CASSRC", Name = "Hdw Accuride CB0115-CASSRC - Locking Handle Kit - Black", Warehouses = new List<Warehouse> { new Warehouse { Name = "WH-C", Count = 13 }, new Warehouse { Name = "WH-B", Count = 5 } } },
            new Material { Id = "3M-Cherry-10mm", Name = "Veneer - Charter Industries - 3M Adhesive Backed - Cherry 10mm - Paper Back", Warehouses = new List<Warehouse> { new Warehouse { Name = "WH-A", Count = 10 }, new Warehouse { Name = "WH-B", Count = 1 } } },
            new Material { Id = "COM-123823", Name = "Veneer - Cherry Rotary 1 FSC", Warehouses = new List<Warehouse> { new Warehouse { Name = "WH-C", Count = 10 } } },
            new Material { Id = "COM-101734", Name = "MDF, CARB2, 1 1/8\"", Warehouses = new List<Warehouse> { new Warehouse { Name = "WH-C", Count = 8 } } }
        };
        private const string OutputString =
            "WH-A (total 50)\r\n3M-Cherry-10mm: 10\r\nCOM-100001: 5\r\nCOM-123906c: 10\r\nCOM-123908: 10\r\nCOM-124047: 15\r\n\r\n"+
            "WH-C (total 33)\r\nCB0115-CASSRC: 13\r\nCOM-101734: 8\r\nCOM-123823: 10\r\nCOM-123906c: 2\r\n\r\n"+
            "WH-B (total 33)\r\n3M-Cherry-10mm: 1\r\nCB0115-CASSRC: 5\r\nCOM-100001: 10\r\nCOM-123906c: 6\r\nCOM-123908: 11\r\n\r\n";

        [SetUp]
        public void Setup()
        {
            _inputMaterialsService = new InputMaterialsService();
        }

        [Test]
        public void CreateMaterialList_EmptyInputLines_ShouldReturnEmptyList_Test()
        {
            var list = _inputMaterialsService.CreateMaterialList(new string[]{});
            Assert.IsEmpty(list);
        }

        [Test]
        public void CreateMaterialList_InputLines_ShouldReturnNonEmptyList_Test()
        {
            var list = _inputMaterialsService.CreateMaterialList(_inputLines);
            Assert.IsNotEmpty(list);
        }

        [Test]
        public void CreateMaterialList_InputLines_ShouldReturnListOf8Elements_Test()
        {
            var list = _inputMaterialsService.CreateMaterialList(_inputLines);
            Assert.AreEqual(8, list.Count);
        }

        [Test]
        public void CreateOutputList_EmptyList_ShouldReturnEmptyString_Test()
        {
            var outputString = _inputMaterialsService.CreateOutputList(new List<Material>());
            Assert.AreEqual("", outputString);
        }

        [Test]
        public void CreateOutputList_FullList_ShouldReturnFullString_Test()
        {
            var outputString = _inputMaterialsService.CreateOutputList(_materialList);
            Assert.AreEqual(OutputString, outputString);
        }
    }
}
