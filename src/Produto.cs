namespace ObserverPattern;


// Modelo de domínio simples: representa um produto da loja.

public class Produto
{
    public string Nome       { get; }
    public decimal Preco     { get; private set; }
    public int Estoque       { get; set; }
    public string Categoria  { get; }

    public Produto(string nome, decimal preco, int estoque, string categoria)
    {
        Nome      = nome;
        Preco     = preco;
        Estoque   = estoque;
        Categoria = categoria;
    }

    public void AplicarDesconto(decimal percentual)
    {
        Preco = Math.Round(Preco * (1 - percentual / 100), 2);
    }

    public override string ToString() =>
        $"{Nome} | R$ {Preco:F2} | Estoque: {Estoque} | Cat: {Categoria}";
}
