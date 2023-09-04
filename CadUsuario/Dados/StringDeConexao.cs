namespace CadUsuario.Dados
{
    public static class StringDeConexao
    {
        public static string conString { get; private set; }

        public static void SetConString(string constring)
        {
            conString = constring;
        }
    }
}
