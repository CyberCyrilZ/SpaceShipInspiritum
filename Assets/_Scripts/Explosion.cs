using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

public class Explosion : MonoBehaviour
{
    private Data _data;

    [Inject]
    private void Construct(Data data)
    {
        _data = data;
    }

    private void Start()
    {
        var cd = GetComponent<Collider>();
        var rd = GetComponent<Renderer>();
        Destroy(gameObject, 3);
        this.UpdateAsObservable().TakeUntilDestroy(this).Subscribe(x =>
        {
            transform.localScale += Vector3.one * _data.settings.ShipSpeed * 1.5f * Time.deltaTime;
        });
        Observable.Timer(TimeSpan.FromSeconds(1.5f)).TakeUntilDestroy(this).Subscribe(x =>
        {
            cd.enabled = true;
            rd.material.color = Color.red;
        });
    }

    public class Factory : PlaceholderFactory<Explosion>
    {
    }
}