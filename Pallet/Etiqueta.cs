using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using Classes;
using System.Data.OracleClient;
// ===============================
// AUTHOR       : JEFFERSON BRANDÃO DA COSTA - ANALISTA/PROGRAMADOR
// CREATE DATE  : 05/12/2020
// DESCRIPTION  : Sistema para imprimir etiqueta dos pallets cliente ROKU
// SPECIAL NOTES:
// ===============================
// Change History: 
//                                   
// Date:
//==================================

namespace Pallet
{
    public partial class Etiqueta : Form
    {
        internal const string TITULO = "PALLET LABEL  V.1.0.0.0";
        internal string pallet_ = null;
        //internal int total = 0;
        internal string FOXCONNPROJECTNO = "RU9026000643";
        internal int contagem_inicial = 0;

        public Etiqueta()
        {
            InitializeComponent();
            //
            #region RODAPÉ

            int anoCriacao = 2020;
            int anoAtual = DateTime.Now.Year;
            string texto = anoCriacao == anoAtual ? " Foxconn CNSBG All Rights Reserved." : "-" + anoAtual + " Foxconn CNSBG All Rights Reserved.";
            //
            lbRodape.Text = "Copyright © " + anoCriacao + texto;

            #endregion
            // 
            lbAviso.Visible = false;

            if (gridPallet.ColumnCount == 0)//Para refresh não duplicar coluna
            {
                gridPallet.Columns.Add("", "PALLET_NO [OK]");//CRIA COLUNA
            }
            //
            if (gridErro.ColumnCount == 0)//Para refresh não duplicar coluna
            {
                gridErro.Columns.Add("", "PALLET_NO [ERRO]");//CRIA COLUNA  
            }

        }

        #region SOM AVISO

        private void SomFalha()
        {
            try
            {
                string caminho = AppDomain.CurrentDomain.BaseDirectory;
                SoundPlayer som = new SoundPlayer(caminho + "/SOUND/fail.wav");
                som.Play();
            }
            catch (Exception ex)
            {
                string erro = ex.Message;

            }
        }
        //
        private void SomAprovado()
        {
            try
            {
                string caminho = AppDomain.CurrentDomain.BaseDirectory;
                SoundPlayer som = new SoundPlayer(caminho + "/SOUND/pass.wav");
                som.Play();
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }
        }

        #endregion

        private void txtQtd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public void LimpaDados()
        {
            txtPO.Enabled = true;
            //
            txtPallet.Clear();
            txtPO.Clear();
            //           
            lbTotalCaixa.Text = "0";
            //lbQtd.Text = "0";

            //limpa linha do grid
            gridPallet.Rows.Clear();
            gridErro.Rows.Clear();

            lbAviso.Text = string.Empty;
            lbAviso.Visible = false;

            lbTotalCaixa.Text = "0";
            // lbQtd.Text = "0";

            contagem_inicial = 0;

            //btnReiprimir.Enabled = false;
            txtPO.Focus();

        }

        private void btLimpar_Click(object sender, EventArgs e)
        {
            LimpaDados();
        }

        public void PalletErro(string pallet)
        {
            gridErro.AllowUserToAddRows = false;//remove a útima linha em branco do grid. Para não contar no ROWCOUNT
            //
            if (gridErro.Rows.Count > 0)
            {
                gridErro.Rows.Add(pallet);
            }
            else
            {
                //[ERRO]
                gridErro.Rows.Add(pallet);
            }

            //Ordem decrescente
            gridErro.Sort(gridErro.Columns[0], ListSortDirection.Descending);
            gridErro.Columns[0].HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.Descending;
            gridErro.Rows[0].Selected = true;//deixar selecionado o item mais recente da lista
        }

        public void Imprimir(string contagem, string totalPecas)
        {
            //PrintDialog pd = new PrintDialog();
            //string impressora = pd.PrinterSettings.PrinterName;

            string LABEL1 = "1";//"RU9026000643_PALLET.LAB";
            string LABEL2 = "2";//"RU9026000643_PALLET2.LAB";

            Impressora ob = new Impressora();
            string IMPRESSORA1 = ob.GetImpressora(LABEL1)[0].Nome;
            string IMPRESSORA2 = ob.GetImpressora(LABEL2)[0].Nome;

            string dia = DateTime.Now.Day.ToString().Length == 1 ? "0" + DateTime.Now.Day.ToString() : DateTime.Now.Day.ToString();
            string mes = DateTime.Now.Month.ToString().Length == 1 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString();
            string SHIPDATE = DateTime.Now.Year + "/" + mes + "/" + dia;
            //
            Carton objCarton = new Carton();
            List<Carton.Etiqueta> listCarton = new List<Carton.Etiqueta>();
            listCarton = objCarton.Dados(txtPO.Text.Trim().ToUpper(), SHIPDATE, FOXCONNPROJECTNO, txtPallet.Text.Trim().ToUpper(), totalPecas, contagem);
            //
            if (listCarton.Count > 0)
            {
                //LABEL codesoft
                Print codesoft = new Print();
                //Gera etiqueta e imprime
                string mensagem = codesoft.Etiqueta_CodeSoft(listCarton, txtPallet.Text.Trim().ToUpper(), IMPRESSORA1, 4, LABEL1);
                if (mensagem.Contains("ERRO"))
                {
                    lbAviso.Text = mensagem;
                    lbAviso.Visible = true;
                    SomFalha();
                }
                else
                {
                    mensagem = codesoft.Etiqueta_CodeSoft(listCarton, txtPallet.Text.Trim().ToUpper(), IMPRESSORA2, 2, LABEL2);
                    if (mensagem.Contains("ERRO"))
                    {
                        lbAviso.Text = mensagem;
                        lbAviso.Visible = true;
                        SomFalha();
                    }
                    else
                    {
                        SomAprovado();
                    }
                }

                //LOG                                    
                Log objLog = new Log();
                objLog.Gravar(txtPallet.Text.Trim().ToUpper(), txtPO.Text.Trim().ToUpper(), "1", mensagem);
            }

        }

        public void Reimprimir(string pallet, string po, string contagem, string totalPecas)
        {
            string LABEL1 = "1";//"RU9026000643_PALLET.LAB";
            string LABEL2 = "2";//"RU9026000643_PALLET2.LAB";

            //PrintDialog pd = new PrintDialog();
            //string impressora = pd.PrinterSettings.PrinterName;

            //
            Impressora ob = new Impressora();
            string IMPRESSORA1 = ob.GetImpressora(LABEL1)[0].Nome;
            string IMPRESSORA2 = ob.GetImpressora(LABEL2)[0].Nome;

            string dia = DateTime.Now.Day.ToString().Length == 1 ? "0" + DateTime.Now.Day.ToString() : DateTime.Now.Day.ToString();
            string mes = DateTime.Now.Month.ToString().Length == 1 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString();
            string SHIPDATE = DateTime.Now.Year + "/" + mes + "/" + dia;
            //
            Carton objCarton = new Carton();
            List<Carton.Etiqueta> listCarton = new List<Carton.Etiqueta>();
            listCarton = objCarton.Dados(po, SHIPDATE, FOXCONNPROJECTNO, pallet, totalPecas, contagem);
            //
            if (listCarton.Count > 0)
            {
                //LABEL codesoft
                Print codesoft = new Print();
                //Gera etiqueta e imprime
                string mensagem = codesoft.Etiqueta_CodeSoft(listCarton, pallet, IMPRESSORA1, 1, LABEL1);
                if (mensagem.Contains("ERRO"))
                {
                    lbAviso.Text = mensagem;
                    lbAviso.Visible = true;
                    SomFalha();
                }
                else
                {
                    mensagem = codesoft.Etiqueta_CodeSoft(listCarton, pallet, IMPRESSORA2, 1, LABEL2);
                    if (mensagem.Contains("ERRO"))
                    {
                        lbAviso.Text = mensagem;
                        lbAviso.Visible = true;
                        SomFalha();
                    }
                    else
                    {
                        SomAprovado();
                    }

                }

                //LOG                                    
                Log objLog = new Log();
                objLog.Gravar(pallet, po, "1", mensagem);
            }

        }

        private void txtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Quando for usar scanner
            //if (e.KeyChar != 13)
            // boxsn_ += e.KeyChar.ToString().Replace("\r\n", string.Empty).Replace("\b", string.Empty).ToUpper().Trim();

            if ((e.KeyChar == 13) || (e.KeyChar == Convert.ToChar(Keys.Enter)))
            {
                //para permitir digitar e usar ENTER 
                pallet_ = string.IsNullOrEmpty(pallet_) ? txtPallet.Text.Replace("\r\n", string.Empty).Replace("\b", string.Empty).ToUpper().Trim() : pallet_;
                string po = txtPO.Text.ToUpper().Trim();
                string quantidade = string.Empty;//txtQtd.Text;
                lbAviso.Text = string.Empty;
                lbAviso.Visible = false;
                string pallet = string.Empty;
                string boxSn = string.Empty;
                //
                Carton objCarton = new Carton();
                if (pallet_.Length >= 30)//scanneado pelo QRcode
                {
                    boxSn = pallet_.Remove(13);//estrai somnete boxsn para procurar pelo pallet                    
                    pallet = objCarton.PalletNo(boxSn);
                    //
                    if (pallet.Contains("ERRO"))
                    {
                        lbAviso.Text = pallet;//ERRO;
                        lbAviso.Visible = true;
                        SomFalha();
                        return;
                    }

                    if (string.IsNullOrEmpty(pallet))
                    {
                        lbAviso.Text = "ERRO: Boxsn não existe ou incorreta " + boxSn;
                        lbAviso.Visible = true;
                        SomFalha();
                        //ERRO
                        PalletErro(pallet);
                        txtPallet.SelectAll();
                        return;
                    }
                    //
                    txtPallet.Text = pallet;
                }
                else
                {
                    pallet = pallet_;
                   
                }
                //
                if (!string.IsNullOrEmpty(po))
                {
                    quantidade = objCarton.QuantidadePecas(pallet);

                    if (!string.IsNullOrEmpty(quantidade))
                    {
                        if (int.Parse(quantidade) > 0)
                        {
                            if (pallet.Length == 17)
                            {
                                if (pallet.Contains("FM393"))//FM3930BR022000050 
                                {
                                    lbTotalCaixa.Text = objCarton.TotalMasterCarton(FOXCONNPROJECTNO, quantidade);
                                    //
                                    #region VERIFICA SE EXITE PALLET REPETIDO

                                    bool itemRepetido = false;
                                    //
                                    gridPallet.AllowUserToAddRows = false;//remove a última linha em branco do grid. Para não contar no ROWCOUNT
                                    if (gridPallet.Rows.Count > 0)
                                    {
                                        for (int index = 0; index < gridPallet.Rows.Count; index++)
                                        {
                                            string item = string.Empty;
                                            //
                                            try
                                            {
                                                item = gridPallet.Rows[index].Cells[0].Value.ToString();
                                            }
                                            catch (Exception)
                                            {
                                                //
                                            }
                                            //
                                            if (item.Contains(pallet))
                                            {
                                                itemRepetido = true;
                                            }

                                        }
                                    }

                                    #endregion
                                    //
                                    if (!itemRepetido)
                                    {
                                        //if (string.IsNullOrEmpty(pallet))
                                        //{
                                        //    lbAviso.Text = "ERRO: Pallet não existe";
                                        //    lbAviso.Visible = true;
                                        //    SomFalha();
                                        //    //ERRO
                                        //    PalletErro(pallet);
                                        //}
                                        //else
                                        //{
                                        string estacao = objCarton.Pallet_Station(pallet);
                                        if (estacao.Contains("ERRO"))
                                        {
                                            lbAviso.Text = estacao;//ERRO;
                                            lbAviso.Visible = true;
                                            SomFalha();
                                            return;
                                        }

                                        if (estacao == "PALLET")//verifica se o pallet da caixa informada está na estação PALLET
                                        {
                                            gridPallet.AllowUserToAddRows = false;//remove a útima linha em branco do grid. Para não contar no ROWCOUNT
                                            gridPallet.Rows.Add(pallet);

                                            //Ordem decrescente
                                            gridPallet.Sort(gridPallet.Columns[0], ListSortDirection.Descending);
                                            gridPallet.Columns[0].HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.Descending;
                                            gridPallet.Rows[0].Selected = true;//deixar selecionado o item mais recente da lista
                                            //
                                            if (gridPallet.Rows.Count > 0)
                                            {
                                                for (int rows = 0; rows < gridPallet.Rows.Count; rows++)
                                                {
                                                    if (rows > 0)//nao permite deixar selecinado nenhum item que nao seja o mais recente da lista
                                                        gridPallet.Rows[rows].Selected = false;
                                                }
                                            }

                                        }
                                        else
                                        {
                                            lbAviso.Text = "ERRO: Carton não está na estação PALLET. Estação atual: " + estacao;
                                            lbAviso.Visible = true;
                                            SomFalha();
                                            //ERRO
                                            PalletErro(pallet);
                                        }
                                        //}
                                    }
                                    else
                                    {
                                        lbAviso.Text = "ERRO: Pallet já foi adicionado";
                                        lbAviso.Visible = true;
                                        SomFalha();
                                    }
                                    //
                                    if (!lbAviso.Text.Contains("ERRO"))
                                    {
                                        contagem_inicial++;
                                        string contagem = contagem_inicial.ToString();//objCarton.ContagemShippingPallet(FOXCONNPROJECTNO);
                                        //
                                        string totalPallets = objCarton.TotalPallets(FOXCONNPROJECTNO, quantidade);
                                        string mensagem = objCarton.Registrar_PO(txtPO.Text.Trim().ToUpper(), FOXCONNPROJECTNO, pallet, contagem);
                                        //
                                        if (mensagem.Contains("OK"))
                                        {
                                            Imprimir(contagem, quantidade);
                                            //
                                            if (contagem == totalPallets)
                                            {
                                                //CONCLUÍDO
                                                objCarton.AtualizaEvento(pallet);//UPDATE NEXTEVENT 
                                                string PO = mensagem.Replace("OK_", string.Empty).Trim();
                                                objCarton.Atuliaza_PO(PO);//(txtPO.Text.Trim().ToUpper());
                                                //
                                                LimpaDados();
                                                lbAviso.Text = "Concluído";
                                                lbAviso.Visible = true;
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            lbAviso.Text = mensagem;
                                            lbAviso.Visible = true;
                                        }

                                    }
                                }
                                else
                                {
                                    lbAviso.Text = "ERRO: Formato inválido";
                                    lbAviso.Visible = true;
                                    SomFalha();
                                }
                            }
                            else
                            {
                                lbAviso.Text = "ERRO: Tamanho inválido";
                                lbAviso.Visible = true;
                                SomFalha();
                            }

                            txtPO.Enabled = false;
                            txtPallet.SelectAll();
                        }
                        else
                        {
                            lbAviso.Text = "ERRO: Quantidade deve ser maior que zero";
                            lbAviso.Visible = true;
                            SomFalha();
                        }
                    }
                    else
                    {
                        lbAviso.Text = "ERRO: Informe a quantidade";
                        lbAviso.Visible = true;
                        SomFalha();
                    }

                }
                else
                {
                    lbAviso.Text = "ERRO: Informe número da PO";
                    lbAviso.Visible = true;
                    SomFalha();
                }
                //
                pallet_ = null;
                this.Text = TITULO;
            }
        }

        private void gridPallet_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                var Row = gridPallet.CurrentRow;
                string pallet = Row.Cells[0].Value.ToString();
                string contagem = (Row.Index + 1).ToString();//contagem_inicial.ToString();//"1";//objCarton.ContagemShippingPallet(FOXCONNPROJECTNO);
                //
                Carton objCarton = new Carton();
                string po = objCarton.Po(pallet);
                string qtdPecas = objCarton.QuantidadePecas(pallet);

                //REPRINT
                Reimprimir(pallet, po, contagem, qtdPecas);
                txtPallet.Select();

            }
            catch (Exception erro)
            {
                lbAviso.Text = "ERRO: O Campo está vázio, efetue uma busca";
                lbAviso.Visible = true;
                SomFalha();
                return;
                throw erro;
            }

        }

        private void gridErro_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                Carton objCarton = new Carton();
                //
                var Row = gridErro.CurrentRow;
                string pallet = Row.Cells[0].Value.ToString();
                string contagem = (Row.Index + 1).ToString();//contagem_inicial.ToString();//"1";//objCarton.ContagemShippingPallet(FOXCONNPROJECTNO);                                 
                string po = objCarton.Po(pallet);
                string qtdPecas = objCarton.QuantidadePecas(pallet);
                //Quando pallet já tem sido passado pela estação PALLET
                string estacao = objCarton.Pallet_Station(pallet);
                if ((estacao.Equals("SHIPPING")) || (estacao.Equals("JOBFINISH")) || (estacao.Equals("OBA")))
                {
                    //REPRINT
                    Reimprimir(pallet, po, contagem, qtdPecas);
                    txtPallet.Select();
                }
            }
            catch (Exception erro)
            {
                lbAviso.Text = "ERRO: O Campo esta vázio, efetue uma busca";
                lbAviso.Visible = true;
                SomFalha();
                return;
                throw erro;
            }

        }
    }
}
