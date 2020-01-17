using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class ExplosionsController : MonoBehaviour
{
    private Explosion.Factory _explosionFactory;
    private float _explosionRate = 5f;
    private GameManager _gameManager;
    private DateTimeOffset _lastExplosion;

    [Inject]
    private void Construct(Explosion.Factory explosion, GameManager gameManager)
    {
        _explosionFactory = explosion;
        _gameManager = gameManager;
    }

    private void Start()
    {
        this.UpdateAsObservable().Where(_ => !_gameManager.gameOver).Timestamp()
            .Where(x => x.Timestamp > _lastExplosion.AddSeconds(_explosionRate))
            .Skip(TimeSpan.FromSeconds(_explosionRate))
            .Subscribe(
                x =>
                {
                    var xPos = Random.Range(-15f, 15f);
                    var zPos = Random.Range(-30f, 30f);
                    var randomPosition = new Vector3(xPos, 0, zPos);
                    var explosion = _explosionFactory.Create();
                    explosion.transform.position = randomPosition;
                    explosion.transform.SetParent(transform);
                    _explosionRate *= .9f;
                    _lastExplosion = x.Timestamp;
                });
    }
}