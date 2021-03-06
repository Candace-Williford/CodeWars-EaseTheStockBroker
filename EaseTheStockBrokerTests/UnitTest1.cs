﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeWars_EaseTheStockBroker.Tests
{
    [TestClass]
    public class OrdersSummaryTests
    {

        [TestMethod]
        public void Test1()
        {
            string l = "GOOG 300 542.0 B, AAPL 50 145.0 B, CSCO 250.0 29 B, GOOG 200 580.0 S";
            string r = "Buy: 169850 Sell: 116000; Badly formed 1: CSCO 250.0 29 B ;";
            Assert.AreEqual(r, OrdersSummary.balanceStatements(l));
        }
    }

}
