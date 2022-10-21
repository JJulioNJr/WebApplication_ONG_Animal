namespace WebApplication_ONGAnimal.Utils {
    public interface IDatabaseSettings {


        string PessoaCollectionName { get; set; }
        string AnimalCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
