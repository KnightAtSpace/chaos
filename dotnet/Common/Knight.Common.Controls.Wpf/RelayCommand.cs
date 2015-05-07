using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Knight.Common.Controls.Wpf
{
    /// <summary>
    /// A command whose sole purpose is to 
    /// relay its functionality to other
    /// objects by invoking delegates. The
    /// default return value for the CanExecute
    /// method is 'true'.
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action<object> execute;
        private readonly Predicate<object> canExecute;

        /// <summary>
        /// Creates a new command that can always execute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public RelayCommand(Action<object> execute) : this(execute, null) { }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="executeAction">The execution logic.</param>
        /// <param name="canExecutePredicate">The execution status logic.</param>
        public RelayCommand(Action<object> executeAction, Predicate<object> canExecutePredicate)
        {
            if (executeAction == null)
                throw new ArgumentNullException("execute");

            this.execute = executeAction;
            this.canExecute = canExecutePredicate;
        }

        /// <summary>
        /// Ckecks if execution is possible.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return this.canExecute == null ? true : this.canExecute(parameter);
        }

        /// <summary>
        /// Event for CanExecute changed.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Execute.
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}