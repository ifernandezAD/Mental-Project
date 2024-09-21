using UnityEngine;

public class ContinuousCloudMover : MonoBehaviour
{
    [SerializeField] RectTransform cloud1;  
    [SerializeField] RectTransform cloud2;  
    [SerializeField] float speed = 25f;  
    [SerializeField] bool moveLeft = true;  
    [SerializeField] float overlapOffset = 5f;  

    private float cloudWidth;  

    void Start()
    {
        cloudWidth = cloud1.rect.width;
    }

    void Update()
    {
        float moveDirection = moveLeft ? -1 : 1;

        cloud1.anchoredPosition += new Vector2(moveDirection * speed * Time.deltaTime, 0);
        cloud2.anchoredPosition += new Vector2(moveDirection * speed * Time.deltaTime, 0);

        if (moveLeft && cloud1.anchoredPosition.x <= -cloudWidth)
        {
            cloud1.anchoredPosition = new Vector2(cloud2.anchoredPosition.x + cloudWidth - overlapOffset, cloud1.anchoredPosition.y);
        }
        else if (moveLeft && cloud2.anchoredPosition.x <= -cloudWidth)
        {
            cloud2.anchoredPosition = new Vector2(cloud1.anchoredPosition.x + cloudWidth - overlapOffset, cloud2.anchoredPosition.y);
        }

        if (!moveLeft && cloud1.anchoredPosition.x >= cloudWidth)
        {
            cloud1.anchoredPosition = new Vector2(cloud2.anchoredPosition.x - cloudWidth + overlapOffset, cloud1.anchoredPosition.y);
        }

        else if (!moveLeft && cloud2.anchoredPosition.x >= cloudWidth)
        {
            cloud2.anchoredPosition = new Vector2(cloud1.anchoredPosition.x - cloudWidth + overlapOffset, cloud2.anchoredPosition.y);
        }
    }
}
