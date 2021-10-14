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
import com.t2ti.fenix.model.cadastros.TipoColaborador;
import com.t2ti.fenix.services.cadastros.TipoColaboradorService;

@RestController
@RequestMapping("/tipoColaborador")
public class TipoColaboradorController {

	@Autowired
	private TipoColaboradorService service;
	
	@GetMapping
	public List<TipoColaborador> listaTodos() {
		return service.listaTodos();
	}
	
	@GetMapping("/lista/{nome}")
	public List<TipoColaborador> listaTodos(@PathVariable String nome) {
		return service.listaTodos(nome);
	}
	
	@GetMapping("/{id}")
	public TipoColaborador buscaPorId(@PathVariable Integer id) {
		try {
			return service.buscaPorId(id);
		} catch (NoSuchElementException e) {
			throw new RecursoNaoEncontradoException("Registro não localizado.");
		}
	}
	
	@PostMapping
	public TipoColaborador salvaObjeto(@RequestBody TipoColaborador tipoColaborador) {
		return service.salvaObjeto(tipoColaborador);
	}
	
	@DeleteMapping("/{id}")
	public void excluirObjeto(@PathVariable Integer id) {
		service.excluirObjeto(id);
	}
	
}
