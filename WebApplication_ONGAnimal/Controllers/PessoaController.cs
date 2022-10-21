using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using WebApplication_ONGAnimal.Models;
using WebApplication_ONGAnimal.Services;

namespace WebApplication_ONGAnimal.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase {


        private readonly PessoaServices _pessoaService;

        public PessoaController(PessoaServices pessoService) {
            _pessoaService = pessoService;
        }

        [HttpGet]
        public ActionResult<List<Pessoa>> Get() => _pessoaService.Get();


        [HttpGet("{id:length(24)}", Name = "GetPessoa")]
        public ActionResult<Pessoa> Get(string id) {
            var pessoa = _pessoaService.Get(id);
            if (pessoa == null) {
                return NotFound();
            } else {
                return Ok(pessoa);
            }
        }

        [HttpGet("Pessoa/{nome}", Name = "GetNomePessoa")]
        public ActionResult<Pessoa> GetNomePessoa(string nome) {
            var pessoa = _pessoaService.GetNome(nome);
            if (pessoa == null) {
                return NotFound();
            } else {
                return Ok(pessoa);
            }
        }

        [HttpPost]
        public ActionResult<Pessoa> Create(Pessoa pessoa) {
            _pessoaService.Create(pessoa);
            return CreatedAtRoute("GetPessoa", new { id = pessoa.Id.ToString() }, pessoa);
        }

        [HttpPut]
        public ActionResult<Pessoa> Update(string id, Pessoa pessoaIn) {

            var pessoa = _pessoaService.Get(id);
            if (pessoa == null) {
                return NotFound();
            } else {
                _pessoaService.Update(id, pessoaIn);
                pessoa = _pessoaService.Get(id);
            }
            return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(string id) {
            var pessoa = _pessoaService.Get(id);
            if (pessoa == null) {
                return NotFound();
            } else {
                _pessoaService.Remove(pessoa);

            }
            return NoContent();

        }

    }
}
