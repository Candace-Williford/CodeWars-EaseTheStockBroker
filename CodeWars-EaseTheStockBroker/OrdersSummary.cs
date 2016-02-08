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
            if(lst.Contains(','))
            {
                List<string[]> multipleOrders = ParseMultiple(lst);
                if(ValidateMultiple(multipleOrders))
                {
                    //do stuff
                }
            }
            else
            {
                string[] singleOrder = ParseSingle(lst);
                if(ValidateSingle(singleOrder))
                {
                    //do stuff
                }
            }

            return "";
        }

        private static bool ValidateSingle(string[] order)
        {
            //used for testing below. Best way to test types is to use TryParse
            int intOutVar;
            double doubleOutVar;

            if (order.Length == 4)
            {
                if (!order[0].Contains(' ') //if quote contains no spaces
                    && int.TryParse(order[1], out intOutVar) //if Quantity is an int
                    && double.TryParse(order[2], out doubleOutVar) //if Price is a double
                    && (order[3].ToUpper() == "B" || order[3].ToUpper() == "S")) //if Status is a 'b' or an 's' (can be upper or lower case)
                {
                    return true;
                }
            }

            return false;
        }

        private static string[] ParseSingle(string lst)
        {
            //position 0: Quote, position 1: Quantity, position 2: Price, position 3: status
            string[] order = lst.Trim().Split(' ');
            return order;
        }

        private static bool ValidateMultiple(List<string[]> orders)
        {
            foreach(string[] order in orders)
            {
                if(!ValidateSingle(order))
                {
                    return false;
                }
            }

            return true;
        }

        private static List<string[]> ParseMultiple(string lst)
        {
            string[] parseOne = lst.Trim().Split(',');
            List<string[]> parseTwo = new List<string[]>();
            
            foreach(string s in parseOne)
            {
                //need to trim each string because they will contain a leading white space from parseOne
                parseTwo.Add(s.Trim().Split(' '));
            }

            return parseTwo;
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