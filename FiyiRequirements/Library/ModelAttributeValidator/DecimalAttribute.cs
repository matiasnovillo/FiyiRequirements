using System;
using System.ComponentModel.DataAnnotations;

namespace FiyiRequirements.Library.ModelAttributeValidator
{
    public class DecimalAttribute : ValidationAttribute
    {
        private string _PropertyName;
        private decimal _MinimumDecimalNumber;
        private decimal _MaximumDecimalNumber;
        private bool _Required;
        public DecimalAttribute(string PropertyName, bool Required, double DecimalMin = (double)Constant.DecimalMINValue, double DecimalMax = (double)Constant.DecimalMAXValue)
        {
            try
            {
                if (PropertyName == null) { throw new Exception("The property name is empty"); }
                if (PropertyName.Length < 0) { throw new Exception($"The length of property name must be equal or greater than 0"); }
                if (PropertyName.Length > int.MaxValue) { throw new Exception($"The length of property name must be equal or less than int.MaxValue"); }

                _PropertyName = PropertyName;

                if (DecimalMin < (double)Constant.DecimalMINValue || DecimalMin > (double)Constant.DecimalMAXValue)
                {
                    throw new Exception("The validator MinimumDecimalNumber must be inside decimalDBMin and decimalDBMax");
                }
                if (DecimalMax < (double)Constant.DecimalMINValue || DecimalMax > (double)Constant.DecimalMAXValue)
                {
                    throw new Exception("The validator MaximumDecimalNumber must be inside decimalDBMin and decimalDBMax");
                }
                _MinimumDecimalNumber = (decimal)DecimalMin;
                _MaximumDecimalNumber = (decimal)DecimalMax;

                _Required = Required;
            }
            catch (Exception) { throw; }
        }

        public override bool IsValid(object? objDecimal)
        {
            try
            {
                if (_Required)
                {
                    if (objDecimal == null) { throw new Exception($"{_PropertyName} is empty"); }
                }
                if (objDecimal.GetType() != typeof(decimal)) { throw new Exception($"{_PropertyName} is not a valid decimal number"); }

                if ((decimal)objDecimal < Constant.DecimalMINValue || (decimal)objDecimal > Constant.DecimalMAXValue)
                {
                    throw new Exception($"{_PropertyName} must be inside decimalDBMin and decimalDBMax");
                }
                if ((decimal)objDecimal < _MinimumDecimalNumber || (decimal)objDecimal > _MaximumDecimalNumber)
                {
                    throw new Exception($"{_PropertyName} must be inside {_MinimumDecimalNumber} and {_MaximumDecimalNumber}");
                }
                return true;
            }
            catch (Exception) { throw; }
        }
    }
}
