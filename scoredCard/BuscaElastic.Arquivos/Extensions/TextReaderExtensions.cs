using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BuscaElastic.Arquivos.Extensions
{
    public static class TextReaderExtensions
    {
        public static IEnumerable<T> ParseCsvData<T>(this TextReader reader, Func<string[], T> mapFunction, char separator = ',', short skipLines = 1)
        {
            IEnumerable<string[]> readCsvLines()
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (!string.IsNullOrEmpty(line) && skipLines-- < 1)
                    {
                        yield return line.Split(separator);
                    }
                }
            }

            foreach (var csvLine in readCsvLines())
            {
                yield return mapFunction.Invoke(csvLine) ?? default;
            }
        }

        public static IList<string[]> ParseCsvDataList(this TextReader reader, char separator = ',')
        {
            List<string[]> lista = new List<string[]>();

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    lista.Add(line.Split(separator));
                }
            }

            return lista;

        }

    }
}
