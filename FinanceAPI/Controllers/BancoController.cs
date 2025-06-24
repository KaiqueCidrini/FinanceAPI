using FinanceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using FinanceAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace FinanceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BancoController : ControllerBase
    {

        //Faz a busca de um banco cadastrado através do ID.
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Banco>> GetById(int id, [FromServices] DataContext context)
        {   
            var bancos = await context.Bancos.AsNoTracking().FirstOrDefaultAsync();
            return Ok(bancos);
        }

        //Lista todos os bancos cadastrados.
        [HttpGet]
        public async Task<ActionResult<List<Banco>>> GetAll([FromServices]DataContext context)
        {

            var bancos = await context.Bancos.AsNoTracking().ToListAsync();
            return Ok(bancos);
        }
        //Deleta um banco cadastrado através do ID.
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Banco>> Delete(int id, [FromServices]DataContext context)
        {
            var banco = await context.Bancos.FirstOrDefaultAsync(x => x.Id == id);
            if(banco == null)
            {
                return NotFound(new { Message = "Banco não encontrado." });
            }
            try
            {
                context.Bancos.Remove(banco);
                await context.SaveChangesAsync();
                return Ok(new { Message = "Banco removido com sucesso!" });
            }
            catch(Exception)
            {
                return BadRequest(new { Message = "Não foi possivel encontrar o banco." });
            }
        }
        //Gera um novo banco no database.
        [HttpPost]
        public async Task<ActionResult<Banco>> Create([FromBody]Banco model, [FromServices]DataContext context)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                context.Bancos.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception)
            {
               return BadRequest(new { Message = "Não foi possivel criar o banco" });
            }
        }
        //Atualiza um banco.
        [HttpPut("")]
        public async Task<ActionResult<Banco>> Update([FromBody]Banco model, [FromServices]DataContext context)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                context.Entry<Banco>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(model); 
            }
            catch(DbUpdateConcurrencyException)
            {
                return BadRequest(new { Message = "Não foi possivel atualizar o Banco" });
            }
            catch(Exception)
            {
                return BadRequest(new { Message = "Não foi possivel atualizar o Banco" });
            }
        }

    }
}
