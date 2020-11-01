using BuscaElastic.Arquivos;
using BuscaElastic.ElasticApi.Core;
using Elasticsearch.Net;
using System;

namespace BuscaElastic.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {

            var leitor = new Leitor_csv();
            var lista = leitor.GetListFromCSVFile(@"C:\Users\DEV\Documents\Pessoal\Projeto\luminar\CollegeScorecard_Raw_Data\MERGED2004_05_PP_Backup.csv");

            var import = new Importador();
            import.Importar(lista);

        }
    }
}
