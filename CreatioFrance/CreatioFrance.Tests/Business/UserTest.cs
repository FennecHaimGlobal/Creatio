﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CreatioUsersBusiness;
using CreatioUsersData;

namespace CreatioFrance.Tests.Business
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            IUsersManagment userManagment = UsersManagment.GetInstance;

            Information info = new Information()
            {
                Id = "B02C8395 - 1B6B - 4BA8 - B151 - B399D77C53D2",
                Nom = "Bentouza",
                Prenom = "Haim",
                Email = "aaa@aaa.com",
                CodePostal = "69140",
                DateDeNaissance = "03031982",
                Ville ="Kfar Saba",
                TelephoneBureau = "0528788818",
                TelephoneDomicile ="05828788818",
                TelephonePortable = "0528788818"
            };

            userManagment.SaveUserInformation(info).Wait(); ;
        }
    }
}
