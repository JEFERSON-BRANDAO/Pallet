using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Classes
{
    class Carton
    {
        public string QuantidadePecas(string pallet)
        {
            #region QUANTIDADE DE PEÇAS DO PALLET

            string qtd = "0";
            OleDbConnect Objconn = new OleDbConnect();
            //
            try
            {
                try
                {
                    Objconn.Conectar();
                    Objconn.Parametros.Clear();
                    //
                    string sql = @"Select count(A.SYSSERIALNO)quantidade From  mfworkstatus A,sfcshippack B 
                                      Where A.Location=B.PackNo 
                                      and parentbundleno ='" + pallet + "'";
                    //
                    Objconn.SetarSQL(sql);
                    Objconn.Executar();
                    //
                    if (Objconn.Tabela.Rows.Count > 0)
                    {
                        qtd = Objconn.Tabela.Rows[0][0].ToString();
                     
                    }

                }
                finally
                {
                    Objconn.Desconectar();
                }
            }
            catch (Exception erro)
            {
                throw erro;
            }
            //
            return qtd;

            #endregion
        }

        public string ContagemShippingPallet(string skuno)
        {
            #region CONTEGEM PALLET

            string valor = "0";
            OleDbConnect Objconn = new OleDbConnect();
            //
            try
            {
                try
                {
                    Objconn.Conectar();
                    Objconn.Parametros.Clear();
                    //
                    string dataAtual = DateTime.Now.Date.ToString("yyyy-MM-dd");
                    string sql = @"select count(*) row_Count from r_shipping_detail where p_no = '" + skuno + "' and to_char(CONTAINER_TIME,'YYYY-MM-DD') = '" + dataAtual + "'";
                    Objconn.SetarSQL(sql);
                    Objconn.Executar();
                    //
                    if (Objconn.Tabela.Rows.Count > 0)
                    {
                        valor = (int.Parse(Objconn.Tabela.Rows[0]["row_Count"].ToString()) + 1).ToString();
                    }
                    else
                    {
                        valor = "1";
                    }

                }
                finally
                {
                    Objconn.Desconectar();
                }
            }
            catch (Exception erro)
            {
                valor = "-";
                throw erro;
            }
            //
            return valor;

            #endregion
        }

        public string TotalMasterCarton(string skuno, string quantidadePecas)
        {
            #region QUANTIDADE CAIXAS PELO VALOR DA NOTA

            string TOTAL_MASTER_CARTONS = "0";
            OleDbConnect Objconn = new OleDbConnect();
            //
            try
            {
                try
                {
                    Objconn.Conectar();
                    Objconn.Parametros.Clear();
                    //   
                    string sql = @"SELECT CARTONUOM, PALLETUOM  FROM sfccodelike WHERE skuno='" + skuno + "'";
                    Objconn.SetarSQL(sql);
                    Objconn.Executar();
                    //
                    if (Objconn.Tabela.Rows.Count > 0)
                    {
                        string QTD_PECAS_POR_CAIXA = string.IsNullOrEmpty(Objconn.Tabela.Rows[0]["CARTONUOM"].ToString()) ? "0" : Objconn.Tabela.Rows[0]["CARTONUOM"].ToString();
                        int QTD_CAIXAS_VALOR_NOTA = (1 * int.Parse(quantidadePecas)) / int.Parse(QTD_PECAS_POR_CAIXA);
                        //
                        TOTAL_MASTER_CARTONS = QTD_CAIXAS_VALOR_NOTA > 0 ? QTD_CAIXAS_VALOR_NOTA.ToString() : "1";
                    }

                }
                finally
                {
                    Objconn.Desconectar();
                }
            }
            catch (Exception erro)
            {
                TOTAL_MASTER_CARTONS = "-";
                throw erro;
            }
            //
            return TOTAL_MASTER_CARTONS;

            #endregion
        }

        public string TotalPallets(string skuno, string quantidadePecas)
        {
            #region QUANTIDADE PALLETS PELO VALOR DA NOTA

            string TOTAL_PALLETS = "0";
            OleDbConnect Objconn = new OleDbConnect();
            //
            try
            {
                try
                {
                    Objconn.Conectar();
                    Objconn.Parametros.Clear();
                    //
                    string sql = @"SELECT CARTONUOM, PALLETUOM  FROM sfccodelike WHERE skuno='" + skuno + "'";
                    //
                    Objconn.SetarSQL(sql);
                    Objconn.Executar();
                    //
                    if (Objconn.Tabela.Rows.Count > 0)
                    {
                        string QTD_PECAS_POR_CAIXA = string.IsNullOrEmpty(Objconn.Tabela.Rows[0]["CARTONUOM"].ToString()) ? "0" : Objconn.Tabela.Rows[0]["CARTONUOM"].ToString();
                        string QTD_CAIXA_POR_PALLET = string.IsNullOrEmpty(Objconn.Tabela.Rows[0]["PALLETUOM"].ToString()) ? "0" : Objconn.Tabela.Rows[0]["PALLETUOM"].ToString();
                        int TOAL_PECAS_POR_PALLET = int.Parse(QTD_PECAS_POR_CAIXA) * int.Parse(QTD_CAIXA_POR_PALLET);
                        int QTD_PALLET_POR_PECAS = (1 * int.Parse(quantidadePecas)) / TOAL_PECAS_POR_PALLET;
                        //
                        TOTAL_PALLETS = QTD_PALLET_POR_PECAS > 0 ? QTD_PALLET_POR_PECAS.ToString() : "1";
                    }

                }
                finally
                {
                    Objconn.Desconectar();
                }
            }
            catch (Exception erro)
            {
                TOTAL_PALLETS = "-";
                throw erro;
            }
            //
            return TOTAL_PALLETS;

            #endregion
        }

        public string Po(string pallet)
        {
            #region NÚMERO PO

            string Po = string.Empty;
            OleDbConnect Objconn = new OleDbConnect();
            //
            try
            {
                try
                {
                    Objconn.Conectar();
                    Objconn.Parametros.Clear();
                    //
                    string sql = @"SELECT DN_NO, DN_NO FROM r_shipping_detail WHERE PALLET_NO = '" + pallet + "' AND ROWNUM=1";
                    //
                    Objconn.SetarSQL(sql);
                    Objconn.Executar();
                    //
                    if (Objconn.Tabela.Rows.Count > 0)
                    {
                        string str = Objconn.Tabela.Rows[0][0].ToString();
                        int end = 0;
                        string aux = string.Empty;
                        for (int index = 0; index < str.Length; index++)
                        {
                            if (end.Equals(0))
                            {
                                aux = str[index].ToString();
                                //
                                if (aux.Contains("_"))
                                {
                                    end = 1;
                                }
                                else
                                {
                                    Po += aux;
                                }

                            }
                        }
                    }

                }
                finally
                {
                    Objconn.Desconectar();
                }
            }
            catch (Exception erro)
            {
                //Po = "-";
                throw erro;
            }
            //
            return Po;

            #endregion
        }

        public bool PO_Existente(string Po)
        {
            #region CONSULTA PO

            bool resultado = false;
            OleDbConnect Objconn = new OleDbConnect();
            //
            try
            {
                try
                {
                    Objconn.Conectar();
                    Objconn.Parametros.Clear();
                    //
                    string sql = @"SELECT COUNT(*) row_Count FROM r_shipping_detail WHERE DN_NO ='" + Po + "' AND K_NO =1";
                    Objconn.SetarSQL(sql);
                    Objconn.Executar();
                    //
                    if (Objconn.Tabela.Rows.Count > 0)
                    {
                        if (int.Parse(Objconn.Tabela.Rows[0]["row_Count"].ToString()) > 0)
                        {
                            resultado = true;
                        }
                    }

                }
                finally
                {
                    Objconn.Desconectar();
                }
            }
            catch
            {
                //
            }
            //
            return resultado;

            #endregion
        }

        public bool Pallet_Existente(string pallet)
        {
            #region CONSULTA PALLET

            bool resultado = false;
            OleDbConnect Objconn = new OleDbConnect();
            //
            try
            {
                try
                {
                    Objconn.Conectar();
                    Objconn.Parametros.Clear();
                    //
                    string sql = @"SELECT COUNT(*) row_Count FROM r_shipping_detail WHERE PALLET_NO ='" + pallet + "'";
                    Objconn.SetarSQL(sql);
                    Objconn.Executar();
                    //
                    if (Objconn.Tabela.Rows.Count > 0)
                    {
                        if (int.Parse(Objconn.Tabela.Rows[0]["row_Count"].ToString()) > 0)
                        {
                            resultado = true;
                        }
                    }

                }
                finally
                {
                    Objconn.Desconectar();
                }
            }
            catch
            {
                //
            }
            //
            return resultado;

            #endregion
        }

        public string Registrar_PO(string Po, string skuno, string pallet, string itemNo)
        {
            #region INSERE PO

            string mensagem = string.Empty;
            OleDbConnect Objconn = new OleDbConnect();
            //
            try
            {
                try
                {
                    Objconn.Conectar();
                    Objconn.Parametros.Clear();
                    //
                    string data = DateTime.Now.Date.ToString("ddMMyyyy");
                    string time = DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                    Po = Po + "_" + data + time;

                    string empty = string.Empty;
                    string sql = @"INSERT INTO r_shipping_detail (DN_NO, DN_ITEM_NO, P_SN, P_NO, CARTON_NO, PALLET_NO, CONTAINER_TIME)
                                                           VALUES('" + Po + "', '" + itemNo + "', '" + pallet + "','" + skuno + "','" + pallet + "','" + pallet + "',SYSDATE)";
                    Objconn.SetarSQL(sql);
                    Objconn.Executar();
                    //
                    if (Objconn.Isvalid)
                    {
                        mensagem = "OK_" + Po;
                    }
                    else
                    {
                        mensagem = "ERRO: " + (Objconn.Message.Contains("ORA-00001") ? "Registro existente na tabela r_shipping_detail " : Objconn.Message);

                    }
                }
                finally
                {
                    Objconn.Desconectar();
                }
            }
            catch (Exception erro)
            {
                mensagem = "ERRO: " + erro;
                throw erro;
            }
            //
            return mensagem;

            #endregion
        }

        public void Atuliaza_PO(string po)
        {
            #region ATUALIZA PO

            string mensagem = string.Empty;
            OleDbConnect Objconn = new OleDbConnect();
            //
            try
            {
                try
                {
                    Objconn.Conectar();
                    Objconn.Parametros.Clear();
                    // 
                    string empty = string.Empty;
                    string sql = @"UPDATE r_shipping_detail SET K_NO='1' WHERE DN_NO='" + po + "'";
                    Objconn.SetarSQL(sql);
                    Objconn.Executar();
                    //
                    if (Objconn.Isvalid)
                    {
                        mensagem = "OK";
                    }
                    else
                    {
                        mensagem = "ERRO: " + Objconn.Message;
                    }
                }
                finally
                {
                    Objconn.Desconectar();
                }
            }
            catch (Exception erro)
            {
                mensagem = "ERRO: " + erro;
                throw erro;
            }

            #endregion
        }

        public string AtualizaEvento(string Pallet)
        {
            #region ATUALIZA EVENTO

            string mensagem = string.Empty;
            OleDbConnect Objconn = new OleDbConnect();
            //
            try
            {
                try
                {
                    Objconn.Conectar();
                    Objconn.Parametros.Clear();
                    //
                    string sqlRota = @"(SELECT NEXTEVENTPOINT from  SFCROUTEDEFB WHERE EVENTPOINT='PALLET')";//PROXIMO EVENTO NA ROTA
                    string sql = @"UPDATE MFWORKSTATUS SET CURRENTEVENT='PALLET', NEXTEVENT=" + sqlRota + ", LASTEDITBY='PALLET' " +
                                        "WHERE SYSSERIALNO IN (Select A.SYSSERIALNO From  mfworkstatus A,sfcshippack B " +
                                        "Where A.Location=B.PackNo " +
                                        "and parentbundleno in ('" + Pallet + "'))";

                    Objconn.SetarSQL(sql);
                    Objconn.Executar();
                    //
                    if (Objconn.Isvalid)
                    {
                        mensagem = "OK";
                    }
                    else
                    {
                        mensagem = "ERRO: " + Objconn.Message;
                    }

                }
                finally
                {
                    Objconn.Desconectar();
                }

            }
            catch (Exception erro)
            {
                mensagem = "ERRO:" + erro;

            }
            //
            return mensagem;

            #endregion
        }

        public string PalletNo(string boxsn)
        {
            #region NÚMERO DO PALLET

            string pallet = string.Empty;
            OleDbConnect Objconn = new OleDbConnect();
            //
            try
            {
                try
                {
                    Objconn.Conectar();
                    Objconn.Parametros.Clear();
                    //
                    string sql = @"SELECT PARENTBUNDLENO as PALLET FROM sfcshippack where PACKNO ='" + boxsn + "'";
                    Objconn.SetarSQL(sql);
                    Objconn.Executar();
                    //
                    if (Objconn.Tabela.Rows.Count > 0)
                    {
                        pallet = Objconn.Tabela.Rows[0]["PALLET"].ToString();
                    }
                    else
                    {
                        if (!Objconn.Isvalid)
                            pallet = "ERRO: " + Objconn.Message;
                    }

                }
                finally
                {
                    Objconn.Desconectar();
                }
            }
            catch (Exception erro)
            {
                pallet = "ERRO: " + erro;
                throw erro;
            }
            //
            return pallet;

            #endregion
        }

        public string Pallet_Station(string pallet)
        {
            #region VERIFICA SE ESTÁ NA ESTAÇÃO 'PALLET'

            string station = string.Empty;
            OleDbConnect Objconn = new OleDbConnect();
            //
            try
            {
                try
                {
                    Objconn.Conectar();
                    Objconn.Parametros.Clear();
                    //
                    string sql = @"Select A.NEXTEVENT as NEXTEVENT From  mfworkstatus A ,sfcshippack B 
                                      Where A.Location=B.PackNo                                       
                                      and B.PARENTBUNDLENO = '" + pallet + "' AND rownum=1";
                    Objconn.SetarSQL(sql);
                    Objconn.Executar();
                    //
                    if (Objconn.Tabela.Rows.Count > 0)
                    {
                        station = Objconn.Tabela.Rows[0]["NEXTEVENT"].ToString();
                    }
                    else
                    {
                        if (!Objconn.Isvalid)
                            station = "ERRO: " + Objconn.Message;
                    }

                }
                finally
                {
                    Objconn.Desconectar();
                }
            }
            catch (Exception)
            {
                //
            }
            //
            return station;

            #endregion
        }

        public List<Etiqueta> Dados(string po, string data, string skuno, string palletNo, string totalQuantity, string contagem)
        {
            #region INFORMAÇÕES DA ETIQUETA

            OleDbConnect Objconn = new OleDbConnect();
            List<Etiqueta> lista = new List<Etiqueta>();
            Etiqueta item = new Etiqueta();
            //
            string TOTALQUANTITY = string.Empty;
            string TOTAL_PALLETS = string.Empty;
            string TOTAL_MASTER_CARTONS = string.Empty;
            string CARTONQTY = string.Empty;
            string DEFAULT_BOX = string.Empty;
            string QTYPERMASTERCARTON = string.Empty;
            string QTYPERPALLET = string.Empty;
            string REV = string.Empty;
            //
            try
            {
                #region TOTAL_QUANTITY

                TOTALQUANTITY = totalQuantity;

                #endregion
                //
                #region TOTAL_PALLETS

                Objconn.Conectar();
                Objconn.Parametros.Clear();
                //
                string sql = @"SELECT CARTONUOM, PALLETUOM  FROM sfccodelike WHERE skuno='" + skuno + "'";
                //
                Objconn.SetarSQL(sql);
                Objconn.Executar();
                //
                if (Objconn.Tabela.Rows.Count > 0)
                {
                    string QTD_PECAS_POR_CAIXA = string.IsNullOrEmpty(Objconn.Tabela.Rows[0]["CARTONUOM"].ToString()) ? "0" : Objconn.Tabela.Rows[0]["CARTONUOM"].ToString();
                    string QTD_CAIXA_POR_PALLET = string.IsNullOrEmpty(Objconn.Tabela.Rows[0]["PALLETUOM"].ToString()) ? "0" : Objconn.Tabela.Rows[0]["PALLETUOM"].ToString();
                    int TOAL_PECAS_POR_PALLET = int.Parse(QTD_PECAS_POR_CAIXA) * int.Parse(QTD_CAIXA_POR_PALLET);
                    int QTD_PALLET_POR_PECAS = (1 * int.Parse(totalQuantity)) / TOAL_PECAS_POR_PALLET;
                    //
                    TOTAL_PALLETS = QTD_PALLET_POR_PECAS > 0 ? QTD_PALLET_POR_PECAS.ToString() : "1";
                }

                #endregion
                //
                #region TOTAL MASTER CARTON

                string QTD_PECAS_POR_CAIXA2 = string.IsNullOrEmpty(Objconn.Tabela.Rows[0]["CARTONUOM"].ToString()) ? "0" : Objconn.Tabela.Rows[0]["CARTONUOM"].ToString();
                int QTD_CAIXAS_VALOR_NOTA = (1 * int.Parse(totalQuantity)) / int.Parse(QTD_PECAS_POR_CAIXA2);
                //
                TOTAL_MASTER_CARTONS = QTD_CAIXAS_VALOR_NOTA > 0 ? QTD_CAIXAS_VALOR_NOTA.ToString() : "1";

                #endregion
                //
                #region QTY PER MASTER CARTON

                QTYPERMASTERCARTON = string.IsNullOrEmpty(Objconn.Tabela.Rows[0]["CARTONUOM"].ToString()) ? "0" : Objconn.Tabela.Rows[0]["CARTONUOM"].ToString();

                #endregion
                //
                #region QTY PER PALLET

                QTYPERPALLET = string.IsNullOrEmpty(Objconn.Tabela.Rows[0]["PALLETUOM"].ToString()) ? "0" : Objconn.Tabela.Rows[0]["PALLETUOM"].ToString();

                #endregion
                //
                #region REV

                //
                Objconn.Parametros.Clear();
                //
                sql = @"select DATA8 REV from r_ap_temp where data1='NEMO'";
                Objconn.SetarSQL(sql);
                Objconn.Executar();
                //
                if (Objconn.Tabela.Rows.Count > 0)
                {
                    REV = Objconn.Tabela.Rows[0]["REV"].ToString();
                }
                //              

                #endregion
                // 
                item.PO = po;
                item.SHIPDATE = data;
                item.FOXCONNPROJECTNO = skuno;
                item.TOTALQUANTITY = TOTALQUANTITY;
                item.TOTALPALLETS = TOTAL_PALLETS;
                item.TOTALMASTERCARTONS = TOTAL_MASTER_CARTONS;
                item.QTYPERMASTERCARTONS = QTYPERMASTERCARTON;
                item.QTYPERPALLET = QTYPERPALLET;
                item.CARTONQTY = contagem;//CARTONQTY; 
                item.REV = REV;
                //
                lista.Add(item);
            }
            finally
            {
                Objconn.Desconectar();
            }

            return lista;

            #endregion
        }

        public class Etiqueta
        {
            public string PO { get; set; }
            public string SHIPDATE { get; set; }
            public string FOXCONNPROJECTNO { get; set; }
            public string TOTALQUANTITY { get; set; }
            public string TOTALPALLETS { get; set; }
            public string TOTALMASTERCARTONS { get; set; }
            public string QTYPERMASTERCARTONS { get; set; }
            public string QTYPERPALLET { get; set; }
            public string CARTONQTY { get; set; }
            public string REV { get; set; }

        }

    }
}
