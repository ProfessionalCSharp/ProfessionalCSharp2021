namespace GenericViewModels.ViewModels;

public interface IItemViewModel<out T>
{
    T? Item { get; }
}
