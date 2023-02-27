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

        public CadastroController(ICadastro cadastro, DataContext dataContex)
        {
            _cadastro = cadastro;           
        }

        [HttpPost("Cadastrar")]
        public async Task<IActionResult> Cadastrar(EntityCadastrosVeiculo cadastro)
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
                    return Ok("Veiculo cadastro realizado com sucesso!");
                }
                else { return BadRequest("Número de chassi já cadastrado!"); }               
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

        [HttpGet("ObterCadastroPorChassi/{chassi}")]
        public async Task<IActionResult> ObterCadastroPorChassi(string chassi)
        {          
            try
            {
                var result = await _cadastro.GetByChassi(chassi);
                if (result == null)
                {
                    return NotFound($"Cadastro com chassi {chassi}, não encontrado!");
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
            return Ok($"O Veículo de Chassi {result.Chassi}, foi deletado com sucesso!");
        }

        [HttpPut("AtualizarCadastro")]
        public async Task<IActionResult> AtualizarCadastro(EntityCadastrosVeiculo cadastro)
        {
           // var result = await _cadastro.ValidacaoCadastro(cadastro);

            if(cadastro != null)
            {
                try
                {
                    await _cadastro.Update(cadastro);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                return Ok($"Cadastro atualizado com sucesso!");

            }
            else { return BadRequest("Objeto não encontrado!"); }            

        }
       
    }
}
