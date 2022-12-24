/*
 * GUID:E6C09DFE-3A3E-461B-B3F9-734AEE05FC7B
 * Licensed to a unique person with this Token:(Token)
 * 
 * Coded by fiyistack.com
 * Copyright © 2021
 * 
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 * 
 * Auto generated code. It should not be modified from here. 
 */

using System;

namespace FiyiRequirements.Library
{
    public static class Math
    {
        public static bool HasDecimalsAfterComma(decimal Decimal)
        {
            return Decimal % 1 != 0;
        }

        public enum RoundType { RoundUp, RoundDown }
        public static int Divide(decimal Dividend, decimal Divisor, RoundType RoundTypeForResult)
        {
            try
            {
                if (Divisor == 0)
                { throw new ArithmeticException("It is not allowed to perform a division with zero divisor"); }

                int Result = 0;
                decimal decResult = Dividend / Divisor;

                switch (RoundTypeForResult)
                {
                    case RoundType.RoundUp:
                        Result = System.Convert.ToInt32(System.Math.Ceiling(decResult));
                        break;
                    case RoundType.RoundDown:
                        Result = System.Convert.ToInt32(System.Math.Floor(decResult));
                        break;
                }

                return Result;
            }
            catch (Exception) { throw; }
        }
    }
}
