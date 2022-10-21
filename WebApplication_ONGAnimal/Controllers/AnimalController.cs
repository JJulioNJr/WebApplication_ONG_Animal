using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplication_ONGAnimal.Models;
using WebApplication_ONGAnimal.Services;

namespace WebApplication_ONGAnimal.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : ControllerBase {

        private readonly AnimalServices _animalService;
        private readonly PessoaServices _pessoaServices;

        public AnimalController(AnimalServices animalServices, PessoaServices pessoaServices) {
            _animalService = animalServices;
            _pessoaServices = pessoaServices;
        }

        [HttpGet]
        public ActionResult<List<Animal>> Get() => _animalService.Get();

        [HttpGet("{chip}", Name = "GetAnimal")]
        public ActionResult<List<Animal>> Get(string chip) {
            var animal = _animalService.Get(chip);
            if (animal == null) {
                return NotFound();
            } else {
                return Ok(animal);
            }
        }

        [HttpGet("Pessoa/{nome}", Name = "GetPessoaNome")]
        public ActionResult<Animal> GetPessoaNome(string nome) {
            var client = _animalService.GetNome(nome);
            if (client == null) {
                return NotFound();
            } else {
                return Ok(client);
            }
        }

        [HttpGet("Pessoa/{id:length(24)}/", Name = "Get")]
        public ActionResult<Animal> GetPessoa(string id) {
            var pessoa = _animalService.GetPessoa(id);

            if (pessoa == null)
                return NotFound();

            return Ok(pessoa);
        }

        [HttpPost]
        public ActionResult<List<Animal>> Create(Animal animal) {

            Pessoa pessoa = _pessoaServices.Create(animal.Pessoa);
            animal.Pessoa = pessoa;

            _animalService.Create(animal);
            return CreatedAtRoute("GetAnimal", new { chip = animal.Chip.ToString() }, animal);

        }

        [HttpPut]
        public ActionResult<Animal> Update(string chip, Animal animalIn) {

            var animal = _animalService.Get(chip);
            if (animal == null) {
                return NotFound();
            } else {
                _animalService.Update(chip, animalIn);
                animal = _animalService.Get(chip);
            }
            return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(string chip) {
            var animal = _animalService.Get(chip);
            if (animal == null) {
                return NotFound();
            } else {
                _animalService.Remove(animal);
              
            }
            return NoContent();

        }

    }
}
