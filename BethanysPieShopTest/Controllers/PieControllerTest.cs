﻿using BethanysPieShop.Controllers;
using BethanysPieShop.ViewModels;
using BethanysPieShopTest.Mocks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BethanysPieShopTest.Controllers
{
    public class PieControllerTest
    {

        //Need Fact for it to be a testing method.
        //Method name should be descripton of what we are testing
        [Fact]
        public void List_EmptyCategory_ReturnsAllPies()
        {
            //Arrange
            var mockPieRepository = RepositoryMocks.GetPieRepository();
            var mockCategoryRepository = RepositoryMocks.GetCategoryRepository();

            var pieController = new PieController(mockPieRepository.Object, mockCategoryRepository.Object);

            //Act
            var result = pieController.List("");

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var pieListViewModel = Assert.IsAssignableFrom<PieListViewModel>
                (viewResult.ViewData.Model);
            Assert.Equal(10, pieListViewModel.Pies.Count());
        }
    }
}