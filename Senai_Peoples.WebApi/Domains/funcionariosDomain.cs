using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Senai_Peoples.WebApi.Interfaces;

namespace Senai_Peoples.WebApi.Domains
{
    public class funcionariosDomain
    {
        public int idUser { get; set; }
        public string nome { get; set; }
        public string sobrenome { get; set; }
    }
}
