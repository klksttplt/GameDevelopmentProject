using Infrastructure.Services;
using Logic.Damage;

namespace UI.Services.Factory
{
    public interface IUIFactory : IService
    {
        void CreateSettings();
        void CreateUIRoot();
        void CreateHud(Health playerHealth);
    }
}