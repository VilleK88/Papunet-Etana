using UnityEngine;
public class GroundCollision : MonoBehaviour
{
    [SerializeField] AudioClip rockandStrawberryBounceSound;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Rock") || collision.gameObject.CompareTag("Strawberry"))
            SoundManager.Instance.PlaySound(rockandStrawberryBounceSound);
    }
}