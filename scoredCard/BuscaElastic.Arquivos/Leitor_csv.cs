using BuscaElastic.Arquivos.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace BuscaElastic.Arquivos
{
    public class Leitor_csv
    {

        public Leitor_csv()
        {
        }
        public void Init()
        {
            string csv_file_path = @"C:\Users\DEV\Documents\Pessoal\Projeto\luminar\CollegeScorecard_Raw_Data\MERGED2004_05_PP_Backup.csv";
            var csvData = GetListFromCSVFile(csv_file_path);
            Console.WriteLine("Rows count:" + csvData.Count);
            Console.ReadLine();
        }

        public  List<ExpandoObject> GetListFromCSVFile(string csv_file_path)
        {
            List<ExpandoObject> lista = new List<ExpandoObject>();

            try
            {
                using (var textReader = new StreamReader(csv_file_path))
                {


                    var formattedStrings = textReader.ParseCsvDataList();
                    string[] colunas = formattedStrings.FirstOrDefault();
                    formattedStrings.Remove(colunas);
                    foreach (var linha in formattedStrings)
                    {
                        dynamic expando = new ExpandoObject();
                       for(int index = 0; index < colunas.Length || index == 998; index++ )
                            AddProperty(expando, colunas[index],linha[index]);
                        lista.Add(expando);
                    }


                    Console.WriteLine("Qtd Linhas: ", lista.Count());
                }
            }
            catch (Exception ex)
            {
            }
            return lista;
        }

        public static void AddProperty(ExpandoObject expando, string propertyName, object propertyValue)
        {
            // ExpandoObject supports IDictionary so we can extend it like this
            var expandoDict = expando as IDictionary<string, object>;
            //if (expandoDict.ContainsKey(propertyName))
            //    expandoDict[propertyName] = propertyValue;
            //else
                expandoDict.Add(propertyName, propertyValue);
        }

        private static DataTable GetDataTabletFromCSVFile(string csv_file_path)
        {

            DataTable csvData = new DataTable();

            try
            {
                using (var textReader = new StreamReader(csv_file_path))
                {
                    var formattedStrings = textReader.ParseCsvData(data => data[1] + " is " + data[2] + " years old.");
                    foreach (var linha in formattedStrings.ToList())

                        Console.WriteLine("Linha: {0} ", linha);

                    Console.WriteLine("Qtd Linhas: ", formattedStrings.Count());
                }
            }
            catch (Exception ex)
            {
            }
            return csvData;
        }
    }
}
