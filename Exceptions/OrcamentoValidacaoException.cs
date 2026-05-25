namespace OficinalAPI.Exceptions
{
    /// <summary>
    /// Exceção de validação de dados
    /// </summary>
    public class OrcamentoValidacaoException : Exception
    {
        /// <summary>
        /// Lista de erros de validação
        /// </summary>
        public List<string> Erros { get; set; } = new();

        public OrcamentoValidacaoException(string mensagem) : base(mensagem)
        {
        }

        public OrcamentoValidacaoException(string mensagem, List<string> erros) : base(mensagem)
        {
            Erros = erros;
        }
    }
}
