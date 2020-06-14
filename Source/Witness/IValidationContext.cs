namespace Witness
{
    using System.Collections.Generic;

    /// <summary>
    /// Base interface for validation context.
    /// </summary>
    public interface IValidationContext
    {
        /// <summary>
        /// Gets list of validation errors.
        /// </summary>
        List<string> ValidationErrors { get; }
        
        /// <summary>
        /// Gets context data dictionary.
        /// </summary>
        Dictionary<string, object> ContextData { get; }
    }

    /// <summary>
    /// Interface for validation context.
    /// </summary>
    /// <typeparam name="T">Type of the root object under validation.</typeparam>
    /// <typeparam name="TR">Type of the current object under validation.</typeparam>
    public interface IValidationContext<T, TR> : IValidationContext
    {
        /// <summary>
        /// Gets or sets root object under validation.
        /// </summary>
        T RootOUV { get; set; }

        /// <summary>
        /// Gets or sets object under validation.
        /// </summary>
        TR OUV { get; set; }

        /// <summary>
        /// Gets or sets OUV property name.
        /// </summary>
        string OUVName { get; set; }
    }
}