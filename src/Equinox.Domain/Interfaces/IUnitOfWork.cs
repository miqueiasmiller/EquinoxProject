using System;
using System.Data;

namespace Equinox.Domain.Interfaces
{
   public interface IUnitOfWork : IDisposable
   {
      bool Commit();
   }

   public interface IUnitOfWorkADO : IUnitOfWork
   {
      IDbCommand CreateCommand();
   }
}
