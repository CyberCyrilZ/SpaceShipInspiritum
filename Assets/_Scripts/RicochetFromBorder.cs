using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

public class RicochetFromBorder : MonoBehaviour
{
    private ShipController _shipController;
    [SerializeField] private LayerMask collisionMask;

    [Inject]
    private void Construct(ShipController shipController)
    {
        _shipController = shipController;
    }

    private void Start()
    {
        this.UpdateAsObservable().Subscribe(x =>
        {
            var ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Time.deltaTime * 5 + .1f, collisionMask))
            {
                IncreaseSpeedTimer();
                var reflectdir = Vector3.Reflect(ray.direction, hit.normal);
                var rot = 90 - Mathf.Atan2(reflectdir.z, reflectdir.x) * Mathf.Rad2Deg;
                transform.eulerAngles = new Vector3(0, rot, 0);
            }
        });
    }

    private void IncreaseSpeedTimer()
    {
        _shipController.speed *= 2;
        Observable.Timer(TimeSpan.FromSeconds(.3f)).Subscribe(x => { SetSpeed(); });
    }

    private void SetSpeed()
    {
        if (Input.GetMouseButton(0))
            _shipController.speed /= 2;
        else
            _shipController.SetSpeedFromData();
    }
}