using FinanceAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinanceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BancoController : ControllerBase
    {
        public BancoController()
        {

        }

        [HttpGet("{bankName}")]
        public string Get([FromRoute] string bankName)
        {
            List<string> bankList = new()
            {
               "Itau",
               "Nubank"
            };
            if (bankList.Contains(bankName))
            {
                return bankName;
            } else
            {
                return ("Valor Invalido");
            }
        }
        [HttpGet]
        public List<string> GetAll()
        {
            List<string> bankList = new()
            {
               "Itau",
               "Nubank"
            };
            return bankList;
        }
        [HttpDelete("{bankName}")]
        public List<string> Delete([FromRoute] string bankName)
        {
            List<string> bankList = new()
            {
               "Itau",
               "Nubank"
            };
            bankList.Remove(bankName);
            return bankList;
        }
        [HttpPost]
        public List<BancoModel> Create([FromBody] BancoModel model)
        {
            List<BancoModel> bancoList = new();
            bancoList.Add(model);
            return bancoList;
        }
        [HttpPut("{id}")]
        public List<BancoModel> Update([FromBody] BancoModel bancoName, [FromRoute] int id)
        {
            List<BancoModel> bancoList = new List<BancoModel>();
            bancoList.Add(new BancoModel(1, "Nubank"));
            bancoList.Add(new BancoModel(2, "Itau"));
            bancoList.Add(new BancoModel(3, "Inter"));

            var oldName = bancoList.Find(b => b.Id == id);
            if (oldName == null)
                return [];

            oldName.Name = bancoName.Name;
            return bancoList;
        }

    }
}
