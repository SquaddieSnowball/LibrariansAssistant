namespace LibrariansAssistant.InfranstructureLayer.ObjectRelationalMappers.Exceptions;

/// <summary>
/// The exception that is thrown when an error occurs during data mapping.
/// </summary>
[Serializable]
public sealed class MappingException : Exception
{
    /// <summary>
    /// Initializes a new instance of the MappingException class.
    /// </summary>
    public MappingException() { }

    /// <summary>
    /// Initializes a new instance of the MappingException class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public MappingException(string message) : base(message) { }

    /// <summary>
    /// Initializes a new instance of the MappingException class with a specified error message 
    /// and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of this exception. If the innerException parameter 
    /// is not a null reference, the current exception is raised in a catch block that handles the inner exception.</param>
    public MappingException(string message, Exception innerException) : base(message, innerException) { }
}