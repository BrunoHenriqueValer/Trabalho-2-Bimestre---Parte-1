namespace ObserverPattern;

public class Loja : ISubject
{
    // Dicionário: evento → lista de observadores inscritos naquele evento
    private readonly Dictionary<string, List<IObserver>> _inscricoes = new();

    public string Nome { get; }

    public Loja(string nome)
    {
        Nome = nome;
    }

    // ── Gestão de inscrições ──────────────────────────────────────────────

    public void Subscribe(string eventType, IObserver observer)
    {
        if (!_inscricoes.ContainsKey(eventType))
            _inscricoes[eventType] = new List<IObserver>();

        if (!_inscricoes[eventType].Contains(observer))
        {
            _inscricoes[eventType].Add(observer);
            Console.WriteLine($"[Loja] {observer.GetType().Name} inscrito no evento '{eventType}'.");
        }
    }

    public void Unsubscribe(string eventType, IObserver observer)
    {
        if (_inscricoes.ContainsKey(eventType))
        {
            _inscricoes[eventType].Remove(observer);
            Console.WriteLine($"[Loja] {observer.GetType().Name} removido do evento '{eventType}'.");
        }
    }

    public void Notify(string eventType, object data)
    {
        if (!_inscricoes.ContainsKey(eventType) || _inscricoes[eventType].Count == 0)
        {
            Console.WriteLine($"[Loja] Evento '{eventType}' disparado, mas sem inscritos.");
            return;
        }

        Console.WriteLine($"\n[Loja:{Nome}] Disparando evento '{eventType}'...");
        foreach (var observer in _inscricoes[eventType].ToList())
            observer.Update(eventType, data);
    }

    // ── Ações de negócio que disparam eventos ────────────────────────────

    public void CadastrarProduto(Produto produto)
    {
        Console.WriteLine($"\n[Loja:{Nome}] Produto cadastrado: {produto}");
        Notify(Eventos.NovoProduto, produto);
    }

    public void AplicarPromocao(Produto produto, decimal percentualDesconto)
    {
        produto.AplicarDesconto(percentualDesconto);
        Console.WriteLine($"[Loja:{Nome}] Promoção aplicada em: {produto}");
        Notify(Eventos.Promocao, produto);
    }

    public void EsgotarEstoque(Produto produto)
    {
        produto.Estoque = 0;
        Console.WriteLine($"[Loja:{Nome}] Produto esgotado: {produto.Nome}");
        Notify(Eventos.SemEstoque, produto);
    }
}
