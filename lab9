namespace lab7;

public interface IMemento
{
    string GetWord();
}

public class Memento : IMemento
{
    private string _word;

    public Memento(string word)
    {
        _word = word;
    }

    public string GetWord()
    {
        return _word;
    }
}

public class Originator
{
    private static Random _random;
    private string _word;

    public Originator(string word, Random random)
    {
        _word = word;
        _random = random;
    }

    public void ContinueConversation()
    {
        var words = new List<string>() { "Hello", "Hi", "Bye Bye" };

        var index = _random.Next(words.Count);
        
        Console.WriteLine(words[index]);
    }
    
    public IMemento Save()
    {
        return new Memento(_word);
    }
    
    public void Restore(IMemento memento)
    {
        _word = memento.GetWord();
        
        Console.Write(_word);
    }
}

class Caretaker
{
    private ICollection<IMemento> _mementos;
    private Originator _originator;

    public Caretaker(Originator originator, ICollection<IMemento> mementos)
    {
        _originator = originator;
        _mementos = mementos;
    }
    
    public void Backup()
    {
        _mementos.Add(_originator.Save());
    }

    public void Undo()
    {
        var memento = _mementos.Last();
        
        _mementos.Remove(memento);
        
        _originator.Restore(memento);
    }
    
    public void ShowHistory()
    {
        foreach (var memento in _mementos)
        {
            Console.WriteLine(memento.GetWord());
        }
    }
}

internal class Program
{
    static void Main(string[] args)
    {
        Originator originator = new Originator("Wassup", new Random());
        Caretaker caretaker = new Caretaker(originator, new List<IMemento>());

        caretaker.Backup();
        originator.ContinueConversation();

        caretaker.Backup();
        originator.ContinueConversation();

        caretaker.Backup();
        originator.ContinueConversation();

        caretaker.ShowHistory();

        caretaker.Undo();

        caretaker.Undo();

        caretaker.ShowHistory();
    }
}
