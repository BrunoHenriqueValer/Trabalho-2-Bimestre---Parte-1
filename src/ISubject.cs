namespace ObserverPattern;

public interface ISubject
{
    //observador para receber notificações.</summary>
    void Subscribe(string eventType, IObserver observer);

    //um observador da lista de inscritos.</summary>
    void Unsubscribe(string eventType, IObserver observer);

    //Notifica todos os observadores inscritos em um determinado evento.</summary>
    void Notify(string eventType, object data);
}
