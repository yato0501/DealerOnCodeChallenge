using System;
using System.Collections.Generic;
using System.Text;

namespace DealerOnCodeChallenge.Utility
{
    public static class DoubleExtension
    {
        public static double GoUpToNearest5Hundredths(this double value)
        {

            var stringValue = value.ToString();

            //make sure we are double precision.
            //the assumption is that we always pass in a double precision value and we don't need this check ever :)
            var parts = stringValue.Split('.');
            if (parts.Length < 2)
                stringValue = $"{stringValue}.00";
            else if (parts[1].Length == 1)
                stringValue = $"{stringValue}0";
                


            var lastDigit = int.Parse(stringValue[stringValue.Length-1].ToString());

            if (lastDigit % 5 == 0)
            {
                return value;
            }

            var diffValue = 0;
            if (value >= 0)
            {
                diffValue = lastDigit > 5 ? 10 : 5;
            }
            else
            {
                diffValue = lastDigit > 5 ? 5 : 0;
            }
            var valueToAdd = double.Parse($"0.0{Math.Abs(diffValue - lastDigit).ToString()}");




            return value + valueToAdd;
        }
    }
}
