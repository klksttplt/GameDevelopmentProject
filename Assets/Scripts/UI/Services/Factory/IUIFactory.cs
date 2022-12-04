using Infrastructure.Services;
using Logic.Damage;
using UI.Views;

namespace UI.Services.Factory
{
    public interface IUIFactory : IService
    {
        HUD Hud { get;  }
        void CreateSettings();
        void CreateMenu();
        void CreateUIRoot();
        void CreateHud(Health playerHealth);
    }
}