using Moq;
using Repositories.Interfaces;
using Repositories.Models;
using Services.Exceptions;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace XUnitTestProject1
{
    public class TheProblemDescriptionTheServiceIsNotPossibleToWriteTest
    {
        public TheProblemDescriptionTheServiceIsNotPossibleToWriteTest()
        {

        }

        /// <summary>
        /// The code is not working to show the problems service have
        /// </summary>
        [Fact]
        public async void ATMServiceBadCodeTest()
        {
            //var mock = new Mock<IATMRepository>();
            //mock.Setup(p => p.All).Returns(new List<BankTransaction> { new BankTransaction { Amount = 1000, ATMAddress = "", IsDebit = true, TransactionDate = DateTime.Now } }.AsQueryable());

            //var service = new ATMServiceBadCode(mock.Object);
            //try
            //{
            //    await service.Withdraw(2000, "ccccc");
            //}
            //catch (ATMNotEnoughMoneyException)
            //{
            //    Assert.True(true);
            //    return;
            //}
            //Assert.True(false);
        }
    }
}
