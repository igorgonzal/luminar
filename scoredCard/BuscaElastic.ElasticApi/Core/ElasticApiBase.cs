using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuscaElastic.ElasticApi.Core
{
    public class ElasticApiBase
    {
        //private Uri _uri = new Uri("https://b3d1622250b146a8a9304a65b571f0e9.eastus2.azure.elastic-cloud.com:9243");
        private Uri _uri = new Uri("https://b82f9a3678994de1a96beb5e3517f6dc.us-central1.gcp.cloud.es.io:9243");
        private ConnectionConfiguration config = null;
        private ElasticLowLevelClient client = null;

        public ElasticLowLevelClient Cliente { get { return client; } }

        public ElasticApiBase()
        {
            config = new ConnectionConfiguration(_uri);
            //config.BasicAuthentication("elastic", "ysXcbM26kJw2LGrON8sq9tBL");
            config.BasicAuthentication("elastic", "o7EiX2GUsQTAkTkqTpEmyOVn");
            client = new ElasticLowLevelClient(config);
        }
    }
}
