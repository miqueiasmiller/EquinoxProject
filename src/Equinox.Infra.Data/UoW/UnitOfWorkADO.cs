using Equinox.Domain.Interfaces;
using Equinox.Infra.Data.Context;
using System.Data;
using System.Data.SqlClient;

namespace Equinox.Infra.Data.UoW
{
   public class UnitOfWorkADO : IUnitOfWorkADO
   {
      protected IDbConnection connection;
      protected IDbTransaction transaction;

      public UnitOfWorkADO()
      {
         connection = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=Equinox;Trusted_Connection=True;MultipleActiveResultSets=true");
         transaction = connection.BeginTransaction();
      }

      public bool Commit()
      {
         try
         {
            if (transaction == null)
               throw new System.Exception("Invalid transaction");

            transaction.Commit();
         }
         catch 
         {
            //TODO: do something with the error
            return false;
         }

         return true;
      }

      public IDbCommand CreateCommand()
      {
         var command = connection.CreateCommand();
         command.Transaction = transaction;

         return command;
      }

      public void Dispose()
      {
         if (transaction != null)
         {
            transaction.Rollback();
            transaction = null;
         }

         if (connection != null)
         {
            connection.Close();
            connection = null;
         }
      }
   }
}
