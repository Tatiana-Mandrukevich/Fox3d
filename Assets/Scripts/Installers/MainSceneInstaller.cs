using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainSceneInstaller : MonoInstaller
{
    public PickUpMechanics PickUpMechanics;
    public InputSystem InputSystem;
    
    public override void InstallBindings()
    {
        Container.Bind<PickUpMechanics>().FromInstance(PickUpMechanics).AsSingle();
    }
}
