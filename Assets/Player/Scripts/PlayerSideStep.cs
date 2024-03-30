using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSideStep : MonoBehaviour
{
    private const float EPSILON = 0.1f;

    [SerializeField] private Transform[] sideStepPositions;
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private AudioClip moveAudioClip;

    private int previousIndex = 0;
    private int currentIndex = 0;

    private void Update()
    {
        Vector2 destination = (Vector2)sideStepPositions[currentIndex].position;

        if (Mathf.Abs(destination.x - transform.position.x) > EPSILON)
        {
            float step = speed * Time.deltaTime;
            float xPosition = Mathf.MoveTowards(transform.position.x, destination.x, step);

            transform.position = new(xPosition, transform.position.y);
        }
        else
        {
            transform.position = new(destination.x, transform.position.y);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Vector2 sideStepDirection = context.ReadValue<Vector2>();

            previousIndex = currentIndex;

            if (sideStepDirection.x > 0)
                currentIndex = 1;
            else if (sideStepDirection.x < 0)
                currentIndex = 0;

            if (previousIndex != currentIndex)
                AudioManager.Instance.PlaySFX(moveAudioClip);
        }
    }
}
