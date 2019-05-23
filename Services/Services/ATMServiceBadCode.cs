using Repositories;
using Repositories.Interfaces;
using Repositories.Repositories;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Repositories.Models;
using Services.Exceptions;

namespace Services.Services
{
    public class ATMServiceBadCode : IATMService
    {
        private readonly ATMRepository _aTMRepository = new ATMRepository(new ApplicationDbContext());
        private readonly CurrencyHttpService _currencyHttpService = new CurrencyHttpService();

        public async Task InitATM()
        {
            await _aTMRepository.CreateAsync(new Repositories.Models.BankTransaction() { Amount = 10000, ATMAddress = "Prospekt Nauki 37", IsDebit = true, TransactionDate = DateTime.Now });
        }

        public async Task Withdraw(int amount, string address)
        {
            if (amount <= 0)
                throw new Exception("Amount cant be less or equals then zero");
            if (amount % 100 > 0)
                throw new Exception("Amount is incorrect");
            if (string.IsNullOrWhiteSpace(address))
                throw new Exception("Address is incorrect");

            var totalAmount = _aTMRepository.All.Sum(x => x.Amount);

            if (totalAmount < amount)
                throw new ATMNotEnoughMoneyException("Not enough money");

            await _aTMRepository.CreateAsync(new Repositories.Models.BankTransaction() { Amount = -amount, ATMAddress = "Prospekt Nauki 37", IsDebit = false, TransactionDate = DateTime.Now });
        }

        public async Task Withdraw(int amount, Currency currency, string address, Country country) {
            //if we withdraw euro we need to conver it to usd, out ATM suppors only USD
            if (currency.Equals(Currency.EURO)) {
                var rate = await _currencyHttpService.GetEuroToUSdRate();
                amount = Convert.ToInt32(amount * rate);
            }

            //if country is not USA then we need to add tax 1% from amount

            if (!country.Equals(Country.USA)) {
                amount = amount - Convert.ToInt32(amount * .1);
            }

            await Withdraw(amount, "Some cool address");
        }
    }
}
