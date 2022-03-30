using BusinessLayer.Interface;
using BusinessLayer.Service;
using CommonLayer.Model;
using FundoNote.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using Xunit;

namespace usertest
{
    public class UnitTest1
    {
        private readonly IUserBL userBL;
        private readonly IUserRL userRL;
        public static DbContextOptions<FundoContext> newContext { get; }
        public static string connectionstring = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FundoDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        static UnitTest1()
        {
            newContext = new DbContextOptionsBuilder<FundoContext>().UseSqlServer(connectionstring).Options;
        }
        public UnitTest1()
        {
            var context = new FundoContext(newContext);
            userRL = new UserRL(context);
            userBL = new UserBL(userRL);
        }

        [Fact]
        public void RegisterApi_AddUser_Returns_OkResult()
        {
            var controller = new UserController(userBL);
            var data = new UserRegistration {
            FirstName="Peter",
            LastName="Parker",
            Email="Peter123@gmail.com",
            Password="Peter@123"
            };
            var result = controller.Registration(data);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
