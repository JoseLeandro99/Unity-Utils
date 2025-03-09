using UnityEngine;

public class Billboard : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = true;
    }

    private void Update()
    {
        transform.LookAt(Core.Singleton.gameManager.GetPlayerObject().transform.position, -Vector3.forward);
    }
}
