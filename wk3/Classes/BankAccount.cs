using System;
using System.Collections.Generic;

namespace classes
{
    public class BankAccount
    {
        // C# - fields and properties
        // field: one piece of data, like a variable, attached to
        //     the class (static) or each instance of the class (non-static)
        // property: halfway between a method and a field
        //       from an external point of view, it's like a field
        //       (read and write it like a field)
        //       internally, it's more like a pair of methods for get and set.


        public string Number { get; }

        public string Owner { get; set; }

        // auto-property (automatically-implemented property)
        // there is a hidden private field behind this that actually stores the data.
        // public decimal Balance { get; set; }

        // full property version of that auto property
        // private decimal _balance;
        // public decimal Balance
        // {
        //     get { return _balance; }
        //     set
        //     {
        //         _balance = value;
        //     }
        // }

        public decimal Balance
        {
            get
            {
                decimal balance = 0;
                foreach (var item in allTransactions)
                {
                    balance += item.Amount;
                }

                return balance;
            }
        }

        private static int accountNumberSeed = 1234567890;

        public BankAccount(string name, decimal initialBalance)
        {
            this.Number = accountNumberSeed.ToString();
            accountNumberSeed++;

            this.Owner = name;

            // this.Balance = initialBalance;

            MakeDeposit(initialBalance, DateTime.Now, "Initial balance");
        }

        private List<Transaction> allTransactions = new List<Transaction>();


        public void MakeDeposit(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
            }
            var deposit = new Transaction(amount, date, note);
            allTransactions.Add(deposit);
        }

        public void MakeWithdrawal(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal must be positive");
            }
            if (Balance - amount < 0)
            {
                throw new InvalidOperationException("Not sufficient funds for this withdrawal");
            }
            var withdrawal = new Transaction(-amount, date, note);
            allTransactions.Add(withdrawal);
        }

        public string GetAccountHistory()
        {
            var report = new System.Text.StringBuilder();

            decimal balance = 0;
            report.AppendLine("Date\t\tAmount\tBalance\tNote");
            foreach (var item in allTransactions)
            {
                balance += item.Amount;
                report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t{item.Notes}");
            }

            return report.ToString();
        }

        public virtual void PerformMonthEndTransactions() { }
    }
}
