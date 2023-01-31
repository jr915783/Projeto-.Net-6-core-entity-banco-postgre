using Data.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Base;
using Repositories.Interface;

namespace Repositories.Repository
{
    public class CadastroRepository : BaseRepositorio<EntityCadastros>, ICadastro
    {
        public readonly DataContext _cadastro;
        public CadastroRepository(DataContext dataContex) : base(dataContex)
        {
            _cadastro = dataContex;
           
        }

        public async Task<int> ValidacaoCadastro(EntityCadastros cadastro)
        {
           var validacaoCadastro = await _cadastro.Set<EntityCadastros>().Where(x => x.Documento == cadastro.Documento && x.Telefone == cadastro.Telefone).ToListAsync();
            return (validacaoCadastro.Count); 
        }

        public  Task<EntityCadastros> GetByName(string name){
           
            try
            {
                return _cadastro.Set<EntityCadastros>().OrderBy(n => n.Nome).Where(x => x.Documento.ToLower().Contains(name.ToLower()))
                 .Select(x => new EntityCadastros
                 {
                     Documento = x.Documento,
                     Telefone = x.Telefone,
                     Nome = x.Nome,
                     Id = x.Id,
                     Status = x.Status,
                     Usuario = x.Usuario
                 }).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }          
     
        }
    }
}
