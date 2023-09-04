using CadUsuario.Dados;
using CadUsuario.Funcoes;
using CadUsuario.Modulos;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Data.Common;

namespace CadUsuario.Repositorio
{
    public class UsuarioRepositorio
    {
        private Conexao _conn;

        public UsuarioRepositorio(Conexao conexao)
        {
            _conn = conexao;
        }

        internal bool AlterarUsuario(AlterarUsuario user)
        {
            try
            {
                string query = string.Format(@"UPDATE Cadastrar_Usuario SET 
                                                Nome_Usuario = @Nome_Usuario, 
                                                Login_Usuario = @Login_Usuario, 
                                                Senha_Usuario = @Senha_Usuario, 
                                                Email_Usuario = @Email_Usuario, 
                                                Telefone_Usuario = @Telefone_Usuario, 
                                                Cpf_Usuario = @Cpf_Usuario, 
                                                Data_Nascimento_Usuario = @Data_Nascimento_Usuario, 
                                                Nome_Mae_Usuario = @Nome_Mae_Usuario, 
                                                Status_Usuario = @Status_Usuario, 
                                                Data_Alteracao_Usuario = @Data_Alteracao_Usuario, 
                                                Idade_Usuario = @Idade_Usuario 
                                               WHERE Id_Usuario = @Id_Usuario"
                );

                int idadeUsu = CodigosGlobais.ObterIdade(user.Data_Nascimento_Usuario);

                List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Nome_Usuario", user.Nome_Usuario),
                    new SqlParameter("@Login_Usuario", user.Login_Usuario),
                    new SqlParameter("@Senha_Usuario", user.Senha_Usuario),
                    new SqlParameter("@Email_Usuario", user.Email_Usuario),
                    new SqlParameter("@Telefone_Usuario", user.Telefone_Usuario == null ? DBNull.Value:user.Telefone_Usuario),
                    new SqlParameter("@Cpf_Usuario", user.Cpf_Usuario),
                    new SqlParameter("@Data_Nascimento_Usuario", user.Data_Nascimento_Usuario == null ? DBNull.Value:user.Data_Nascimento_Usuario),
                    new SqlParameter("@Nome_Mae_Usuario", user.Nome_Mae_Usuario == null ? DBNull.Value:user.Nome_Mae_Usuario),
                    new SqlParameter("@Status_Usuario", user.Status_Usuario),
                    new SqlParameter("@Data_Alteracao_Usuario",DateTime.Now),
                    new SqlParameter("@Id_Usuario",user.Id_Usuario),
                    new SqlParameter("@Idade_Usuario",idadeUsu)
                };

                return _conn.ExecuteNonQueryTransaction(query, parameters) > 0;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        internal bool IncluirUsuario(CadastroUsuario user)
        {
            try
            {

                string query = @"INSERT INTO Cadastrar_Usuario(
                                Nome_Usuario, 
                                Login_Usuario, 
                                Senha_Usuario, 
                                Email_Usuario, 
                                Telefone_Usuario, 
                                Cpf_Usuario, 
                                Data_Nascimento_Usuario, 
                                Nome_Mae_Usuario, 
                                Status_Usuario, 
                                Data_Inclusao_Usuario,
                                Data_Alteracao_Usuario,
                                Idade_Usuario
                                )
                                VALUES
                                (
                                @Nome_Usuario, 
                                @Login_Usuario, 
                                @Senha_Usuario, 
                                @Email_Usuario, 
                                @Telefone_Usuario, 
                                @Cpf_Usuario, 
                                @Data_Nascimento_Usuario, 
                                @Nome_Mae_Usuario, 
                                @Status_Usuario, 
                                @Data_Inclusao_Usuario,
                                @Data_Alteracao_Usuario,
                                @Idade_Usuario);";

                int idadeUsu = CodigosGlobais.ObterIdade(user.Data_Nascimento_Usuario);

                List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Nome_Usuario", user.Nome_Usuario),
                    new SqlParameter("@Login_Usuario", user.Login_Usuario),
                    new SqlParameter("@Senha_Usuario", user.Senha_Usuario),
                    new SqlParameter("@Email_Usuario", user.Email_Usuario),
                    new SqlParameter("@Telefone_Usuario", user.Telefone_Usuario == null ? DBNull.Value:user.Telefone_Usuario),
                    new SqlParameter("@Cpf_Usuario", user.Cpf_Usuario),
                    new SqlParameter("@Data_Nascimento_Usuario", user.Data_Nascimento_Usuario == null ? DBNull.Value:user.Data_Nascimento_Usuario),
                    new SqlParameter("@Nome_Mae_Usuario", user.Nome_Mae_Usuario == null ? DBNull.Value:user.Nome_Mae_Usuario),
                    new SqlParameter("@Status_Usuario", user.Status_Usuario.ToString().ToUpper()),
                    new SqlParameter("@Data_Inclusao_Usuario", DateTime.Now),
                    new SqlParameter("@Data_Alteracao_Usuario",DateTime.Now),
                    new SqlParameter("@Idade_Usuario",idadeUsu)

                };

                return _conn.ExecuteNonQueryTransaction(query, parameters) > 0;
            }
            catch (Exception)
            {
                throw;
            }

        }

        internal List<Usuario>? ListarUsuario()
        {
            try
            {
                string query = @"SELECT 
                                Id_Usuario,
                                Nome_Usuario, 
                                Login_Usuario, 
                                Senha_Usuario, 
                                Email_Usuario, 
                                Telefone_Usuario, 
                                Cpf_Usuario, 
                                Data_Nascimento_Usuario, 
                                Nome_Mae_Usuario, 
                                Status_Usuario, 
                                Data_Inclusao_Usuario,
                                Data_Alteracao_Usuario;
                                Idade_Usuario 
                                FROM Cadastrar_Usuario;";

                List<Usuario> retorno = new List<Usuario>();

                using (SqlDataReader dataReader = _conn.ExecuteReader(query))
                {
                    while (dataReader.Read())
                    {
                        retorno.Add(new Usuario()
                        {
                            Id_Usuario = dataReader.GetInt16("Id_Usuario"),
                            Nome_Usuario = dataReader.GetString("Nome_Usuario"),
                            Login_Usuario = dataReader.GetString("Login_Usuario"),
                            Senha_Usuario = dataReader.GetString("Senha_Usuario"),
                            Email_Usuario = dataReader.GetString("Email_Usuario"),
                            Telefone_Usuario = dataReader.GetString("Telefone_Usuario"),
                            Cpf_Usuario = dataReader.GetString("Cpf_Usuario"),
                            Data_Nascimento_Usuario = dataReader.GetDateTime("Data_Nascimento_Usuario"),
                            Nome_Mae_Usuario = dataReader.GetString("Nome_Mae_Usuario"),
                            Status_Usuario = dataReader.GetString("Status_Usuario"),
                            Data_Inclusao_Usuario = dataReader.GetDateTime("Data_Inclusao_Usuario"),
                            Data_Alteracao_Usuario = dataReader.GetDateTime("Data_Alteracao_Usuario"),
                            Idade_Usuario = dataReader.GetInt32("Idade_Usuario")
                        });
                    }
                }

                
                return retorno.Count > 0 ? retorno : null;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        internal bool AlterarStatusUsuario(long idUsuario, char status)
        {
            try
            {
                string query = string.Format(@"UPDATE Cadastrar_Usuario SET 
                                               Status_Usuario = @Status_Usuario,
                                               Data_Alteracao_Usuario = @Data_Alteracao_Usuario
                                               WHERE Id_Usuario = @Id_Usuario"
                );

                List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Status_Usuario", status),
                    new SqlParameter("@Data_Alteracao_Usuario",DateTime.Now),
                    new SqlParameter("@Id_Usuario",idUsuario)

                };

                return _conn.ExecuteNonQueryTransaction(query, parameters) > 0;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        internal bool ExisteUsuarioCadastrado(string cpf)
        {
            try
            {
                string query = @"SELECT 
                                Id_Usuario 
                                from Cadastrar_Usuario 
                                where Cpf_Usuario = @Cpf_Usuario;";

                SqlParameter parameters = new SqlParameter("@Cpf_Usuario", cpf);

                bool retorno = false;

                using (SqlDataReader dataReader = _conn.ExecuteReader(query, parameters))
                {
                    if (dataReader.Read())
                    {
                        if (Convert.ToInt64(dataReader["Id_Usuario"]) > 0)
                        {
                            retorno = true;
                        }
                    }
                }


                return retorno;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        internal bool DelatarUsuario(long idUsuario)
        {
            try
            {
                string query = string.Format(@"UPDATE Cadastrar_Usuario SET 
                                               Status_Usuario = @Status_Usuario,
                                               Data_Alteracao_Usuario = @Data_Alteracao_Usuario
                                               WHERE Id_Usuario = @Id_Usuario"
                );

                char statusNovo = 'I';
                List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Status_Usuario", statusNovo),
                    new SqlParameter("@Data_Alteracao_Usuario",DateTime.Now),
                    new SqlParameter("@Id_Usuario",idUsuario)

                };

                return _conn.ExecuteNonQueryTransaction(query, parameters) > 0;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        internal Usuario? ListarUsuarioPorId(long idUsuario)
        {
            try
            {
                string query = @"SELECT 
                                Id_Usuario,
                                Nome_Usuario, 
                                Login_Usuario, 
                                Senha_Usuario, 
                                Email_Usuario, 
                                Telefone_Usuario, 
                                Cpf_Usuario, 
                                Data_Nascimento_Usuario, 
                                Nome_Mae_Usuario, 
                                Status_Usuario, 
                                Data_Inclusao_Usuario,
                                Data_Alteracao_Usuario,
                                Idade_Usuario 
                                FROM Cadastrar_Usuario 
                                WHERE Id_Usuario = @Id_Usuario;";

                Usuario retorno = new Usuario();

                SqlParameter parameters = new SqlParameter("@Id_Usuario", idUsuario);

                using (SqlDataReader dataReader = _conn.ExecuteReader(query,parameters))
                {
                    if (dataReader.Read())
                    {
                        retorno.Id_Usuario = dataReader.GetInt16("Id_Usuario");
                        retorno.Nome_Usuario = dataReader.GetString("Nome_Usuario");
                        retorno.Login_Usuario = dataReader.GetString("Login_Usuario");
                        retorno.Senha_Usuario = dataReader.GetString("Senha_Usuario");
                        retorno.Email_Usuario = dataReader.GetString("Email_Usuario");
                        retorno.Telefone_Usuario = dataReader.GetString("Telefone_Usuario");
                        retorno.Cpf_Usuario = dataReader.GetString("Cpf_Usuario");
                        retorno.Data_Nascimento_Usuario = dataReader.GetDateTime("Data_Nascimento_Usuario");
                        retorno.Nome_Mae_Usuario = dataReader.GetString("Nome_Mae_Usuario");
                        retorno.Status_Usuario = dataReader.GetString("Status_Usuario");
                        retorno.Data_Inclusao_Usuario = dataReader.GetDateTime("Data_Inclusao_Usuario");
                        retorno.Data_Alteracao_Usuario = dataReader.GetDateTime("Data_Alteracao_Usuario");
                        retorno.Idade_Usuario = dataReader.GetInt32("Idade_Usuario");
                    }
                }


                return retorno;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        internal Usuario? LogonUsuario(string login, string senha)
        {
            try
            {
                string query = @"SELECT 
                                Id_Usuario,
                                Nome_Usuario, 
                                Login_Usuario, 
                                Senha_Usuario, 
                                Email_Usuario, 
                                Telefone_Usuario, 
                                Cpf_Usuario, 
                                Data_Nascimento_Usuario, 
                                Nome_Mae_Usuario, 
                                Status_Usuario, 
                                Data_Inclusao_Usuario,
                                Data_Alteracao_Usuario, 
                                Idade_Usuario
                                FROM Cadastrar_Usuario 
                                ";


                query += $"WHERE Login_Usuario = '{login}' and Senha_Usuario = '{senha}';";

                Usuario retorno = new Usuario();

                //List<SqlParameter> parameters = new List<SqlParameter>()
                //{
                //    new SqlParameter("@Login_Usuario", login),
                //    new SqlParameter("@Senha_Usuario", senha)
                //};

                using (SqlDataReader dataReader = _conn.ExecuteReader(query))
                {
                    if (dataReader.Read())
                    {
                        retorno.Id_Usuario = dataReader.GetInt16("Id_Usuario");
                        retorno.Nome_Usuario = dataReader.GetString("Nome_Usuario");
                        retorno.Login_Usuario = dataReader.GetString("Login_Usuario");
                        retorno.Senha_Usuario = dataReader.GetString("Senha_Usuario");
                        retorno.Email_Usuario = dataReader.GetString("Email_Usuario");
                        retorno.Telefone_Usuario = dataReader.GetString("Telefone_Usuario");
                        retorno.Cpf_Usuario = dataReader.GetString("Cpf_Usuario");
                        retorno.Data_Nascimento_Usuario = dataReader.GetDateTime("Data_Nascimento_Usuario");
                        retorno.Nome_Mae_Usuario = dataReader.GetString("Nome_Mae_Usuario");
                        retorno.Status_Usuario = dataReader.GetString("Status_Usuario");
                        retorno.Data_Inclusao_Usuario = dataReader.GetDateTime("Data_Inclusao_Usuario");
                        retorno.Data_Alteracao_Usuario = dataReader.GetDateTime("Data_Alteracao_Usuario");
                        retorno.Idade_Usuario = dataReader.GetInt32("Idade_Usuario");
                    }
                }


                return retorno;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        internal List<Usuario>? ListarUsuarioFiltro(FiltroUsuario filtro)
        {
            try
            {
                string query = @"SELECT 
                                Id_Usuario,
                                Nome_Usuario, 
                                Login_Usuario, 
                                Senha_Usuario, 
                                Email_Usuario, 
                                Telefone_Usuario, 
                                Cpf_Usuario, 
                                Data_Nascimento_Usuario, 
                                Nome_Mae_Usuario, 
                                Status_Usuario, 
                                Data_Inclusao_Usuario,
                                Data_Alteracao_Usuario,
                                Idade_Usuario 
                                FROM Cadastrar_Usuario";

                if(filtro != null)
                {
                    query += MotarFiltroListarUsuario(filtro);
                }

                query += ";";

                List<Usuario> retorno = new List<Usuario>();

                using (SqlDataReader dataReader = _conn.ExecuteReader(query))
                {
                    while (dataReader.Read())
                    {
                        retorno.Add(new Usuario()
                        {
                            Id_Usuario = dataReader.GetInt16("Id_Usuario"),
                            Nome_Usuario = dataReader.GetString("Nome_Usuario"),
                            Login_Usuario = dataReader.GetString("Login_Usuario"),
                            Senha_Usuario = dataReader.GetString("Senha_Usuario"),
                            Email_Usuario = dataReader.GetString("Email_Usuario"),
                            Telefone_Usuario = dataReader.GetString("Telefone_Usuario"),
                            Cpf_Usuario = dataReader.GetString("Cpf_Usuario"),
                            Data_Nascimento_Usuario = dataReader.GetDateTime("Data_Nascimento_Usuario"),
                            Nome_Mae_Usuario = dataReader.GetString("Nome_Mae_Usuario"),
                            Status_Usuario = dataReader.GetString("Status_Usuario"),
                            Data_Inclusao_Usuario = dataReader.GetDateTime("Data_Inclusao_Usuario"),
                            Data_Alteracao_Usuario = dataReader.GetDateTime("Data_Alteracao_Usuario"),
                            Idade_Usuario = dataReader.GetInt32("Idade_Usuario")
                        });
                    }
                }


                return retorno.Count > 0 ? retorno : null;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private string MotarFiltroListarUsuario(FiltroUsuario filtroUsuario)
        {
            string retorno = " Where ";

            // Nome_Usuario
            if (retorno == " Where ")
            {
                if(!string.IsNullOrEmpty(filtroUsuario.nome))
                {
                    retorno += $"Nome_Usuario like '%{filtroUsuario.nome}%'";
                }
            }
            //---------------------------------------------------------------
            // Cpf_Usuario
            if (retorno == " Where ")
            {
                if (!string.IsNullOrEmpty(filtroUsuario.cpf))
                {
                    retorno += $" Cpf_Usuario like '%{filtroUsuario.cpf}%'";
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(filtroUsuario.cpf))           {
                    retorno += $" and Cpf_Usuario like '%{filtroUsuario.cpf}%'";
                }
            }
            //---------------------------------------------------------------
            // Login_Usuario
            if (retorno == " Where ")
            {
                if (!string.IsNullOrEmpty(filtroUsuario.login))        {
                    retorno += $" Login_Usuario like '%{filtroUsuario.login}%'";
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(filtroUsuario.login))
                {
                    retorno += $" and Login_Usuario like '%{filtroUsuario.login}%'";
                }
            }
            //---------------------------------------------------------------
            // Status_Usuario
            if (retorno == " Where ")
            {
                if (!string.IsNullOrEmpty(filtroUsuario.status))
                {
                    retorno += $" Status_Usuario = '{filtroUsuario.status}'";
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(filtroUsuario.status))
                {
                    retorno += $" and Status_Usuario = '{filtroUsuario.status}'";
                }
            }
            //---------------------------------------------------------------
            // Data_Nascimento_Usuario
            if (retorno == " Where ")
            {
                if (!string.IsNullOrWhiteSpace(filtroUsuario.dataNascimentoIntervalo.dataInicial) && !string.IsNullOrWhiteSpace(filtroUsuario.dataNascimentoIntervalo.dataFinal))
                {
                    retorno += $" Data_Nascimento_Usuario between '{CodigosGlobais.TratarDataParaComparacoesInicio(filtroUsuario.dataNascimentoIntervalo.dataInicial)}' and '{CodigosGlobais.TratarDataParaComparacoesFinal(filtroUsuario.dataNascimentoIntervalo.dataFinal)}'";
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(filtroUsuario.dataNascimentoIntervalo.dataInicial) && !string.IsNullOrWhiteSpace(filtroUsuario.dataNascimentoIntervalo.dataFinal))
                {
                    retorno += $" and Data_Nascimento_Usuario between '{CodigosGlobais.TratarDataParaComparacoesInicio(filtroUsuario.dataNascimentoIntervalo.dataInicial)}' and '{CodigosGlobais.TratarDataParaComparacoesFinal(filtroUsuario.dataNascimentoIntervalo.dataFinal)}'"; ;
                }
            }
            //---------------------------------------------------------------
            // Data_Inclusao_Usuario
            if (retorno == " Where ")
            {
                if (!string.IsNullOrWhiteSpace(filtroUsuario.dataInclusaoIntervalo.dataInicial) && !string.IsNullOrWhiteSpace(filtroUsuario.dataInclusaoIntervalo.dataFinal))
                {
                    retorno += $" Data_Inclusao_Usuario between '{CodigosGlobais.TratarDataParaComparacoesInicio(filtroUsuario.dataInclusaoIntervalo.dataInicial)}' and '{CodigosGlobais.TratarDataParaComparacoesFinal(filtroUsuario.dataInclusaoIntervalo.dataFinal)}'";
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(filtroUsuario.dataInclusaoIntervalo.dataInicial) && !string.IsNullOrWhiteSpace(filtroUsuario.dataInclusaoIntervalo.dataFinal))
                {
                    retorno += $" and Data_Inclusao_Usuario between '{CodigosGlobais.TratarDataParaComparacoesInicio(filtroUsuario.dataInclusaoIntervalo.dataInicial)}' and '{CodigosGlobais.TratarDataParaComparacoesFinal(filtroUsuario.dataInclusaoIntervalo.dataFinal)}'"; ;
                }
            }
            //---------------------------------------------------------------
            // Data_Alteracao_Usuario
            if (retorno == " Where ")
            {
                if (!string.IsNullOrWhiteSpace(filtroUsuario.dataAlteracaoIntervalo.dataInicial) && !string.IsNullOrWhiteSpace(filtroUsuario.dataAlteracaoIntervalo.dataFinal))
                {
                    retorno += $" Data_Alteracao_Usuario between '{CodigosGlobais.TratarDataParaComparacoesInicio(filtroUsuario.dataAlteracaoIntervalo.dataInicial)}' and '{CodigosGlobais.TratarDataParaComparacoesFinal(filtroUsuario.dataAlteracaoIntervalo.dataFinal)}'";
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(filtroUsuario.dataNascimentoIntervalo.dataInicial) && !string.IsNullOrWhiteSpace(filtroUsuario.dataNascimentoIntervalo.dataFinal))
                {
                    retorno += $" and Data_Alteracao_Usuario between '{CodigosGlobais.TratarDataParaComparacoesInicio(filtroUsuario.dataAlteracaoIntervalo.dataInicial)}' and '{CodigosGlobais.TratarDataParaComparacoesFinal(filtroUsuario.dataAlteracaoIntervalo.dataFinal)}'"; ;
                }
            }
            //---------------------------------------------------------------
            // Idade_Usuario
            if (retorno == " Where ")
            {
                if (!string.IsNullOrWhiteSpace(filtroUsuario.idadeIntervalo.idadeInicial) && !string.IsNullOrWhiteSpace(filtroUsuario.idadeIntervalo.idadeFinal))
                {
                    retorno += $" Idade_Usuario between {filtroUsuario.idadeIntervalo.idadeInicial} and {filtroUsuario.idadeIntervalo.idadeFinal}";
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(filtroUsuario.idadeIntervalo.idadeInicial) && !string.IsNullOrWhiteSpace(filtroUsuario.idadeIntervalo.idadeFinal))
                {
                    retorno += $" and Idade_Usuario between {filtroUsuario.idadeIntervalo.idadeInicial} and {filtroUsuario.idadeIntervalo.idadeFinal}"; ;
                }
            }
            //---------------------------------------------------------------


            if (retorno == " Where ")
            {
                retorno = "";
            }
            
            return retorno;
        }

        internal bool RedefinirSenhaUsuario(RedefinirSenhaUsuario redefinir)
        {
            try
            {
                string query = string.Format(@"UPDATE Cadastrar_Usuario SET 
                                               Senha_Usuario = @Senha_Usuario,
                                               Data_Alteracao_Usuario = @Data_Alteracao_Usuario
                                               WHERE Cpf_Usuario = @Cpf_Usuario"
                );

                List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Senha_Usuario", redefinir.novaSenha),
                    new SqlParameter("@Data_Alteracao_Usuario",DateTime.Now),
                    new SqlParameter("@Cpf_Usuario",redefinir.cpf)

                };

                return _conn.ExecuteNonQueryTransaction(query, parameters) > 0;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        internal bool ValidaRedefinicaoSenhaUsuario(RedefinirSenhaUsuario valida)
        {
            try
            {
                string query = @"SELECT 
                                Id_Usuario 
                                from Cadastrar_Usuario 
                                where 
                                Nome_Usuario = @Nome_Usuario, 
                                Login_Usuario = @Login_Usuario, 
                                Email_Usuario = @Email_Usuario, 
                                Cpf_Usuario = @Cpf_Usuario;";

                List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Nome_Usuario", valida.nome),
                    new SqlParameter("@Login_Usuario",valida.login),
                    new SqlParameter("@Email_Usuario",valida.email),
                    new SqlParameter("@Cpf_Usuario", valida.cpf)
                };

                bool retorno = false;

                using (SqlDataReader dataReader = _conn.ExecuteReaderParametroLista(query, parameters))
                {
                    if (dataReader.Read())
                    {
                        if (Convert.ToInt64(dataReader["Id_Usuario"]) > 0)
                        {
                            retorno = true;
                        }
                    }
                }


                return retorno;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        internal Usuario? ListarUsuarioPorCpf(string cpf)
        {
            try
            {
                string query = @"SELECT 
                                Id_Usuario,
                                Nome_Usuario, 
                                Login_Usuario, 
                                Senha_Usuario, 
                                Email_Usuario, 
                                Telefone_Usuario, 
                                Cpf_Usuario, 
                                Data_Nascimento_Usuario, 
                                Nome_Mae_Usuario, 
                                Status_Usuario, 
                                Data_Inclusao_Usuario,
                                Data_Alteracao_Usuario,
                                Idade_Usuario 
                                FROM Cadastrar_Usuario 
                                WHERE Cpf_Usuario = @Cpf_Usuario;";

                Usuario retorno = new Usuario();

                SqlParameter parameters = new SqlParameter("@Cpf_Usuario", cpf);

                using (SqlDataReader dataReader = _conn.ExecuteReader(query, parameters))
                {
                    if (dataReader.Read())
                    {
                        retorno.Id_Usuario = dataReader.GetInt16("Id_Usuario");
                        retorno.Nome_Usuario = dataReader.GetString("Nome_Usuario");
                        retorno.Login_Usuario = dataReader.GetString("Login_Usuario");
                        retorno.Senha_Usuario = dataReader.GetString("Senha_Usuario");
                        retorno.Email_Usuario = dataReader.GetString("Email_Usuario");
                        retorno.Telefone_Usuario = dataReader.GetString("Telefone_Usuario");
                        retorno.Cpf_Usuario = dataReader.GetString("Cpf_Usuario");
                        retorno.Data_Nascimento_Usuario = dataReader.GetDateTime("Data_Nascimento_Usuario");
                        retorno.Nome_Mae_Usuario = dataReader.GetString("Nome_Mae_Usuario");
                        retorno.Status_Usuario = dataReader.GetString("Status_Usuario");
                        retorno.Data_Inclusao_Usuario = dataReader.GetDateTime("Data_Inclusao_Usuario");
                        retorno.Data_Alteracao_Usuario = dataReader.GetDateTime("Data_Alteracao_Usuario");
                        retorno.Idade_Usuario = dataReader.GetInt32("Idade_Usuario");
                    }
                }


                return retorno;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        internal bool ExisteloginUtilizado(string login)
        {
            try
            {
                string query = @"SELECT 
                                Id_Usuario 
                                from Cadastrar_Usuario 
                                where 
                                Login_Usuario = @Login_Usuario";

                SqlParameter parameters = new SqlParameter("@Login_Usuario", login);

                bool retorno = false;

                using (SqlDataReader dataReader = _conn.ExecuteReader(query, parameters))
                {
                    if (dataReader.Read())
                    {
                        if (Convert.ToInt64(dataReader["Id_Usuario"]) > 0)
                        {
                            retorno = true;
                        }
                    }
                }


                return retorno;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        internal Usuario? LogonUsuarioPorProcedure(string login, string senha)
        {
            try
            {
                string query = @"sp_Login_Usuario";

                Usuario retorno = new Usuario();

                List<SqlParameter> parameters = new List<SqlParameter>()
                {
                    new SqlParameter("@Login_Usuario", login),
                    new SqlParameter("@Senha_Usuario", senha)
                };

                using (SqlDataReader dataReader = _conn.ExecuteReader_SP(query, parameters))
                {
                    if (dataReader.Read())
                    {
                        retorno.Id_Usuario = dataReader.GetInt16("Id_Usuario");
                        retorno.Nome_Usuario = dataReader.GetString("Nome_Usuario");
                        retorno.Login_Usuario = dataReader.GetString("Login_Usuario");
                        retorno.Senha_Usuario = dataReader.GetString("Senha_Usuario");
                        retorno.Email_Usuario = dataReader.GetString("Email_Usuario");
                        retorno.Telefone_Usuario = dataReader.GetString("Telefone_Usuario");
                        retorno.Cpf_Usuario = dataReader.GetString("Cpf_Usuario");
                        retorno.Data_Nascimento_Usuario = dataReader.GetDateTime("Data_Nascimento_Usuario");
                        retorno.Nome_Mae_Usuario = dataReader.GetString("Nome_Mae_Usuario");
                        retorno.Status_Usuario = dataReader.GetString("Status_Usuario");
                        retorno.Data_Inclusao_Usuario = dataReader.GetDateTime("Data_Inclusao_Usuario");
                        retorno.Data_Alteracao_Usuario = dataReader.GetDateTime("Data_Alteracao_Usuario");
                        retorno.Idade_Usuario = dataReader.GetInt32("Idade_Usuario");
                    }
                }


                return retorno;

            }
            catch (Exception ex)
            {
                throw;
            }
        }


    }
}
