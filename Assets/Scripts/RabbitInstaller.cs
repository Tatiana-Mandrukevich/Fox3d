using Zenject;

public class RabbitInstaller : MonoInstaller
{
    public RabbitConfig config;
    
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<Health>().FromNew().AsSingle();
        Container.BindInterfacesAndSelfTo<PickUpAndEatStrategy>().FromNew().AsSingle();
        Container.Bind<RabbitConfig>().FromScriptableObject(config).AsSingle();
    }
}