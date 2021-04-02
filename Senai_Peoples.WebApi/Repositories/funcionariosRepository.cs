using Senai_Peoples.WebApi.Domains;
using Senai_Peoples.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Senai_Peoples.WebApi.Repositories
{
    public class funcionariosRepository : IFuncionariosRepository
    {
        //stringConexao é uma variavel com as informações do servidor, banco de dados, usuário e senha do banco de dados
        private string stringConexao = "Data Source=LAPTOP-ENV70MDE\\SQLEXPRESS; initial catalog=M_Peoples; user Id=sa; pwd=15w21a30";
        public void AtualizarIdCorpo(funcionariosDomain funcionarios)
        {
            using(SqlConnection con = new SqlConnection(stringConexao))
            {
                con.Open();

                string queryUpdate = "UPDATE Funcionarios SET Nome = @Nome, Sobrenome = @Sobrenome WHERE idUser = @ID";

                using(SqlCommand cmd = new SqlCommand(queryUpdate, con))
                {
                    con.Open();

                    cmd.Parameters.AddWithValue("@Nome", funcionarios.nome);
                    cmd.Parameters.AddWithValue("@Sobrenome", funcionarios.sobrenome);
                    cmd.Parameters.AddWithValue("@ID", funcionarios.idUser);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AtualizarIdUrl(int id, funcionariosDomain funcionariosUrl)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                con.Open();

                string queryUpdateURL = "UPDATE Funcionarios SET Nome = @Nome, Sobrenome = @Sobrenome WHERE idUser = @ID";


                using(SqlCommand cmd = new SqlCommand(queryUpdateURL, con))
                {
                    cmd.Parameters.AddWithValue("@Nome",funcionariosUrl.nome);
                    cmd.Parameters.AddWithValue("@Sobrenome" , funcionariosUrl.sobrenome);
                    cmd.Parameters.AddWithValue("@ID" , id);

                    con.Open();

                    cmd.ExecuteNonQuery();
           
                }
            }
        }

        public funcionariosDomain BuscarPorId(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                con.Open();

                string queryFind = "SELECT * FROM Funcionarios WHERE idUser = @ID";

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(queryFind, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);

                    rdr = cmd.ExecuteReader();

                    // Verifica se o resultado da query retornou algum registro
                    if (rdr.Read())
                    {
                        //instancia novos valores para as váriaveis caso seja encontrado no banco de dados
                        funcionariosDomain funcBuscado = new funcionariosDomain()
                        {
                            idUser = Convert.ToInt32(rdr[0]),
                            nome = rdr[1].ToString(),
                            sobrenome = rdr[2].ToString()
                        };

                        //retorna os novos valores obtidos pela query
                        return funcBuscado;
                    }
                    else
                    {
                        //retorna nulo, caso não tenha encontrado nada
                        return null;
                    }
                }

            }

        }

        public void Cadastrar(funcionariosDomain novoFuncionario)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                //Abre conexão com o banco de dados
                con.Open();

                //string para inserir dados no banco de dados
                string queryInsert = "INSERT INTO Funcionarios(Nome,Sobrenome) VALUES (@Nome , @Sobrenome)";

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    //cmd.Parameters.AddWithValue server para relacionar uma váriavel 
                    cmd.Parameters.AddWithValue("@Nome", novoFuncionario.nome);
                    cmd.Parameters.AddWithValue("@Sobrenome", novoFuncionario.sobrenome);

                    //abre conexão como banco de dados
                    con.Open();

                    //executa a query
                    cmd.ExecuteNonQuery();
                }

            }
        }

        public void Deletar(int id)
        {

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                con.Open();

                string queryDelete = "DELETE FROM Funcionarios WHERE idUser = @ID";

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    //Define o valor do id recebido no método como o valor do parâmetro @ID
                    cmd.Parameters.AddWithValue("@ID", id);

                    //Abre a conexão com o banco de dados
                    con.Open();

                    //Executa a query
                    cmd.ExecuteNonQuery();
                }


            }
        }

        public List<funcionariosDomain> listarTodos()
        {
            List<funcionariosDomain> funcionarios = new List<funcionariosDomain>();

            //SqlConnection é utilizado para fazer a conexão com o banco de dados
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                //con.Open() - serve para abrir a conexão com o banco de dados
                con.Open();

                //essa string serve para fazer a solicitação no banco de dados
                string querySelectAll = "SELECT idUser, Nome , Sobrenome FROM Funcionarios";

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {

                        funcionariosDomain funcionario = new funcionariosDomain()
                        {

                            idUser = Convert.ToInt32(rdr[0]),

                            nome = rdr[1].ToString(),

                            sobrenome = rdr[2].ToString()

                        };

                        funcionarios.Add(funcionario);

                    }
                }
            }

            return funcionarios;
        }
    }
}
