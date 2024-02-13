using System.Windows.Input;

namespace ooap7
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
    public class CutCommand : ICommand
    {
        public string Text { get; set; }
        public NoteBook Notebook { get; set; }
        public void Execute()
        {
            Text = Notebook.Text;
            Notebook.Text = string.Empty;
        }

        public void Undo()
        {
            Notebook.Text = Text;
        }
    }

    public class CopyCommand : ICommand
    {
        public string Text { get; set; }
        public NoteBook Notebook { get; set; }
        public void Execute()
        {
            Text = Notebook.Text;
        }

        public void Undo()
        {
            Text = string.Empty;
        }
    }

    public class PasteCommand : ICommand
    {
        public string Text { get; set; }
        public NoteBook Notebook { get; set; }
        public void Execute()
        {
            Notebook.Text += Text;
        }

        public void Undo()
        {
            Notebook.Text = string.Empty;
        }
    }

    public class ItalicCommand : ICommand
    {
        public string Text { get; set; }
        public NoteBook Notebook { get; set; }
        public void Execute()
        {
            Notebook.Font = "Italic";
        }

        public void Undo()
        {
            Notebook.Text = "Arial";
        }
    }

    public class NoteBook
    {
        public string Text { get; set;}
        public string Font { get; } = "Arial";
    }

    public class CommandInvoker
    {
        public List<ICommand> Commands { get; set; }
        public CommandIncoker()
        {
            Commands = new List<ICommand>();
        }

        public void ExecuteCommand(ICommand command)
        {
            Commands.Add(command);
            command.Execute();
        }

        public void Undo()
        {
            var lastCommand = Commands[Commands.Count - 1];
            lastCommand.Undo();
            Commands.Remove(lastCommand);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            CommandInvoker invoker = new CommandInvoker();

            NoteBook note = new NoteBook() { Text = "my text"};

            invoker.Commands.Add(new CutCommand() { Notebook = note });
        }
    }
}
