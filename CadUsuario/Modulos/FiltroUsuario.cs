namespace CadUsuario.Modulos
{
    public class FiltroUsuario
    {
        public string? nome { get; set; }
        public string? cpf { get; set; }
        public string? login { get; set; }
        public string? status { get; set; }
        public FiltroUsuarioIntervaloData? dataNascimentoIntervalo { get; set; } = new FiltroUsuarioIntervaloData();
        public FiltroUsuarioIntervaloData? dataInclusaoIntervalo { get; set; } = new FiltroUsuarioIntervaloData();
        public FiltroUsuarioIntervaloData? dataAlteracaoIntervalo { get; set; } = new FiltroUsuarioIntervaloData();
        public FiltroUsuarioIntervaloIdade? idadeIntervalo { get; set; } = new FiltroUsuarioIntervaloIdade();
    }
}
