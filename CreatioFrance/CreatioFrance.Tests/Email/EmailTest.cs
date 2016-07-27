using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CreatioEmailProvider;

namespace CreatioFrance.Tests.Email
{
    [TestClass]
    public class EmailTest
    {
        IEmailServiceProvider _emailServiceProvider = EmailServiceProvider.GetInstance;

        [TestMethod]
        public void TestMethod1()
        {
            _emailServiceProvider.SendSubscriptionConfirmationEmailAsync("Test", "Test", "fennechaim@gmail.com").Wait();
        }
    }
}
