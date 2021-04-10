using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Senai_peoples_WebApi.Domains;
using Senai_peoples_WebApi.Interfaces;
using Senai_peoples_WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Senai_peoples_WebApi.Controllers
{
    [Produces("application/json")]

    // Define que a rota de uma requisição será no formato dominio/api/nomeController
    // ex: http://localhost:5000/api/Funcionarios
    [Route("api/[controller]")]

    // Define que é um controlador de API
    [ApiController]

    public class funcionariosController : ControllerBase
    {
        // Define que o tipo de resposta da API será no formato JSON
        

            private IFuncionariosRepository _funcionarioRepository { get; set; }

            public funcionariosController()
            {
                _funcionarioRepository = new funcionariosRepository();
            }


            /// http://localhost:5000/api/funcionarios
            [HttpGet]
            public IActionResult Get()
            {
                // Cria uma lista nomeada listaFuncionarios para receber os dados
                List<funcionariosDomain> listaFuncionarios = _funcionarioRepository.listarTodos();

                // Retorna o status code 200 (Ok) com a lista de  no formato JSON
                return Ok(listaFuncionarios);
            }


            /// http://localhost:5000/api/funcionarios/1
            [HttpGet("{id}")]
            public IActionResult GetById(int id)
            {
                funcionariosDomain funcionarioBuscado = _funcionarioRepository.BuscarPorId(id);

                if (funcionarioBuscado == null)
                {
                    // Caso não seja encontrado, retorna um status code 404 - Not Found com a mensagem personalizada
                    return NotFound("Nenhum funcionário com esse nome  foi encontrado!");
                }

                return Ok(funcionarioBuscado);
            }


            /// http://localhost:5000/api/funcionarios
            [HttpPost]
            public IActionResult Post(funcionariosDomain novoFuncionario)
            {
                // Faz a chamada para o método .Cadastrar()
                _funcionarioRepository.Cadastrar(novoFuncionario);

                // Retorna um status code 201 - Created
                return StatusCode(201);
            }

           
            /// http://localhost:5000/api/funcionarios/3
            [HttpPut("{id}")]
            public IActionResult PutIdUrl(int id, funcionariosDomain funcionarioAtualizado)
            {
                // Cria um objeto generoBuscado que irá receber o gênero buscado no banco de dados
                funcionariosDomain funcionarioBuscado = _funcionarioRepository.BuscarPorId(id);

                // Caso não seja encontrado, retorna NotFound com uma mensagem personalizada
                // e um bool para apresentar que houve erro
                if (funcionarioBuscado == null)
                {
                    return NotFound
                        (new
                        {
                            mensagem = "Funcionário não encontrado!",
                            erro = true
                        }
                        );
                }

                // Tenta atualizar o registro
                try
                {
                    // Faz a chamada para o método .AtualizarIdUrl()
                    _funcionarioRepository.AtualizarIdUrl(id, funcionarioAtualizado);

                    // Retorna um status code 204 - No Content
                    return NoContent();
                }
                // Caso ocorra algum erro
                catch (Exception erro)
                {
                    // Retorna um status 400 - BadRequest e o código do erro
                    return BadRequest(erro);
                }
            }

            /// <summary>
            /// Atualiza um gênero existente passando o seu id pelo corpo da requisição
            /// </summary>
            /// <param name="generoAtualizado">Objeto generoAtualizado com as novas informações</param>
            /// <returns>Um status code</returns>
            [HttpPut]
            public IActionResult PutIdBody(funcionariosDomain funcionarioAtualizado)
            {
                // Cria um objeto funcionarioBuscado que irá receber o gên buscado no banco de dados
                funcionariosDomain funcionarioBuscado = _funcionarioRepository.BuscarPorId(funcionarioAtualizado.idUser);

                // Verifica se algum gênero foi encontrado
                // ! -> negação (não)
                if (funcionarioBuscado != null)
                {
                    // Se sim, tenta atualizar o registro
                    try
                    {
                        // Faz a chamada para o método .AtualizarIdCorpo()
                        _funcionarioRepository.AtualizarIdCorpo(funcionarioAtualizado);

                        // Retorna um status code 204 - No Content
                        return NoContent();
                    }
                    // Caso ocorra algum erro
                    catch (Exception erro)
                    {
                        // Retorna um BadRequest e o código do erro
                        return BadRequest(erro);
                    }
                }

                // Caso não seja encontrado, retorna NotFoun com uma mensagem personalizada
                return NotFound
                    (
                        new
                        {
                            mensagem = "Funcionário não encontrado!"
                        }
                    );
            }

            /// <summary>
            /// Deleta um gênero existente
            /// </summary>
            /// <param name="id">id do gênero que será deletado</param>
            /// <returns>Um status code 204 - No Content</returns>
            /// http://localhost:5000/api/funcionario/4
            [HttpDelete("{id}")]
            public IActionResult Delete(int id)
            {
                // Faz a chamada para o método .Deletar()
                _funcionarioRepository.Deletar(id);

                // Retorna um status code 204 - No Content
                return StatusCode(204);
            }

        
    }
}

