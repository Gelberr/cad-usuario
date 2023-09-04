using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Reflection.Metadata;



namespace CadUsuario.Funcoes
{
    public static class CodigosGlobais
    {

        public static bool ValidarCPf(string cpf)
        {
            try
            {
                if (string.IsNullOrEmpty(cpf))
                {
                    return false;
                }
                else if (!long.TryParse(cpf, out long intret))
                {
                    return false;
                }

                int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                string tempCpf;
                string digito;
                int soma;
                int resto;

                cpf = cpf.Trim();

                cpf = cpf.Replace(".", "").Replace("-", "");

                if (cpf.Length != 11)
                {
                    return false;
                }
                tempCpf = cpf.Substring(0, 9);
                soma = 0;

                for (int i = 0; i < 9; i++)
                {
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
                }
                resto = soma % 11;
                if (resto < 2)
                {
                    resto = 0;
                }
                else
                {
                    resto = 11 - resto;
                }
                digito = resto.ToString();
                tempCpf = tempCpf + digito;
                soma = 0;

                for (int i = 0; i < 10; i++)
                {
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
                }
                resto = soma % 11;
                if (resto < 2)
                {
                    resto = 0;
                }
                else
                {
                    resto = 11 - resto;
                }

                digito = digito + resto.ToString();

                return cpf.EndsWith(digito);
            }

            catch (Exception e)
            {
                throw;
            }
        }

        public static int ObterIdade(DateTime dataNacimento)
        {
            int idade = 0;

            idade = DateTime.Now.Year - dataNacimento.Year;

            // Se o dia de nascimento for superior a data de hoje então devemos diminuir uma unidade da idade.
            if (DateTime.Now.DayOfYear < dataNacimento.DayOfYear)
            {
                idade = idade - 1;
            }

            return idade;
        }

        public static string TratarDataParaComparacoesInicio(string data)
        {

            try
            {
                string dataTratada = "";

                DateTime validacao = new DateTime();


                if (!string.IsNullOrEmpty(data) && DateTime.TryParseExact(data, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out validacao))
                {
                    dataTratada = validacao.ToString("yyyy-dd-MM 00:00:00", CultureInfo.InvariantCulture);

                    return dataTratada;
                }
                else if (!string.IsNullOrEmpty(data) && DateTime.TryParseExact(data, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out validacao))
                {
                    dataTratada = validacao.ToString("yyyy-dd-MM 00:00:00", CultureInfo.InvariantCulture);
                    return dataTratada;
                }
                else if (!string.IsNullOrEmpty(data) && DateTime.TryParseExact(data, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out validacao))
                {
                    dataTratada = validacao.ToString("yyyy-dd-MM 00:00:00", CultureInfo.InvariantCulture);
                    return dataTratada;
                }
                else if (!string.IsNullOrEmpty(data) && DateTime.TryParse(data, out validacao))
                {
                    dataTratada = validacao.ToString("yyyy-dd-MM 00:00:00", CultureInfo.InvariantCulture);
                    return dataTratada;
                }
                else
                {
                    return data;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string TratarDataParaComparacoesFinal(string data)
        {

            try
            {
                string dataTratada = "";

                DateTime validacao = new DateTime();


                if (!string.IsNullOrEmpty(data) && DateTime.TryParseExact(data, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out validacao))
                {
                    dataTratada = validacao.ToString("yyyy-dd-MM 23:59:59", CultureInfo.InvariantCulture);

                    return dataTratada;
                }
                else if (!string.IsNullOrEmpty(data) && DateTime.TryParseExact(data, "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out validacao))
                {
                    dataTratada = validacao.ToString("yyyy-dd-MM 23:59:59", CultureInfo.InvariantCulture);
                    return dataTratada;
                }
                else if (!string.IsNullOrEmpty(data) && DateTime.TryParseExact(data, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out validacao))
                {
                    dataTratada = validacao.ToString("yyyy-dd-MM 23:59:59", CultureInfo.InvariantCulture);
                    return dataTratada;
                }
                else if (!string.IsNullOrEmpty(data) && DateTime.TryParse(data, out validacao))
                {
                    dataTratada = validacao.ToString("yyyy-dd-MM 23:59:59", CultureInfo.InvariantCulture);
                    return dataTratada;
                }
                else
                {
                    return data;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }


        
    }
}
