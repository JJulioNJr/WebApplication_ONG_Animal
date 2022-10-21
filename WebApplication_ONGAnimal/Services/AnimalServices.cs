using MongoDB.Driver;
using System.Collections.Generic;
using System.Dynamic;
using WebApplication_ONGAnimal.Models;
using WebApplication_ONGAnimal.Utils;

namespace WebApplication_ONGAnimal.Services {
    public class AnimalServices {


        private readonly IMongoCollection<Pessoa> _pessoa;
        private readonly IMongoCollection<Animal> _animal;

        public AnimalServices(IDatabaseSettings settings) {
            var animal = new MongoClient(settings.ConnectionString);
            var database = animal.GetDatabase(settings.DatabaseName);
            _animal = database.GetCollection<Animal>(settings.AnimalCollectionName);
        }

        public Animal Create(Animal animal) {
            _animal.InsertOne(animal);
            return animal;
        }
        public List<Animal> Get() => _animal.Find<Animal>(animal => true).ToList();
        public Animal Get(string chip) => _animal.Find<Animal>(animal => animal.Chip == chip).FirstOrDefault();
        public List<Animal> GetNome(string nome) => _animal.Find<Animal>(animal => animal.Pessoa.Nome == nome).ToList();
        public Animal GetPessoa(string id) => _animal.Find<Animal>(animal => animal.Pessoa.Id == id).FirstOrDefault();
        public void Update(string chip, Animal animalIn) {
            _animal.ReplaceOne(animal => animal.Chip == chip, animalIn);
        }
        public void Remove(Animal animalIn) => _animal.DeleteOne(animal => animal.Chip == animalIn.Chip);

    }
}
