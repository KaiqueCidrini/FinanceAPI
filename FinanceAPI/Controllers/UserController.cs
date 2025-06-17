using Microsoft.AspNetCore.Mvc;
using static Finance.API.Controllers.UserController;

namespace Finance.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        public UserController()
        {

        }

        public class User
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        [HttpPost]
        public List<User> Create([FromBody] User user)
        {
            // Simula uma lista de usuários já cadastrados
            List<User> userList = new();

            // Adiciona um novo usuário na lista
            userList.Add(user);
            
            // Retorna a lista atualizada
            return userList;
        }

        [HttpPut("{id}")]
        public List<User> Update([FromBody] User user, [FromRoute] int id)
        {
            List<User> userList = [
                new(){Id = 1, Name = "Lucas"},
                new(){Id = 2, Name = "João"},
                new(){Id = 3, Name = "Nautinha"}
            ];

            var oldUser = userList.Find(u => u.Id == id);

            if (oldUser is null) return [];

            oldUser.Name = user.Name;
               
            return userList;

        }


        [HttpGet]
        public List<string> GetAll()
        {
            List<string> userNameList =
            [
                "Lucas",
                "Kaique"
            ];

            return userNameList;
        }

        [HttpDelete("{userName}")]
        public List<string> Delete([FromRoute] string userName)
        {
            List<string> userNameList =
            [
                "Lucas",
                "Kaique"
            ];

            userNameList.Remove(userName);

            return userNameList;
        }
    }
}