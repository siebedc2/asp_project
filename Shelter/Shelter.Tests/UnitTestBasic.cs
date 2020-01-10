using NUnit.Framework;
using ShelterMvc.Controllers;
using Moq;
using Shelter.MVC;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shelter;

namespace Shelter.Tests
{
    public class Tests
    {
        private Mock<IShelterDataAccess> _mockedDataAccess;
        private Mock<ILogger<ShelterApiController>> _mockedLogger;
        private ShelterApiController _controller;


        [SetUp]
        public void Setup()
        {
            _mockedDataAccess = new Mock<IShelterDataAccess>(MockBehavior.Strict);
            _mockedLogger = new Mock<ILogger<ShelterApiController>>(MockBehavior.Strict);
            _controller = new ShelterApiController(_mockedLogger.Object, _mockedDataAccess.Object);
        }

        [Test]
        public void Test_GetAllShelters()
        {
        var shelters = new List<Shelter.shared.Shelter>();

        _mockedDataAccess.Setup(x => x.GetAllShelters()).Returns(shelters);

        var result = _controller.GetAllShelters();

        Assert.IsInstanceOf(typeof(JsonResult), result);
        Assert.AreEqual(((JsonResult)result).Value, shelters);

        }

        [Test]
        public void Test_GetOneShelter()
        {
        var shelter = new List<Shelter.shared.Shelter>()
        {
            
        };

        _mockedDataAccess.Setup(x => x.GetShelterById("507f1f77bcf86cd799439011")).Returns(shelter);

        var result = _controller.GetShelter("507f1f77bcf86cd799439011");

        Assert.IsInstanceOf(typeof(List<Shelter.shared.Shelter>), result);
        Assert.AreEqual(((List<Shelter.shared.Shelter>)result), shelter);
        }   


    }
}