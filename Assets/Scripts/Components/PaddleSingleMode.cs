using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class PaddleSingleMode : Paddle
{
    public override void PrepareGame() 
    {
        transform.position = Vector2.down * m_TableSize.y +
                             Vector2.up * m_Settings.Indent +
                             Vector2.up * m_Settings.Height * 0.5f;
    }

    public override void Setup(PaddleSettings settings, Vector2 tableSize) 
    {
        base.Setup(settings, tableSize);

        SpawnSecondPaddle();
    }

    private void SpawnSecondPaddle()
    {
        var paddleSecond = Instantiate(gameObject, transform);
        Destroy(paddleSecond.GetComponent<Paddle>());
        paddleSecond.transform.localScale = Vector3.one;
        paddleSecond.transform.localPosition = (Vector2.up * m_TableSize.y * 2 +
                                                Vector2.down * m_Settings.Indent * 2 +
                                                Vector2.down * m_Settings.Height) * (1 / m_Settings.Height);
    }
}
