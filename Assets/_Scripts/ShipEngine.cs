using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

public class ShipEngine : MonoBehaviour
{
    private ShipController _shipController;

    [Inject]
    private void Construct(ShipController shipController)
    {
        _shipController = shipController;
    }

    private void Start()
    {
        this.UpdateAsObservable().Subscribe(x =>
        {
            transform.Translate(Vector3.forward * Time.deltaTime * _shipController.speed);
            _shipController.shipPosition = transform.position;
        });

        this.UpdateAsObservable().Where(_ => Input.GetMouseButton(0) || _shipController.move).Subscribe(x =>
        {
            var direction = Quaternion.LookRotation(_shipController.newPosition - transform.position, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, Time.deltaTime);
        });
    }
}