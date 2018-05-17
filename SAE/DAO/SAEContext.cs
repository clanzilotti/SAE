namespace SAE.DAO
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using SAE.Models;
    using System.Data.Entity.ModelConfiguration;
    using System.Data.Entity.Validation;
    using System.Data.Entity.Infrastructure;
    using System.Data.SqlClient;

    public class SAEContext : DbContext
    {
        // Your context has been configured to use a 'SAEContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'SAE.DAO.SAEContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'SAEContext' 
        // connection string in the application configuration file.
        public SAEContext() : base("name=SAEContext")
        {
            Database.SetInitializer<SAEContext>(null);
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public DbSet<Aluno> Alunos { get; set; }

        public DbSet<AlunoSituacao> AlunosSituacao { get; set; }

        public DbSet<Empresa> Empresas { get; set; }

        //        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //        //{
        //        //    //base.OnModelCreating(modelBuilder);
        //        //    //EntityTypeConfiguration<PessoaEmail> cfg = 
        //        //    //modelBuilder.Entity<PessoaEmail>()
        //        //    //    .HasMany<Pessoa>(p => p.PessoaIDPessoa);
        //        //    //    //.WithOptional(pr => pr.PessoaEmail);

        //        //    //modelBuilder.Entity<PessoaEmail>()
        //        //    //    .
        //        //}

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors) // <-- Coloque um Breakpoint aqui para conferir os erros de validação.
                {
                    Console.WriteLine("Entidade do tipo \"{0}\" no estado \"{1}\" tem os seguintes erros de validação:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Erro: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (DbUpdateException e)
            {
                foreach (var eve in e.Entries)
                {
                    Console.WriteLine("Entidade do tipo \"{0}\" no estado \"{1}\" tem os seguintes erros de validação:",
                        eve.Entity.GetType().Name, eve.State);
                }
                throw;
            }
            catch (SqlException s)
            {
                Console.WriteLine("- Message: \"{0}\", Data: \"{1}\"",
                            s.Message, s.Data);
                throw;
            }
        }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}