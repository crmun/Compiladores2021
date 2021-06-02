using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompilerWebApp
{
    public class ClassMain
    {
        public static void Main() {

            string myJsonResponse = "\"nodes\": [" + Environment.NewLine +
"            {" + Environment.NewLine +
"            \"data\": {\"id\": \"a\", \"label\": \"Gene1\"}" + Environment.NewLine +
"            }," + Environment.NewLine +
"            {" + Environment.NewLine +
"            \"data\": {\"id\": \"b\", \"label\": \"Gene2\"}" + Environment.NewLine +
"            }," + Environment.NewLine +
"            {" + Environment.NewLine +
"            \"data\": {\"id\": \"c\", \"label\": \"Gene3\"}" + Environment.NewLine +
"            }," + Environment.NewLine +
"            {" + Environment.NewLine +
"            \"data\": {\"id\": \"d\", \"label\": \"Gene4\"}" + Environment.NewLine +
"            }," + Environment.NewLine +
"            {" + Environment.NewLine +
"            \"data\": {\"id\": \"e\", \"label\": \"Gene5\"}" + Environment.NewLine +
"            }," + Environment.NewLine +
"            {" + Environment.NewLine +
"            \"data\": {\"id\": \"f\", \"label\": \"Gene6\"}" + Environment.NewLine +
"            }" + Environment.NewLine +
"    ]," + Environment.NewLine +
"            \"edges\": [" + Environment.NewLine +
"            {" + Environment.NewLine +
"            \"data\": {" + Environment.NewLine +
"            \"id\": \"ab\"," + Environment.NewLine +
"                    \"source\": \"a\"," + Environment.NewLine +
"                    \"target\": \"b\"," + Environment.NewLine +
"                    \"label\": \"etiqueta\"" + Environment.NewLine +
"            }" + Environment.NewLine +
"            }," + Environment.NewLine +
"            {" + Environment.NewLine +
"            \"data\": {" + Environment.NewLine +
"            \"id\": \"cd\"," + Environment.NewLine +
"                    \"source\": \"c\"," + Environment.NewLine +
"                    \"target\": \"d\"" + Environment.NewLine +
"            }" + Environment.NewLine +
"            }," + Environment.NewLine +
"            {" + Environment.NewLine +
"            \"data\": {" + Environment.NewLine +
"            \"id\": \"ef\"," + Environment.NewLine +
"                    \"source\": \"e\"," + Environment.NewLine +
"                    \"target\": \"f\"" + Environment.NewLine +
"            }" + Environment.NewLine +
"            }," + Environment.NewLine +
"            {" + Environment.NewLine +
"            \"data\": {" + Environment.NewLine +
"            \"id\": \"ac\"," + Environment.NewLine +
"                    \"source\": \"a\"," + Environment.NewLine +
"                    \"target\": \"d\"" + Environment.NewLine +
"            }" + Environment.NewLine +
"            }," + Environment.NewLine +
"            {" + Environment.NewLine +
"            \"data\": {" + Environment.NewLine +
"             \"id\": \"be\"," + Environment.NewLine +
"                    \"source\": \"b\"," + Environment.NewLine +
"                    \"target\": \"e\"" + Environment.NewLine +
"                }" + Environment.NewLine +
"                }]";

            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
            Console.WriteLine(myJsonResponse);
        }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Data
    {
        [JsonConstructor]
        public Data(
            [JsonProperty("id")] string id,
            [JsonProperty("label")] string label,
            [JsonProperty("source")] string source,
            [JsonProperty("target")] string target
        )
        {
            this.Id = id;
            this.Label = label;
            this.Source = source;
            this.Target = target;
        }

        [JsonProperty("id")]
        public readonly string Id;

        [JsonProperty("label")]
        public readonly string Label;

        [JsonProperty("source")]
        public readonly string Source;

        [JsonProperty("target")]
        public readonly string Target;
    }

    public class Node
    {
        [JsonConstructor]
        public Node(
            [JsonProperty("data")] Data data
        )
        {
            this.Data = data;
        }

        [JsonProperty("data")]
        public readonly Data Data;
    }

    public class Edge
    {
        [JsonConstructor]
        public Edge(
            [JsonProperty("data")] Data data
        )
        {
            this.Data = data;
        }

        [JsonProperty("data")]
        public readonly Data Data;
    }

    public class Root
    {
        [JsonConstructor]
        public Root(
            [JsonProperty("nodes")] List<Node> nodes,
            [JsonProperty("edges")] List<Edge> edges
        )
        {
            this.Nodes = nodes;
            this.Edges = edges;
        }

        [JsonProperty("nodes")]
        public readonly List<Node> Nodes;

        [JsonProperty("edges")]
        public readonly List<Edge> Edges;
    }


}