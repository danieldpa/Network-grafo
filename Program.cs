public class Graph
{
    private int numElements;
    private Dictionary<int, HashSet<int>> connections;

    public Graph(int numElements)
    {
        if (numElements <= 0)
        {
            throw new ArgumentException("O número de elementos deve ser um inteiro maior que zero.");
        }
        this.numElements = numElements;
        this.connections = new Dictionary<int, HashSet<int>>();//lista de adjacência
    }

    public void Connect(int elem1, int elem2)
    {
        if (elem1 <= 0 || elem2 <= 0 || elem1 > numElements || elem2 > numElements)
        {
            throw new ArgumentException("Os elementos de conexão estão fora do intervalo válido.");
        }
        if (elem1 == elem2)
        {
            throw new ArgumentException("Não é permitido conectar um elemento a ele mesmo.");
        }
        connections.TryAdd(elem1, new HashSet<int>());
        connections.TryAdd(elem2, new HashSet<int>());
        connections[elem1].Add(elem2);
        connections[elem2].Add(elem1);
    }

    //Utilizar o método de busca BFS
    public bool Query(int elem1, int elem2)
    {
        if (elem1 <= 0 || elem2 <= 0 || elem1 > numElements || elem2 > numElements)
        {
            throw new ArgumentException("Os elementos de consulta estão fora do intervalo.");
        }
        if (!connections.ContainsKey(elem1))
        {
            return false;
        }

        HashSet<int> visitados = new HashSet<int>();
        Queue<int> fila = new Queue<int>();

        // Iniciar a busca pelo vértice s
        fila.Enqueue(elem1);
        visitados.Add(elem1);

        while (fila.Count > 0)
        {
            int u = fila.Dequeue();

            foreach (int v in connections[u])
            {
                if (v == elem2) return true;

                if (!visitados.Contains(v))
                {
                    fila.Enqueue(v);
                    visitados.Add(v);
                }
            }
        }

        return false;
    }

    public static void Main(string[] args)
    {
        Graph network = new Graph(8);
        network.Connect(1, 2);
        network.Connect(6, 2);
        network.Connect(2, 4);
        network.Connect(5, 8);

        Console.WriteLine(network.Query(1, 6));  //  True
        Console.WriteLine(network.Query(6, 4));  //  True
        Console.WriteLine(network.Query(7, 4));  //  False
        Console.WriteLine(network.Query(5, 6));  //  False
    }
}
