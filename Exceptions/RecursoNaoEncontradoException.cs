namespace OficinalAPI.Exceptions
{
    /// <summary>
    /// Exceção para recursos não encontrados
    /// </summary>
    public class RecursoNaoEncontradoException : Exception
    {
        public RecursoNaoEncontradoException(string mensagem) : base(mensagem)
        {
        }
    }
}
