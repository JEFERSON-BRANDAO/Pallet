using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Classes
{
    class Impressora
    {
        public List<Informacao> GetImpressora(string label)
        {
            List<Informacao> lista = new List<Informacao>();

            try
            {
                string caminho = AppDomain.CurrentDomain.BaseDirectory + @"\CONFIGURACAO\IMPRESSORA.txt";
                string linha;
                int row = 0;
                string str = string.Empty;              
              
                Informacao item = new Informacao();
                //
                if (System.IO.File.Exists(caminho))
                {
                    System.IO.StreamReader arqTXT = new System.IO.StreamReader(caminho);
                    //
                    while ((linha = arqTXT.ReadLine()) != null)
                    {
                        if (label.Trim().ToUpper() == "1")//ETIQUETA 1
                        {
                            if (row == 0)//primeira linha do .txt
                            {
                                for (int indice = 0; indice < linha.Length; indice++)
                                {
                                    if (indice > 6)
                                    {
                                        str += linha[indice];
                                    }
                                }
                            }
                        }
                        else if (label.Trim().ToUpper() == "2")//ETIQUETA 2
                        {
                            if (row == 1)//segunda linha do .txt
                            {
                                for (int indice = 0; indice < linha.Length; indice++)
                                {
                                    if (indice > 6)
                                    {
                                        str += linha[indice];
                                    }
                                }
                            }
                        }                       
                        //
                        row++;
                    }
                    //
                    arqTXT.Close();

                    item.Nome = str;                    
                    lista.Add(item);
                }

            }
            catch
            {
                //
            }

            return lista;
        }

        public class Informacao
        {
            public string Nome { get; set; }
        }
    }

}
