package com.t2ti.fenix.controller.cadastros;

import java.util.List;
import java.util.NoSuchElementException;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.t2ti.fenix.exception.RecursoNaoEncontradoException;
import com.t2ti.fenix.model.cadastros.Fornecedor;
import com.t2ti.fenix.services.cadastros.FornecedorService;

@RestController
@RequestMapping("/fornecedor")
public class FornecedorController {

	@Autowired
	private FornecedorService service;
	
	@GetMapping
	public List<Fornecedor> listaTodos() {
		return service.listaTodos();
	}
	
	@GetMapping("/lista/{nome}")
	public List<Fornecedor> listaTodos(@PathVariable String nome) {
		return service.listaTodos(nome);
	}
	
	@GetMapping("/{id}")
	public Fornecedor buscaPorId(@PathVariable Integer id) {
		try {
			return service.buscaPorId(id);
		} catch (NoSuchElementException e) {
			throw new RecursoNaoEncontradoException("Registro não localizado.");
		}
	}
	
	@PostMapping
	public Fornecedor salvaObjeto(@RequestBody Fornecedor fornecedor) {
		return service.salvaObjeto(fornecedor);
	}
	
	@DeleteMapping("/{id}")
	public void excluirObjeto(@PathVariable Integer id) {
		service.excluirObjeto(id);
	}
	
}
