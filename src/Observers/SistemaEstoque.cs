namespace ObserverPattern;

public class SistemaEstoque : IObserver
{
    private readonly List<string> _pedidosReposicao = new();

    public void Update(string eventType, object data)
    {
        if (eventType != Eventos.SemEstoque) return;
        if (data is not Produto produto) return;

        _pedidosReposicao.Add(produto.Nome);
        Console.WriteLine($"  📦 [Estoque] Alerta: '{produto.Nome}' esgotado! " +
                          $"Pedido de reposição nº {_pedidosReposicao.Count} gerado.");
    }

    public void ExibirPedidos()
    {
        Console.WriteLine("\n[Sistema de Estoque] Pedidos de reposição pendentes:");
        if (_pedidosReposicao.Count == 0)
        {
            Console.WriteLine("  Nenhum pedido.");
            return;
        }
        for (int i = 0; i < _pedidosReposicao.Count; i++)
            Console.WriteLine($"  {i + 1}. {_pedidosReposicao[i]}");
    }
}
