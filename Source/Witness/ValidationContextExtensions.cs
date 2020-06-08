// ReSharper disable UnusedMember.Global
namespace Witness
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// <see cref="IValidationContext{T,TR}"/> extensions.
    /// </summary>
    public static class ValidationContextExtensions
    {
        /// <summary>
        /// Map validation context to change object under validation.
        /// </summary>
        /// <param name="context">Current validation context.</param>
        /// <param name="map">Map function.</param>
        /// <param name="valueName">New OUV name.</param>
        /// <typeparam name="T">Type of the root OUV.</typeparam>
        /// <typeparam name="TR">Type of current OUV.</typeparam>
        /// <typeparam name="T2">Type of mapped OUV.</typeparam>
        /// <returns>Validation context with T2 OUV.</returns>
        [Pure]
        public static IValidationContext<T, T2> Map<T, TR, T2>(
            this IValidationContext<T, TR> context,
            Func<IValidationContext<T, TR>, T2> map, 
            string valueName)
            where T : class
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (map == null)
            {
                throw new ArgumentNullException(nameof(map));
            }

            var contextInstance = context;
            return new ValidationContext<T, T2>(
                contextInstance.RootOUV, 
                map(contextInstance), 
                valueName,
                contextInstance.ValidationErrors);
        }

        /// <summary>
        /// Initialize setup of validation pipeline for the validation context object.
        /// </summary>
        /// <param name="context">Validation context object.</param>
        /// <typeparam name="T">Type of the root OUV (object under validation).</typeparam>
        /// <typeparam name="TR">Type of the current OUV.</typeparam>
        /// <returns>Validation pipeline function.</returns>
        [Pure]
        public static Func<IValidationContext<T, TR>> SetupValidation<T, TR>(this IValidationContext<T, TR> context)
        {
            return () => context;
        }

        /// <summary>
        /// Add an abstract function to the validation pipeline.
        /// </summary>
        /// <param name="input">Input validation pipeline function.</param>
        /// <param name="second">Validation pipeline function to add.</param>
        /// <typeparam name="T">Type of the root OUV.</typeparam>
        /// <typeparam name="T11">Type of the current OUV for input function.</typeparam>
        /// <typeparam name="T22">Type of the current OUV for additional validation function.</typeparam>
        /// <returns>Resulting validation pipeline function.</returns>
        [Pure]
        public static Func<IValidationContext<T, T22>> Should<T, T11, T22>(
            this Func<IValidationContext<T, T11>> input,
            Func<Func<IValidationContext<T, T11>>, IValidationContext<T, T22>> second)
        {
            return () => second(input);
        }

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
            return () => new ValidationContext<T, T>(input().RootOUV);
        }

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
                var result = input();
                if (string.IsNullOrEmpty(result.OUV))
                {
                    throw new ValidationException($"{result.OUVName} cannot be empty or null");
                }

                return result;
            };
        }

        /// <summary>
        /// Execute validation pipeline.
        /// </summary>
        /// <param name="context">Validation context.</param>
        /// <typeparam name="T">Type of the root OUV.</typeparam>
        /// <typeparam name="T1">Type of the current OUV for input function.</typeparam>
        /// <returns>Is valid flag and array of validation errors.</returns>
        [Pure]
        [SuppressMessage("ReSharper", "CA1031", Justification = "It's not know what exception will occur")]
        public static (bool IsValid, string[] ValidationErrors) ExecuteValidation<T, T1>(
            this Func<IValidationContext<T, T1>> context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            try
            {
                context();
                return (true, Array.Empty<string>());
            }
            catch (Exception e)
            {
                return (false, new[] {e.Message});
            }
        }
    }
}