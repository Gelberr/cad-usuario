using System.Net;

namespace CadUsuario.Funcoes
{
    public class ResultadoDados
    {
        public string? Error { get; set; }
        public int Status { get; set; } 
        public bool Success { get; set; } 
        public object? Data { get; set; }
        public List<string> Errors { get; set; }

        public ResultadoDados()
        {
            Status = (int)HttpStatusCode.OK;
            Success = true;
            Errors = new List<string>();
        }


        public void InternalError()
        {
            Status = (int)HttpStatusCode.InternalServerError;
            Success = false;
            Error = "Erro interno do servidor(Internal Server Error)";
        }

        public void BadRequest()
        {
            Status = (int)HttpStatusCode.BadRequest;
            Success = false;
            Error = "Solicitação ruim(BadRequest)";
        }

        public void NotFound()
        {
            Status = (int)HttpStatusCode.NotFound;
            Success = false;
            Error = "Dado não encontrado(Data was not found)";
        }

        public void ErroDiversos(string mensagem)
        {
            Status = (int)HttpStatusCode.BadRequest;
            Success = false;
            Error = mensagem;
        }

        public void AdicionarErrosValidacao(string error)
        {
            Status = (int)HttpStatusCode.BadRequest;
            Success = false;
            Errors.Add(error);
        }

        public void AdicionarErrosStatisValidacao(string error, int status)
        {
            Status = status;
            Success = false;
            Errors.Add(error);
        }
    }
}
