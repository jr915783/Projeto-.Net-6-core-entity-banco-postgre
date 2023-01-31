using Domain.Entities;
using Repositories.Base;

namespace Repositories.Interface
{
    public interface ICadastro: IBaseRepository<EntityCadastros>
    {
        public Task<int> ValidacaoCadastro(EntityCadastros cadastro);
        public Task<EntityCadastros> GetByName(string name);
    }

    
}
