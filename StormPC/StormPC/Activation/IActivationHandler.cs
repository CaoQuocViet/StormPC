namespace StormPC.Activation;

public interface IActivationHandler
{
    /// <summary>
    /// Kiểm tra xem handler có thể xử lý đối số kích hoạt không
    /// </summary>
    /// <param name="args">Đối số kích hoạt</param>
    /// <returns>True nếu có thể xử lý, ngược lại là False</returns>
    bool CanHandle(object args);

    /// <summary>
    /// Xử lý sự kiện kích hoạt
    /// </summary>
    /// <param name="args">Đối số kích hoạt</param>
    Task HandleAsync(object args);
}
