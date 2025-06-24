using FinanceAPI.Data;
using FinanceAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Finance.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContaController : ControllerBase
    {
        //Faz a busca de todas contas bancarias cadastradas.
        [HttpGet]
        [Route("")]
        //[Authorize(Roles = "manager")]
        public async Task<ActionResult<List<ContaBancaria>>> Get([FromServices]DataContext context) 
        {
            var conta = await context.Contas.AsNoTracking().ToListAsync();
            return Ok(conta);
        }


        [HttpPost]
        [Route("")]
        //[AllowAnonymous] -- Questão de autenticação não entendi muito bem.
        public async Task<ActionResult<ContaBancaria>> Post([FromServices]DataContext context, [FromBody]ContaBancaria model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var banco = await context.Bancos.FindAsync(model.BancoId);
            if (banco == null)
            {
                return BadRequest(new {message = "Banco não encontrado."});
            }
            try
            {
                model.Banco = banco;
                //model.Role = "Funcionario"; <- Confirmar com lucão
                context.Contas.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possivel criar o usuário." });
            }
        }
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromServices] DataContext context, [FromBody] ContaBancaria model)
        {
            var conta = await context.Contas.AsNoTracking().Include(b =>  b.Banco ).Where(x => x.UserName == model.UserName && x.Password == model.Password).FirstOrDefaultAsync();
            if (conta == null)
            {
                return NotFound(new { message = "Nome de Usuário ou senha invalido" });
            }

            return Ok(conta);
        }

        [HttpPut]
        [Route("{id:int}")]
        //[Authorize(Roles = "manager")] <- Confirmar com lucao
        public async Task<ActionResult<ContaBancaria>> Put([FromServices]DataContext context, int id, [FromBody]ContaBancaria model)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            if (id != model.Id) 
            {
                return BadRequest(new {message = "Usuário não encontrado"});
            }
            try
            {
                context.Entry(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possivel atualizar o usuário." });
            }
        }

    }
}