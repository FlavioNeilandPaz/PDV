/*
Title: T2Ti ERP Fenix                                                                
Description: PersistePage relacionada à tabela [PRODUTO_GRUPO] 
                                                                                
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
import 'package:flutter/material.dart';
import 'package:flutter/gestures.dart';
import 'package:provider/provider.dart';

import 'package:fenix/src/model/model.dart';
import 'package:fenix/src/view_model/view_model.dart';
import 'package:fenix/src/view/shared/view_util_lib.dart';


class ProdutoGrupoPersistePage extends StatefulWidget {
  final ProdutoGrupo produtoGrupo;
  final String title;
  final String operacao;

  const ProdutoGrupoPersistePage({Key key, this.produtoGrupo, this.title, this.operacao})
      : super(key: key);

  @override
  ProdutoGrupoPersistePageState createState() => ProdutoGrupoPersistePageState();
}

class ProdutoGrupoPersistePageState extends State<ProdutoGrupoPersistePage> {
  final GlobalKey<ScaffoldState> _scaffoldKey = GlobalKey<ScaffoldState>();
  final GlobalKey<FormState> _formKey = GlobalKey<FormState>();
  bool _autoValidate = false;
  bool _formFoiAlterado = false;

  @override
  Widget build(BuildContext context) {
    var produtoGrupoProvider = Provider.of<ProdutoGrupoViewModel>(context);
	
	
    return Scaffold(
      drawerDragStartBehavior: DragStartBehavior.down,
      key: _scaffoldKey,
      appBar: AppBar(
        title: Text(widget.title),
        actions: <Widget>[
          IconButton(
            icon: ViewUtilLib.getIconBotaoSalvar(),
            onPressed: () async {
              final FormState form = _formKey.currentState;
              if (!form.validate()) {
                _autoValidate = true;
                ViewUtilLib.showInSnackBar(
                    'Por favor, corrija os erros apresentados antes de continuar.',
                    _scaffoldKey);
              } else {
                form.save();
                if (widget.operacao == 'A') {
                  await produtoGrupoProvider.alterar(widget.produtoGrupo);
                } else {
                  await produtoGrupoProvider.inserir(widget.produtoGrupo);
                }
                Navigator.pop(context);
              }
            },
          ),
        ],
      ),
      body: SafeArea(
        top: false,
        bottom: false,
        child: Form(
          key: _formKey,
          autovalidate: _autoValidate,
          onWillPop: _avisarUsuarioFormAlterado,
          child: Scrollbar(
            child: SingleChildScrollView(
              dragStartBehavior: DragStartBehavior.down,
              padding: const EdgeInsets.symmetric(horizontal: 16.0),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.stretch,
                children: <Widget>[
				  const SizedBox(height: 24.0),
				  TextFormField(
				  	maxLength: 100,
				  	maxLines: 1,
				  	initialValue: widget.produtoGrupo?.nome ?? '',
				  	decoration: ViewUtilLib.getInputDecorationPersistePage(
				  		'Informe o Nome do Grupo',
				  		'Nome',
				  		false),
				  	onSaved: (String value) {
				  		widget.produtoGrupo.nome = value;
				  	},
				  	onChanged: (text) {
				  		_formFoiAlterado = true;
				  	},
				  	),
				  const SizedBox(height: 24.0),
				  TextFormField(
				  	maxLength: 250,
				  	maxLines: 3,
				  	initialValue: widget.produtoGrupo?.descricao ?? '',
				  	decoration: ViewUtilLib.getInputDecorationPersistePage(
				  		'Informe a Descrição do Grupo',
				  		'Descrição',
				  		false),
				  	onSaved: (String value) {
				  		widget.produtoGrupo.descricao = value;
				  	},
				  	onChanged: (text) {
				  		_formFoiAlterado = true;
				  	},
				  	),
                  const SizedBox(height: 24.0),
                  Text(
                    '* indica que o campo é obrigatório',
                    style: Theme.of(context).textTheme.caption,
                  ),
                  const SizedBox(height: 24.0),
                ],
              ),
            ),
          ),
        ),
      ),
    );
  }

  Future<bool> _avisarUsuarioFormAlterado() async {
    final FormState form = _formKey.currentState;
    if (form == null || !_formFoiAlterado) return true;

    return await ViewUtilLib.gerarDialogBoxFormAlterado(context);
  }
}