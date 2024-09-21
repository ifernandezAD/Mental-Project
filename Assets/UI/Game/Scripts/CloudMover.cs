using UnityEngine;

public class CloudMover : MonoBehaviour
{
    [SerializeField] float speed = 50f;
    [SerializeField] bool moveLeft = true;  
    [SerializeField] float leftResetPositionX = -400f;  
    [SerializeField] float rightResetPositionX = 400f;  
    [SerializeField] float startPositionX = 0f;   

    private Vector3 startPosition;

    void Start()
    {
        startPosition = new Vector3(startPositionX, transform.position.y, transform.position.z);
    }

    void Update()
    {
        float moveDirection = moveLeft ? -1 : 1;

        transform.Translate(Vector3.right * moveDirection * speed * Time.deltaTime);

        if (moveLeft && transform.position.x <= leftResetPositionX)
        {
            transform.position = startPosition;
        }
        else if (!moveLeft && transform.position.x >= rightResetPositionX)
        {
            transform.position = startPosition;
        }
    }
}
