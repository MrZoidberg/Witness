namespace Witness
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Validation context extensions for dependency injection.
    /// </summary>
    public static class ValidationContextExtensions
    {
        private const string ServiceProviderKey = "_ServiceProvider";

        /// <summary>
        /// Get service of type T from the attached service provider.
        /// </summary>
        /// <param name="context">Validation pipeline function.</param>
        /// <typeparam name="T">The type of the service object to get.</typeparam>
        /// <returns>Service of type T.</returns>
        public static T Resolve<T>(this IValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var serviceProvider = context.ContextData[ServiceProviderKey] as IServiceProvider;
            if (serviceProvider == null)
            {
                throw new Exception("Validation context does not have IServiceProvider instance. Please use WithServiceProvider method in the beginning of the validation pipeline");
            }

            return serviceProvider.GetService<T>();
        }
        
        /// <summary>
        /// Add service provider to the validation context to resolve dependencies.
        /// </summary>
        /// <param name="context">Input validation pipeline function.</param>
        /// <param name="serviceProvider"><see cref="IServiceProvider"/> instance.</param>
        /// <typeparam name="T1">Type of the root OUV.</typeparam>
        /// <typeparam name="T2">Type of the current OUV for input function.</typeparam>
        /// <returns>Resulting validation pipeline function.</returns>
        public static Func<IValidationContext<T1, T2>> WithServiceProvider<T1, T2>(this Func<IValidationContext<T1, T2>> context, IServiceProvider serviceProvider)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return () =>
            {
                var c = context();
                c.ContextData[ServiceProviderKey] = serviceProvider;

                return c;
            };
        }
    }
}