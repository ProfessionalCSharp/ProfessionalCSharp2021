using CommunityToolkit.Mvvm.Messaging.Messages;

namespace BooksLib.Events;

public class NavigationInfo
{
    public bool UseNavigation { get; set; }
}
public class NavigationMessage(NavigationInfo navigationInfo) : 
    ValueChangedMessage<NavigationInfo>(navigationInfo)
{
}
