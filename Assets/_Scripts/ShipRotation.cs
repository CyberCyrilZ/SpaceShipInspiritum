using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

public class ShipRotation : MonoBehaviour
{
    private ShipController _shipController;

    [Inject]
    private void Construct(ShipController shipController)
    {
        _shipController = shipController;
    }

    private void Start()
    {
        this.UpdateAsObservable().Subscribe(x => { transform.position = _shipController.shipPosition; });

        this.UpdateAsObservable().Where(_ => Input.GetMouseButton(0) || _shipController.move).Subscribe(x =>
        {
            var direction = Quaternion.LookRotation(_shipController.newPosition - transform.position, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, Time.deltaTime * 3);
        });
    }
}