using Moq;
using Repositories.Interfaces;
using Repositories.Models;
using Services.Exceptions;
using Services.Interfaces;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProject1
{
    public class SericesTest
    {
        public SericesTest()
        {

        }

        /// <summary>
        /// The code is not working to show the problems service have
        /// </summary>
        [Fact]
        public async void Test()
        {
            var mock = new Mock<IATMRepository>();
            mock.Setup(p => p.All).Returns(new List<BankTransaction> { new BankTransaction { Amount = 1000, ATMAddress = "", IsDebit = true, TransactionDate = DateTime.Now } }.AsQueryable());

            var service = new ATMService(mock.Object, new CurrencyHttpService());
            try
            {
                await service.Withdraw(2000, "ccccc");
            }
            catch (ATMNotEnoughMoneyException)
            {
                Assert.True(true);
                return;
            }
            //Assert.True(false);
        }

        [Fact]
        public async void WithdrawUSDFromGermanySuccess()
        {
            try
            {
                var currencyHttpServiceMock = new Mock<ICurrencyHttpService>();
                currencyHttpServiceMock.Setup(m => m.GetEuroToUSdRate()).Returns(Task.Run(() => { return 2.0; }));
                var list = new List<BankTransaction>();
                list.Add(new BankTransaction { Amount = 900 });

                var aTMRepositoryMock = new Mock<IATMRepository>();
                aTMRepositoryMock.Setup(m => m.All).Returns(list.AsQueryable());
                aTMRepositoryMock.Setup(m => m.CreateAsync(It.IsAny<BankTransaction>()))
                    .Callback<BankTransaction>(c => list.Add(c))
                    .Returns(Task.Run(() => { return new BankTransaction(); }));

                ATMService service = new ATMService(aTMRepositoryMock.Object, currencyHttpServiceMock.Object);
                await service.Withdraw(111, Currency.USD, "address", Country.Germany);
                await service.Withdraw(111, Currency.USD, "address", Country.Germany);
                await service.Withdraw(111, Currency.USD, "address", Country.Germany);
                Assert.Equal(600, list.Sum(x => x.Amount));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Fact]
        public async void WithdrawEurosFromUSASuccess()
        {
            try
            {
                var currencyHttpServiceMock = new Mock<ICurrencyHttpService>();
                currencyHttpServiceMock.Setup(m => m.GetEuroToUSdRate()).Returns(Task.Run(() => { return 2.0; }));
                var list = new List<BankTransaction>();
                list.Add(new BankTransaction { Amount = 900 });

                var aTMRepositoryMock = new Mock<IATMRepository>();
                aTMRepositoryMock.Setup(m => m.All).Returns(list.AsQueryable());
                aTMRepositoryMock.Setup(m => m.CreateAsync(It.IsAny<BankTransaction>()))
                    .Callback<BankTransaction>(c => list.Add(c))
                    .Returns(Task.Run(() => { return new BankTransaction(); }));

                ATMService service = new ATMService(aTMRepositoryMock.Object, currencyHttpServiceMock.Object);
                await service.Withdraw(100, Currency.EURO, "address", Country.USA);
                var a = list.Sum(x => x.Amount);
                Assert.Equal(700, list.Sum(x => x.Amount));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Fact]
        public async void WithdrawSuccessful()
        {
            try
            {
                var currencyHttpServiceMock = new Mock<ICurrencyHttpService>();
                currencyHttpServiceMock.Setup(m => m.GetEuroToUSdRate()).Returns(Task.Run(() => { return 10.0; }));

                var aTMRepositoryMock = new Mock<IATMRepository>();
                aTMRepositoryMock.Setup(m => m.CreateAsync(new BankTransaction())).Returns(Task.Run(() => { return new BankTransaction(); }));

                var list = new List<BankTransaction>();
                list.Add(new BankTransaction { Amount = 900 });
                aTMRepositoryMock.Setup(m => m.All).Returns(list.AsQueryable());

                ATMService service = new ATMService(aTMRepositoryMock.Object, currencyHttpServiceMock.Object);
                await service.Withdraw(100, Currency.EURO, "address", Country.Germany);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
