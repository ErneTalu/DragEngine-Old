using System;
using System.Drawing;

namespace DragEngine
{
    public class Physics : Prop
    {
        public Vector2 velocity, collPos = Vector2.zero;
        public VarObject collObject = null;
        public PhysicMat physicMat = null;

        public bool isSolid => varObject.GetProp<Collider>() != null;
        public bool gravity;
        public int mass;

        public Action OnCollEnter, OnCollExit;

        public Physics(bool gravity = false, int mass = 1) : base()
        {
            this.gravity = gravity;
            this.mass = mass;
        }


        public void Update()
        {
            if (velocity == null) velocity = Vector2.zero;
            if (gravity)
            {
                velocity += new Vector2(velocity.x, 9.81f * mass * Time.deltaTime); // Basit yer çekimi ekle
            }

            Vector2 horizontalMovement = new Vector2(velocity.x, 0);
            Vector2 verticalMovement = new Vector2(0, velocity.y);

            ResolveMove(horizontalMovement);
            ResolveMove(verticalMovement);

            if (physicMat != null && collPos != Vector2.zero)
            {
                if (collPos.x != 0)
                {
                    if (physicMat.bounciness > 0) velocity.x = -velocity.x * physicMat.bounciness;
                    if (physicMat.friction > 0) velocity.x *= (1 - physicMat.friction);
                }
                if (collPos.y != 0)
                {
                    if (physicMat.bounciness > 0) velocity.y = -velocity.y * physicMat.bounciness;
                    if (physicMat.friction > 0) velocity.y *= (1 - physicMat.friction);
                }

                collPos = Vector2.zero;
            }
        }

        public void Move(Vector2 movement)
        {
            velocity = movement;

            Vector2 horizontalMovement = new Vector2(velocity.x, 0);
            Vector2 verticalMovement = new Vector2(0, velocity.y);

            ResolveMove(horizontalMovement);
            ResolveMove(verticalMovement);
        }

        public void MoveTo(Vector2 pos, float speed = 1.0f, float stopDistance = 0)
        {
            if (pos == null) return;

            Vector2 direction = (pos - varObject.position).normalized;
            float distance = (pos - varObject.position).magnitude;

            if (distance > stopDistance)
            {
                float step = speed * Time.deltaTime;
                Vector2 movement = direction * Math.Min(step, distance - stopDistance);
                Move(movement);
            }
            else
            {
                velocity = Vector2.zero;
            }
        }


        public bool IsColliding(VarObject a, VarObject b) =>
            a.position.y + a.scale.y > b.position.y && b.position.y + b.scale.y > a.position.y &&
            a.position.x + a.scale.x > b.position.x && b.position.x + b.scale.x > a.position.x;
        public bool IsColliding(VarObject a, string name)
        {
            foreach (VarObject b in DragEngine.varObjects)
            {
                if (b.name == name && IsColliding(a, b)) return true;
            }

            return false;
        }


        private void ResolveMove(Vector2 movement)
        {
            Vector2 resolvedMovement = movement;
            collObject = null;

            foreach (VarObject v in DragEngine.varObjects)
            {
                if (v != varObject && v.GetProp<Collider>() != null)
                {
                    RectangleF futureBounds = new RectangleF(varObject.position.x + resolvedMovement.x, varObject.position.y + resolvedMovement.y, varObject.scale.x, varObject.scale.y);
                    RectangleF otherBounds = new RectangleF(v.position.x, v.position.y, v.scale.x, v.scale.y);

                    if (futureBounds.IntersectsWith(otherBounds))
                    {
                        collObject = v;
                        resolvedMovement = ResolveCollision(v, movement);
                        OnCollEnter?.Invoke();
                    }
                }
            }

            OnCollExit?.Invoke();

            varObject.position += resolvedMovement;
        }
        public Vector2 ResolveCollision(VarObject v, Vector2 velocity)
        {
            if (!isSolid) return velocity;

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

        public void AddForce(Vector2 force) => velocity = force;
        public void IncreaseVelocity(Vector2 amount)
        {
            if (velocity.x >= 0) velocity.x += amount.x;
            else velocity.x -= amount.x;

            if (velocity.y >= 0) velocity.y += amount.y;
            else velocity.y -= amount.y;
        }
    }
}
