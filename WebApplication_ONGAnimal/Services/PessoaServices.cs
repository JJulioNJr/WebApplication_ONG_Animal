using MongoDB.Driver;
using System;
using System.Collections.Generic;
using WebApplication_ONGAnimal.Models;
using WebApplication_ONGAnimal.Utils;

namespace WebApplication_ONGAnimal.Services {
    public class PessoaServices {


        private readonly IMongoCollection<Pessoa> _pessoa;


        public PessoaServices(IDatabaseSettings settings) {

            var pessoa = new MongoClient(settings.ConnectionString);
            var database = pessoa.GetDatabase(settings.DatabaseName);
            _pessoa=database.GetCollection<Pessoa>(settings.PessoaCollectionName);

        }

        public Pessoa Create(Pessoa pessoa) {
            _pessoa.InsertOne(pessoa);
            return pessoa;
        }

        public List<Pessoa> Get() => _pessoa.Find<Pessoa>(pessoa => true).ToList();
        public Pessoa Get(string id) => _pessoa.Find<Pessoa>(pessoa => pessoa.Id == id).FirstOrDefault();
        public void Update(string id, Pessoa pessoaIn) {
            _pessoa.ReplaceOne(pessoa => pessoa.Id == id, pessoaIn);
        }
        public void Remove(Pessoa pessoaIn) => _pessoa.DeleteOne(pessoa => pessoa.Id == pessoaIn.Id);
        public List<Pessoa> GetNome(string nome) => _pessoa.Find<Pessoa>(pessoa => pessoa.Nome == nome).ToList();

    }
}
