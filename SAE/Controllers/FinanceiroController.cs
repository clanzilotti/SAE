using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Boleto2Net;
using SAE.Models;
using SAE.DAO;
using NUnit.Framework;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace SAE.Controllers
{
    public class FinanceiroController : Controller
    {
        // GET: Financeiro
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EmiteBoletoAluno(int? IDAluno)
        {
            if (IDAluno != null)
            {
                AlunoDAO alunoDAO = new AlunoDAO();
                Aluno aluno;
                aluno = alunoDAO.BuscaPorId((int)IDAluno);
                ViewBag.Aluno = aluno;
                string boletoHTML = MontaBoletoHTML(ViewBag.Aluno);
                //boletoHTML = Regex.Replace(boletoHTML, @"\t|\n|\r", "");
                GeraBoletoArquivo(aluno);
                ViewBag.BoletoHTML = boletoHTML;
            }

            return View();
        }

        private void GeraBoletoArquivo(Aluno aluno)
        {

            var mensagemErro = "";
            var retorno = false;
            var classeProxy = new Boleto2NetProxy();

            // Define os dados do Cedente, Conta Bancária e Carteira de Cobrança
            retorno = classeProxy.SetupCobranca("12.123.123/1234-46", "Cedente Teste Classe Proxy",
                                                  "Av Testador", "12", "sala 30", "Centro", "Cidade", "SP", "11223-445", "Observacoes do Cedente",
                                                   237, "1234", "X", "", "123456", "X",
                                                   "1213141", "0", "", "09", "",
                                                   (int)TipoCarteira.CarteiraCobrancaSimples, (int)TipoFormaCadastramento.ComRegistro, (int)TipoImpressaoBoleto.Empresa, (int)TipoDocumento.Escritural,
                                                   ref mensagemErro);
            Assert.AreEqual(true, retorno, "SetupCobrança: " + mensagemErro);

            // Cria um novo Boleto Bancario na coleção de Boletos Bancarios
            retorno = classeProxy.NovoBoleto(ref mensagemErro);
            Assert.AreEqual(true, retorno, "NovoBoleto: " + mensagemErro);

            // Define os dados do Sacado
            retorno = classeProxy.DefinirSacado("123.456.789-09", aluno.Nome, aluno.Logradouro, aluno.NumeroLogradouro.ToString(), aluno.Complemento, aluno.Bairro, aluno.Cidade, aluno.UF, aluno.CEP.ToString(), "Observação do Sacado", ref mensagemErro);
            Assert.AreEqual(true, retorno, "DefinirSacado: " + mensagemErro);

            // Define os dados do Boleto
            retorno = classeProxy.DefinirBoleto("DM", "DP123456AZ", "445566", DateTime.Now.AddDays(-3), DateTime.Now, DateTime.Now.AddDays(+30), (decimal)123456.78, "CHAVEPRIMARIABANCO=12345!", "N", ref mensagemErro);
            Assert.AreEqual(true, retorno, "DefinirBoleto: " + mensagemErro);

            // Define multa (2%)
            retorno = classeProxy.DefinirMulta(classeProxy.boleto.DataVencimento, classeProxy.boleto.ValorTitulo * (decimal)0.02, (decimal)0.02, ref mensagemErro);
            Assert.AreEqual(true, retorno, "DefinirMulta: " + mensagemErro);

            // Define juros (6% ao mês)
            retorno = classeProxy.DefinirJuros(classeProxy.boleto.DataVencimento, classeProxy.boleto.ValorTitulo * (decimal)(0.06 / 30), (decimal)(0.06 / 30), ref mensagemErro);
            Assert.AreEqual(true, retorno, "DefinirJuros: " + mensagemErro);

            // Define desconto (5 dias antes do vencimento, R$ 10 de desconto)
            retorno = classeProxy.DefinirDesconto(classeProxy.boleto.DataVencimento.AddDays(-5), (decimal)10.00, ref mensagemErro);
            Assert.AreEqual(true, retorno, "DefinirDesconto: " + mensagemErro);

            // Define instruções de cobrança para o arquivo remessa e para ser impresso no boleto
            retorno = classeProxy.DefinirInstrucoes("Mensagem para ser impressa no boleto", "Mensagem para o arquivo remessa", "01", "02", "03", "04", "05", "06", ref mensagemErro);
            Assert.AreEqual(true, retorno, "DefinirInstrucoes: " + mensagemErro);

            // Fecha o boleto atual, valida os dados, etc.
            retorno = classeProxy.FecharBoleto(ref mensagemErro);
            Assert.AreEqual(true, retorno, "FecharBoleto: " + mensagemErro);

            // Repita os métodos acima para adicionar novos boletos, quantos necessários: NovoBoleto, DefinirSacado, DefinirBoleto, FecharBoleto, etc.
            // Após preencher a coleção com os boletos, siga com os exemplos abaixo...

            // Verifica se existe a pasta temporaria para receber os arquivos do teste:
            var nomePasta = "C:\\Boleto2Net\\";
            if (Directory.Exists(nomePasta) == false)
                Directory.CreateDirectory(nomePasta);

            // Para gerar o arquivo remessa, após preencher a coleção de Boletos:
            classeProxy.GerarRemessa((int)TipoArquivo.CNAB400, nomePasta + @"ClasseProxy_Cnab400.Rem", 1, ref mensagemErro);
            Assert.AreEqual(true, retorno, "GerarRemessa: " + mensagemErro);

            // Para gerar o arquivo PDF
            classeProxy.GerarBoletos(nomePasta + @"ClasseProxy_Boleto.Pdf", ref mensagemErro);
            Assert.AreEqual(true, retorno, "GerarBoletos: " + mensagemErro);

            return;
        }

        private string MontaBoletoHTML(Aluno aluno)
        {
            Boletos boletos = new Boletos();
            Boleto boleto;

            // Banco, Cedente, Conta Corrente
            boletos.Banco = Banco.Instancia(237);
            boletos.Banco.Cedente = new Cedente
            {
                CPFCNPJ = "12.123.123/1234-46",
                Nome = "Cedente Teste Classe Proxy",
                Observacoes = "Observacoes do Cedente",
                ContaBancaria = new ContaBancaria
                {
                    Agencia = "1234",
                    DigitoAgencia = "X",
                    OperacaoConta = "",
                    Conta = "123456",
                    DigitoConta = "X",
                    CarteiraPadrao = "09",
                    VariacaoCarteiraPadrao = "",
                    TipoCarteiraPadrao = TipoCarteira.CarteiraCobrancaSimples,
                    TipoFormaCadastramento = TipoFormaCadastramento.ComRegistro,
                    TipoImpressaoBoleto = TipoImpressaoBoleto.Empresa,
                    TipoDocumento = TipoDocumento.Escritural
                },
                Codigo = "1213141",
                CodigoDV = "0",
                CodigoTransmissao = "",
                Endereco = new Endereco
                {
                    LogradouroEndereco = "Av. 5 de Julho",
                    LogradouroNumero = "1194",
                    LogradouroComplemento = "",
                    Bairro = "Centro",
                    Cidade = "Indaiatuba",
                    UF = "SP",
                    CEP = "13330-220"
                }
            };
            boletos.Banco.FormataCedente();

            // Novo boleto:
            boleto = new Boleto(boletos.Banco);

            var sacado = new Sacado
            {
                CPFCNPJ = aluno.CPF,
                Nome = aluno.Nome,
                Observacoes = "",
                Endereco = new Endereco
                {
                    LogradouroEndereco = aluno.Logradouro,
                    LogradouroNumero = aluno.NumeroLogradouro.ToString(),
                    LogradouroComplemento = aluno.Complemento,
                    Bairro = aluno.Bairro,
                    Cidade = aluno.Cidade,
                    UF = aluno.UF,
                    CEP = aluno.CEP.ToString()
                }
            };
            boleto.Sacado = sacado;

            boleto.NumeroDocumento = "DP123456AZ";
            boleto.NumeroControleParticipante = "CHAVEPRIMARIABANCO=12345!";
            boleto.NossoNumero = "445566";
            boleto.DataEmissao = DateTime.Now.AddDays(-3);
            boleto.DataProcessamento = DateTime.Now;
            boleto.DataVencimento = DateTime.Now.AddDays(+30);
            boleto.ValorTitulo = (decimal)123456.78;
            boleto.Aceite = "N";
            boleto.EspecieDocumento = TipoEspecieDocumento.DM;

            //boleto.DataDesconto = dataDesconto;
            //boleto.ValorDesconto = valorDesconto;

            //boleto.DataMulta = dataMulta;
            //boleto.PercentualMulta = percMulta;
            //boleto.ValorMulta = valorMulta;

            //boleto.DataJuros = dataJuros;
            //boleto.PercentualJurosDia = percJuros;
            //boleto.ValorJurosDia = valorJuros;

            boleto.MensagemInstrucoesCaixa = "Mensagem para ser impressa no boleto";
            boleto.MensagemArquivoRemessa = "Mensagem para o arquivo remessa";
            boleto.CodigoInstrucao1 = "01";
            boleto.ComplementoInstrucao1 = "Instrução 01";
            boleto.CodigoInstrucao2 = "02"; ;
            boleto.ComplementoInstrucao2 = "Instrução 02";
            boleto.CodigoInstrucao3 = "03"; ;
            boleto.ComplementoInstrucao3 = "Instrução 03";

            //boleto.CodigoProtesto = (TipoCodigoProtesto)codigoProtesto;
            //boleto.DiasProtesto = diasProtesto;

            //boleto.CodigoBaixaDevolucao = (TipoCodigoBaixaDevolucao)codigoBaixaDevolucao;
            //boleto.DiasBaixaDevolucao = diasBaixaDevolucao;

            boleto.ValidarDados();
            boletos.Add(boleto);

            var html = new StringBuilder();
            html.Append("");
            foreach (Boleto boletoTmp in boletos)
            {
                //if (html.Length != 0)
                //{
                //    // Já existe um boleto, inclui quebra de linha.
                //    html.Append("</br></br></br></br></br></br></br></br></br></br>");
                //}
                using (BoletoBancario imprimeBoleto = new BoletoBancario
                {
                    //CodigoBanco = (short)boletoTmp.Banco.Codigo,
                    Boleto = boletoTmp,
                    OcultarInstrucoes = false,
                    MostrarComprovanteEntrega = true,
                    MostrarEnderecoCedente = true
                })
                {
                    //html.Append("<div style=\"page-break-after: always;\">");
                    html.Append(imprimeBoleto.MontaHtmlEmbedded(true));
                    //html.Append("</div>");
                }
            }

            return html.ToString();
        }

    }
}