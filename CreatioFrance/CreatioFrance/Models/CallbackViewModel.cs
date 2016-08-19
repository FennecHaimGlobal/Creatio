using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Linq;

namespace CreatioFrance.Models
{
    public class CallbackViewModel
    {
        [Required]
        [Phone]
        [StringLength(10, ErrorMessage = "The {0} must be {2} characters long.", MinimumLength = 10)]
        [RegularExpression("^[0]([1-7]|[9])+[0-9]*$", ErrorMessage = "Le Telephone n'est pas valide")]
        [HasNoRepeatSequence]
        [HasNoConsecutiveSequenceAttribute]
        [Display(Name = "Telephone")]
        public string PhoneNumber { get; set; }
    }

    #region Custom Validation Attributes

	    // Summary:
        //     Specifies that a data field value is required.
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
        public class HasNoRepeatSequenceAttribute : ValidationAttribute
        {

            // Summary:
            //     Checks that the value of the required data field has no repeat number sequences.
            //
            // Parameters:
            //   value:
            //     The data field value to validate.
            //
            // Returns:
            //     true if validation is successful; otherwise, false.
            //
            // Exceptions:
            //   System.ComponentModel.DataAnnotations.ValidationException:
            //     The data field value was null.
            public override bool IsValid(object value)
            {
                bool isValid = true;
                int maxRepeatAllowed = 4;
                try
                {
                    var strValue = value.ToString();
                    var badMatch = new Regex("(.)\\1{4}");
                    isValid = !badMatch.IsMatch(strValue);

                }
                catch (Exception ex)
                {
                    isValid = false;
                    throw ex;
                }
                return isValid;
            }

            public override string FormatErrorMessage(string name)
            {
                return String.Format("{0} can not have a number that repeats more than 4 times in a sequence.", name);
            }
        }

        // Summary:
        //     Specifies that a data field value is required.
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
        public class HasNoConsecutiveSequenceAttribute : ValidationAttribute
        {

            // Summary:
            //     Checks that the value of the required data field has no logical consecutive sequences.
            //
            // Parameters:
            //   value:
            //     The data field value to validate.
            //
            // Returns:
            //     true if validation is successful; otherwise, false.
            //
            // Exceptions:
            //   System.ComponentModel.DataAnnotations.ValidationException:
            //     The data field value was null.
            public override bool IsValid(object value)
            {
                bool isValid = true;
                int maxRepeatAllowed = 5;
                try
                {
                    var chrList = value.ToString().ToCharArray();

                    var isNotValid = chrList
                    .GroupWhile((x, y) => y - x == 1)
                    .Select(x => new { i = x.First(), len = x.Count() })
                    .Any(x => x.len > 5);

                    isNotValid = isNotValid || chrList
                        .GroupWhile((x, y) => x - y == 1)
                        .Select(x => new { i = x.First(), len = x.Count() })
                        .Any(x => x.len > 5);


                    isValid = !isNotValid;
                }
                catch (Exception ex)
                {
                    isValid = false;
                    throw ex;
                }
                return isValid;
            }

            public override string FormatErrorMessage(string name)
            {
                return String.Format("{0} can not have a consecutive sequence.", name);
            }
        }

	#endregion

    #region Helpers
        public static class ValidationHelpers
        {

            public static IEnumerable<IEnumerable<T>> GroupWhile<T>(this IEnumerable<T> seq, Func<T, T, bool> condition)
            {
                T prev = seq.First();
                List<T> list = new List<T>() { prev };

                foreach (T item in seq.Skip(1))
                {
                    if (condition(prev, item) == false)
                    {
                        yield return list;
                        list = new List<T>();
                    }
                    list.Add(item);
                    prev = item;
                }

                yield return list;
            }
        }
    #endregion
}
