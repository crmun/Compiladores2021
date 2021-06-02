using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace CompilerConsoleApp
{
    class Program
    {
//        static void Main(string[] args)
//        {
//            string myJsonResponse = "{\"nodes\": [" + Environment.NewLine +
//"            {" + Environment.NewLine +
//"            \"data\": {\"id\": \"a\", \"label\": \"Gene1\"}" + Environment.NewLine +
//"            }," + Environment.NewLine +
//"            {" + Environment.NewLine +
//"            \"data\": {\"id\": \"b\", \"label\": \"Gene2\"}" + Environment.NewLine +
//"            }," + Environment.NewLine +
//"            {" + Environment.NewLine +
//"            \"data\": {\"id\": \"c\", \"label\": \"Gene3\"}" + Environment.NewLine +
//"            }," + Environment.NewLine +
//"            {" + Environment.NewLine +
//"            \"data\": {\"id\": \"d\", \"label\": \"Gene4\"}" + Environment.NewLine +
//"            }," + Environment.NewLine +
//"            {" + Environment.NewLine +
//"            \"data\": {\"id\": \"e\", \"label\": \"Gene5\"}" + Environment.NewLine +
//"            }," + Environment.NewLine +
//"            {" + Environment.NewLine +
//"            \"data\": {\"id\": \"f\", \"label\": \"Gene6\"}" + Environment.NewLine +
//"            }" + Environment.NewLine +
//"    ]," + Environment.NewLine +
//"            \"edges\": [" + Environment.NewLine +
//"            {" + Environment.NewLine +
//"            \"data\": {" + Environment.NewLine +
//"            \"id\": \"ab\"," + Environment.NewLine +
//"                    \"source\": \"a\"," + Environment.NewLine +
//"                    \"target\": \"b\"," + Environment.NewLine +
//"                    \"label\": \"etiqueta\"" + Environment.NewLine +
//"            }" + Environment.NewLine +
//"            }," + Environment.NewLine +
//"            {" + Environment.NewLine +
//"            \"data\": {" + Environment.NewLine +
//"            \"id\": \"cd\"," + Environment.NewLine +
//"                    \"source\": \"c\"," + Environment.NewLine +
//"                    \"target\": \"d\"" + Environment.NewLine +
//"            }" + Environment.NewLine +
//"            }," + Environment.NewLine +
//"            {" + Environment.NewLine +
//"            \"data\": {" + Environment.NewLine +
//"            \"id\": \"ef\"," + Environment.NewLine +
//"                    \"source\": \"e\"," + Environment.NewLine +
//"                    \"target\": \"f\"" + Environment.NewLine +
//"            }" + Environment.NewLine +
//"            }," + Environment.NewLine +
//"            {" + Environment.NewLine +
//"            \"data\": {" + Environment.NewLine +
//"            \"id\": \"ac\"," + Environment.NewLine +
//"                    \"source\": \"a\"," + Environment.NewLine +
//"                    \"target\": \"d\"" + Environment.NewLine +
//"            }" + Environment.NewLine +
//"            }," + Environment.NewLine +
//"            {" + Environment.NewLine +
//"            \"data\": {" + Environment.NewLine +
//"             \"id\": \"be\"," + Environment.NewLine +
//"                    \"source\": \"b\"," + Environment.NewLine +
//"                    \"target\": \"e\"" + Environment.NewLine +
//"                }" + Environment.NewLine +
//"                }]}";

//            //var jsonResult = JsonConvert.DeserializeObject(myJsonResponse).ToString();
//            //Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(jsonResult);
//            //Console.WriteLine(myJsonResponse);

//            RegexToNFA regexToNFA = new RegexToNFA();
//            //Graph graph = regexToNFA.processRegexSequential("(ab)*c*");
//            Graph graph = regexToNFA.processRegexSequential("a|b|c|d");
            
//            string strGraph = JsonConvert.SerializeObject(graph);
//            Console.WriteLine(strGraph);

//        }
    }

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

     
        public Data(
            [JsonProperty("id")] string id,
            [JsonProperty("label")] string label
        )
        {
            this.Id = id;
            this.Label = label;
        }

        [JsonProperty("id")]
        public readonly string Id;

        [JsonProperty("label")]
        public readonly string Label;

        [JsonProperty("source")]
        public readonly string Source;

        [JsonProperty("target")]
        public readonly string Target;
        private string v1;
        private string v2;
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

    //para deserializar debemos usar esta clase
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

    //para serializar debemos usar esta clase
    public class Graph
    {
        [JsonConstructor]
        public Graph()
        {
            this.Nodes = new List<Node>();
            this.Edges = new List<Edge>();
        }

        [JsonProperty("nodes")]
        public List<Node> Nodes;

        [JsonProperty("edges")]
        public List<Edge> Edges;

        /**
     * Adds the directed edge v→w to this digraph.
     *
     * @param  v the tail vertex
     * @param  w the head vertex
     * @throws IllegalArgumentException unless both {@code 0 <= v < V} and {@code 0 <= w < V}
     */
        public void addEdge(string label, int v, int w)
        {
            //validateVertex(v);
            var nodeV = validateVertex(v);
            //validateVertex(w);
            var nodeW = validateVertex(w);
            // adj[v].add(w);
            this.Edges.Add(new Edge(new Data(v + ""+ w, label, nodeV.Data.Id, nodeW.Data.Id)));
            //indegree[w]++;
            //E++;
        }

        public Node validateVertex(int v) {
            var nodeV = this.Nodes.FirstOrDefault(f => f.Data.Id == v + "");
            if (nodeV is null)
            {
                nodeV = new Node(new Data(v + "", v + ""));
                this.Nodes.Add(nodeV);
            }
            return nodeV;
        }
    }

    public class RegexToNFA{
        char[] repetition = { '?','*','+' };

        public Graph processRegexSequential(String regexPart)
        {
            char[] charArrayRegex = regexPart.ToCharArray();
            int pendingClose = 0;
            Stack<Int32> stack = new Stack<Int32>();
            List<Int32> listOR = null;
            Graph graph= new Graph();
            bool modeOR = false;
            for (int i = 0; i < charArrayRegex.Length; i++)
            {
                switch (charArrayRegex[i])
                {
                    case '(':
                        //Se debe poner como estado inicial del nodo actual la posicion del parentesis para cualquier OR que pueda estar adentro

                        //Avanzar al que viene despues
                        graph.addEdge('ϵ' + "", i, (i + 1));
                        pendingClose++;                        
                        //poner en una pila la posicion del parentesis
                        stack.Push(i);                        
                        break;
                    case ')':
                        //Se debe verificar si este bloque encerrado en parentesis contenía ORs en cuyo caso el cierre de paréntesis marca el final del último nodo

                        //Avanzar al que viene despues
                        graph.addEdge('ϵ' + "", i, (i + 1));
                        pendingClose--;
                        //sacar de la pila y procesar
                        int beginIndex = stack.Pop();
                        int endIndex = i;
                        //si la siguiente posicion es repeticion, procesamos la repeticion
                        if (i + 1 < charArrayRegex.Length && repetition.Contains(charArrayRegex[i + 1]))
                        {
                            //procesar repeticion y luego i++
                            switch (charArrayRegex[i+1])
                            {
                                case '*':
                                    //Avanzar al que viene despues (conecta del parentesis al asterisco)
                                    graph.addEdge('ϵ' + "", i, (i+1));
                                    //Retroceder al nodo que estaba al principio
                                    graph.addEdge('ϵ' + "", i, beginIndex+1);
                                    break;
                                default:
                                    
                                    break;
                            }
                            //i++;
                            //no nos podemos saltar ningun caracter porque cada caracter nos indica un estado
                            //si nos saltamos un caracter nuestro grafico queda desconectado
                        }
                        else {
                            //Si despues del parentesis no viene repeticion nos estan trolleando y no hay que hacer nada
                            //Tal vez deberiamos procesar todos los ORs internos sea que haya o no repeticion despues
                        }
                        break;
                    //podemos verificar si despues viene * ? + para procesar repeticion y luego i++
                    case '*':
                        ;
                        if (charArrayRegex[i - 1] == ')')
                        {
                            //repeticion sobre expresion paréntesis
                            //este caso ya fue tratado y se le dio i++ no se puede dar aqui
                            //Avanzar al que viene despues (conecta del asterisco al siguiente estado)                          
                            graph.addEdge('ϵ' + "", i, (i + 1));
                        }
                        else
                        {
                            //repetición sólo sobre el caracter anterior
                            //Avanzar al que viene despues                           
                            graph.addEdge('ϵ' + "", i, (i + 1));
                            //Retroceder al nodo anterior
                            graph.addEdge('ϵ' + "", i, i - 1);
                        }
                        break;
                    case '|':
                        //Cuando viene un OR tengo que saber si esta en el contexto de un paréntesis
                        //si es así, su scope se termina al encontrar un parentesis de cierre
                        //si no es así, se debe asumir que todo lo que venía antes se considera un solo nodo compuesto o que
                        //estamos ante una sucesión de ORs en cuyo caso el nuevo OR nos está indicando la finalización del nodo anterior

                        if (listOR == null) {
                            listOR = new List<Int32>();
                        }
                        listOR.Add(i);
                        //Prever caso 1|2|3|4
                        // Prever caso 1|(2|5)*|3|4 se podría transformar a 1|2*|5*|3|4

                        //desde el principio hasta lo que venia antes
                        //desde el principio hasta lo que viene despues
                        //desde lo que venia antes a lo que viene despues de despues
                        //desde lo que venia despues a lo que viene despues de despues
                        break;
                    default:
                        graph.addEdge(charArrayRegex[i] + "", i, i+1);
                        break;
                }

                //esta condicion hay que corregir para soportar caso: a | a b c | c                                                                    
                //0 1 2 3 4 5 6     POSICIONES
                //0   2       6     ESTADOS INICIALES DE C/ NODO
                //agregar en la posicion 0 cada estado inicial de cada nodo

                //crear un estado inicial para la cadena de caracteres que se esté manejando
                //cuando se encuentra un OR si está al mismo nivel que el estado inicial (no hay paréntesis anidados)
                //se debe agregar un estado inicial hermano del estado inicial original
                //cuando el OR no está al mismo nivel que el estado inicial, se debe crear un estado inicial para el nodo 
                //que termina en este PIPE


                if (i>0 && charArrayRegex[i-1] == '|' && 
                        (i==charArrayRegex.Length-1 || charArrayRegex[i+1] != '|') ) {
                    //Ya no hay mas opciones, es hora de resolver los OR
                    for (int indexOr = 0; indexOr < listOR.Count(); indexOr++) {
                        //desde el principio hasta esta opcion                          
                        graph.addEdge('ϵ' + "", listOR.ElementAt(0), listOR.ElementAt(indexOr));
                        //desde esta opcion hasta el final
                        graph.addEdge('ϵ' + "", listOR.ElementAt(indexOr), listOR.ElementAt(listOR.Count()-1));
                    }
                    listOR = null;
                }

            }

            return graph;
        }
    }

}
