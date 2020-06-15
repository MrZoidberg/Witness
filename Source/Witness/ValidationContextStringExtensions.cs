namespace Witness
{
    using System;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// <see cref="IValidationContext{T,TR}"/> extensions.
    /// </summary>
    public static class ValidationContextStringExtensions
    {
        /// <summary>
        /// Add validation that the specified string is not null or empty.
        /// </summary>
        /// <param name="input">Input validation pipeline function.</param>
        /// <typeparam name="T">Type of the root OUV.</typeparam>
        /// <returns>Resulting validation pipeline function.</returns>
        [Pure]
        public static Func<IValidationContext<T, string>> ShouldNotBeEmptyOrNull<T>(
            this Func<IValidationContext<T, string>> input)
        {
            return () =>
            {
                var c = input();
                if (string.IsNullOrEmpty(c.OUV))
                {
                    c.ValidationErrors.Add($"{c.OUVName} cannot be empty or null");
                }

                return c;
            };
        }

        /// <summary>
        /// Add validation that the specified string is not null or empty.
        /// </summary>
        /// <param name="input">Input validation pipeline function.</param>
        /// <param name="from">Minimal string length.</param>
        /// <param name="to">Maximal string length.</param>
        /// <typeparam name="T">Type of the root OUV.</typeparam>
        /// <returns>Resulting validation pipeline function.</returns>
        [Pure]
        public static Func<IValidationContext<T, string>> ShouldHaveLengthWithin<T>(
            this Func<IValidationContext<T, string>> input, uint from, uint to)
        {
            if (from > to)
            {
                throw new ArgumentException("`from` value cannot be bigger than `to`");
            }

            return () =>
            {
                var c = input();
                int len = c.OUV?.Length ?? 0;
                
                if (len < from)
                {
                    c.ValidationErrors.Add($"{c.OUVName} length cannot be less than {from}");
                }
                
                if (len > to)
                {
                    c.ValidationErrors.Add($"{c.OUVName} length cannot be bigger than {to}");
                }

                return c;
            };
        }
    }
}