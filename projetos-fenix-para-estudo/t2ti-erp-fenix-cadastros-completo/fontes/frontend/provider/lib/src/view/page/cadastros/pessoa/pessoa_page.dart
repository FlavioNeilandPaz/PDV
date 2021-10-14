/*
Title: T2Ti ERP Fenix                                                                
Description: AbaMestre Page relacionada à tabela [PESSOA] 
                                                                                
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
import 'dart:async';

import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import 'package:fenix/src/model/model.dart';
import 'package:fenix/src/view_model/view_model.dart';
import 'package:fenix/src/view/shared/view_util_lib.dart';

import 'pessoa_persiste_page.dart';
import 'pessoa_contato_lista_page.dart';
import 'pessoa_endereco_lista_page.dart';
import 'pessoa_fisica_persiste_page.dart';
import 'pessoa_juridica_persiste_page.dart';
import 'pessoa_telefone_lista_page.dart';

List<Aba> _todasAsAbas = <Aba>[];

List<Aba> getAbasAtivas() {
  List<Aba> retorno = [];
  for (var item in _todasAsAbas) {
    if (item.visible) retorno.add(item);
  }
  return retorno;
}

class PessoaPage extends StatefulWidget {
  final Pessoa pessoa;
  final String title;
  final String operacao;

  PessoaPage({this.pessoa, this.title, this.operacao, Key key})
      : super(key: key);

  @override
  PessoaPageState createState() => PessoaPageState();
}

class PessoaPageState extends State<PessoaPage>
    with SingleTickerProviderStateMixin {
  TabController _abasController;
  String _estiloBotoesAba = 'iconsAndText';

  // Pessoa
  final GlobalKey<FormState> _pessoaPersisteFormKey = GlobalKey<FormState>();
  final GlobalKey<ScaffoldState> _pessoaPersisteScaffoldKey = GlobalKey<ScaffoldState>();

  final GlobalKey<FormState> _pessoaFisicaPersisteFormKey = GlobalKey<FormState>();
  final GlobalKey<ScaffoldState> _pessoaFisicaPersisteScaffoldKey = GlobalKey<ScaffoldState>();
  final GlobalKey<FormState> _pessoaJuridicaPersisteFormKey = GlobalKey<FormState>();
  final GlobalKey<ScaffoldState> _pessoaJuridicaPersisteScaffoldKey = GlobalKey<ScaffoldState>();

  @override
  void initState() {
    super.initState();
    atualizarAbas();
    _abasController = TabController(vsync: this, length: getAbasAtivas().length);
    _abasController.addListener(salvarForms);
    ViewUtilLib.paginaMestreDetalheFoiAlterada = false; // vamos controlar as alterações nas paginas filhas aqui para alertar ao usuario sobre possivel perda de dados
  }

  @override
  void dispose() {
    _abasController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return ViewUtilLib.getScaffoldAbaPage(
        widget.title,
        context,
        _abasController,
        getAbasAtivas(),
        getIndicator(),
        _estiloBotoesAba,
        salvarPessoa,
        alterarEstiloBotoes,
        avisarUsuarioAlteracoesNaPagina);
  }

  void atualizarAbas() {
    _todasAsAbas.clear();
	 // a primeira aba sempre é a de Persistencia da tabela Mestre
    _todasAsAbas.add(Aba(
        icon: Icons.receipt,
        text: 'Detalhes',
        visible: true,
        pagina: PessoaPersistePage(
          formKey: _pessoaPersisteFormKey,
          scaffoldKey: _pessoaPersisteScaffoldKey,
          pessoa: widget.pessoa,
          atualizaPessoaCallBack: this.atualizarDados,
        )));
    _todasAsAbas.add(Aba(
    	icon: Icons.person,
    	text: 'Pessoa Fisica',
    	visible: widget.pessoa.tipo == 'Física',
    	pagina: PessoaFisicaPersistePage(
    		formKey: _pessoaFisicaPersisteFormKey,
    		scaffoldKey: _pessoaFisicaPersisteScaffoldKey,
    		pessoa: widget.pessoa)));
    _todasAsAbas.add(Aba(
    	icon: Icons.business,
    	text: 'Pessoa Juridica',
    	visible: widget.pessoa.tipo == 'Jurídica',
    	pagina: PessoaJuridicaPersistePage(
    		formKey: _pessoaJuridicaPersisteFormKey,
    		scaffoldKey: _pessoaJuridicaPersisteScaffoldKey,
    		pessoa: widget.pessoa)));
    _todasAsAbas.add(Aba(
    	icon: Icons.group,
    	text: 'Relação - Pessoa Contato',
    	visible: true,
    	pagina: PessoaContatoListaPage(pessoa: widget.pessoa)));
    _todasAsAbas.add(Aba(
    	icon: Icons.place,
    	text: 'Relação - Pessoa Endereco',
    	visible: true,
    	pagina: PessoaEnderecoListaPage(pessoa: widget.pessoa)));
    _todasAsAbas.add(Aba(
    	icon: Icons.phone,
    	text: 'Relação - Pessoa Telefone',
    	visible: true,
    	pagina: PessoaTelefoneListaPage(pessoa: widget.pessoa)));
  }

  void atualizarDados() { // serve para atualizar algum dado após alguma ação numa página filha
    setState(() {
      if (widget.pessoa.tipo == 'Física') {
          widget.pessoa.pessoaJuridica = null;
      } else {
          widget.pessoa.pessoaFisica = null;
      }
      atualizarAbas();
    });
  }

  void salvarForms() {
    // valida e salva o form PessoaDetalhe
    FormState formPessoa = _pessoaPersisteFormKey.currentState;
    if (formPessoa != null) {
      if (!formPessoa.validate()) {
        _abasController.animateTo(0);
      } else {
        _pessoaPersisteFormKey.currentState?.save();
      }
    }

    // valida e salva os forms OneToOne
    FormState formPessoaFisica = _pessoaFisicaPersisteFormKey.currentState;
    if (formPessoaFisica != null) {
    	if (!formPessoaFisica.validate()) {
    		_abasController.animateTo(1);
    	} else {
    		_pessoaFisicaPersisteFormKey.currentState?.save();
    	}
    }
    FormState formPessoaJuridica = _pessoaJuridicaPersisteFormKey.currentState;
    if (formPessoaJuridica != null) {
    	if (!formPessoaJuridica.validate()) {
    		_abasController.animateTo(1);
    	} else {
    		_pessoaJuridicaPersisteFormKey.currentState?.save();
    	}
    }
  }

  void salvarPessoa() async {
    salvarForms();
    var pessoaProvider = Provider.of<PessoaViewModel>(context);
    if (widget.operacao == 'A') {
      await pessoaProvider.alterar(widget.pessoa);
    } else {
      await pessoaProvider.inserir(widget.pessoa);
    }
    Navigator.pop(context);
  }

  void alterarEstiloBotoes(String style) {
    setState(() {
      _estiloBotoesAba = style;
    });
  }

  Decoration getIndicator() {
    return ViewUtilLib.getShapeDecorationAbaPage(_estiloBotoesAba);
  }

  Future<bool> avisarUsuarioAlteracoesNaPagina() async {
    if (!ViewUtilLib.paginaMestreDetalheFoiAlterada) return true;
    return await ViewUtilLib.gerarDialogBoxFormAlterado(context);
  }
}