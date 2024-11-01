using UnityEngine;
public class ScrollingBackground : MonoBehaviour
{
    Vector2 startPos;
    float repeatWidth;
    [SerializeField] GameObject sprite1;
    [SerializeField] GameObject sprite2;
    float repeatDistance;
    float angle = 14f;
    Vector2 movementDirection;
    public bool horizontal = true;
    private void Start()
    {
        startPos = transform.position;
        if (horizontal) repeatWidth = sprite1.GetComponent<BoxCollider2D>().size.x / 2 + sprite2.GetComponent<BoxCollider2D>().size.x / 2;
        else
        {
            float width1 = sprite1.GetComponent<BoxCollider2D>().size.x;
            float width2 = sprite2.GetComponent<BoxCollider2D>().size.x;
            float averageWidth = (width1 + width2) / 2;
            float angleInRadians = angle * Mathf.Deg2Rad;
            movementDirection = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));
            repeatWidth = averageWidth;
        }
    }
    private void Update()
    {
        if (horizontal) HorizontalRepeat();
        else RepeatWithAngle();
    }
    void HorizontalRepeat()
    {
        if (transform.position.x < startPos.x - repeatWidth) transform.position = startPos;
    }
    void RepeatWithAngle()
    {
        float distance = Vector2.Distance(startPos, transform.position);
        if (distance >= repeatWidth) transform.position = startPos;
    }
}