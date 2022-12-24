using System;
using System.Security.Cryptography;
using System.Text;

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
    public static class Security
    {
        /// <summary>
        /// Considerations:  <br/>
        /// 1. Used to encode passwords or sensible data. For more security ensure to pass UserName + UserPassword inside StringToEncode <br/>
        /// 2. One way encoding, that means that there is no way to decode the encoded string <br/>
        /// 3. Extremely secure if the input StringToEncode contains a unique ID, actual time or any kind of unpredictable data.
        /// <param name="StringToEncode"></param>
        /// <returns></returns>
        [Obsolete]
        public static string EncodeString(string StringToEncode)
        {
            try
            {
                UnicodeEncoding Encoder = new UnicodeEncoding();
                SHA512Managed SHA512 = new SHA512Managed();

                string EncodedString = Convert.ToBase64String(SHA512.ComputeHash(Encoder.GetBytes(StringToEncode)));

                return EncodedString;
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// [NEED TEST INSIDE HTTP PROTOCOLS AS A JSON VARIABLE] <br/>
        /// 1. Extremely secure  <br/>
        /// 2. The returned string contains VERY odd symbols
        /// </summary>
        /// <returns></returns>
        public static string GenerateRandomString()
        {
            using (var Generator = RandomNumberGenerator.Create())
            {
                var RandomByteArray = new byte[64];
                Generator.GetBytes(RandomByteArray);

                return Encoding.Default.GetString(RandomByteArray);
            }
        }

        public enum EWaterMarkFor { MSSQLServer, CSharp, TypeScript };
        public static string WaterMark(EWaterMarkFor EWaterMarkFor, string GUID, string Token, string Owner, string CopyrightYear)
        {
            string WaterMark = "";
            switch (EWaterMarkFor)
            {
                case EWaterMarkFor.MSSQLServer:
                    WaterMark = $@"/*
 * GUID:{GUID}
 * Licensed to a unique person with this Token:{Token}
 * 
 * Coded by {Owner}
 * Copyright © {CopyrightYear}
 * 
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 * 
 * Auto generated code. It should not be modified from here.
 */";
                    break;
                case EWaterMarkFor.CSharp:
                    WaterMark = $@"/*
 * GUID:{GUID}
 * Licensed to a unique person with this Token:{Token}
 * 
 * Coded by {Owner}
 * Copyright © {CopyrightYear}
 * 
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 * 
 * Auto generated code. Add your custom code after the last line of auto generation
 */";
                    break;
                case EWaterMarkFor.TypeScript:
                    WaterMark = $@"/*
 * GUID:{GUID}
 * Licensed to a unique person with this Token:{Token}
 * 
 * Coded by {Owner}
 * Copyright © {CopyrightYear}
 * 
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 * 
 * Auto generated code. Add your custom code after the last line of auto generation
*/";
                    break;
                default:
                    break;
            }

            return WaterMark;
        }
    }
}
