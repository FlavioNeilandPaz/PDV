/*******************************************************************************
Title: T2Ti ERP Fenix                                                                
Description: Model relacionado à tabela [PESSOA] 
                                                                                
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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace T2TiERPFenix.Models
{
    [Table("PESSOA")]
    public class Pessoa
    {	
		[Key]
		[Column("ID")]
		public int Id { get; set; }

		[Column("NOME")]
		public string Nome { get; set; }

		[Column("TIPO")]
		public string Tipo { get; set; }

		[Column("SITE")]
		public string Site { get; set; }

		[Column("EMAIL")]
		public string Email { get; set; }

		[Column("CLIENTE")]
		public string Cliente { get; set; }

		[Column("FORNECEDOR")]
		public string Fornecedor { get; set; }

		[Column("TRANSPORTADORA")]
		public string Transportadora { get; set; }

		[Column("COLABORADOR")]
		public string Colaborador { get; set; }

		[Column("CONTADOR")]
		public string Contador { get; set; }

		[InverseProperty("Pessoa")]
		public PessoaFisica PessoaFisica { get; set; }

		[InverseProperty("Pessoa")]
		public PessoaJuridica PessoaJuridica { get; set; }

		[InverseProperty("Pessoa")]
		public IList<PessoaContato> ListaPessoaContato { get; set; }

		[InverseProperty("Pessoa")]
		public IList<PessoaEndereco> ListaPessoaEndereco { get; set; }

		[InverseProperty("Pessoa")]
		public IList<PessoaTelefone> ListaPessoaTelefone { get; set; }

    }
}
