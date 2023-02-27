using Domain.Entities;
using Repositories.Base;

namespace Repositories.Interface
{
    public interface ICadastro: IBaseRepository<EntityCadastrosVeiculo>
    {
        public Task<int> ValidacaoCadastro(EntityCadastrosVeiculo cadastro);
        public Task<EntityCadastrosVeiculo> GetByChassi(string chassi);
    }

    
}
