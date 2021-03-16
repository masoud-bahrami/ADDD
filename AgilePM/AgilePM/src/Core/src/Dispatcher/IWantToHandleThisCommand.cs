using System.Threading.Tasks;

namespace AgilePM.Core.Dispatcher
{
    public interface IWantToHandleThisCommand<in TCommand> where TCommand : ICommand
    {
        Task Handle(TCommand command);
    }
}