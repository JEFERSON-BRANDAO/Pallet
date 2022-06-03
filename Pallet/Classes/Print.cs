using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using LabelManager2;
using System.Threading;
using System.Diagnostics;
using Classes;

namespace Pallet
{
    class Print
    {
        public class RawPrinterHelper
        {
            #region IMPRIMIR

            // Structure and API declarions:
            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public class DOCINFOA
            {
                [MarshalAs(UnmanagedType.LPStr)]
                public string pDocName;
                [MarshalAs(UnmanagedType.LPStr)]
                public string pOutputFile;
                [MarshalAs(UnmanagedType.LPStr)]
                public string pDataType;
            }

            //
            [DllImport("shell32.dll", EntryPoint = "ShellExecute")]
            public static extern int ShellExecuteA(int hwnd, string lpOperation,
                  string lpFile, string lpParameters, string lpDirectory, int nShowCmd);


            [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

            [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern bool ClosePrinter(IntPtr hPrinter);

            [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

            [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern bool EndDocPrinter(IntPtr hPrinter);

            [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern bool StartPagePrinter(IntPtr hPrinter);

            [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern bool EndPagePrinter(IntPtr hPrinter);

            [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);

            // SendBytesToPrinter()
            // When the function is given a printer name and an unmanaged array
            // of bytes, the function sends those bytes to the print queue.
            // Returns true on success, false on failure.
            public static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, Int32 dwCount)
            {
                Int32 dwError = 0, dwWritten = 0;
                IntPtr hPrinter = new IntPtr(0);
                DOCINFOA di = new DOCINFOA();
                bool bSuccess = false; // Assume failure unless you specifically succeed.

                di.pDocName = "Etiqueta SN Document";
                di.pDataType = "RAW";

                // Open the printer.
                if (OpenPrinter(szPrinterName.Normalize(), out hPrinter, IntPtr.Zero))
                {
                    // Start a document.
                    if (StartDocPrinter(hPrinter, 1, di))
                    {
                        // Start a page.
                        if (StartPagePrinter(hPrinter))
                        {
                            // Write your bytes.
                            bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
                            EndPagePrinter(hPrinter);
                        }
                        EndDocPrinter(hPrinter);
                    }
                    ClosePrinter(hPrinter);
                }
                // If you did not succeed, GetLastError may give more information
                // about why not.
                if (bSuccess == false)
                {
                    dwError = Marshal.GetLastWin32Error();
                }
                return bSuccess;
            }
            //
            public static bool SendFileToPrinter(string szPrinterName, string szFileName)
            {
                // Open the file.
                FileStream fs = new FileStream(szFileName, FileMode.Open);
                // Create a BinaryReader on the file.
                BinaryReader br = new BinaryReader(fs);
                // Dim an array of bytes big enough to hold the file's contents.
                Byte[] bytes = new Byte[fs.Length];
                bool bSuccess = false;
                // Your unmanaged pointer.
                IntPtr pUnmanagedBytes = new IntPtr(0);
                int nLength;

                nLength = Convert.ToInt32(fs.Length);
                // Read the contents of the file into the array.
                bytes = br.ReadBytes(nLength);
                // Allocate some unmanaged memory for those bytes.
                pUnmanagedBytes = Marshal.AllocCoTaskMem(nLength);
                // Copy the managed byte array into the unmanaged array.
                Marshal.Copy(bytes, 0, pUnmanagedBytes, nLength);
                // Send the unmanaged bytes to the printer.
                bSuccess = SendBytesToPrinter(szPrinterName, pUnmanagedBytes, nLength);
                // Free the unmanaged memory that you allocated earlier.
                Marshal.FreeCoTaskMem(pUnmanagedBytes);

                //fs.Flush();//
                fs.Close();//Para não dar erro: The process cannot access the file 'C:\IMPRESSAO\etiqueta.prn' because it is being used by another process.
                return bSuccess;
            }
            //
            public static bool SendStringToPrinter(string szPrinterName, string szString)
            {
                IntPtr pBytes;
                Int32 dwCount;
                // How many characters are in the string?
                dwCount = szString.Length;
                // Assume that the printer is expecting ANSI text, and then convert
                // the string to ANSI text.
                pBytes = Marshal.StringToCoTaskMemAnsi(szString);
                // Send the converted ANSI string to the printer.
                SendBytesToPrinter(szPrinterName, pBytes, dwCount);
                Marshal.FreeCoTaskMem(pBytes);
                return true;
            }

            #endregion
        }

        public string Etiqueta_CodeSoft(List<Carton.Etiqueta> Lista_Carton, string pallet, string Impressora, int labelCopia, string label)
        {
            string mensagem = string.Empty;
            string arquivoCodeSoft = string.Empty;
            int lista_row_Count = Lista_Carton.Count;
            //
            string PO = string.Empty;
            string SHIPDATE = string.Empty;
            string SKUNO = string.Empty;
            string TOTALQUANTITY = string.Empty;
            string TOTALPALLETS = string.Empty;
            string TOTALMASTERCARTONS = string.Empty;
            string QTYPERMASTERCARTONS = string.Empty;
            string QTYPERPALLET = string.Empty;
            string PALLETID = string.Empty;
            string CARTONQTY = string.Empty;
            string REV = string.Empty;
            //
            if (lista_row_Count > 0)
            {
                #region NOME ARQUIVO CODESOFT

                if (label == "1")
                {
                    arquivoCodeSoft = "RU9026000643_PALLET.Lab";
                }
                else if (label == "2")
                {
                    arquivoCodeSoft = "RU9026000643_PALLET2.Lab";
                }
                //
                string strFile = AppDomain.CurrentDomain.BaseDirectory + @"\IMPRESSAO\" + arquivoCodeSoft;
                if (File.Exists(strFile))
                {
                    try
                    {
                        LabelManager2.ApplicationClass lbl = new LabelManager2.ApplicationClass();
                        lbl.Documents.Open(strFile, false);
                        lbl.ActiveDocument.Printer.SwitchTo(Impressora, "", true);
                        //
                        #region LABEL 1

                        if (arquivoCodeSoft == "RU9026000643_PALLET.Lab")
                        {
                            PO = Lista_Carton[0].PO;
                            SHIPDATE = Lista_Carton[0].SHIPDATE;
                            SKUNO = Lista_Carton[0].FOXCONNPROJECTNO;
                            TOTALQUANTITY = Lista_Carton[0].TOTALQUANTITY;
                            TOTALPALLETS = Lista_Carton[0].TOTALPALLETS;
                            TOTALMASTERCARTONS = Lista_Carton[0].TOTALMASTERCARTONS;
                            QTYPERMASTERCARTONS = Lista_Carton[0].QTYPERMASTERCARTONS;
                            QTYPERPALLET = Lista_Carton[0].QTYPERPALLET;
                            PALLETID = pallet;
                            CARTONQTY = Lista_Carton[0].CARTONQTY;

                            //VAREAVEL CODESOFT LABEL
                            lbl.ActiveDocument.Variables.FormVariables.Item("PO").Value = PO;
                            lbl.ActiveDocument.Variables.FormVariables.Item("SHIPDATE").Value = SHIPDATE;
                            lbl.ActiveDocument.Variables.FormVariables.Item("SKUNO").Value = SKUNO;
                            lbl.ActiveDocument.Variables.FormVariables.Item("TOTALQUANTITY").Value = TOTALQUANTITY;
                            lbl.ActiveDocument.Variables.FormVariables.Item("TOTALPALLETS").Value = TOTALPALLETS;
                            lbl.ActiveDocument.Variables.FormVariables.Item("TOTALMASTERCARTONS").Value = TOTALMASTERCARTONS;
                            lbl.ActiveDocument.Variables.FormVariables.Item("QTYPERMASTERCARTON").Value = QTYPERMASTERCARTONS;
                            lbl.ActiveDocument.Variables.FormVariables.Item("QTYPERPALLET").Value = QTYPERPALLET;
                            lbl.ActiveDocument.Variables.FormVariables.Item("PALLETID").Value = PALLETID;
                            lbl.ActiveDocument.Variables.FormVariables.Item("CARTONQTY").Value = CARTONQTY;
                        }

                        #endregion
                        //
                        #region LABEL 2

                        if (arquivoCodeSoft == "RU9026000643_PALLET2.Lab")
                        {
                            PALLETID = pallet;
                            TOTALPALLETS = Lista_Carton[0].TOTALPALLETS;
                            REV = Lista_Carton[0].REV;
                            SKUNO = Lista_Carton[0].FOXCONNPROJECTNO;
                            TOTALQUANTITY = Lista_Carton[0].TOTALQUANTITY;
                            //VAREAVEL CODESOFT LABEL
                            lbl.ActiveDocument.Variables.FormVariables.Item("PALLETID").Value = PALLETID;
                            lbl.ActiveDocument.Variables.FormVariables.Item("TOTALPALLETS").Value = TOTALPALLETS;
                            lbl.ActiveDocument.Variables.FormVariables.Item("REV").Value = REV;
                            lbl.ActiveDocument.Variables.FormVariables.Item("TOTALQUANTITY").Value = TOTALQUANTITY;

                        }

                        #endregion
                        //
                        lbl.ActiveDocument.PrintDocument(labelCopia);//IMPRIMIR 4 LABEL
                        lbl.ActiveDocument.PrintLabel(1, 1, 1, 1, 1, "ETIQUETA-" + SKUNO);

                        //Encerra o processo do programa CODSOFT
                        string nomeExecutavel = "lppa"; //"LPPA.exe";
                        foreach (Process pr in Process.GetProcessesByName(nomeExecutavel))
                        {
                            if (!pr.HasExited)
                                pr.Kill();
                        }
                        //
                        mensagem = "OK, Impressão realizado com sucesso";
                    }
                    catch (Exception erro)
                    {
                        mensagem = "ERRO: " + erro.Message;

                        //MATA PROCESSOS DO PROGRAMA CODSOFT EM ABERTO
                        string nomeExecutavel = "lppa"; //"LPPA.exe";
                        foreach (Process pr in Process.GetProcessesByName(nomeExecutavel))
                        {
                            if (!pr.HasExited)
                                pr.Kill();
                        }
                    }
                }
                else
                {
                    mensagem = "ERRO: Arquivo RU9026000643_PALLET.Lab não encontrado";
                }

                #endregion
            }
            else
            {
                mensagem = "ERRO: lista vázia";
            }
            //
            return mensagem;
        }
    }
}
