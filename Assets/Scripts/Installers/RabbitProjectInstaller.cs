using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using Zenject;

public class RabbitProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<InputSystem>().FromNew().AsSingle();
    }
}