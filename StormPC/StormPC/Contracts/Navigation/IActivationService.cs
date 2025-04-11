namespace StormPC.Contracts;

public interface IActivationService
{
    /// <summary>
    /// Kích hoạt ứng dụng với đối số kích hoạt
    /// </summary>
    Task ActivateAsync(object activationArgs);
}
