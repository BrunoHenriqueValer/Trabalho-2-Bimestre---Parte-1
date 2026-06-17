namespace ObserverPattern;

public class EmailMarketing : IObserver
{
    private readonly List<Produto> _ofertas = new();

    public void Update(string eventType, object data)
    {
        if (eventType != Eventos.Promocao) return;
        if (data is not Produto produto) return;

        _ofertas.Add(produto);
        Console.WriteLine($"  📣 [Marketing] Promoção registrada para newsletter: {produto.Nome}");
    }

    public void DispararNewsletter()
    {
        Console.WriteLine("\n[Marketing] Disparando newsletter com as ofertas da semana:");
        if (_ofertas.Count == 0)
        {
            Console.WriteLine("  Nenhuma oferta registrada.");
            return;
        }
        foreach (var p in _ofertas)
            Console.WriteLine($"  • {p.Nome} — R$ {p.Preco:F2}");

        _ofertas.Clear();
    }
}
