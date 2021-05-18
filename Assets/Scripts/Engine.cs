using Characters.Player;
using Components;
using UnityEngine;

public class Engine : MonoBehaviour
{
    private static Engine _e;
    public static Engine e => _e;

    public Player player;

    public float maxFallSpeed = -50.0f;

    [SerializeField] private InputWrapper inputWrapper;
    public InputWrapper input => inputWrapper;

    private void Awake() {
        if (_e != null && _e != this)
            Destroy (this.gameObject);
        else
        {
            _e = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
