﻿using System;
using System.Collections.Generic;
using System.Linq;
using Tokenio.Proto.Common.MoneyProtos;
using Tokenio.Proto.Common.TransactionProtos;
using Tokenio.Tpp;
using static Tokenio.Proto.Common.SecurityProtos.Key.Types;
using TppMember = Tokenio.Tpp.Member;

namespace TokenioSample

{
    /// <summary>
    /// Two ways to get balances of a member's bank accounts.
    /// </summary>
    public static class GetBalanceSample
    {
        /// <summary>
        /// Get a member's balances.
        /// </summary>
        /// <param name="member">Member</param>
        /// <returns>map currency: total</returns>
        public static IDictionary<string, double> MemberGetBalanceSample(TppMember member)
        {
            Dictionary<string, double> sums = new Dictionary<string, double>();

            IList<Account> accounts = member.GetAccountsBlocking();

            foreach (Account account in accounts)
            {
                Money balance = member.GetBalanceBlocking(account.Id(), Level.Standard)
                           .Current;

                sums[balance.Currency] = Double.Parse(balance.Value) + SampleExtensions.GetValueOrDefault(sums, balance.Currency, 0.0);
            }
            return sums;
        }

        /// <summary>
        /// Get a member's balances.
        /// </summary>
        /// <param name="member">Member.</param>
        /// <returns>map currency: total</returns>
        public static IDictionary<string, double> AccountGetBalanceSample(TppMember member)
        {
            Dictionary<string, double> sums = new Dictionary<string, double>();

            IList<Account> accounts = member.GetAccountsBlocking();

            foreach (Account account in accounts)
            {
                Money balance = account.GetBalanceBlocking(Level.Standard)
                           .Current;

                sums[balance.Currency] = Double.Parse(balance.Value) + SampleExtensions.GetValueOrDefault(sums, balance.Currency, 0.0);
            }
            return sums;

        }

        /// <summary>
        /// Get a member's list of balances.
        /// </summary>
        /// <param name="member">Member.</param>
        /// <returns>list of balances</returns>
        public static IList<Balance> memberGetBalanceListSample(TppMember member)
        {
            List<string> accountIds = member
                    .GetAccountsBlocking().Select(acc => acc.Id()).ToList();

            var balances = member.GetBalancesBlocking(accountIds, Level.Standard);

            return balances;
        }

    }

}