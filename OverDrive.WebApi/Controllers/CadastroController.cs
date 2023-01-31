using Data.Context;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interface;

namespace OverDrive.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CadastroController : ControllerBase
    {
        private readonly ICadastro _cadastro;
        private readonly IHistoricoCadastro _historicoCadastro;

        public CadastroController(ICadastro cadastro, IHistoricoCadastro historicoCadastro, DataContext dataContex)
        {
            _cadastro = cadastro;
            _historicoCadastro = historicoCadastro;
        }

        [HttpPost("Cadastrar")]
        public async Task<IActionResult> Cadastrar(EntityCadastros cadastro)
        {
            try
            {
                if (cadastro == null)
                {
                    return BadRequest($"Não Foi possível realizar esse cadastro {cadastro} !");
                }

                var result = await _cadastro.ValidacaoCadastro(cadastro);
                if (result == 0)
                {
                    await _cadastro.Insert(cadastro);
                    return Ok("Cadastro realizado com sucesso!");
                }
                else { return BadRequest("Documento e telefone já cadastrado por outra pessoa!"); }               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }

        [HttpGet("ListaCadastros")]
        public async Task<IActionResult> ListaCadastros()
        {
            var result = await _cadastro.GetAll();            
            return Ok(result.ToList());
        }

        [HttpGet("ObterCadastroPorId/{id}")]
        public async Task<IActionResult> ObterCadastroPorId(int id)
        {
            var result = await _cadastro.GetById(id);
            if(result == null)
            {
                return NotFound($"Cadastro com id {id}, não encontrado!");
            }
            return Ok(result);
        }

        [HttpGet("ObterCadastroPorNome/{nome}")]
        public async Task<IActionResult> ObterCadastroPorNome(string nome)
        {          
            try
            {
                var result = await _cadastro.GetByName(nome);
                if (result == null)
                {
                    return NotFound($"Cadastro com nome {nome}, não encontrado!");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }         

        }

        [HttpDelete("DeletarCadastro/{id}")]
        public async Task<IActionResult> DeletarCadastro(int id)
        {
            var result = await _cadastro.GetById(id);
            if (result == null)
            {
                return NotFound($"id {id}, não encontrado!");
            }
            await _cadastro.Delete(id);
            return Ok($"O Cadastro de {result.Nome}, foi deletado com sucesso!");
        }

        [HttpPut("AtualizarCadastro/{id}")]
        public async Task<IActionResult> AtualizarCadastro(int id, EntityCadastros cadastro)
        {
            
            if (id != cadastro.Id)
            {
                return BadRequest($"Cadastro com id {id} não encontrado!");
            }
            try
            {
                await _cadastro.Update(id, cadastro);

                await _historicoCadastro.Insert(new HistoricoCadastro
                {
                    Id = cadastro.Id,
                    Status = cadastro.Status,
                    Data = DateTime.Now.ToString(),
                    Usuario= cadastro.Usuario,
                });              


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
            return Ok($"Cadastro atualizado com sucesso!");
        }

        [HttpGet("ObterHistoricoPorId/{id}")]
        public async Task<IActionResult> ObterHistoricoPorId(int id)
        {
            var result = await _historicoCadastro.GetById(id);
            if (result == null)
            {
                return NotFound($"Histórico com id {id}, não encontrado!");
            }
            return Ok(result);
        }
    }
}
