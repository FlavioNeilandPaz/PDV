/*******************************************************************************
Title: T2Ti ERP Fenix                                                                
Description: Controller relacionado à tabela [PESSOA] 
                                                                                
The MIT License                                                                 
                                                                                
Copyright: Copyright (C) 2020 T2Ti.COM                                          
                                                                                
Permission is hereby granted, free of charge, to any person                     
obtaining a copy of this software and associated documentation                  
files (the "Software"), to deal in the Software without                         
restriction, including without limitation the rights to use,                    
copy, modify, merge, publish, distribute, sublicense, and/or sell               
copies of the Software, and to permit persons to whom the                       
Software is furnished to do so, subject to the following                        
conditions:                                                                     
                                                                                
The above copyright notice and this permission notice shall be                  
included in all copies or substantial portions of the Software.                 
                                                                                
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,                 
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES                 
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND                        
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT                     
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,                    
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING                    
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR                   
OTHER DEALINGS IN THE SOFTWARE.                                                 
                                                                                
       The author may be contacted at:                                          
           t2ti.com@gmail.com                                                   
                                                                                
@author Albert Eije (alberteije@gmail.com)                    
@version 1.0.0
*******************************************************************************/
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using T2TiERPFenix.Models;
using T2TiERPFenix.Repository;
using T2TiERPFenix.Services;

namespace T2TiERPFenix.Controllers
{
    [Route("pessoa")]
    [Produces("application/json")]
    public class PessoaController : Controller
    {
        private IRepositoryWrapper _repository;
        private PessoaService _service;

        public PessoaController(IRepositoryWrapper repository)
        {
            _repository = repository;
            _service = new PessoaService();
        }

        [HttpGet]
        public IActionResult ConsultarListaPessoa([FromQuery]string filter)
        {
            try
            {
                IEnumerable<Pessoa> lista;
                if (filter == null)
                {
                    //lista = _repository.Pessoa.ConsultarLista();
                    lista = _service.ConsultarLista();
                }
                else
                {
                    // define o filtro
                    Filtro filtro = new Filtro(filter);
//                    lista = _repository.Pessoa.ConsultarListaFiltro(filtro);
                    lista = _service.ConsultarListaFiltro(filtro);
                }
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new RetornoJsonErro(500, "Erro no Servidor [Consultar Lista Pessoa]", ex));
            }
        }

        [HttpGet("{id}", Name = "ConsultarObjetoPessoa")]
        public IActionResult ConsultarObjetoPessoa(int id)
        {
            try
            {
                //              var objeto = _repository.Pessoa.ConsultarObjeto(id);
                var objeto = _service.ConsultarObjeto(id);

                if (objeto == null)
                {
                    return StatusCode(404, new RetornoJsonErro(404, "Registro não localizado [Consultar Objeto Pessoa]", null));
                }
                else
                {
                    return Ok(objeto);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new RetornoJsonErro(500, "Erro no Servidor [Consultar Objeto Pessoa]", ex));
            }
        }

        [HttpPost]
        public IActionResult InserirPessoa([FromBody]Pessoa objJson)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(400, new RetornoJsonErro(400, "Objeto inválido [Inserir Pessoa]", null));
                }
//                _repository.Pessoa.Inserir(objJson);
                _service.Inserir(objJson);

                return CreatedAtRoute("ConsultarObjetoPessoa", new { id = objJson.Id }, objJson);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new RetornoJsonErro(500, "Erro no Servidor [Inserir Pessoa]", ex));
            }
        }

        [HttpPut("{id}")]
        public IActionResult AlterarPessoa([FromBody]Pessoa objJson, int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(400, new RetornoJsonErro(400, "Objeto inválido [Alterar Pessoa]", null));
                }

                if (objJson.Id != id)
                {
                    return StatusCode(400, new RetornoJsonErro(400, "Objeto inválido [Alterar Pessoa] - ID do objeto difere do ID da URL.", null));
                }

                //var objBanco = _repository.Pessoa.ConsultarObjeto(objJson.Id);

                //if (objBanco == null)
                //{
                //    return StatusCode(400, new RetornoJsonErro(400, "Objeto com ID inválido [Alterar Pessoa]", null));
                //}
                _service.Alterar(objJson);

                //_repository.Pessoa.Alterar(objBanco, objJson);

                return ConsultarObjetoPessoa(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new RetornoJsonErro(500, "Erro no Servidor [Alterar Pessoa]", ex));
            }
        }

        [HttpDelete("{id}")]
        public IActionResult ExcluirPessoa(int id)
        {
            try
            {
                //var objeto = _repository.Pessoa.ConsultarObjeto(id);
                var objeto = _service.ConsultarObjeto(id);

                //                _repository.Pessoa.Excluir(objeto);
                _service.Excluir(objeto);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new RetornoJsonErro(500, "Erro no Servidor [Excluir Pessoa]", ex));
            }
        }

    }
}