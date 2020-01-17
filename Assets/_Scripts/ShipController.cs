using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

public class ShipController : MonoBehaviour
{
    private Data _data;
    [NonSerialized] public bool move;
    [NonSerialized] public Vector3 newPosition;
    [NonSerialized] public Vector3 shipPosition;
    [NonSerialized] public float speed;

    [Inject]
    private void Construct(Data data)
    {
        _data = data;
    }

    private void Start()
    {
        SetSpeedFromData();
        this.UpdateAsObservable().Where(_ => Input.GetMouseButtonDown(0) && !move).Subscribe(x =>
        {
            speed *= 2;
            move = true;
            DelayClick();
        });

        this.UpdateAsObservable().Where(_ => Input.GetMouseButtonUp(0)).Subscribe(x => { SetSpeedFromData(); });

        this.UpdateAsObservable().Where(_ => Input.GetMouseButton(0)).Subscribe(x =>
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit)) newPosition = hit.point;
        });
    }

    public void SetSpeedFromData()
    {
        speed = _data.settings.ShipSpeed;
    }

    private void DelayClick()
    {
        Observable.Timer(TimeSpan.FromSeconds(.5f)).Subscribe(x => move = false);
    }
}