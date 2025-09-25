using UnityEngine;
using UnityEngine.UI;

public class UltimateRenderer : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Image _image;

    private void Start()
    {
        Hide();
    }

    private void OnEnable()
    {
        _player.UltimateUsed += Show;
        _player.UltimateReload += Hide;
    }

    private void OnDisable()
    {
        _player.UltimateUsed -= Show;
        _player.UltimateReload -= Hide;
    }

    private void Show()
    {
        _image.gameObject.SetActive(true);
    }

    private void Hide()
    {
        _image.gameObject.SetActive(false);
    }
}
