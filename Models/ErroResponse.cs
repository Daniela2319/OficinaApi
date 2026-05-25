namespace OficinalAPI.Models
{
    /// <summary>
    /// DTO para retornar erros padronizados
    /// </summary>
    public class ErroResponse
    {
        /// <summary>
        /// Código do erro
        /// </summary>
        public string Codigo { get; set; }

        /// <summary>
        /// Mensagem de erro
        /// </summary>
        public string Mensagem { get; set; }

        /// <summary>
        /// Detalhes do erro (validações)
        /// </summary>
        public List<string> Detalhes { get; set; } = new();

        /// <summary>
        /// Timestamp do erro
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.Now;

        public ErroResponse(string codigo, string mensagem)
        {
            Codigo = codigo;
            Mensagem = mensagem;
        }

        public ErroResponse(string codigo, string mensagem, List<string> detalhes)
        {
            Codigo = codigo;
            Mensagem = mensagem;
            Detalhes = detalhes;
        }
    }
}
