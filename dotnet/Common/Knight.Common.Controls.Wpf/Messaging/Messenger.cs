using System;
using System.Collections.Generic;

namespace Knight.Common.Controls.Wpf.Messaging
{
    /// <summary>
    /// Provides a custom messenger base on a <see cref="GalaSoft.MvvmLight.Messaging.Messenger"/> for MVVM purposes with WPF.
    /// </summary>
    public class Messenger
    {
        private readonly GalaSoft.MvvmLight.Messaging.Messenger messengerInstance;

        private static readonly Messenger defaultMessanger = new Messenger();

        private readonly Dictionary<object, Action> parameterlessActions = new Dictionary<object, Action>();

        private Messenger()
        {
            messengerInstance = new GalaSoft.MvvmLight.Messaging.Messenger();
        }

        /// <summary>
        /// Gets the default.
        /// </summary>
        /// <value>
        /// The default.
        /// </value>
        public static Messenger Default { get { return defaultMessanger; } }

        /// <summary>
        /// Registers the specified token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="action">The action.</param>
        public void Register(object token, Action action)
        {
            if (!this.parameterlessActions.ContainsKey(token))
            {
                this.parameterlessActions.Add(token, action);
            }
        }

        /// <summary>
        /// Registers the specified receipient.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="receipient">The receipient.</param>
        /// <param name="token">The token.</param>
        /// <param name="action">The action.</param>
        public void Register<T>(object receipient, object token, Action<T> action)
        {
            this.messengerInstance.Register<T>(receipient, token, action);
        }

        /// <summary>
        /// Unregisters the specified token.
        /// </summary>
        /// <param name="token">The token.</param>
        public void Unregister(object token)
        {
            if (this.parameterlessActions.ContainsKey(token))
            {
                this.parameterlessActions.Remove(token);
            }
        }

        /// <summary>
        /// Unregisters the specified receipient.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="receipient">The receipient.</param>
        /// <param name="token">The token.</param>
        public void Unregister<T>(object receipient, object token)
        {
            this.messengerInstance.Unregister<T>(receipient, token);
        }

        /// <summary>
        /// Sends the specified token.
        /// </summary>
        /// <param name="token">The token.</param>
        public void Send(object token)
        {
            if (this.parameterlessActions.ContainsKey(token))
            {
                this.parameterlessActions[token].Invoke();
            }
        }

        /// <summary>
        /// Sends the specified token.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="token">The token.</param>
        /// <param name="message">The message.</param>
        public void Send<T>(object token, T message)
        {
            this.messengerInstance.Send<T>(message, token);
        }

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        public void Cleanup()
        {
            this.parameterlessActions.Clear();
            this.messengerInstance.Cleanup();
        }
    }
}
