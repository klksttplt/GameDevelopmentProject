using Infrastructure.Services;
using StaticData.Windows;
using UI.Services.Windows;

namespace StaticData
{
    public interface IStaticDataService : IService
    {
        LevelStaticData ForLevel(string sceneKey);
        WindowConfig ForWindow(WindowId windowId);
        void LoadData();
    }
}