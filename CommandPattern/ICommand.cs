namespace CommandPattern
{
    public interface ICommand
    {
        void Call();
        void Undo();
    }
}