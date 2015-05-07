using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using log4net;

namespace Knight.Common.Controls.Wpf.ViewModel
{
    /// <summary>
    /// Base class for all ViewModel classes in the application.
    /// It provides support for property change notifications 
    /// and has a DisplayName property.  This class is abstract.
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ViewModelBase));

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelBase"/> class.
        /// </summary>
        protected ViewModelBase() { }

        /// <summary>
        /// Warns the developer if this object does not have
        /// a public property with the specified name. This 
        /// method does not exist in a Release build.
        /// </summary>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            // Verify that the property name matches a real,  
            // public, instance property on this object.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = "Invalid property name: ." + propertyName;

                if (this.ThrowOnInvalidPropertyName)
                    throw new Exception(msg);
                else
                    Logger.Debug(msg);
            }
        }

        /// <summary>
        /// Returns whether an exception is thrown, or if a Debug.Fail() is used
        /// when an invalid property name is passed to the VerifyPropertyName method.
        /// The default value is false, but subclasses used by unit tests might 
        /// override this property's getter to return true.
        /// </summary>
        protected virtual bool ThrowOnInvalidPropertyName { get; private set; }

        /// <summary>
        /// Raised when a property on this object has a new value.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The property that has a new value.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.VerifyPropertyName(propertyName);

            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        /// <summary>
        /// Raises object's PropertyChanged event on each public property of this that is not readonly.
        /// </summary>
        protected virtual void OnAllPublicPropertiesChanged()
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this);
            foreach (PropertyDescriptor property in properties)
            {
                if (property.PropertyType.IsPublic && !property.IsReadOnly)
                {
                    this.OnPropertyChanged(property.Name);
                }
            }
        }

        /// <summary>
        /// Sets the given field to the given value and raises a property changed event on the calling property.
        /// Usage: SetProperty(ref 'localFieldName', value);.
        /// </summary>
        /// <typeparam name="T">Implicit field type.</typeparam>
        /// <param name="field">A local field.</param>
        /// <param name="value">The value to set</param>
        /// <param name="name">Optionally property name. If not given, Compiler service for <see cref="CallerMemberName"/> is used to provide the calling property name.</param>
        protected void SetProperty<T>(ref T field, T value, [CallerMemberName] string name = "")
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                PropertyChangedEventHandler handler = this.PropertyChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(name));
                }
            }
        }

        /// <summary>
        /// Invoked when this object is being removed from the application
        /// and will be subject to garbage collection.
        /// </summary>
        public void Dispose()
        {
            this.OnDispose();
        }

        /// <summary>
        /// Child classes can override this method to perform 
        /// clean-up logic, such as removing event handlers.
        /// </summary>
        protected virtual void OnDispose()
        {
        }

#if DEBUG
        /// <summary>
        /// Useful for ensuring that ViewModel objects are properly garbage collected.
        /// </summary>
        ~ViewModelBase()
        {
            string msg = string.Format("{0} ({1}) Finalized.", this.GetType().Name, this.GetHashCode());
            Logger.Debug(msg);
        }
#endif
    }
}
