using UnityEngine;

public class Core : MonoBehaviour
{
    // Add your managers or global dependecies here.
    // example:
    // public GameManager gameManager;
    // public PlayerController playerController;

    public static Core Singleton { get; private set; }

    private void Awake()
    {
        if (Singleton != null && Singleton != this)
        {
            gameObject.SetActive(false);
            return;
        }

        Singleton = this;
    }
}
