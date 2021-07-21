using BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUI.Controllers;
using WebUI.Models;
using Xunit;

namespace UnitTest
{
    public class CustomerControllerTest
    {
        [Fact]
        public void DisplayAllCustomersActionShouldReturnRestaurantList()
        {
            //Arrange
            var mockBL = new Mock<IBL>();
            //Sets up and uses a fake BL through moq
            //This mockBL will always returns the following list
            mockBL.Setup(bl => bl.GetAllCustomers()).Returns
            (
                new List<Customer>
                {
                    new Customer() { Fname = "Hieu"},
                    new Customer() { Fname = "NotHieu"}
                }
            );

            var customerController = new CustomerController(mockBL.Object);

            //Act
            var result = customerController.DisplayAllCustomers();

            //Assert
            //checks the viewResult the same type of ViewResult
            var viewResult = Assert.IsType<ViewResult>(result);
            //ensures that the model inside of viewResult is a collection of CustomerVM
            var model = Assert.IsAssignableFrom<IEnumerable<CustomerVM>>(viewResult.ViewData.Model);

            Assert.Equal(2, model.Count());
        }
    }
}
