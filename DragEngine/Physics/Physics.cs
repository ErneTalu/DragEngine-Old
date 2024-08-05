using System;
using System.Drawing;

namespace DragEngine
{
    public class Physics : prop
    {
        public Vector2 velocity, collPos = Vector2.zero;
        public VarObject collObject = null;
        
        public bool isSolid;

        public Physics(bool isSolid) : base()
        {
            this.isSolid = isSolid;
        }

        public void Move(Vector2 movement)
        {
            velocity = movement;
            Vector2 newPosition = varObject.position + velocity;

            Vector2 horizontalMovement = new Vector2(velocity.x, 0);
            Vector2 resolvedHorizontalMovement = horizontalMovement;

            collObject = null;

            foreach (VarObject v in DragEngine.varObjects)
            {
                if (v != varObject && v.GetProp<Physics>().isSolid)
                {
                    Vector2 newHorizontalPosition = varObject.position + resolvedHorizontalMovement;
                    RectangleF futureBounds = new RectangleF(newHorizontalPosition.x, newHorizontalPosition.y, varObject.scale.x, varObject.scale.y);
                    RectangleF otherBounds = new RectangleF(v.position.x, v.position.y, v.scale.x, v.scale.y);

                    if (futureBounds.IntersectsWith(otherBounds))
                    {
                        collObject = v;
                        resolvedHorizontalMovement = ResolveCollision(v, horizontalMovement);
                    }
                }
            }

            varObject.position += resolvedHorizontalMovement;

            Vector2 verticalMovement = new Vector2(0, velocity.y);
            Vector2 resolvedVerticalMovement = verticalMovement;

            foreach (VarObject v in DragEngine.varObjects)
            {
                if (v != varObject && v.GetProp<Physics>().isSolid)
                {
                    Vector2 newVerticalPosition = varObject.position + resolvedVerticalMovement;
                    RectangleF futureBounds = new RectangleF(newVerticalPosition.x, newVerticalPosition.y, varObject.scale.x, varObject.scale.y);
                    RectangleF otherBounds = new RectangleF(v.position.x, v.position.y, v.scale.x, v.scale.y);

                    if (futureBounds.IntersectsWith(otherBounds))
                    {
                        collObject = v;
                        resolvedVerticalMovement = ResolveCollision(v, verticalMovement);
                    }
                }
            }

            varObject.position += resolvedVerticalMovement;
        }

        public void MoveTo(Vector2 pos, float speed = 1.0f, float stopDistance = 0)
        {
            if (pos == null)
                return;

            Vector2 targetPosition = pos;
            Vector2 direction = targetPosition - varObject.position;
            float distance = direction.magnitude;

            if (distance / 10 > stopDistance)
            {
                direction = direction.normalized;

                float step = speed * Time.deltaTime;
                Vector2 movement = direction * step;

                if (movement.magnitude > distance - stopDistance)
                {
                    movement = direction * (distance - stopDistance);
                }

                Move(movement);
            }
            else velocity = Vector2.zero;
        }

        public Vector2 ResolveCollision(VarObject v, Vector2 velocity)
        {
            if (!isSolid) return velocity;

            RectangleF futureBounds = new RectangleF(varObject.position.x + velocity.x, varObject.position.y + velocity.y, varObject.scale.x, varObject.scale.y);
            RectangleF otherBounds = new RectangleF(v.position.x, v.position.y, v.scale.x, v.scale.y);

            float xOverlap = Math.Min(varObject.position.x + varObject.scale.x, v.position.x + v.scale.x) - Math.Max(varObject.position.x, v.position.x);
            float yOverlap = Math.Min(varObject.position.y + varObject.scale.y, v.position.y + v.scale.y) - Math.Max(varObject.position.y, v.position.y);

            if (xOverlap < yOverlap)
            {
                collPos = new Vector2(velocity.x > 0 ? -1 : 1, 0);
                return new Vector2(velocity.x > 0 ? -xOverlap : xOverlap, 0);
            }
            else if (xOverlap > yOverlap)
            {
                collPos = new Vector2(0, velocity.y > 0 ? -1 : 1);
                return new Vector2(0, velocity.y > 0 ? -yOverlap : yOverlap);
            }
            else
            {
                collPos = new Vector2(velocity.x > 0 ? -1 : 1, velocity.y > 0 ? -1 : 1);
                return new Vector2(velocity.x > 0 ? -xOverlap : xOverlap, velocity.y > 0 ? -yOverlap : yOverlap);
            }
        }
    }
}
