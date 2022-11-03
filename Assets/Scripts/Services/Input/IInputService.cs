using System.Numerics;
using Infrastructure.Services;

namespace Services.Input
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }
        bool IsJumpButtonDown();
        bool IsAttackButtonUp();
    }
}
