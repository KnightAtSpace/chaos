using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Knight.Common.Collections
{
    /// <summary>
    /// A serializable observable collection.
    /// </summary>
    /// <typeparam name="T"><see cref="ISerializable"/></typeparam>
    public class ObservableSingeltonSerializable<T> : ObservableCollection<T> where T : ISerializable
    {
        private static ObservableCollection<T> instance;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static ObservableCollection<T> Instance
        {
            get
            {
                if (instance != null) return instance;
                return instance = new ObservableCollection<T>();
            }
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="ObservableSingeltonSerializable{T}"/> class from being created.
        /// </summary>
        private ObservableSingeltonSerializable() : base() { }
    }
}
