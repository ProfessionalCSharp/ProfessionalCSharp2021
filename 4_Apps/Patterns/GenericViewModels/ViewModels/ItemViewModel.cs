namespace GenericViewModels.ViewModels;

public abstract class ItemViewModel<T>(T? item) : ViewModelBase, IItemViewModel<T>
{
    private T? _item = item;
    public virtual T? Item
    {
        get => _item;
        set => SetProperty(ref _item, value);
    }
}
