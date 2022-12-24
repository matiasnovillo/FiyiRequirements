using System;
using System.Text.RegularExpressions;

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

namespace FiyiRequirements.Library
{
    public static class Constant
    {
        #region int
        /// <summary>
        /// This value is equal to <b>2,147,483,647.0</b>.
        /// </summary>
        public const int IntMAXValue = int.MaxValue;
        /// <summary>
        /// This value is equal to <b>-2,147,483,648.0</b>.
        /// </summary>
        public const int IntMINValue = int.MinValue;
        #endregion


        #region decimal
        /// <summary>
        /// This value is equal to <b>79,228,162,514,264,337,593,543,950,335.0</b>.
        /// </summary>
        public const decimal DecimalMAXValue = decimal.MaxValue;
        /// <summary>
        /// This value is equal to <b>-79,228,162,514,264,337,593,543,950,335.0</b>.
        /// </summary>
        public const decimal DecimalMINValue = decimal.MinValue;
        #endregion


        #region HexColour
        public const string HexColourMAXValue = "FFFFFF";
        public const string HexColourMINValue = "000000"; 
        #endregion


        #region Regular expressions
        /// <summary>
        /// This Regex accept letters (a to z, A to Z), numbers (0 to 9), ., _, and -
        /// </summary>
        public static Regex RegexAlphanumeric = new Regex(@"^[a-zA-Z0-9._-]+$");

        /// <summary>
        /// This Regex accept letters (a to z)
        /// </summary>
        public static Regex RegexLowerCase = new Regex(@"^[a-z]+$");

        /// <summary>
        /// This Regex accepts only numbers (0 to 9)
        /// </summary>
        public static Regex RegexOnlyNumbers = new Regex(@"^[0-9]+$"); 
        #endregion


        /// <summary>
        /// This is the FiyiStack GUID
        /// </summary>
        public static Guid FiyiStackGUID = new Guid("E6C09DFE-3A3E-461B-B3F9-734AEE05FC7B");
    }
}
