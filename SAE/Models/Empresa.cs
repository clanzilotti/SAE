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
    [Table("tbEmpresa")]
    public class Empresa
    {
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo Nome deve ser preenchido.")]
        //[StringLength(150, MinimumLength=4, ErrorMessage="Campo Nome deve ter entre 4 e 150 caracters.")]
        //[Remote("ValidarNomes", "CadastroAluno", ErrorMessage = "Nome já Cadastrado.", AdditionalFields = "IdAluno")]
        public string Nome { get; set; }

        [Display(Name = "E-mail")]
        [RegularExpression(@"^[a-zA-Z]+(([\'\,\.\- ][a-zA-Z ])?[a-zA-Z]*)*\s+<(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})>$|^(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})$", ErrorMessage = "Formato do E-mail Inválido.")]
        [EmailBrasil(ErrorMessage = "E-mail inválido.", EmailRequerido = false)]
        public string Email { get; set; }

        [Display(Name = "CNPJ")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:##.###.###/####-##}")]
        [Required(ErrorMessage = "Campo CNPJ deve ser informado.")]
        [StringLength(14, MinimumLength = 11, ErrorMessage = "Campo CNPJ deve ter 14 caracters.")]
        ////[Remote("ValidarNomes", "CadastroAluno", ErrorMessage = "Nome já Cadastrado.", AdditionalFields = "IdAluno")]
        public string CNPJ { get; set; }

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

        [Display(Name = "Telefone")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:(##) ####-####}")]
        [StringLength(15)]
        public string Telefone { get; set; }

        [Display(Name = "Responsável")]
        [Required(ErrorMessage = "Campo Responsável deve ser informado.")]
        [StringLength(50)]
        public string Responsavel { get; set; }

        //*************************************** DADOS BANCARIOS ****************************************

        [Display(Name = "Nº do Banco")]
        [Required(ErrorMessage = "Campo Nº do Banco deve ser informado.")]
        [Range(minimum: 1, maximum: 9999)]
        public int Banco { get; set; }

        [Display(Name = "Nº da Agência")]
        [Required(ErrorMessage = "Campo Nº Agência deve ser informado.")]
        [Range(minimum: 1, maximum: 9999)]
        public int Agencia { get; set; }

        [Display(Name = "Dígito da Agência")]
        [Required(ErrorMessage = "Campo Dígito Agência deve ser informado.")]
        [Range(minimum: 1, maximum: 99)]
        public int DigitoAgencia { get; set; }

        [Display(Name = "Nº da Conta")]
        [Required(ErrorMessage = "Campo Nº Conta deve ser informado.")]
        [Range(minimum: 1, maximum: 9999)]
        public int Conta { get; set; }

        [Display(Name = "Dígito da Conta")]
        [Required(ErrorMessage = "Campo Dígito da Conta deve ser informado.")]
        [Range(minimum: 1, maximum: 99)]
        public int DigitoConta { get; set; }

    }
}