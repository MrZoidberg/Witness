namespace Witness
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface for validation context.
    /// </summary>
    /// <typeparam name="T">Type of the root object under validation.</typeparam>
    /// <typeparam name="TR">Type of the current object under validation.</typeparam>
    public interface IValidationContext<T, TR>
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

        /// <summary>
        /// Gets list of validation errors.
        /// </summary>
        List<string> ValidationErrors { get; }
    }
}