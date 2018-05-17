using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAE.Models;
using SAE.DAO;
using PagedList;

namespace SAE.Controllers
{
    public class CadastroAlunoController : Controller
    {
        // GET: CadastroAluno
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastroAluno()
        {
            Aluno aluno = new Aluno();
            SetarValoresDeCadastroNaViewBag();

            return View(aluno); // "CadastroAluno", "EncheGridAluno");
        }

        public ActionResult AdicionaAluno(Aluno aluno)
        {
            AlunoDAO alunoDAO = new AlunoDAO();
            alunoDAO.Adiciona(aluno);
            ViewBag.Aluno = aluno;

            //git

            return View("ListaGridAluno");
        }

        public ActionResult AlteraAluno(int IDAluno)
        {
            if (IDAluno > 0)
            {
                SetarValoresDeCadastroNaViewBag();
                AlunoDAO alunoDAO = new AlunoDAO();
                Aluno aluno = new Aluno();
                aluno = alunoDAO.BuscaPorId(IDAluno);
                //alunoDAO.Adiciona(aluno);
                ViewBag.Aluno = aluno;

                return View(aluno);
            }
            else
            {
                return View("ListaGridAluno");
            }
        }

        public ActionResult ListaGridAluno(int? pagina)
        {
            //AlunoDAO alunoDAO = new AlunoDAO();
            //IList<Aluno> alunos = alunoDAO.Lista();

            var contexto = new SAEContext();
            var listaAlunos = contexto.Alunos.ToList();
            int paginaTamanho = 10;
            int paginaNumero = (pagina ?? 1);

            //ViewBag.Alunos = alunos;

            return View(listaAlunos.ToPagedList(paginaNumero, paginaTamanho));
        }

        private void SetarValoresDeCadastroNaViewBag()
        {
            var AlunoSituacaoIDList = new List<dynamic>();
            AlunoSituacaoIDList.Add(new { Id = 1, Text = "Ativo" });
            AlunoSituacaoIDList.Add(new { Id = 2, Text = "Evadido" });
            AlunoSituacaoIDList.Add(new { Id = 3, Text = "Concluinte" });
            AlunoSituacaoIDList.Add(new { Id = 4, Text = "Transferido" });
            AlunoSituacaoIDList.Add(new { Id = 5, Text = "Desligado" });
            AlunoSituacaoIDList.Add(new { Id = 6, Text = "Inativo" });
            ViewBag.AlunoSituacaoID = new SelectList(AlunoSituacaoIDList, "Id", "Text");

            var MatriculaSituacaoIDList = new List<dynamic>();
            MatriculaSituacaoIDList.Add(new { Id = 1, Text = "Aguardando" });
            MatriculaSituacaoIDList.Add(new { Id = 2, Text = "Cursando" });
            MatriculaSituacaoIDList.Add(new { Id = 3, Text = "Promovido" });
            MatriculaSituacaoIDList.Add(new { Id = 4, Text = "Retido" });
            MatriculaSituacaoIDList.Add(new { Id = 5, Text = "Pendente" });
            ViewBag.MatriculaSituacaoID = new SelectList(MatriculaSituacaoIDList, "Id", "Text");

            var TurmaSituacaoIDList = new List<dynamic>();
            TurmaSituacaoIDList.Add(new { Id = 1, Text = "Criação" });
            TurmaSituacaoIDList.Add(new { Id = 2, Text = "Aberta" });
            TurmaSituacaoIDList.Add(new { Id = 3, Text = "Iniciada" });
            TurmaSituacaoIDList.Add(new { Id = 4, Text = "Cancelada" });
            TurmaSituacaoIDList.Add(new { Id = 5, Text = "Concluída" });
            ViewBag.TurmaSituacaoIDList = new SelectList(TurmaSituacaoIDList, "Id", "Text");

            var SexoList = new List<dynamic>();
            //SexoList.Add(new { Id = "", Text = "" });
            SexoList.Add(new { Id = "M", Text = "Masculino" });
            SexoList.Add(new { Id = "F", Text = "Feminino" });
            ViewBag.Sexo = new SelectList(SexoList, "Id", "Text");

            var EstadoCivilList = new List<dynamic>();
            EstadoCivilList.Add(new { Id = "S", Text = "Solteiro" });
            EstadoCivilList.Add(new { Id = "C", Text = "Casado" });
            EstadoCivilList.Add(new { Id = "P", Text = "Separado" });
            EstadoCivilList.Add(new { Id = "D", Text = "Divorciado" });
            EstadoCivilList.Add(new { Id = "V", Text = "Viúvo" });
            ViewBag.EstadoCivil = new SelectList(EstadoCivilList, "Id", "Text");

            //var cargos = repository.Cargos.Where(x => x.EmpresaId == SessopmEmpresaId).Select(x => new
            //{
            //    Id = x.Id,
            //    Descricao = x.Descricao
            //}).ToList<dynamic>();
            //cargos.Insert(0, new { Id = "", Descricao = "" });
            //ViewBag.Cargos = new SelectList(cargos, "Id", "Descricao");
        }
    }
}