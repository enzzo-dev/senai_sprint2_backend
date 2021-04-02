using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Senai_Peoples.WebApi.Domains;

namespace Senai_Peoples.WebApi.Interfaces
{
    interface IFuncionariosRepository
    {
        /// <summary>
        /// Método irá retornar todos os funcionários cadastrados no banco de dados
        /// </summary>
        /// <returns></returns>
        List<funcionariosDomain> listarTodos();

        /// <summary>
        /// Método para buscar o id de algum funcionário no banco de dados
        /// </summary>
        /// <param name="id"></param>
        /// <returns> OBJETO </returns>
        funcionariosDomain BuscarPorId(int id);

        /// <summary>
        /// Método irá criar um novo funcionário
        /// </summary>
        /// <param name="novoFuncionario"></param>
        void Cadastrar(funcionariosDomain novoFuncionario);

        /// <summary>
        /// Método irá passar o id pelo corpo da requisição
        /// </summary>
        /// <param name="funcionario"></param>
        void AtualizarIdCorpo(funcionariosDomain funcionarios);

        /// <summary>
        /// Método irá atualizar o funcionário pelo id passado pela URL
        /// </summary>
        /// <param name="funcionariosUrl"></param>
        void AtualizarIdUrl(int id, funcionariosDomain funcionariosUrl);

        /// <summary>
        /// Método irá deletar usuário pelo id que foi passado no corpo da requisição ou URL
        /// </summary>
        /// <param name="id"></param>
        void Deletar(int id);
    }
}
