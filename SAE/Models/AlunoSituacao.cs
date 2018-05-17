using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SAE.Models
{
    [Table("tbAlunoSituacao")]
    public class AlunoSituacao
    {
        //[Key]
        public int ID { get; set; }

        public string Descricao { get; set; }

        public int Ativo { get; set; }

        public virtual IList<Aluno> Alunos { get; set; }
    }
}