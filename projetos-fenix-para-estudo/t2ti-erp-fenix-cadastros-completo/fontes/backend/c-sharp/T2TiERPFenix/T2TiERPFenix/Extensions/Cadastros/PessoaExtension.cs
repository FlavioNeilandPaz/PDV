/*******************************************************************************
Title: T2Ti ERP Fenix                                                                
Description: Extension relacionado à tabela [PESSOA] 
                                                                                
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
using T2TiERPFenix.Models;

namespace T2TiERPFenix.Extensions
{
    public static class PessoaExtension
    {
        public static void Map(this Pessoa objBanco, Pessoa objJson)
        {
			objBanco.Nome = objJson.Nome;
			objBanco.Tipo = objJson.Tipo;
			objBanco.Site = objJson.Site;
			objBanco.Email = objJson.Email;
			objBanco.Cliente = objJson.Cliente;
			objBanco.Fornecedor = objJson.Fornecedor;
			objBanco.Transportadora = objJson.Transportadora;
			objBanco.Colaborador = objJson.Colaborador;
			objBanco.Contador = objJson.Contador;
			
			objBanco.PessoaFisica = objJson.PessoaFisica;
			objBanco.PessoaJuridica = objJson.PessoaJuridica;
			objBanco.ListaPessoaContato = objJson.ListaPessoaContato;
			objBanco.ListaPessoaEndereco = objJson.ListaPessoaEndereco;
			objBanco.ListaPessoaTelefone = objJson.ListaPessoaTelefone;
        }
    }
}
