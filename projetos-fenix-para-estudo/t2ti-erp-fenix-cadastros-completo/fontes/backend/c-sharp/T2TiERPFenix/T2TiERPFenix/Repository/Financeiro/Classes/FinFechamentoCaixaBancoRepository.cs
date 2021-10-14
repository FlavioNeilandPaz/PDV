/*******************************************************************************
Title: T2Ti ERP Fenix                                                                
Description: Repository relacionado à tabela [FIN_FECHAMENTO_CAIXA_BANCO] 
                                                                                
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
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using PoweredSoft.DynamicLinq;
using System;
using System.Linq;
using T2TiERPFenix.Extensions;
using T2TiERPFenix.Models;

namespace T2TiERPFenix.Repository
{
    public class FinFechamentoCaixaBancoRepository : RepositoryBase<FinFechamentoCaixaBanco>, IFinFechamentoCaixaBancoRepository
    {
        public FinFechamentoCaixaBancoRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<FinFechamentoCaixaBanco> ConsultarLista()
        {
            var query = from obj in RepositoryContext.FinFechamentoCaixaBancos
						.Include(finFechamentoCaixaBanco => finFechamentoCaixaBanco.BancoContaCaixa)
                        select obj;
            return query.AsNoTracking().ToList();
        }

        public IEnumerable<FinFechamentoCaixaBanco> ConsultarListaFiltro(Filtro filtro)
        {
            var query = RepositoryContext.FinFechamentoCaixaBancos
						.Include(finFechamentoCaixaBanco => finFechamentoCaixaBanco.BancoContaCaixa)
                .AsQueryable();
            query = query.Where(filtro.Campo, ConditionOperators.Contains, filtro.Valor, stringComparision: StringComparison.OrdinalIgnoreCase);
            return query.AsNoTracking().ToList();
        }

        public FinFechamentoCaixaBanco ConsultarObjeto(int id)
        {
            var query = from obj in RepositoryContext.FinFechamentoCaixaBancos
						.Include(finFechamentoCaixaBanco => finFechamentoCaixaBanco.BancoContaCaixa)
                        where obj.Id == id
                        select obj;
            return query.AsNoTracking().SingleOrDefault();
        }

        public void Inserir(FinFechamentoCaixaBanco objeto)
        {
			RepositoryContext.Entry(objeto.BancoContaCaixa).State = EntityState.Unchanged; //não queremos inserir o objeto vinculado
            Create(objeto);
            Save();
        }

        public void Alterar(FinFechamentoCaixaBanco objBanco, FinFechamentoCaixaBanco objJson)
        {
            objBanco.Map(objJson);
            Update(objBanco);
            Save();
        }

        public void Excluir(FinFechamentoCaixaBanco objeto)
        {
            Delete(objeto);
            Save();
        }

    }

}