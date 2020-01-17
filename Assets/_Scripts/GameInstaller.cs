using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private ShipController _ship;
    [SerializeField] private Explosion explosion;
    [SerializeField] private GameManager gameManager;

    public override void InstallBindings()
    {
        Container.BindInstance(_ship);
        Container.BindInstance(gameManager);
        Container.Bind<Data>().AsSingle();
        Container.BindFactory<Explosion, Explosion.Factory>().FromComponentInNewPrefab(explosion);
    }
}