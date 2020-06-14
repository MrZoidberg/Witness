namespace Witness
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Validation context object.
    /// </summary>
    /// <typeparam name="T">Type of the root object under validation.</typeparam>
    /// <typeparam name="TR">Type of the current object under validation.</typeparam>
    /// <inheritdoc/>
    public class ValidationContext<T, TR> : IValidationContext<T, TR>
        where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationContext{T, TR}"/> class.
        /// </summary>
        /// <param name="rootUov">Root UOV.</param>
        /// <exception cref="ArgumentException">Thrown when root OUV has wrong type.</exception>
        public ValidationContext(TR rootUov)
        {
            if (!rootUov.GetType().IsEquivalentTo(typeof(T)))
            {
                throw new ArgumentException($"T should be {typeof(T).Name} to use this constructor", nameof(rootUov));
            }

            this.RootOUV = rootUov as T;
            this.OUV = rootUov;
            this.OUVName = typeof(T).Name;
            this.ValidationErrors = new List<string>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationContext{T, TR}"/> class.
        /// </summary>
        /// <param name="rootUov">Root UOV type.</param>
        /// <param name="ouv">Current UOV.</param>
        /// <param name="ouvName">Current OUV name.</param>
        /// <param name="validationErrors">Validation errors.</param>
        internal ValidationContext(T rootUov, TR ouv, string ouvName, List<string> validationErrors)
        {
            this.RootOUV = rootUov;
            this.OUV = ouv;
            this.OUVName = ouvName;
            this.ValidationErrors = validationErrors;
        }

        /// <inheritdoc/>
        public T RootOUV { get; set; }

        /// <inheritdoc/>
        public TR OUV { get; set; }

        /// <inheritdoc/>
        public string OUVName { get; set; }

        /// <inheritdoc/>
        public List<string> ValidationErrors { get; private set; }
    }
}