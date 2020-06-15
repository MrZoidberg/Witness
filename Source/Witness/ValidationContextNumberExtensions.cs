namespace Witness
{
    using System;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// <see cref="IValidationContext{T,TR}"/> extensions.
    /// </summary>
    public static class ValidationContextNumberExtensions
    {
        /// <summary>
        /// Add validation that the specified value is in valid range.
        /// </summary>
        /// <param name="input">Input validation pipeline function.</param>
        /// <param name="from">Minimal value.</param>
        /// <param name="to">Maximal value.</param>
        /// <typeparam name="T">Type of the root OUV.</typeparam>
        /// <typeparam name="TR">Type of the current OUV.</typeparam>
        /// <returns>Resulting validation pipeline function.</returns>
        [Pure]
        public static Func<IValidationContext<T, TR>> ShouldBeInRange<T, TR>(
            this Func<IValidationContext<T, TR>> input, TR from, TR to)
            where TR : IComparable<TR>
        {
            if (from.CompareTo(to) == 1)
            {
                throw new ArgumentException("`from` value cannot be bigger than `to`");
            }

            return () =>
            {
                var c = input();

                if (c.OUV.CompareTo(from) == -1)
                {
                    c.ValidationErrors.Add($"{c.OUVName} length cannot be less than {from}");
                }
                
                if (c.OUV.CompareTo(to) == 1)
                {
                    c.ValidationErrors.Add($"{c.OUVName} length cannot be bigger than {to}");
                }

                return c;
            };
        }

        /// <summary>
        /// Add validation that the specified integer value is in valid range.
        /// </summary>
        /// <param name="input">Input validation pipeline function.</param>
        /// <param name="from">Minimal value.</param>
        /// <param name="to">Maximal value.</param>
        /// <typeparam name="T">Type of the root OUV.</typeparam>
        /// <returns>Resulting validation pipeline function.</returns>
        [Pure]
        public static Func<IValidationContext<T, int>> ShouldBeInRange<T>(
            this Func<IValidationContext<T, int>> input, int from, int to)
        {
            return input.ShouldBeInRange<T, int>(from, to);
        }
        
        /// <summary>
        /// Add validation that the specified uint value is in valid range.
        /// </summary>
        /// <param name="input">Input validation pipeline function.</param>
        /// <param name="from">Minimal value.</param>
        /// <param name="to">Maximal value.</param>
        /// <typeparam name="T">Type of the root OUV.</typeparam>
        /// <returns>Resulting validation pipeline function.</returns>
        [Pure]
        public static Func<IValidationContext<T, uint>> ShouldBeInRange<T>(
            this Func<IValidationContext<T, uint>> input, uint from, uint to)
        {
            return input.ShouldBeInRange<T, uint>(from, to);
        }
        
        /// <summary>
        /// Add validation that the specified double value is in valid range.
        /// </summary>
        /// <param name="input">Input validation pipeline function.</param>
        /// <param name="from">Minimal value.</param>
        /// <param name="to">Maximal value.</param>
        /// <typeparam name="T">Type of the root OUV.</typeparam>
        /// <returns>Resulting validation pipeline function.</returns>
        [Pure]
        public static Func<IValidationContext<T, double>> ShouldBeInRange<T>(
            this Func<IValidationContext<T, double>> input, double from, double to)
        {
            return input.ShouldBeInRange<T, double>(from, to);
        }
    }
}