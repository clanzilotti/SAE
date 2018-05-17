using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace SAE.Models
{
    [Table("tbAluno")]
    public class Aluno
    {
        [Display(Name = "RA")]
        public int ID { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo Nome deve ser preenchido.")]
        //[StringLength(150, MinimumLength=4, ErrorMessage="Campo Nome deve ter entre 4 e 150 caracters.")]
        //[Remote("ValidarNomes", "CadastroAluno", ErrorMessage = "Nome já Cadastrado.", AdditionalFields = "IdAluno")]
        public string Nome { get; set; }

        [Display(Name = "Data Nascimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataBrasil(ErrorMessage = "Data de Nascimento inválida.", DataRequerida = false)]
        public DateTime? DataNascimento { get; set; }

        [Display(Name = "Sexo")]
        [DefaultValue("F")]
        [Required(ErrorMessage = "O campo {0} é necessário")]
        // [StringLength(50, MinimumLength=4, ErrorMessage="Campo Nome deve ter entre 4 e 50 caracters.")]
        //[Remote("ValidarNomes", "CadastroAluno", ErrorMessage = "Nome já Cadastrado.", AdditionalFields = "IdAluno")]
        public string Sexo { get; set; }

        [Display(Name = "E-mail")]
        [RegularExpression(@"^[a-zA-Z]+(([\'\,\.\- ][a-zA-Z ])?[a-zA-Z]*)*\s+<(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})>$|^(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})$", ErrorMessage = "Formato do E-mail Inválido.")]
        [EmailBrasil(ErrorMessage = "E-mail inválido.", EmailRequerido = false)]
        public string Email { get; set; }

        [Display(Name = "Est. Civil")]
        [DefaultValue("S")]
        [Required(ErrorMessage = "Campo Estado Civil deve ser informado.")]
        // [StringLength(50, MinimumLength=4, ErrorMessage="Campo Nome deve ter entre 4 e 50 caracters.")]
        //[Remote("ValidarNomes", "CadastroAluno", ErrorMessage = "Nome já Cadastrado.", AdditionalFields = "IdAluno")]
        public string EstadoCivil { get; set; }

        [Display(Name = "Cidade Natal")]
        [Required(ErrorMessage = "Campo Cidade Natal deve ser informado.")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Campo Cidade Natal deve 4 e 50 caracters.")]
        //[Remote("ValidarNomes", "CadastroAluno", ErrorMessage = "Nome já Cadastrado.", AdditionalFields = "IdAluno")]
        public string CidadeNatal { get; set; }

        [Display(Name = "Profissão")]
        [Required(ErrorMessage = "Campo Profissão deve ser informado.")]
        //[Remote("ValidarNomes", "CadastroAluno", ErrorMessage = "Nome já Cadastrado.", AdditionalFields = "IdAluno")]
        public string Profissao { get; set; }

        [Display(Name = "Empresa")]
        public string NomeEmpresa { get; set; }

        [Display(Name = "RG")]
        [Required(ErrorMessage = "Campo RG deve ser informado.")]
        [StringLength(12, MinimumLength = 6, ErrorMessage = "Campo RG deve ter no mínimo 4 e no máximo 12 caracters.")]
        public string RG { get; set; }

        [Display(Name = "Orgão Exp.")]
        [Required(ErrorMessage = "Campo Órgão Expedidor deve ser informado.")]
        [StringLength(6)]
        public string OrgaoExpRG { get; set; }

        [Display(Name = "Data Exp. RG")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataBrasil(ErrorMessage = "Data Exp. RG inválida.", DataRequerida = false)]
        public DateTime? DataExpRG { get; set; }

        [Display(Name = "CPF")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:###.###.###-##}")]
        [Required(ErrorMessage = "Campo CPF deve ser informado.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Campo CPF deve ter 11 caracters.")]
        ////[Remote("ValidarNomes", "CadastroAluno", ErrorMessage = "Nome já Cadastrado.", AdditionalFields = "IdAluno")]
        public string CPF { get; set; }

        [Display(Name = "Reservista")]
        [StringLength(15)]
        public string Reservista { get; set; }

        [Display(Name = "Passaporte")]
        [StringLength(10)]
        public string Passaporte { get; set; }

        [Display(Name = "Titulo Eleitor")]
        [StringLength(15)]
        public string TituloEleitor { get; set; }

        [Display(Name = "Zona")]
        [StringLength(6)]
        public string Zona { get; set; }

        [Display(Name = "Seção")]
        [StringLength(6)]
        public string Secao { get; set; }

        [Display(Name = "Dt.Emissão TE")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataBrasil(ErrorMessage = "Data Exp. RG inválida.", DataRequerida = false)]
        public DateTime? DataEmissaoTE { get; set; }

        [Display(Name = "Data Inclusão")]
        public DateTime? DataInclusao { get; set; }

        //*************************************** ENDEREÇO ****************************************

        [Display(Name = "CEP")]
        [Range(minimum: 10000000, maximum: 99999999, ErrorMessage = "CEP deve ser entre 1000000 e 99999999.")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{#####-###}")]
        //[NumeroBrasil(PontoMilharPermitido = false, DecimalRequerido = false)]
        public int CEP { get; set; }

        [Display(Name = "Logradouro")]
        [Required(ErrorMessage = "Campo Logradouro deve ser informado.")]
        //[StringLength(50, MinimumLength = 4, ErrorMessage = "Campo Empresa deve 4 e 50 caracters.")]
        //[Remote("ValidarNomes", "CadastroAluno", ErrorMessage = "Nome já Cadastrado.", AdditionalFields = "IdAluno")]
        public string Logradouro { get; set; }

        [Display(Name = "Nr.")]
        //[DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        //[NumeroBrasil(PontoMilharPermitido = false, DecimalRequerido = false)]
        public int NumeroLogradouro { get; set; }

        [Display(Name = "Complemento")]
        [StringLength(30)]
        public string Complemento { get; set; }

        [Display(Name = "Cidade")]
        [Required(ErrorMessage = "Campo Cidade deve ser informado.")]
        [StringLength(50)]
        public string Cidade { get; set; }

        [Display(Name = "UF")]
        [Required(ErrorMessage = "Campo UF deve ser informado.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Campo UF deve ter 2 caracteres.")]
        public string UF { get; set; }

        [Display(Name = "Bairro")]
        [Required(ErrorMessage = "Campo Bairro deve ser informado.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Campo Bairro deve ter de 5 a 50 caracteres.")]
        public string Bairro { get; set; }

        [Display(Name = "Fone Res.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:(##) ####-####}")]
        [StringLength(15)]
        public string TelefoneResidencial { get; set; }

        [Display(Name = "Fone Com.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:(##) ####-####}")]
        [StringLength(15)]
        //[Required(ErrorMessage = "Campo Fone Comercial deve ser informado.")]
        public string TelefoneComercial { get; set; }

        [Display(Name = "Fone Celular")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:(##) #####-####}")]
        [StringLength(15)]
        public string TelefoneCelular { get; set; }

        [Display(Name = "Quero receber e-mail")]
        public bool ReceberEmail { get; set; }

        //*************************************** DADOS BANCARIOS ****************************************

        [Display(Name = "% Bolsa")]
        [Range(minimum:0, maximum:100, ErrorMessage="% Bolsa deve estar entre 0% e 100%.")]
        //[DisplayFormat(DataFormatString = "{0:###%}", ApplyFormatInEditMode = true)]
        public double PercentualBolsa { get; set; }

        [Display(Name = "Dia de Vencimento")]
        [Required(ErrorMessage = "Campo Dia de Vencimento deve ser informado.")]
        [Range(minimum: 1, maximum: 31, ErrorMessage = "Dia de Vencimento deve estar entre 1 e 31.")]
        public int DiaVencimento { get; set; }

        //*************************************** RESPONSÁVEIS ****************************************

        //[Display(Name = "Nr.")]
        //[DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        //[NumeroBrasil(PontoMilharPermitido = false, DecimalRequerido = false)]
        //public int NumeroLogradouro { get; set; }

        //[Display(Name = "Complemento")]
        ////[Required(ErrorMessage = "Campo Complemento deve ser informado.")]
        ////[StringLength(50, MinimumLength = 4, ErrorMessage = "Campo Empresa deve 4 e 50 caracters.")]
        ////[Remote("ValidarNomes", "CadastroAluno", ErrorMessage = "Nome já Cadastrado.", AdditionalFields = "IdAluno")]
        //public string Complemento { get; set; }

        public virtual AlunoSituacao AlunoSituacao { get; set; }

        //[ForeignKey("AlunoSituacaoID")]
        [Display(Name = "Situação")]
        [DefaultValue(1)]
        public int AlunoSituacaoID { get; set; }

    }
}