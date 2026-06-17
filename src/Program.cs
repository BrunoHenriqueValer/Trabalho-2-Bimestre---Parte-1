using ObserverPattern;

Console.WriteLine("════════════════════════════════════════════════════════");
Console.WriteLine("   PADRÃO OBSERVER — Sistema de Notificações da Loja    ");
Console.WriteLine("════════════════════════════════════════════════════════");

// ── 1. Cria o Subject (a Loja) ────────────────────────────────────────────
var loja = new Loja("TechShop Online");

// ── 2. Cria os Observers ──────────────────────────────────────────────────
var clienteAna    = new ClienteEmail("Ana Lima",    "ana@email.com",    "Eletrônicos");
var clienteCarlos = new ClienteEmail("Carlos Melo", "carlos@email.com", "Livros");
var clienteSofia  = new ClienteEmail("Sofia Ramos", "sofia@email.com",  "Eletrônicos");
var estoque       = new SistemaEstoque();
var marketing     = new EmailMarketing();
var log           = new LogSistema();

// ── 3. Inscrições nos eventos de interesse ───────────────────────────────
Console.WriteLine("\n─── Configurando inscrições ───────────────────────────");

loja.Subscribe(Eventos.NovoProduto, clienteAna);
loja.Subscribe(Eventos.NovoProduto, clienteCarlos);
loja.Subscribe(Eventos.NovoProduto, clienteSofia);

loja.Subscribe(Eventos.Promocao, clienteAna);
loja.Subscribe(Eventos.Promocao, clienteCarlos);
loja.Subscribe(Eventos.Promocao, clienteSofia);
loja.Subscribe(Eventos.Promocao, marketing);

loja.Subscribe(Eventos.SemEstoque, estoque);

// Log ouve TODOS os eventos
loja.Subscribe(Eventos.NovoProduto, log);
loja.Subscribe(Eventos.Promocao,    log);
loja.Subscribe(Eventos.SemEstoque,  log);

// ── 4. Ações de negócio que disparam eventos ─────────────────────────────
Console.WriteLine("\n─── Cadastro de novos produtos ────────────────────────");

var notebook   = new Produto("Notebook Ultra",        3_499.90m, 50, "Eletrônicos");
var foneOuvido = new Produto("Fone Bluetooth Pro",      249.90m, 30, "Eletrônicos");
var livroCs    = new Produto("Clean Code - R. Martin",   89.90m, 20, "Livros");
var tablet     = new Produto("Tablet 10\" 128GB",      1_299.90m, 15, "Eletrônicos");

loja.CadastrarProduto(notebook);
loja.CadastrarProduto(livroCs);

Console.WriteLine("\n─── Aplicando promoções ───────────────────────────────");
loja.AplicarPromocao(notebook,   20); 
loja.AplicarPromocao(livroCs,    15); 
loja.AplicarPromocao(foneOuvido, 30); 

Console.WriteLine("\n─── Esgotando estoque ─────────────────────────────────");
loja.EsgotarEstoque(notebook);
loja.EsgotarEstoque(foneOuvido);

// ── 5. Demonstra remoção de inscrição ─────────────────────────────────────
Console.WriteLine("\n─── Carlos cancela inscrição em promoções ─────────────");
loja.Unsubscribe(Eventos.Promocao, clienteCarlos);

Console.WriteLine("\n─── Nova promoção após cancelamento de Carlos ──────────");
loja.AplicarPromocao(tablet, 10); // Carlos NÃO deve receber

// ── 6. Relatórios dos observadores 
estoque.ExibirPedidos();
marketing.DispararNewsletter();
log.ExibirLog();

Console.WriteLine("\n════════════════════════════════════════════════════════");
Console.WriteLine("   Demonstração concluída!                              ");
Console.WriteLine("   Padrão Observer aplicado com sucesso.                ");
Console.WriteLine("════════════════════════════════════════════════════════");
