using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "DataInstaller", menuName = "Installers/DataInstaller")]
public class DataInstaller : ScriptableObjectInstaller<DataInstaller>
{
    public Data.Settings Data;

    public override void InstallBindings()
    {
        Container.BindInstance(Data);
    }
}