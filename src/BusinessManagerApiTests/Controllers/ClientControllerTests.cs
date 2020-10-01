using AutoMapper;
using BusinessManager.DataAccess.UnitOfWork;
using BusinessManager.DataAccess.UnitOfWork.Abstractions;
using BusinessManager.Models.Models;
using BusinessManagerApi.Controllers;
using BusinessManagerApi.Data;
using BusinessManagerApi.MappingConfiguration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace BusinessManagerApiTests.Controllers
{
    public class ClientControllerTests : IDisposable
    {

        DbContextOptionsBuilder<ApplicationDbContext> optionsBuilder;
        ApplicationDbContext dbContext;
        ClientController controller;

        public ClientControllerTests()
        {
            #region Context
            optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase("UnitTestInMemBD");
            dbContext = new ApplicationDbContext(optionsBuilder.Options);
            #endregion

            #region Logger
            var serviceProvider = new ServiceCollection()
                                        .AddLogging()
                                        .BuildServiceProvider();
            var factory = serviceProvider.GetService<ILoggerFactory>();
            var logger = factory.CreateLogger<ClientController>();
            #endregion

            #region Mapper
            var mockMapper = new MapperConfiguration(cfg =>
               {
                   cfg.AddProfile(new ClientProfile());
               });
            var mapper = mockMapper.CreateMapper(); 
            #endregion

            var unitOfWork = new UnitOfWork(dbContext);

            controller = new ClientController(unitOfWork, logger, mapper);
        }

        public void Dispose()
        {
            foreach (var client in dbContext.Clients)
            {
                dbContext.Clients.Remove(client);
            }
            dbContext.SaveChanges();
        }

        //**************************************************
        //*
        //GET   /api/commands Unit Tests
        //*
        //**************************************************

        [Fact]
        public async void GetClientById_ReturnsStatus200_WhenDBHasObject()
        {
            //Arrange
            var client = ExpectedClientResult();

            dbContext.Clients.Add(client);
            dbContext.SaveChanges();

            //Act
            var result = await controller.GetClientById(client.Id);

            //Assert
            Assert.IsType<OkObjectResult>(result);

            Dispose();
        }

        [Fact]
        public async void GetAllClients_ReturnsStatus200_WhenDBHasObjects()
        {
            //Arrange
            var client = ExpectedClientResult();

            dbContext.Clients.Add(client);
            dbContext.SaveChanges();
            client.Id = Guid.NewGuid();
            dbContext.Clients.Add(client);
            dbContext.SaveChanges();

            //Act
            var result = await controller.GetAllClients();

            //Assert
            Assert.IsType<OkObjectResult>(result);

            Dispose();
        }

        //**************************************************
        //*
        //POST   /api/client Unit Tests
        //*
        //**************************************************

        [Fact]
        public void CreateClient_IncrementCount_WhenSuccessfullyCreated()
        {
            //Arrange
            var client = ExpectedClientResult();
            var oldCount = dbContext.Clients.Count();

            //Act
            var result = controller.CreateClient(client);

            //Assert
            Assert.Equal(oldCount + 1, dbContext.Clients.Count());

            Dispose();
        }

        [Fact]
        public void CreateClient_Returns201Created_WhenValidObject()
        {
            //Arrange
            var client = ExpectedClientResult();

            //Act
            var result = controller.CreateClient(client);

            //Assert
            Assert.IsType<CreatedAtRouteResult>(result.Result);

            Dispose();
        }

        //**************************************************
        //*
        //PUT   /api/client/{Id} Unit Tests
        //*
        //**************************************************


        [Fact]
        public async void UpdateClient_AttributeUpdated_WhenValidObject()
        {
            //Arrange
            var client = ExpectedClientResult();
            dbContext.Clients.Add(client);
            dbContext.SaveChanges();

            var clientId = client.Id;

            client.City = "Beograd";

            //Act
            await controller.UpdateClient(clientId, client);
            var result = dbContext.Clients.Find(clientId);

            //Assert
            Assert.Equal(client.City, result.City);

            Dispose();
        }

        [Fact]
        public async void UpdateClient_Returns204_WhenValidObject()
        {
            //Arrange
            var client = ExpectedClientResult();
            dbContext.Clients.Add(client);
            dbContext.SaveChanges();

            var clientId = client.Id;

            client.City = "Beograd";
            client.PostalCode = "11000";

            //Act
            var result = await controller.UpdateClient(clientId, client);

            //Assert
            Assert.IsType<NoContentResult>(result);

            // Test comment

            Dispose();
        }

        [Fact]
        public async void UpdateClient_Returns400_WhenInvalidObject()
        {
            //Arrange
            var client = ExpectedClientResult();
            dbContext.Clients.Add(client);
            dbContext.SaveChanges();

            var clientId = Guid.NewGuid();

            client.City = "Beograd";
            client.PostalCode = "11000";

            //Act
            var result = await controller.UpdateClient(clientId, client);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);

            Dispose();
        }

        //**************************************************
        //*
        //DELETE   /api/client/{Id} Unit Tests
        //*
        //**************************************************

        [Fact]
        public async void DeleteClient_Returns204_WhenValidObjectID()
        {
            //Arrange
            var client = ExpectedClientResult();
            dbContext.Clients.Add(client);
            dbContext.SaveChanges();

            var clientId = client.Id;

            //Act
            var result = await controller.SoftDeleteClient(clientId);

            //Assert
            Assert.IsType<NoContentResult>(result);

            Dispose();

        }

        private Clients ExpectedClientResult()
        {
            return new Clients
            {
                Id = new Guid("ea3a8dde-fc8b-4a3d-9ac8-2d61dfd5c942"),
                Name = "Milos Mitrovic",
                Address = "Rade Koncara 6",
                City = "Nova Pazova",
                Email = "milos.mitrovicbgnp@gmail.com",
                Note = "Prazan Note",
                PhoneNumber = "060 025 5622",
                PIB = "123123",
                PostalCode = "22330",
                Deleted = false
            };
        }
    }
}
