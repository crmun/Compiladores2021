using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace RegexToNFAProgramApp
{
    class RegexToNFAProgram
    {

        static void Main(string[] args)
        {
            string myJsonResponse = "{\"nodes\": [" + Environment.NewLine +
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
"                }]}";

            //var jsonResult = JsonConvert.DeserializeObject(myJsonResponse).ToString();
            //Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(jsonResult);
            //Console.WriteLine(myJsonResponse);

            RegexToNFA regexToNFA = new RegexToNFA();
            Graph graph = regexToNFA.processRegexSequential("(ab)*c*");
            //Graph graph = regexToNFA.processRegexSequential("a|b|c|d"); //NO SOPORTADO
            //Graph graph = regexToNFA.processRegexSequential("a|b");

            string strGraph = JsonConvert.SerializeObject(graph);
            Console.WriteLine(strGraph);

            NFAToDFA nFAToDFA = new NFAToDFA(graph);

        }
    }

    public class NFAToDFA
    {
        private Graph graph;

        public NFAToDFA(Graph graph)
        {
            this.graph = graph;
        }

        public void processGraph() {
            //Destados = 0;

        }
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

    public class StatesPointer {
        public Int32 InitialState;
        public Int32 FinalState;

        //public StatesPointer(int initialState, int finalState)
        //{
        //    InitialState = initialState;
        //    FinalState = finalState;
        //}
    }

    public class ParenthesesPointer {
        public Int32 PreviouState;
        public Int32 CurrentState;
        public Int32 CharIndex;
        public Boolean PendingOR;

        public ParenthesesPointer(int previousState, int currentState, int charIndex, bool pendingOR)
        {
            PreviouState = previousState;
            CurrentState = currentState;
            CharIndex = charIndex;
            PendingOR = pendingOR;
        }
    }

    public class RegexToNFA{
        int stateIndex = 0;
        Graph graph;
        char[] repetition = { '?','*','+' };

        int createNewState() {
            return stateIndex++;
        }
      
        public Graph processRegexSequential(String regexPart)
        {
            regexPart = "(" + regexPart + ")";
            Stack<ParenthesesPointer> stackParentheses = new Stack<ParenthesesPointer>();
            Stack<Int32> stackORFinalStates = new Stack<Int32>();
            //Stack<StatesPointer> statesStack = new Stack<StatesPointer>();
            int initialStateLeftSideOr = createNewState();
            int finalState = createNewState();
            bool pendingOR = false;
            //StatesPointer currentStatePointer = new StatesPointer(initialState, finalState);
            //statesStack.Push(currentStatePointer);
            graph = new Graph();
            
            //Dibujar este al final
            //graph.addEdge('ϵ' + "", initialStateLeftSideOr, finalState);

            char[] charArrayRegex = regexPart.ToCharArray();

            int currentState = initialStateLeftSideOr;
            int stateBeforeLastChar = 0;
            for (int i = 0; i < charArrayRegex.Length; i++)
            {
                int newCurrentState;
                bool mustJumpNextChar = false;
                switch (charArrayRegex[i])
                {
                    case '(':
                        //newCurrentState = createNewState();
                        //graph.addEdge("ϵ", currentState, newCurrentState);
                        //stackParentheses.Push(new ParenthesesPointer(currentState,newCurrentState, i));
                        //currentState = newCurrentState;
                        stackParentheses.Push(new ParenthesesPointer(currentState, currentState, i, pendingOR));
                        pendingOR = false;
                        break;
                    case ')':
                        newCurrentState = currentState;
                        //newCurrentState = createNewState();
                        //graph.addEdge("ϵ", currentState, newCurrentState);
                        ParenthesesPointer initialParentheses = stackParentheses.Pop();
                        ParenthesesPointer finalParentheses = new ParenthesesPointer(currentState, newCurrentState, i, pendingOR);
                        if (regexPart.Substring(initialParentheses.CharIndex, finalParentheses.CharIndex - initialParentheses.CharIndex).Contains("|")) {
                            int finalStateOR = processOREnd(newCurrentState, ref stackORFinalStates);
                            finalParentheses.CurrentState = finalStateOR;
                        }
                        if (i + 1 < charArrayRegex.Length && charArrayRegex[i + 1] == '*') {
                            newCurrentState = processRepetition(initialParentheses.CurrentState, finalParentheses.CurrentState);
                            mustJumpNextChar = true;
                        }
                        if (initialParentheses.CharIndex - 1 > 0 ){
                            if (charArrayRegex[initialParentheses.CharIndex] == '|'){
                                
                            }
                            else {
                                graph.addEdge("ϵ", initialParentheses.PreviouState, finalParentheses.CurrentState);
                            }
                        }
                        currentState = newCurrentState;
                        pendingOR = initialParentheses.PendingOR;
                        break;
                    
                    case '*':                        
                        currentState = processRepetition(stateBeforeLastChar, currentState);
                        break;
                    case '|':
                        if (pendingOR) {
                            currentState = processOREnd(currentState, ref stackORFinalStates);
                        }
                        
                        currentState = processORBegin(ref initialStateLeftSideOr, currentState, ref stackORFinalStates);
                        pendingOR = true;
                        break;
                    default:
                        stateBeforeLastChar = currentState;
                        newCurrentState = createNewState();                        
                        graph.addEdge(charArrayRegex[i] + "", currentState, newCurrentState);
                        currentState = newCurrentState;
                        break;
                }
                if (mustJumpNextChar) {
                    i++;
                }
            }
            return graph;
        }

        private int processORBegin(ref int initialStateLeftSideOr, int finalStateLeftSideOR, ref Stack<int> stackORFinalStates)
        {
            int newInitialState = createNewState();
            int newFinalState = createNewState();
            int newInitialStateRightSideOr = createNewState();
            graph.addEdge("Initial Left ϵ", newInitialState, initialStateLeftSideOr);
            graph.addEdge("Initial Right ϵ", newInitialState, newInitialStateRightSideOr);
            graph.addEdge("Final Left ϵ", finalStateLeftSideOR, newFinalState);
            stackORFinalStates.Push(newFinalState);

            initialStateLeftSideOr = newInitialState;
            return newInitialStateRightSideOr;
        }

        private int processOREnd(int currentState, ref Stack<int> stackORFinalStates)
        {
            int finalStateOR = stackORFinalStates.Pop();
            graph.addEdge("Final Right ϵ", currentState, finalStateOR);
            return finalStateOR;
        }

        private int processRepetition(int initialState, int finalState)
        {
            //int newInitialState = createNewState();
            int newFinalState = createNewState();

            graph.addEdge("ϵ", finalState, initialState);
            graph.addEdge("ϵ", finalState, newFinalState);
            graph.addEdge("ϵ", initialState, newFinalState);           

            return newFinalState;
        }
    }

}
