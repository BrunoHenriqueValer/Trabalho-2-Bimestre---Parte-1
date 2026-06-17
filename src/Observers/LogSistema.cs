namespace ObserverPattern;

public class LogSistema : IObserver
{
    private readonly List<string> _log = new();

    public void Update(string eventType, object data)
    {
        var entrada = $"[{DateTime.Now:HH:mm:ss}] Evento='{eventType}' | Dados={data}";
        _log.Add(entrada);
        Console.WriteLine($"  📋 [Log] {entrada}");
    }

    public void ExibirLog()
    {
        Console.WriteLine("\n[Log do Sistema] Histórico completo de eventos:");
        foreach (var linha in _log)
            Console.WriteLine($"  {linha}");
    }
}
