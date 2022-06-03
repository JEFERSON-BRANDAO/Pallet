using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Classes;

namespace Classes
{
    class Nota
    {
        public bool Usada(string Nota)
        {
            #region VERIFICA SE A NOTA JÁ FOI USADA

            bool usada = false;
            //
            OleDbConnect Objconn = new OleDbConnect();
            //
            try
            {
                Objconn.String_Connection();//string de conexao
                Objconn.Conectar();
                Objconn.Parametros.Clear();
                //
                string sql = @"select count(distinct(dn_no))quantidade from r_shipping_detail where dn_no='" + Nota + "'";
                //
                Objconn.SetarSQL(sql);
                Objconn.Executar();
                //
                if (Objconn.Tabela.Rows.Count > 0)
                {
                    string quantidade = Objconn.Tabela.Rows[0]["quantidade"].ToString();
                    usada = int.Parse(quantidade) > 0 ? true : false;                   
                }
            }
            finally
            {
                Objconn.Desconectar();
            }

            //
            return usada;

            #endregion
        }
    }
}
