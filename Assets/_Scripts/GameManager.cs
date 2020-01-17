using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [NonSerialized] public bool gameOver;
    [SerializeField] private GameObject gameOverPopUp;

    private void Start()
    {
        this.UpdateAsObservable().Where(_ => gameOver).Subscribe(x => { gameOverPopUp.SetActive(true); });
    }
}