namespace StormPC.Contracts;

public interface IActivationService
{
    Task ActivateAsync(object activationArgs);
}
