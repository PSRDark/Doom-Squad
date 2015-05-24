using UnityEngine;
using System.Collections;

public class StupidEnemy : Enemy {

    public override void Die()
    {
        Destroy(gameObject);
    }

	protected override IEnumerator patrol()
    {
        while (!playerFound)
        {
            if(Physics2D.Raycast(transform.position, direction, distanceToFlip, layersRaycast))
            {
                Flip();
            }

            Erigidbody.velocity = new Vector2(direction.x * speed, Erigidbody.velocity.y);
            yield return null;
        }
    }

    protected override IEnumerator hunting()
    {
        while (playerFound)
        {
            if (Player.transform.position.x - transform.position.x <= 0)
            {
                direction = Vector3.left;
            }
            else direction = Vector3.right;

            if (Physics2D.Raycast(transform.position, direction, distanceToFlip, layersRaycast))
            {
                Erigidbody.velocity = new Vector2(Erigidbody.velocity.x, jumpForce);
            }

            Erigidbody.velocity = new Vector2(direction.x * speed, Erigidbody.velocity.y);
            yield return null;
        }
    }
}
