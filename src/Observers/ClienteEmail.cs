namespace ObserverPattern;

public class ClienteEmail : IObserver
{
    public string Nome       { get; }
    public string Email      { get; }
    public string Categoria  { get; }   // só quer notificações desta categoria

    public ClienteEmail(string nome, string email, string categoria)
    {
        Nome      = nome;
        Email     = email;
        Categoria = categoria;
    }

    public void Update(string eventType, object data)
    {
        if (data is not Produto produto) return;

        // Filtra: só notifica se o produto for da categoria de interesse
        if (!produto.Categoria.Equals(Categoria, StringComparison.OrdinalIgnoreCase))
            return;

        var mensagem = eventType switch
        {
            Eventos.NovoProduto =>
                $"Novo produto disponível: {produto.Nome} por R$ {produto.Preco:F2}",
            Eventos.Promocao =>
                $"PROMOÇÃO! {produto.Nome} agora por R$ {produto.Preco:F2}. Aproveite!",
            _ => null
        };

        if (mensagem is not null)
            Console.WriteLine($"  📧 [E-mail → {Email}] Olá, {Nome}! {mensagem}");
    }
}
