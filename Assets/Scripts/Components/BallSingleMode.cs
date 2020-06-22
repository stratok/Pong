using UnityEngine;

public class BallSingleMode : Ball
{
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(PaddleTag))
        {
            IncreaseVelocity();
            EventController.HandleBallСaught();
        }
    }
}