using CadUsuario.Funcoes;
using CadUsuario.Modulos;
using CadUsuario.Regras;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CadUsuario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        UsuarioRegra usuarioRegra;

        [HttpGet]
        [Route("listar")]
        public IActionResult ListarUsuario()
        {

            try
            {
                usuarioRegra = new UsuarioRegra();

                ResultadoDados retorno = usuarioRegra.ListarUsuario();

                return StatusCode(retorno.Status, retorno);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message); //(int)HttpStatusCode.InternalServerError
            }

        }

        [HttpPost]
        [Route("incluir")]
        public IActionResult IncluirUsuario(CadastroUsuario usuario)
        {

            try
            {
                usuarioRegra = new UsuarioRegra();

                ResultadoDados retorno = usuarioRegra.IncluirUsuario(usuario);

                return StatusCode(retorno.Status, retorno);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message); //(int)HttpStatusCode.InternalServerError
            }

        }

        [HttpPut]
        [Route("alterar")]
        public IActionResult AlterarUsuario(AlterarUsuario usuario)
        {

            try
            {
                usuarioRegra = new UsuarioRegra();

                ResultadoDados retorno = usuarioRegra.AlterarUsuario(usuario);

                return StatusCode(retorno.Status, retorno);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message); //(int)HttpStatusCode.InternalServerError
            }

        }

        [HttpPut]
        [Route("deletar/{idUsuario}")]
        public IActionResult DeletarUsuario(long idUsuario)
        {

            try
            {
                usuarioRegra = new UsuarioRegra();

                ResultadoDados retorno = usuarioRegra.DeletarUsuario(idUsuario);

                return StatusCode(retorno.Status, retorno);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message); //(int)HttpStatusCode.InternalServerError
            }

        }

        [HttpPut]
        [Route("modificar/status/{idUsuario}/{status}")]
        public IActionResult AlterarStatusUsuario(long idUsuario, char status)
        {

            try
            {
                usuarioRegra = new UsuarioRegra();

                ResultadoDados retorno = usuarioRegra.AlterarStatusUsuario(idUsuario, status);

                return StatusCode(retorno.Status, retorno);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message); //(int)HttpStatusCode.InternalServerError
            }

        }

        [HttpGet]
        [Route("listar/porId/{idUsuario}")]
        public IActionResult ListarUsuarioPorId(long idUsuario)
        {

            try
            {
                usuarioRegra = new UsuarioRegra();

                ResultadoDados retorno = usuarioRegra.ListarUsuarioPorId(idUsuario);

                return StatusCode(retorno.Status, retorno);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message); //(int)HttpStatusCode.InternalServerError
            }

        }

        [HttpGet]
        [Route("logon/{login}/{senha}")]
        public IActionResult ListarUsuarioPorId(string login, string senha)
        {
            try
            {
                usuarioRegra = new UsuarioRegra();

                ResultadoDados retorno = usuarioRegra.LogonUsuario(login, senha);

                return StatusCode(retorno.Status, retorno);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message); //(int)HttpStatusCode.InternalServerError
            }

        }

        //[HttpGet]
        //[Route("listar/filtro")]
        //public IActionResult ListarUsuarioFiltro([FromQuery] FiltroUsuario filtro)
        //{

        //    try
        //    {
        //        usuarioRegra = new UsuarioRegra();

        //        ResultadoDados retorno = usuarioRegra.ListarUsuarioFiltro(filtro);

        //        return StatusCode(retorno.Status, retorno);
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, e.Message); //(int)HttpStatusCode.InternalServerError
        //    }

        //}

        [HttpPost]
        [Route("listar/filtro")]
        public IActionResult ListarUsuarioFiltro(FiltroUsuario filtro)
        {
            try
            {
                usuarioRegra = new UsuarioRegra();

                ResultadoDados retorno = usuarioRegra.ListarUsuarioFiltro(filtro);

                return StatusCode(retorno.Status, retorno);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message); //(int)HttpStatusCode.InternalServerError
            }

        }

        [HttpPost]
        [Route("redefinir/senha")]
        public IActionResult ListarUsuarioFiltro(RedefinirSenhaUsuario redefinir)
        {
            try
            {
                usuarioRegra = new UsuarioRegra();

                ResultadoDados retorno = usuarioRegra.RedefinirSenhaUsuario(redefinir);

                return StatusCode(retorno.Status, retorno);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message); //(int)HttpStatusCode.InternalServerError
            }

        }


        [HttpPost]
        [Route("Gerar/Pdf/listar/filtro")]
        public IActionResult GerarPdfListarUsuarioFiltro(FiltroUsuario filtro)
        {
            try
            {
                usuarioRegra = new UsuarioRegra();

                ResultadoDados retorno = usuarioRegra.GerarPdfListarUsuarioFiltro(filtro);

                return StatusCode(retorno.Status, retorno);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message); //(int)HttpStatusCode.InternalServerError
            }

        }
    }
}
