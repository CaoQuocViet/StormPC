namespace StormPC.Contracts;

public interface INavigationAware
{
    /// <summary>
    /// Được gọi khi điều hướng đến trang
    /// </summary>
    void OnNavigatedTo(object parameter);

    /// <summary>
    /// Được gọi khi rời khỏi trang
    /// </summary>
    void OnNavigatedFrom();
}
