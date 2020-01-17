using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

public class StopwatchScore : MonoBehaviour
{
    private GameManager _gameManager;
    public TextMeshProUGUI textBox;
    public float timeStart;

    [Inject]
    private void Construct(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    private void Start()
    {
        textBox.text = "TIME " + timeStart.ToString("F0");
        this.UpdateAsObservable().Where(_ => !_gameManager.gameOver).Subscribe(x =>
        {
            timeStart += Time.deltaTime;
            textBox.text = "TIME " + timeStart.ToString("F0");
        });
    }
}