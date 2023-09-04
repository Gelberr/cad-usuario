namespace CadUsuario.Modulos
{
    public class Usuario
    {
        public long Id_Usuario { get; set; }
        public string Nome_Usuario { get; set; }
        public string Login_Usuario { get; set; }
        public string Senha_Usuario { get; set; }
        public string Email_Usuario { get; set; }
        public string Telefone_Usuario { get; set; }
        public string Cpf_Usuario { get; set; }
        public DateTime Data_Nascimento_Usuario { get; set; }
        public string Nome_Mae_Usuario { get; set; }
        public string Status_Usuario { get; set; }
        public DateTime Data_Inclusao_Usuario { get; set; }
        public DateTime Data_Alteracao_Usuario { get; set; }
        public int Idade_Usuario { get; set; }
    }
}
