using UnityEngine;
public class ScrollingBackground : MonoBehaviour
{
    Vector2 startPos;
    float repeatWidth;
    [SerializeField] GameObject sprite1;
    [SerializeField] GameObject sprite2;
    private void Start()
    {
        startPos = transform.position;
        repeatWidth = sprite1.GetComponent<BoxCollider2D>().size.x / 2 + sprite2.GetComponent<BoxCollider2D>().size.x / 2;
    }
    private void Update()
    {
        if (transform.position.x < startPos.x - repeatWidth)
            transform.position = startPos;
    }
}