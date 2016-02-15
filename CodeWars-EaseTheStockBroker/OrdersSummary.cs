using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars_EaseTheStockBroker
{
    public class OrdersSummary
    {
        public static string balanceStatements(string lst)
        {
            string output = "Buy: 0 Sell: 0"; //default value is passed string is null
            if(!String.IsNullOrEmpty(lst))
            {
                List<string[]> orders;

                orders = Parse(lst);
                if (Validate(orders))
                {
                    output = Format(orders);
                }
                else
                {
                    output = GetBadInfo(orders);
                }
            }
            
            return output;
        }

        private static string GetBadInfo(List<string[]> orders)
        {
            List<string[]> badOrders = new List<string[]>();
            List<string[]> goodOrders = new List<string[]>();

            foreach (string[] order in orders)
            {
                if (order.Length == 4)
                {
                    if (order[0].Contains(' ') //if quote contains spaces
                        || order[1].Contains('.') //if Quantity is not an int
                        || !order[2].Contains('.') //if Price is not a double
                        || !(order[3].ToUpper() == "B" || order[3].ToUpper() == "S")) //if Status is not a 'b' or an 's' (can be upper or lower case)
                    {
                        badOrders.Add(order);
                        continue;
                    }
                }
                else if (order.Length != 4)
                {
                    badOrders.Add(order);
                    continue;
                }

                goodOrders.Add(order);
            }

            //"Buy: b Sell: s; Badly formed nb: badly-formed 1st simple order ;badly-formed nth simple order ;"
            string output = Format(goodOrders) + "; Badly formed " + badOrders.Count() + ": ";
            foreach(string[] order in badOrders)
            {
                foreach (string o in order)
                {
                    output += o + " ";
                }

                output += ";";
            }

            return output;
        }

        private static List<string[]> Parse(string lst)
        {
            string[] parseOne = lst.Trim().Split(',');
            List<string[]> parseTwo = new List<string[]>();

            foreach (string s in parseOne)
            {
                //need to trim each string because they will contain a leading white space from parseOne
                parseTwo.Add(s.Trim().Split(' '));
            }

            return parseTwo;
        }

        private static bool Validate(List<string[]> orders)
        {
            foreach (string[] order in orders)
            {
                if (order.Length == 4)
                {
                    if (order[0].Contains(' ') //if quote contains spaces
                        || order[1].Contains('.') //if Quantity is not an int
                        || !order[2].Contains('.') //if Price is not a double
                        || !(order[3].ToUpper() == "B" || order[3].ToUpper() == "S")) //if Status is not a 'b' or an 's' (can be upper or lower case)
                    {
                        return false;
                    }
                }
                else if (order.Length != 4)
                {
                    return false;
                }
            }

            return true;
        }

        private static string Format(List<string[]> multipleOrders)
        {
            double totalSellPrice = 0.00;
            double totalBuyPrice = 0.00;

            foreach (string[] order in multipleOrders)
            {
                //position 0: Quote, position 1: Quantity, position 2: Price, position 3: status
                //"Buy: 162600 Sell: 0;
                double quantity;
                double price;
                Double.TryParse(order[1], out quantity);
                Double.TryParse(order[2], out price);
                double totalPrice = quantity * price;

                if (order[3].ToUpper() == "S")
                {
                    totalSellPrice += totalPrice;
                }
                else if (order[3].ToUpper() == "B")
                {
                    totalBuyPrice += totalPrice;
                }
            }

            return "Buy: " + totalBuyPrice + " Sell: " + totalSellPrice;
        }
    }
}

//Clients place orders to a stockbroker as strings. The order can be simple or multiple.

//Type of a simple order: Quote /space/ Quantity /space/ Price /space/ Status

//where Quote is formed of non-whitespace character, Quantity is an int, Price a double 
//(with mandatory decimal point "." ), Status is represented by the letter B (buy) or the 
//letter S (sell).

//Example:

//"GOOG 300 542.0 B"

//A multiple order is the concatenation of simple orders with a comma between each.

//Example:

//"ZNGA 1300 2.66 B, CLH15.NYM 50 56.32 B, OWW 1000 11.623 B, OGG 20 580.1 B"

//To ease the stockbroker your task is to produce a string of type

//"Buy: b Sell: s" where b and s are 'double' formatted with no decimal (rounded to integers), 
//b representing the total price of bought stocks and s the total price of sold stocks.

//Example:

//"Buy: 294990 Sell: 0"

//Unfortunately sometimes clients make mistakes. When you find mistakes in orders, you must 
//pinpoint these badly formed orders and produce a string of type:

//"Buy: b Sell: s; Badly formed nb: badly-formed 1st simple order ;badly-formed nth simple order ;"

//where nb is the number of badly formed simple orders, b representing the total price of 
//bought stocks with correct simple order and s the total price of sold stocks with correct 
//simple order.

//Examples:

//"Buy: 263 Sell: 11802; Badly formed 2: CLH16.NYM 50 56 S ;OWW 1000 11 S ;"

//"Buy: 100 Sell: 56041; Badly formed 1: ZNGA 1300 2.66 ;"