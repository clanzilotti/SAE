using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAE.Models;

namespace SAE.DAO
{
    public class AlunoDAO
    {
        public void Adiciona(Aluno aluno)
        {
            using (var context = new SAEContext())
            {
                context.Alunos.Add(aluno);
                context.SaveChanges();
            }
        }

        public IList<Aluno> Lista()
        {
            using (var contexto = new SAEContext())
            {
                return contexto.Alunos.Include("AlunoSituacao").ToList();
            }
        }

        public Aluno BuscaPorId(int id)
        {
            using (var contexto = new SAEContext())
            {
                return contexto.Alunos.Find(id);
            }
        }

        public List<Aluno> BuscaPorNome(string nome)
        {
            using (var contexto = new SAEContext())
            {
                //List<Aluno> lstPerson = contexto.GetTable<Aluno>().StartWith(c => c.nome).ToList();
                List<Aluno> lstPerson2 = contexto.Alunos.Where(c => c.Nome.StartsWith(nome)).ToList();
                return lstPerson2;
                //contexto.Alunos.Where(u => u.Nome as nome)
            }
        }

        public void Atualiza(Aluno Aluno)
        {
            using (var contexto = new SAEContext())
            {
                contexto.Entry(Aluno).State = System.Data.Entity.EntityState.Modified;
                contexto.SaveChanges();
            }
        }

    }
}