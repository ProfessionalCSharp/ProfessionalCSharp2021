using System;
using System.ComponentModel;

namespace GenericViewModels.ViewModels
{
    public abstract class ItemViewModel<T> : ViewModelBase, IItemViewModel<T>
    {
        private T? _item;
        public virtual T? Item
        {
            get => _item;
            set => SetProperty(ref _item, value);
        }
    }
}
