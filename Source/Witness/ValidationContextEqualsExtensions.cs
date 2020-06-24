namespace Witness
{
    using System;
    using System.Collections;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// <see cref="IValidationContext{T,TR}"/> extensions.
    /// </summary>
    public static class ValidationContextEqualsExtensions
    {
        /// <summary>
        /// Add validation that the specified field equals some value.
        /// </summary>
        /// <param name="input">Input validation pipeline function.</param>
        /// <param name="value">Value to compare.</param>
        /// <param name="comparer">Equality comparer.</param>
        /// <typeparam name="T">Type of the root OUV.</typeparam>
        /// <typeparam name="TR">Type of the current OUV.</typeparam>
        /// <returns>Resulting validation pipeline function.</returns>
        [Pure]
        public static Func<IValidationContext<T, TR>> ShouldBeEqual<T, TR>(
            this Func<IValidationContext<T, TR>> input, TR value, IEqualityComparer comparer = null)
            where TR : IComparable<TR>
        {
            return () =>
            {
                var c = input();

                bool equals = Compare(value, c.OUV, comparer);
                
                if (!equals)
                {
                    c.ValidationErrors.Add($"{c.OUVName} should be equal to {value}");
                }

                return c;
            };
        }
        
        /// <summary>
        /// Add validation that the specified field does not equal some value.
        /// </summary>
        /// <param name="input">Input validation pipeline function.</param>
        /// <param name="value">Value to compare.</param>
        /// <param name="comparer">Equality comparer.</param>
        /// <typeparam name="T">Type of the root OUV.</typeparam>
        /// <typeparam name="TR">Type of the current OUV.</typeparam>
        /// <returns>Resulting validation pipeline function.</returns>
        [Pure]
        public static Func<IValidationContext<T, TR>> ShouldNotBeEqual<T, TR>(
            this Func<IValidationContext<T, TR>> input, TR value, IEqualityComparer comparer = null)
            where TR : IComparable<TR>
        {
            return () =>
            {
                var c = input();

                bool equals = Compare(value, c.OUV, comparer);
                
                if (equals)
                {
                    c.ValidationErrors.Add($"{c.OUVName} should not be equal to {value}");
                }

                return c;
            };
        }
        
        private static bool Compare(object comparisonValue, object propertyValue, IEqualityComparer comparer) 
        {
            if (comparer != null) 
            {
                return comparer.Equals(comparisonValue, propertyValue);
            }

            return Equals(comparisonValue, propertyValue);
        }
    }
}