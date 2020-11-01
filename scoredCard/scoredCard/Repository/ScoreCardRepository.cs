using Nest;
using scoredCard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace scoredCard.Repository
{
    public interface IScoreCardRepository
    {
        bool Excluir(string id);
        IEnumerable<Scorecard> Listar();
        IEnumerable<Scorecard> ListarDashBoard();
        Scorecard Selecionar(string id);
        bool Persistir(Scorecard scorecard);
        bool Atualizar(Scorecard scorecard);
    }


    public class ScoreCardRepository : IScoreCardRepository
    {
        private readonly IElasticClient _elasticClient;

        public ScoreCardRepository(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;

        }

        public bool Excluir(string id)
        {
            bool status;

            var response = _elasticClient.Delete<Scorecard>(id, d => d
            .Index(nameof(Scorecard).ToLower()));

            if (response.IsValid)
            {
                status = true;
            }
            else
            {
                status = false;
            }

            return status;
        }

        public IEnumerable<Scorecard> Listar()
        {
            // var searchResponse = _elasticClient.Search<Scorecard>(s => s
            //.Index(nameof(Scorecard).ToLower()));

            var searchResponse = _elasticClient.Search<Scorecard>(s => s
                    .Query(q => q
                        .MultiMatch(m => m
                            .Fields(f => f
                                .Field("UNITID")
                                .Field("CITY")
                                .Field("ZIP")
                                .Field("INSTNM")
                                .Field("REGION")
                            )
                        )
                    ));

            var score = searchResponse.Documents;
            return score?.ToList();
        }

        public IEnumerable<Scorecard> ListarDashBoard()
        {
            var searchResponse = _elasticClient.Search<Scorecard>(s => s
                    .Query(q => q
                        .MultiMatch(m => m
                            .Fields(f => f
                                .Field("UNITID")
                                .Field("CITY")
                                .Field("ZIP")
                                .Field("INSTNM")
                                .Field("REGION")
                            )
                        )
                    ));

            var score = searchResponse.Documents;
            return score?.ToList();
        }

        public Scorecard Selecionar(string id)
        {
            var result = _elasticClient.Get<Scorecard>(id);

            return result.Source;
        }

        public bool Persistir(Scorecard scorecard)
        {
            bool status;


            var response = _elasticClient.IndexDocument(scorecard);

            if (response.IsValid)
            {
                status = true;
            }
            else
            {
                status = false;
            }

            return status;
        }

        public bool Atualizar(Scorecard scorecard)
        {
            bool status;

            var response = _elasticClient.Update<Scorecard, Scorecard>(scorecard.UNITID, d => d
            .Index(nameof(scorecard).ToLower())
            .Doc(scorecard));

            if (response.IsValid)
            {
                status = true;
            }
            else
            {
                status = false;
            }

            return status;
        }
    }
}
