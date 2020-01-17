using UnityEngine;
using Zenject;

public class ShipLose : MonoBehaviour
{
    private GameManager _gameManager;

    [Inject]
    private void Construct(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("explosion"))
        {
            _gameManager.gameOver = true;
            Destroy(gameObject);
        }
    }
}