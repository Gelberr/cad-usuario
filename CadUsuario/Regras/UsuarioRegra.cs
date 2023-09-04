using CadUsuario.Dados;
using CadUsuario.Funcoes;
using CadUsuario.Modulos;
using CadUsuario.Repositorio;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;


namespace CadUsuario.Regras
{
    public class UsuarioRegra
    {
        private Conexao _conn;
        private ResultadoDados resultado;

        private UsuarioRepositorio usuarioRepositorio;

        public UsuarioRegra()
        {
            _conn = new Conexao();
            usuarioRepositorio = new UsuarioRepositorio(_conn);
            resultado = new ResultadoDados();
        }

        public ResultadoDados ListarUsuario()
        {
            try
            {
                _conn.Open();

                List<Usuario>? retorno = usuarioRepositorio.ListarUsuario();


                if (retorno == null)
                {
                    resultado.NotFound();
                }

                resultado.Data = retorno;

                return resultado;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        public ResultadoDados AlterarUsuario(AlterarUsuario usuario)
        {
            try
            {
                resultado = ValidarCamposUsuarioAlteracao(usuario);

                if (resultado.Errors.Count > 0)
                {
                    return resultado;
                }

                //-----------------------------------------------------------------------------------------------
                _conn.Open();

                _conn.BeginTransaction();

                bool retorno = usuarioRepositorio.AlterarUsuario(usuario);


                if (retorno == false)
                {
                    resultado.NotFound();

                    if (_conn.IsOpen() && _conn.ExisteTransacao())
                    {
                        _conn.RollBackTransaction();
                    }
                }
                else
                {
                    _conn.CommitTransaction();
                }

                resultado.Data = retorno;

                return resultado;

            }
            catch (Exception)
            {
                if (_conn.IsOpen() && _conn.ExisteTransacao())
                {
                    _conn.RollBackTransaction();
                }
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        public ResultadoDados IncluirUsuario(CadastroUsuario usuario)
        {
            try
            {

                resultado = ValidarCamposUsuarioCadastro(usuario);

                if (resultado.Errors.Count > 0)
                {
                    return resultado;
                }

                //-----------------------------------------------------------------------------------------------

                _conn.Open();

                //-----------------------------------------------------------------------------------------------
                if (usuarioRepositorio.ExisteUsuarioCadastrado(usuario.Cpf_Usuario))
                {
                    resultado.ErroDiversos("Usuário já cadastrado.");

                    return resultado;
                }


                if (usuarioRepositorio.ExisteloginUtilizado(usuario.Login_Usuario) == true)
                {
                    resultado.AdicionarErrosStatisValidacao("Login já utilizado. Favor informa outro login.", (int)HttpStatusCode.BadRequest);

                    return resultado;
                }
                //-----------------------------------------------------------------------------------------------
                _conn.BeginTransaction();

                bool retorno = usuarioRepositorio.IncluirUsuario(usuario);


                if (retorno == false)
                {
                    resultado.NotFound();

                    if (_conn.IsOpen() && _conn.ExisteTransacao())
                    {
                        _conn.RollBackTransaction();
                    }

                    resultado.Data = retorno;

                    return resultado;
                }
                else
                {
                    _conn.CommitTransaction();
                }

                resultado.Data = retorno;

                return resultado;

            }
            catch (Exception)
            {
                if (_conn.IsOpen() && _conn.ExisteTransacao())
                {
                    _conn.RollBackTransaction();
                }
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        public ResultadoDados AlterarStatusUsuario(long idUsuario, char status)
        {
            try
            {
                _conn.Open();

                _conn.BeginTransaction();

                bool retorno = usuarioRepositorio.AlterarStatusUsuario(idUsuario, status);


                if (retorno == false)
                {
                    resultado.NotFound();

                    if (_conn.IsOpen() && _conn.ExisteTransacao())
                    {
                        _conn.RollBackTransaction();
                    }
                }
                else
                {
                    _conn.CommitTransaction();
                }

                resultado.Data = retorno;

                return resultado;

            }
            catch (Exception)
            {
                if (_conn.IsOpen() && _conn.ExisteTransacao())
                {
                    _conn.RollBackTransaction();
                }
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        private ResultadoDados ValidarCamposUsuarioCadastro(CadastroUsuario usuario)
        {

            ResultadoDados validacao = new ResultadoDados();

            if (usuario.Nome_Usuario == "" || usuario.Nome_Usuario == null)
            {
                validacao.AdicionarErrosValidacao("Nome do usuário inválido");
            }

            if (usuario.Login_Usuario == "" || usuario.Login_Usuario == null)
            {
                validacao.AdicionarErrosValidacao("Login do usuário inválido");
            }

            if (usuario.Senha_Usuario == "" || usuario.Senha_Usuario == null)
            {
                validacao.AdicionarErrosValidacao("Senha do usuário inválido");
            }

            if (!CodigosGlobais.ValidarCPf(usuario.Cpf_Usuario))
            {
                validacao.AdicionarErrosValidacao("Cpf inválido");
            }

            return validacao;
        }

        private ResultadoDados ValidarCamposUsuarioAlteracao(AlterarUsuario usuario)
        {

            ResultadoDados validacao = new ResultadoDados();

            if (usuario.Nome_Usuario == "" || usuario.Nome_Usuario == null)
            {
                validacao.AdicionarErrosValidacao("Nome do usuário inválido");
            }

            if (usuario.Login_Usuario == "" || usuario.Login_Usuario == null)
            {
                validacao.AdicionarErrosValidacao("Login do usuário inválido");
            }

            if (usuario.Senha_Usuario == "" || usuario.Senha_Usuario == null)
            {
                validacao.AdicionarErrosValidacao("Senha do usuário inválido");
            }

            if (!CodigosGlobais.ValidarCPf(usuario.Cpf_Usuario))
            {
                validacao.AdicionarErrosValidacao("Cpf inválido");
            }

            return validacao;
        }

        public ResultadoDados DeletarUsuario(long idUsuario)
        {
            try
            {
                _conn.Open();

                _conn.BeginTransaction();

                bool retorno = usuarioRepositorio.DelatarUsuario(idUsuario);


                if (retorno == false)
                {
                    resultado.NotFound();

                    if (_conn.IsOpen() && _conn.ExisteTransacao())
                    {
                        _conn.RollBackTransaction();
                    }
                }
                else
                {
                    _conn.CommitTransaction();
                }

                resultado.Data = retorno;

                return resultado;

            }
            catch (Exception)
            {
                if (_conn.IsOpen() && _conn.ExisteTransacao())
                {
                    _conn.RollBackTransaction();
                }
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        public ResultadoDados ListarUsuarioPorId(long idUsuario)
        {
            try
            {
                _conn.Open();

                Usuario? retorno = usuarioRepositorio.ListarUsuarioPorId(idUsuario);


                if (retorno == null || retorno.Id_Usuario <= 0)
                {
                    resultado.NotFound();

                    return resultado;
                }

                resultado.Data = retorno;

                return resultado;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        public ResultadoDados LogonUsuario(string login, string senha)
        {
            try
            {
                _conn.Open();

                //Usuario? retorno = usuarioRepositorio.LogonUsuario(login,senha);

                Usuario? retorno = usuarioRepositorio.LogonUsuarioPorProcedure(login, senha); 

                if (retorno == null || retorno.Id_Usuario <= 0)
                {
                    resultado.AdicionarErrosStatisValidacao("Usuário não cadastrado", (int)HttpStatusCode.NotFound);

                    return resultado;
                }

                if (retorno.Status_Usuario.ToUpper() == "I" || retorno.Status_Usuario.ToUpper() == "B")
                {
                    resultado.AdicionarErrosStatisValidacao("Usuário inativo ou bloqueado", (int)HttpStatusCode.NotFound);

                    return resultado;
                }

                resultado.Data = retorno;

                return resultado;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        public ResultadoDados ListarUsuarioFiltro(FiltroUsuario filtro)
        {
            try
            {
                _conn.Open();

                List<Usuario>? retorno = usuarioRepositorio.ListarUsuarioFiltro(filtro);


                if (retorno == null)
                {
                    resultado.NotFound();
                    return resultado;
                }

                resultado.Data = retorno;

                return resultado;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        public ResultadoDados RedefinirSenhaUsuario(RedefinirSenhaUsuario redefinir)
        {
            try
            {
                _conn.Open();
                //-----------------------------------------------------------------------------------------------
                if (usuarioRepositorio.ValidaRedefinicaoSenhaUsuario(redefinir))
                {
                    resultado.ErroDiversos("Usuário não cadastrado.");

                    return resultado;
                }

                //-----------------------------------------------------------------------------------------------
                _conn.BeginTransaction();

                bool retorno = usuarioRepositorio.RedefinirSenhaUsuario(redefinir);


                if (retorno == false)
                {
                    resultado.NotFound();

                    if (_conn.IsOpen() && _conn.ExisteTransacao())
                    {
                        _conn.RollBackTransaction();
                    }
                }
                else
                {
                    _conn.CommitTransaction();
                }

                resultado.Data = retorno;

                return resultado;

            }
            catch (Exception)
            {
                if (_conn.IsOpen() && _conn.ExisteTransacao())
                {
                    _conn.RollBackTransaction();
                }
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        public ResultadoDados GerarPdfListarUsuarioFiltro(FiltroUsuario filtro)
        {
            try
            {
                _conn.Open();

                List<Usuario>? retorno = usuarioRepositorio.ListarUsuarioFiltro(filtro);


                if (retorno == null)
                {
                    resultado.NotFound();
                    return resultado;
                }

                

                resultado.Data = retorno;

                return resultado;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }
    }
}
