using Infrastructure.Services;

namespace UI.Services.Factory
{
    public interface IUIFactory : IService
    {
        void CreateSettings();
        void CreateUIRoot();
    }
}