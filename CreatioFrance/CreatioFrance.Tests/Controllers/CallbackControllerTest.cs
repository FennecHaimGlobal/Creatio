using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CreatioFrance;
using CreatioFrance.Controllers;
using CreatioFrance.Models;
using CreatioUsersData;
using System.ComponentModel.DataAnnotations;

namespace CreatioFrance.Tests.Controllers
{
    [TestClass]
    public class CallbackControllerTest
    {
        [TestMethod]
        public void NotValidHasNoConsecutiveSequence()
        {
            // Arrange
            CallbackController controller = new CallbackController();

            List<CallbackViewModel> models = new List<CallbackViewModel>()
            {
                new CallbackViewModel
                {
                    PhoneNumber = "0612345600"
                },
                new CallbackViewModel
                {
                    PhoneNumber = "0498765433"
                },
                new CallbackViewModel
                {
                    PhoneNumber = "0123456789"
                },
                new CallbackViewModel
                {
                    PhoneNumber = "0987654321"
                },
                new CallbackViewModel
                {
                    PhoneNumber = "0501234508"
                },
            };

            foreach (var model in models)
            {
                controller.ViewData.ModelState.Clear();
                // Act
                controller.ValidateViewModel(model);

                // Assert
                Assert.IsFalse(controller.ModelState.IsValid);
                Assert.AreEqual(1, controller.ModelState.Count);
                Assert.AreEqual(controller.ModelState["PhoneNumber"].Errors[0].ErrorMessage, "Telephone can not have a consecutive sequence."); 
            }
        }

        [TestMethod]
        public void NotValidHasNoRepeatSequence()
        {
            // Arrange
            CallbackController controller = new CallbackController();

            List<CallbackViewModel> models = new List<CallbackViewModel>()
            {
                new CallbackViewModel
                {
                    PhoneNumber = "0788888525"
                },
                new CallbackViewModel
                {
                    PhoneNumber = "0510000012"
                },
                new CallbackViewModel
                {
                    PhoneNumber = "0111111111"
                },
                new CallbackViewModel
                {
                    PhoneNumber = "0111114321"
                },
                new CallbackViewModel
                {
                    PhoneNumber = "0501222222"
                },
            };

            foreach (var model in models)
            {
                controller.ViewData.ModelState.Clear();
                // Act
                controller.ValidateViewModel(model);

                // Assert
                Assert.IsFalse(controller.ModelState.IsValid);
                Assert.AreEqual(1, controller.ModelState.Count);
                Assert.AreEqual(controller.ModelState["PhoneNumber"].Errors[0].ErrorMessage, "Telephone can not have a number that repeats more than 4 times in a sequence.");
            }
        }

        [TestMethod]
        public void NotValidStartsWith01To09AndNumbersOnly()
        {
            // Arrange
            CallbackController controller = new CallbackController();

            List<CallbackViewModel> models = new List<CallbackViewModel>()
            {
                new CallbackViewModel
                {
                    PhoneNumber = "1256125121"
                },
                new CallbackViewModel
                {
                    PhoneNumber = "1122445566"
                },
                new CallbackViewModel
                {
                    PhoneNumber = "0055125664"
                },
                new CallbackViewModel
                {
                    PhoneNumber = "9090521456"
                },
                new CallbackViewModel
                {
                    PhoneNumber = "5678112247"
                },
            };

            foreach (var model in models)
            {
                controller.ViewData.ModelState.Clear();
                // Act
                controller.ValidateViewModel(model);

                // Assert
                Assert.IsFalse(controller.ModelState.IsValid);
                Assert.AreEqual(1, controller.ModelState.Count);
                Assert.AreEqual(controller.ModelState["PhoneNumber"].Errors[0].ErrorMessage, "Le Telephone n'est pas valide");
            }
        }

        [TestMethod]
        public void NotValidMinLength()
        {
            // Arrange
            CallbackController controller = new CallbackController();

            List<CallbackViewModel> models = new List<CallbackViewModel>()
            {
                new CallbackViewModel
                {
                    PhoneNumber = "01"
                },
                new CallbackViewModel
                {
                    PhoneNumber = "012"
                },
                new CallbackViewModel
                {
                    PhoneNumber = "05425019"
                },
                new CallbackViewModel
                {
                    PhoneNumber = "054250199"
                },
                new CallbackViewModel
                {
                    PhoneNumber = "0123"
                },
            };

            foreach (var model in models)
            {
                controller.ViewData.ModelState.Clear();
                // Act
                controller.ValidateViewModel(model);

                // Assert
                Assert.IsFalse(controller.ModelState.IsValid);
                Assert.AreEqual(1, controller.ModelState.Count);
                Assert.AreEqual(controller.ModelState["PhoneNumber"].Errors[0].ErrorMessage, "The Telephone must be at least 10 characters long.");
            }
        }

        [TestMethod]
        public void NotValidManyRequestsException()
        {
            // Arrange
            CallbackController controller = new CallbackController();


            var model = new CallbackViewModel
            {
                PhoneNumber = "0542501908"
            };

            controller.ViewData.ModelState.Clear();
            // Act
            controller.SaveCallback(model).Wait();
            Assert.AreEqual(controller.ViewBag.Message, "Merci, nous allons vous contacter bientôt");
            controller.SaveCallback(model).Wait();
            Assert.AreEqual(controller.ViewBag.Message, "Votre demande de rappel est en cours de traitement, il faut compter un délai d’environ 20 min pour être rappelé. Merci de votre compréhension.");

            using (CREATIO_DB_PRODEntities context = new CREATIO_DB_PRODEntities((new UserDataManagment()).ConnectionString))
            { // To make sure that if the test runs again it will not be affected by the delay check.
                var c = context.Callback.Where(t => t.Telephone.Equals(model.PhoneNumber));
                context.Callback.RemoveRange(c);
                context.SaveChanges();
            }

            
        }

        [TestMethod]
        public void CallbackIsOK()
        {
            // Arrange
            CallbackController controller = new CallbackController();


            var model = new CallbackViewModel
            {
                PhoneNumber = "0542501908"
            };

            controller.ViewData.ModelState.Clear();
            // Act
            controller.SaveCallback(model).Wait();
            Assert.AreEqual(controller.ViewBag.Message, "Merci, nous allons vous contacter bientôt");
            
            using (CREATIO_DB_PRODEntities context = new CREATIO_DB_PRODEntities((new UserDataManagment()).ConnectionString))
            { // To make sure that if the test runs again it will not be affected by the delay check.
                var c = context.Callback.SingleOrDefault(t => t.Telephone.Equals(model.PhoneNumber) && t.CallbackOk.Equals(0));
                c.CallbackOk = 1;
                context.SaveChanges();
            }

            controller.SaveCallback(model).Wait();
            Assert.AreEqual(controller.ViewBag.Message, "Merci, nous allons vous contacter bientôt");

            using (CREATIO_DB_PRODEntities context = new CREATIO_DB_PRODEntities((new UserDataManagment()).ConnectionString))
            { // To make sure that if the test runs again it will not be affected by the delay check.
                var c = context.Callback.Where(t => t.Telephone.Equals(model.PhoneNumber));
                context.Callback.RemoveRange(c);
                context.SaveChanges();
            }


        }

        [TestMethod]
        public void UserWaitEnoughToReCallback()
        {
            // Arrange
            CallbackController controller = new CallbackController();


            var model = new CallbackViewModel
            {
                PhoneNumber = "0542501908"
            };

            controller.ViewData.ModelState.Clear();
            // Act
            controller.SaveCallback(model).Wait();
            Assert.AreEqual(controller.ViewBag.Message, "Merci, nous allons vous contacter bientôt");
            
            controller.SaveCallback(model).Wait();
            Assert.AreEqual(controller.ViewBag.Message, "Votre demande de rappel est en cours de traitement, il faut compter un délai d’environ 20 min pour être rappelé. Merci de votre compréhension.");

            using (CREATIO_DB_PRODEntities context = new CREATIO_DB_PRODEntities((new UserDataManagment()).ConnectionString))
            { // To make sure that if the test runs again it will not be affected by the delay check.
                var c = context.Callback.SingleOrDefault(t => t.Telephone.Equals(model.PhoneNumber) && t.CallbackOk.Equals(0));
                c.Date = c.Date.AddMinutes(-20);
                context.SaveChanges();
            }

            controller.SaveCallback(model).Wait();
            Assert.AreEqual(controller.ViewBag.Message, "Merci, nous allons vous contacter bientôt");

            using (CREATIO_DB_PRODEntities context = new CREATIO_DB_PRODEntities((new UserDataManagment()).ConnectionString))
            { // To make sure that if the test runs again it will not be affected by the delay check.
                var c = context.Callback.SingleOrDefault(t => t.Telephone.Equals(model.PhoneNumber));
                Assert.AreEqual(c.Repeat, 1);
                var cl = context.Callback.Where(t => t.Telephone.Equals(model.PhoneNumber));
                context.Callback.RemoveRange(cl);
                context.SaveChanges();
            }


        }

    }

    public static class UnitTestsHelpers
    {
        public static void ValidateViewModel<TViewModel, TController>(this TController controller, TViewModel viewModelToValidate)
    where TController : Controller
        {
            var validationContext = new ValidationContext(viewModelToValidate, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(viewModelToValidate, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                controller.ModelState.AddModelError(validationResult.MemberNames.FirstOrDefault() ?? string.Empty, validationResult.ErrorMessage);
            }
        }
    }


}
