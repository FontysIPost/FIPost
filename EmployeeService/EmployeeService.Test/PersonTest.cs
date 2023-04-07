using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using EmployeeService.Controllers;
using EmployeeService.Helpers;
using EmployeeService.Models;
using EmployeeService.Services;
using System;
using System.Collections.Generic;
using System.Linq;


public class Tests
{
    /*
    private Mock<IPersonService> serviceMock;

    private Person person1;
    private Person person2;
    private readonly List<Person> persons = new List<Person>();

    [SetUp]
    public void Setup()
    {
        // Instantiate mocks:
        serviceMock = new Mock<IPersonService>();

        // Create mock data:
        person1 = new Person("1", "Person_1", "person_1@testmail.com");
        person2 = new Person("2", "Person_2", "person_2@testmail.com");
        
        persons.Clear();
        persons.Add(person1);
        persons.Add(person2);
    }
    
    private static T GetObjectResultContent<T>(ActionResult<T> result)
    {
        return (T) ((ObjectResult) result.Result).Value;
    }

    [Test]
    public void GetPersons_Ok()
    {
        // Arrange
        serviceMock.Setup(x => x.GetAll()).Returns(persons);
        var controller = new PersonController(serviceMock.Object);

        // Act
        var actionResult = controller.GetPerson();

        // Assert
        Assert.IsNotNull(actionResult);
        Assert.IsInstanceOf<OkObjectResult>(actionResult.Result);
        Assert.AreEqual(persons, GetObjectResultContent(actionResult));
    }


    [Test]
    public void GetPersonById_Ok()
    {
        // Arrange
        serviceMock.Setup(x => x.GetById(person1.Id)).Returns(person1);
        var controller = new PersonController(serviceMock.Object);

        // Act
        var actionResult = controller.GetPerson(person1.Id);

        // Assert
        Assert.IsNotNull(actionResult);
        Assert.IsInstanceOf<OkObjectResult>(actionResult.Result);
        Assert.AreEqual(person1, GetObjectResultContent(actionResult));
    }
    
    [Test]
    public void GetPersonById_NotFound()
    {
        // Arrange
        serviceMock.Setup(x => x.GetById(person1.Id)).Throws<NotFoundException>();
        var controller = new PersonController(serviceMock.Object);

        // Act
        var actionResult = controller.GetPerson(person1.Id);

        // Assert
        Assert.IsNotNull(actionResult);
        Assert.IsInstanceOf<NotFoundObjectResult>(actionResult.Result);
    }

    [Test]
    public void Health() 
    {
        var controller = new PersonController(serviceMock.Object);

        // Act
        var actionResult = controller.Health();

        // Assert
        Assert.IsNotNull(actionResult);
        Assert.IsInstanceOf<OkResult>(actionResult);
    }
      */
}

