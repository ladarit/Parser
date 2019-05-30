using System;
using System.Threading;
using System.Threading.Tasks;

namespace GovernmentParse.Helpers
{
    public sealed class ProgressReporter
    {
        /// <summary> 
        /// планировщик задач для контекста синхорнизации UI
        /// </summary> 
        private readonly TaskScheduler _scheduler;

        public ProgressReporter()
        {
            _scheduler = TaskScheduler.FromCurrentSynchronizationContext();
        }

        public TaskScheduler Scheduler => _scheduler;

        /// <summary> 
        /// Запускает Task асинхронно в UI потоке. Метод вызывается из Task.
        /// </summary> 
        /// <param name="action"></param> 
        /// <returns></returns> 
        public Task ReportProgressAsync(Action action)
        {
            return Task.Factory.StartNew(action, CancellationToken.None, TaskCreationOptions.None, _scheduler);
        }

        /// <summary> 
        /// Запускает Task асинхронно в UI потоке, при єтом останавливая поток, из которого вызван. Метод вызывается из Task 
        /// </summary> 
        /// <param name="action"></param> 
        public void ReportProgress(Action action)
        {
            ReportProgressAsync(action).Wait();
        }

        /// <summary> 
        /// метод в контексте UI потока продолжает задачу после её завершения (независимо от результата). 
        /// </summary> 
        /// <param name="task">Задача, которую нужно продолжить</param> 
        /// <param name="action">действие, выполняемое в контексте потока UI</param> 
        /// <returns>Возвращаемое значение игнорируется вызывающим методом</returns> 
        public Task RegisterContinuation(Task task, Action action)
        {
            return task.ContinueWith(_ => action(), CancellationToken.None, TaskContinuationOptions.None, _scheduler);
        }

        /// <summary> 
        /// метод в контексте UI потока  продолжает задачу после её завершения (независимо от результата). 
        /// </summary> 
        /// <typeparam name="TResult">тип возвращаемого результата</typeparam> 
        /// <param name="task">Задача, которую нужно продолжить</param> 
        /// <param name="action">действие, выполняемое в контексте потока UI</param> 
        /// <returns>Возвращаемое значение игнорируется вызывающим методом</returns> 
        public Task RegisterContinuation<TResult>(Task<TResult> task, Action action)
        {
            return task.ContinueWith(_ => action(), CancellationToken.None, TaskContinuationOptions.None, _scheduler);
        }

        /// <summary> 
        /// метод в контексте UI потока продолжает задачу после её успешного завершения
        /// </summary> 
        /// <param name="task">Задача, которую нужно продолжить</param> 
        /// <param name="action">действие, выполняемое в контексте потока UI</param> 
        /// <returns>Возвращаемое значение игнорируется вызывающим методом</returns> 
        public Task RegisterSucceededHandler(Task task, Action action)
        {
            return task.ContinueWith(_ => action(), CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, _scheduler);
        }

        /// <summary> 
        /// метод в контексте UI потока продолжает задачу после её успешного завершения
        /// </summary> 
        /// <typeparam name="TResult">тип возвращаемого результата</typeparam> 
        /// <param name="task">Задача, которую нужно продолжить</param> 
        /// <param name="action">действие, выполняемое в контексте потока UI</param> 
        /// <returns>Возвращаемое значение игнорируется вызывающим методом</returns> 
        public Task RegisterSucceededHandler<TResult>(Task<TResult> task, Action<TResult> action)
        {
            return task.ContinueWith(t => action(t.Result), CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, Scheduler);
        }

        /// <summary> 
        /// метод в контексте UI потока продолжает задачу после её неудачного завершения
        /// </summary> 
        /// <param name="task">Задача, которую нужно продолжить</param> 
        /// <param name="action">действие, выполняемое в контексте потока UI</param> 
        /// <returns>Возвращаемое значение игнорируется вызывающим методом</returns> 
        public Task RegisterFaultedHandler(Task task, Action<Exception> action)
        {
            return task.ContinueWith(t => action(t.Exception), CancellationToken.None, TaskContinuationOptions.OnlyOnFaulted, Scheduler);
        }

        /// <summary> 
        /// метод в контексте UI потока продолжает задачу после её неудачного завершения
        /// </summary> 
        /// <typeparam name="TResult">тип возвращаемого результата</typeparam> 
        /// <param name="task">Задача, которую нужно продолжить</param> 
        /// <param name="action">действие, выполняемое в контексте потока UI</param> 
        /// <returns>Возвращаемое значение игнорируется вызывающим методом</returns> 
        public Task RegisterFaultedHandler<TResult>(Task<TResult> task, Action<Exception> action)
        {
            return task.ContinueWith(t => action(t.Exception), CancellationToken.None, TaskContinuationOptions.OnlyOnFaulted, Scheduler);
        }

        /// <summary> 
        /// метод в контексте UI потока продолжает задачу в случае её отмены
        /// </summary> 
        /// <param name="task">Задача, которую нужно продолжить</param> 
        /// <param name="action">действие, выполняемое в контексте потока UI</param> 
        /// <returns>Возвращаемое значение игнорируется вызывающим методом</returns> 
        public Task RegisterCancelledHandler(Task task, Action action)
        {
            return task.ContinueWith(_ => action(), CancellationToken.None, TaskContinuationOptions.OnlyOnCanceled, Scheduler);
        }

        /// <summary> 
        /// метод в контексте UI потока продолжает задачу в случае её отмены
        /// </summary> 
        /// <typeparam name="TResult">тип возвращаемого результата</typeparam> 
        /// <param name="task">Задача, которую нужно продолжить</param> 
        /// <param name="action">действие, выполняемое в контексте потока UI</param> 
        /// <returns>Возвращаемое значение игнорируется вызывающим методом</returns> 
        public Task RegisterCancelledHandler<TResult>(Task<TResult> task, Action action)
        {
            return task.ContinueWith(_ => action(), CancellationToken.None, TaskContinuationOptions.OnlyOnCanceled, Scheduler);
        }
    }
}
