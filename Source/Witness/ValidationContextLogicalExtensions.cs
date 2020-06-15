namespace Witness
{
    using System;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// <see cref="IValidationContext{T,TR}"/> extensions.
    /// </summary>
    public static class ValidationContextLogicalExtensions
    {
        /// <summary>
        /// Implement boolean condition for the validation pipeline.
        /// </summary>
        /// <param name="input">Input validation pipeline function.</param>
        /// <param name="condition">Condition to check.</param>
        /// <param name="positive">Validation pipeline to execute for positive check.</param>
        /// <typeparam name="T">Type of the root OUV.</typeparam>
        /// <typeparam name="T11">Type of the current OUV for input function.</typeparam>
        /// <typeparam name="T22">Type of the current OUV for additional validation function.</typeparam>
        /// <returns>Resulting validation pipeline function.</returns>
        [Pure]
        public static Func<IValidationContext<T, T11>> If<T, T11, T22>(
            this Func<IValidationContext<T, T11>> input,
            Func<IValidationContext<T, T11>, bool> condition,
            Func<Func<IValidationContext<T, T11>>, Func<IValidationContext<T, T22>>> positive)
        {
            return () =>
            {
                var result = input();
                if (condition(result))
                {
                    positive(input)();
                }

                return result;
            };
        }

        /// <summary>
        /// Implements AND logic for the validation pipeline by returning the validation pipeline for the root OUV.
        /// </summary>
        /// <param name="input">Input validation pipeline function.</param>
        /// <typeparam name="T">Type of the root OUV.</typeparam>
        /// <typeparam name="T1">Type of the current OUV for input function.</typeparam>
        /// <returns>Resulting validation pipeline function.</returns>
        public static Func<IValidationContext<T, T>> And<T, T1>(this Func<IValidationContext<T, T1>> input)
            where T : class
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            
            return () =>
            {
                var c = input();
                return new ValidationContext<T, T>(
                    c.RootOUV, 
                    c.RootOUV, 
                    c.RootOUV.GetType().Name, 
                    c.ValidationErrors,
                    c.ContextData);
            };
        }
    }
}