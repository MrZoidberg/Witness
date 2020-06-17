namespace Witness
{
    using System;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// <see cref="IValidationContext{T,TR}"/> extensions.
    /// </summary>
    public static class ValidationContextBooleanExtensions
    {
        /// <summary>
        /// Add validation that the specified boolean property is true.
        /// </summary>
        /// <param name="input">Input validation pipeline function.</param>
        /// <typeparam name="T">Type of the root OUV.</typeparam>
        /// <returns>Resulting validation pipeline function.</returns>
        [Pure]
        public static Func<IValidationContext<T, bool>> ShouldBeTrue<T>(
            this Func<IValidationContext<T, bool>> input)
        {
            return () =>
            {
                var c = input();

                if (!c.OUV)
                {
                    c.ValidationErrors.Add($"{c.OUVName} should be `true`");
                }

                return c;
            };
        }
        
        /// <summary>
        /// Add validation that the specified boolean property is false.
        /// </summary>
        /// <param name="input">Input validation pipeline function.</param>
        /// <typeparam name="T">Type of the root OUV.</typeparam>
        /// <returns>Resulting validation pipeline function.</returns>
        [Pure]
        public static Func<IValidationContext<T, bool>> ShouldBeFalse<T>(
            this Func<IValidationContext<T, bool>> input)
        {
            return () =>
            {
                var c = input();

                if (c.OUV)
                {
                    c.ValidationErrors.Add($"{c.OUVName} should be `false`");
                }

                return c;
            };
        }
    }
}