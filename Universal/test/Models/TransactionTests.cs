// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System;



using Xunit;

namespace Wangkanai.Universal.Models
{
    public class TransactionTests
    {
        // [Fact]
        // public void TestCalculationRevenue()
        // {
        //     var transaction = new Transaction("1234", "testing", 1.0, 1.0);
        //     transaction.Items.Add(new Item("product a", "a001", "fertilizer", 1.0, 1));
        //     Assert.Equal(3.0, transaction.Revenue);
        // }
        //
        // [Fact]
        // public void TestCalcuationTwoItems()
        // {
        //     var transaction = new Transaction("1234", "testing", 1.0, 1.0);
        //     transaction.Items.Add(new Item("product a", "a001", "fertilizer", 1.0, 1));
        //     transaction.Items.Add(new Item("product b", "b001", "fertilizer", 1.0, 1));
        //     Assert.Equal(4.0, transaction.Revenue);
        // }
        //
        // [Fact]
        // public void TestJsTransactionScript()
        // {
        //     var transaction = new Transaction("1234", "testing", 1.0, 1.0);
        //     Assert.Equal("ga('ecommerce:addTransaction',{'id':'1234','affiliation':'testing','revenue':'2','shipping':'1','tax':'1'});",
        //                  transaction.JsTransactionScript());
        // }
        //
        // [Fact]
        // public void TestJsEcommerceScript()
        // {
        //     var transaction = new Transaction("1234", "testing", 1.0, 1.0);
        //     transaction.Items.Add(new Item("product a", "a001", "fertilizer", 1.0, 1));
        //     transaction.Items.Add(new Item("product b", "b001", "fertilizer", 1.0, 1));
        //     Console.WriteLine(transaction.ToString());
        // }
        //
        // [Fact]
        // public void TestEcommerceScriptBlock()
        // {
        //     var analytic    = Analytic.Instance;
        //     var transaction = new Transaction("1234", "testing", 1.0, 1.0);
        //     transaction.Items.Add(new Item("product a", "a001", "fertilizer", 1.0, 1));
        //     transaction.Items.Add(new Item("product b", "b001", "fertilizer", 1.0, 1));
        //     var session = new Session();
        //     session.Transaction = transaction;
        //     Console.WriteLine(analytic.Render(session));
        // }
    }
}