using BuscaElastic.Arquivos;
using BuscaElastic.ElasticApi.Core;
using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace BuscaElastic.ConsoleTest
{
    public class Importador
    {

        public async void Importar(List<ExpandoObject> lista)
        {
            var elastic = new ElasticApiBase();
            int total = lista.Count;
            int index = 1;
            foreach (dynamic item in lista)
            {
                var ndexResponse =  elastic.Cliente.Index<StringResponse>("scorecard", item.UNITID, PostData.Serializable(item));
                string responseBytes = ndexResponse.Body;
                Console.WriteLine("Importou {0} de {1}, {2}", index, total, responseBytes);
                index++;
            }
        }

    }
}
