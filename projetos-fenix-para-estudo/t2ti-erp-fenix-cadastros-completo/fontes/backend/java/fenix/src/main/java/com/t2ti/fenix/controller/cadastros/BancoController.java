/*******************************************************************************
Title: T2Ti ERP Fenix                                                                
Description: Controller relacionado à tabela [BANCO] 
                                                                                
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
package com.t2ti.fenix.controller.cadastros;

import java.util.List;
import java.util.NoSuchElementException;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import com.t2ti.fenix.exception.ExcecaoGenericaServidorException;
import com.t2ti.fenix.exception.RecursoNaoEncontradoException;
import com.t2ti.fenix.exception.RequisicaoRuimException;
import com.t2ti.fenix.model.cadastros.Banco;
import com.t2ti.fenix.model.transiente.Filtro;
import com.t2ti.fenix.service.cadastros.BancoService;

@RestController
@RequestMapping(value = "/banco", produces = "application/json;charset=UTF-8")
public class BancoController {

	@Autowired
	private BancoService service;
	
	@GetMapping
	public List<Banco> consultarLista(@RequestParam(required = false) String filter) {
		try {
			if (filter == null) {
				return service.consultarLista();
			} else {
				// define o filtro
				Filtro filtro = new Filtro(filter);
				return service.consultarLista(filtro);				
			}
		} catch (Exception e) {
			throw new ExcecaoGenericaServidorException("Erro no Servidor [Consultar Lista Banco] - Exceção: " + e.getMessage());
		}
	}

	@GetMapping("/{id}")
	public Banco ConsultarObjeto(@PathVariable Integer id) {
		try {
			try {
				return service.consultarObjeto(id);
			} catch (NoSuchElementException e) {
				throw new RecursoNaoEncontradoException("Registro não localizado [Consultar Objeto Banco].");
			}
		} catch (Exception e) {
			throw new ExcecaoGenericaServidorException("Erro no Servidor [Consultar Objeto Banco] - Exceção: " + e.getMessage());
		}
	}
	
	@PostMapping
	public Banco inserir(@RequestBody Banco objJson) {
		try {
			return service.salvar(objJson);
		} catch (Exception e) {
			throw new ExcecaoGenericaServidorException("Erro no Servidor [Inserir Banco] - Exceção: " + e.getMessage());
		}
	}

	@PutMapping("/{id}")
	public Banco alterar(@RequestBody Banco objJson, @PathVariable Integer id) {	
		try {			
			if (!objJson.getId().equals(id)) {
				throw new RequisicaoRuimException("Objeto inválido [Alterar Banco] - ID do objeto difere do ID da URL.");
			}

			Banco objeto = service.consultarObjeto(objJson.getId());
			if (objeto != null) {
				return service.salvar(objJson);				
			} else
			{
				throw new RequisicaoRuimException("Objeto com ID inválido [Alterar Banco].");				
			}
		} catch (Exception e) {
			throw new ExcecaoGenericaServidorException("Erro no Servidor [Alterar Banco] - Exceção: " + e.getMessage());
		}
	}
	
	@DeleteMapping("/{id}")
	public void excluir(@PathVariable Integer id) {
		try {
			service.excluir(id);
		} catch (Exception e) {
			throw new ExcecaoGenericaServidorException("Erro no Servidor [Excluir Banco] - Exceção: " + e.getMessage());
		}
	}
	
}