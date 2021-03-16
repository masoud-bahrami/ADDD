namespace AgilePM.Core.Dispatcher
{
    public interface ISemaphore
    {
        void Enter();
        void Exit();
        bool IsThereAnyoneStill();
    }

    public class Semaphore : ISemaphore
    {
        private int _incrementer = 0;
        public void Enter() => Increment();
        public bool IsThereAnyoneStill() => _incrementer > 0;
        public void Exit() => Decrement();
        
        private void Decrement() => _incrementer--;
        private void Increment() => _incrementer++;
    }
}