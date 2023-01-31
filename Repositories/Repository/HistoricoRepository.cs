using Data.Context;
using Domain.Entities;
using Repositories.Base;
using Repositories.Interface;

namespace Repositories.Repository
{
    public class HistoricoRepository : BaseRepositorio<HistoricoCadastro>, IHistoricoCadastro
    {
        public HistoricoRepository(DataContext dataContex) : base(dataContex)
        {
        }

    }
}
