namespace FinanceAPI.Models
{
    public class BancoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public BancoModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
