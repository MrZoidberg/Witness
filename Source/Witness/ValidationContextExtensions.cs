namespace Witness
{
    using System;

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
        public static IValidationContext<T, T2> Map<T, TR, T2>(this IValidationContext<T, TR> context,  Func<IValidationContext<T, TR>, T2> map, string valueName)
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
            return new ValidationContext<T, T2>(contextInstance.RootOUV, map(contextInstance), valueName, contextInstance.ValidationErrors);
        }

        [Pure]
        public static Func<IValidationContext<T, TR>> SetupValidation<T, TR>(this IValidationContext<T, TR> context)
        {
            return () => context;
        }

        [Pure]
        public static Func<IValidationContext<T, T22>> Should<T, T11, T22>(this Func<IValidationContext<T, T11>> input,
            Func<Func<IValidationContext<T, T11>>, IValidationContext<T, T22>> second)
        {
            return () => second(input);
        }

        [Pure]
        public static Func<IValidationContext<T, T11>> If<T, T11, T22>(this Func<IValidationContext<T, T11>> input, Func<IValidationContext<T, T11>, bool> condition,
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

        public static Func<IValidationContext<T, T>> And<T, TIN>(this Func<IValidationContext<T, TIN>> input) where T : class
        {
            return () => new ValidationContext<T ,T>(input().RootOUV);
        }

        [Pure]
        public static Func<IValidationContext<T,string>> ShouldNotBeEmptyOrNull<T>(this Func<IValidationContext<T, string>> input)
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

        [Pure]
        public static (bool IsValid, string[] ValidationErrors) ExecuteValidation<T1, T11>(this Func<IValidationContext<T1, T11>> context)
        {
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