namespace SAE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1st : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbEmpresa",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Email = c.String(),
                        CNPJ = c.String(nullable: false, maxLength: 14),
                        DataInclusao = c.DateTime(),
                        CEP = c.Int(nullable: false),
                        Logradouro = c.String(nullable: false),
                        NumeroLogradouro = c.Int(nullable: false),
                        Complemento = c.String(maxLength: 30),
                        Cidade = c.String(nullable: false, maxLength: 50),
                        UF = c.String(nullable: false, maxLength: 2),
                        Bairro = c.String(nullable: false, maxLength: 50),
                        Telefone = c.String(maxLength: 15),
                        Responsavel = c.String(nullable: false, maxLength: 50),
                        Banco = c.Int(nullable: false),
                        Agencia = c.Int(nullable: false),
                        DigitoAgencia = c.Int(nullable: false),
                        Conta = c.Int(nullable: false),
                        DigitoConta = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tbEmpresa");
        }
    }
}
