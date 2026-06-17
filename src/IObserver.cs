namespace ObserverPattern;


// Interface do Observador (Observer).
// Todo assinante que quiser receber notificações deve implementar esta interface.

public interface IObserver
{
    
    //Método chamado automaticamente pelo Subject quando um evento ocorre.
    // </summary>
    // <param name="eventType">Tipo/nome do evento disparado.</param>
    // <param name="data">Dados adicionais sobre o evento.</param>
    void Update(string eventType, object data);
}
