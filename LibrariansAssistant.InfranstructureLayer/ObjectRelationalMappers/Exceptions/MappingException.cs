namespace LibrariansAssistant.InfranstructureLayer.ObjectRelationalMappers.Exceptions;

[Serializable]
public sealed class MappingException : Exception
{
    public MappingException() { }

    public MappingException(string message) : base(message) { }

    public MappingException(string message, Exception inner) : base(message, inner) { }
}