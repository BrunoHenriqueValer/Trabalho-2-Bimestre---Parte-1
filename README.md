# 🔔 Design Pattern: Observer — Sistema de Notificações de Loja Online

> Implementação do padrão de projeto **Observer** (Comportamental) em **C# (.NET 8)**  
> Referência: [refactoring.guru/pt-br/design-patterns/observer](https://refactoring.guru/pt-br/design-patterns/observer)

---

## 📖 O que é o Padrão Observer?

O **Observer** é um padrão de projeto comportamental que define um mecanismo de assinatura para notificar múltiplos objetos sobre quaisquer eventos que aconteçam com o objeto que eles estão observando.

```
┌─────────────────────────────────────────────────────────┐
│                   <<interface>>                         │
│                    ISubject                             │
│  + Subscribe(eventType, observer)                       │
│  + Unsubscribe(eventType, observer)                     │
│  + Notify(eventType, data)                              │
└───────────────────────┬─────────────────────────────────┘
                        │ implementa
                        ▼
              ┌─────────────────┐
              │     Loja        │  ──────────────────────────┐
              │  (Subject)      │                            │
              │                 │  notifica →    ┌───────────────────────┐
              │ CadastrarProd() │                │   <<interface>>       │
              │ AplicarPromo()  │                │    IObserver          │
              │ EsgotarEstoque()│                │  + Update(evt, data)  │
              └─────────────────┘                └──────────┬────────────┘
                                                            │ implementam
                                          ┌─────────────────┼──────────────────┐
                                          ▼                 ▼                  ▼
                                   ClienteEmail      SistemaEstoque      EmailMarketing
                                   LogSistema
```

---

## 🏗️ Estrutura do Projeto

```
ObserverPattern/
├── ObserverPattern.csproj
├── src/
│   ├── IObserver.cs          ← Interface do Observador
│   ├── ISubject.cs           ← Interface do Sujeito
│   ├── Eventos.cs            ← Constantes dos tipos de eventos
│   ├── Produto.cs            ← Modelo de domínio
│   ├── Loja.cs               ← Subject concreto (publicador)
│   ├── Program.cs            ← Demonstração completa
│   └── Observers/
│       ├── ClienteEmail.cs   ← Observer: notificação por e-mail por categoria
│       ├── SistemaEstoque.cs ← Observer: controle de reposição de estoque
│       ├── EmailMarketing.cs ← Observer: registro para newsletter
│       └── LogSistema.cs     ← Observer: auditoria de todos os eventos
└── README.md
```

---

## 🎯 Cenário de Demonstração

Uma **loja online** (`Loja`) dispara eventos de negócio:

| Evento | Quando ocorre |
|---|---|
| `novo_produto` | Um produto é cadastrado |
| `promocao` | Um desconto é aplicado |
| `sem_estoque` | Um produto é esgotado |

Os **observadores** se inscrevem apenas nos eventos que lhes interessam:

| Observer | Eventos de interesse | Ação |
|---|---|---|
| `ClienteEmail` | `novo_produto`, `promocao` | Envia e-mail ao cliente (filtra por categoria) |
| `SistemaEstoque` | `sem_estoque` | Gera pedido de reposição |
| `EmailMarketing` | `promocao` | Registra oferta para newsletter |
| `LogSistema` | todos | Registra entrada de auditoria |

---

## ▶️ Como Executar

**Pré-requisito:** .NET 8 SDK ([download](https://dotnet.microsoft.com/download))

```bash
# Clone o repositório
git clone https://github.com/seu-usuario/observer-pattern-csharp.git
cd observer-pattern-csharp

# Execute
dotnet run
```

### Saída esperada (trecho)

```
════════════════════════════════════════════════════════
   PADRÃO OBSERVER — Sistema de Notificações da Loja
════════════════════════════════════════════════════════

─── Configurando inscrições ───────────────────────────
[Loja] ClienteEmail inscrito no evento 'novo_produto'.
[Loja] SistemaEstoque inscrito no evento 'sem_estoque'.
...

[Loja:TechShop Online] Disparando evento 'novo_produto'...
  📧 [E-mail → ana@email.com] Olá, Ana Lima! Novo produto disponível: Notebook Ultra por R$ 3499,90
  📧 [E-mail → sofia@email.com] Olá, Sofia Ramos! Novo produto disponível: Notebook Ultra por R$ 3499,90
  📋 [Log] [10:35:22] Evento='novo_produto' | Dados=Notebook Ultra | R$ 3499,90 | ...

[Loja:TechShop Online] Disparando evento 'sem_estoque'...
  📦 [Estoque] Alerta: 'Notebook Ultra' esgotado! Pedido de reposição nº 1 gerado.
  📋 [Log] [10:35:22] Evento='sem_estoque' | Dados=Notebook Ultra | R$ 2799,92 | ...
...
```

---

## 🧠 Por que Observer?

### Problema sem o padrão
Sem Observer, a `Loja` precisaria conhecer e chamar diretamente cada serviço:

```csharp
// ❌ Sem Observer — alto acoplamento
public void CadastrarProduto(Produto p) {
    clienteAna.EnviarEmail(p);        // Loja conhece cada cliente
    clienteCarlos.EnviarEmail(p);     // Adicionar um novo cliente = alterar Loja
    sistemaEstoque.Registrar(p);      // Violação do Princípio Aberto/Fechado
    logSistema.Registrar(p);
}
```

### Solução com Observer
```csharp
// ✅ Com Observer — baixo acoplamento
public void CadastrarProduto(Produto p) {
    Notify(Eventos.NovoProduto, p);   // Loja não sabe quem está ouvindo
}
// Novos observers: basta chamar Subscribe() — sem alterar a Loja
```

### Princípios SOLID aplicados
- **OCP** (Aberto/Fechado): novos observers sem modificar a `Loja`
- **SRP** (Responsabilidade Única): cada observer tem sua única responsabilidade
- **DIP** (Inversão de Dependência): `Loja` depende de `IObserver`, não de classes concretas

---

## 📚 Referências

- GAMMA, E. et al. **Design Patterns: Elements of Reusable Object-Oriented Software**. Addison-Wesley, 1994.
- REFACTORING.GURU. **Padrão Observer**. Disponível em: https://refactoring.guru/pt-br/design-patterns/observer
- MICROSOFT. **Documentação C# / .NET 8**. Disponível em: https://learn.microsoft.com/dotnet/csharp/

---

*Implementado como atividade prática da disciplina de Padrões de Projeto.*
