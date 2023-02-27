using Data.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Base;
using Repositories.Interface;

namespace Repositories.Repository
{
    public class CadastroRepository : BaseRepositorio<EntityCadastrosVeiculo>, ICadastro
    {
        public readonly DataContext _cadastro;
        public CadastroRepository(DataContext dataContex) : base(dataContex)
        {
            _cadastro = dataContex;
           
        }

        public async Task<int> ValidacaoCadastro(EntityCadastrosVeiculo cadastro)
        {
           var validacaoCadastro = await _cadastro.Set<EntityCadastrosVeiculo>().Where(x => x.Chassi == cadastro.Chassi).ToListAsync();
            return (validacaoCadastro.Count); 
        }

        public  Task<EntityCadastrosVeiculo> GetByChassi(string chassi){
           
            try
            {
                return _cadastro.Set<EntityCadastrosVeiculo>().OrderBy(n => n.Chassi).Where(x => x.Chassi.ToLower().Contains(chassi.ToLower()))
                 .Select(x => new EntityCadastrosVeiculo
                 {
                     Chassi = x.Chassi,
                     Tipo = x.Tipo,
                     NumeroPassageiros = x.NumeroPassageiros,
                     Cor = x.Cor

                 }).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }          
     
        }
    }
}
