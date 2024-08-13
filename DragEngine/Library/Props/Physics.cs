using System;
using System.Drawing;

namespace DragEngine
{
    public class Physics : Prop
    {
        public Vector2 velocity, collPos, checkPos = Vector2.zero;
        public VarObject collObject = null;
        public PhysicMat physicMat = null;

        public bool hasCollider => varObject.GetProp<Collider>() != null;
        public bool gravity;
        public int mass;

        public Action OnCollEnter, OnCollExit;
        public Physics(bool gravity = false, int mass = 1) : base()
        {
            this.gravity = gravity;
            this.mass = mass;
            velocity = Vector2.zero;
            collPos = Vector2.zero;
        }


        public void Update()
        {
            Push();
            ApplyVelocity(velocity);
            Gravity();
            PhysicMaterial();
        }

        void ApplyVelocity(Vector2 movement)
        {
            Vector2 resolvedMovement = movement;
            collObject = null;
            checkPos = collPos;
            collPos = Vector2.zero;

            foreach (VarObject v in DragEngine.varObjects)
            {
                if (v != varObject && v.GetProp<Collider>() != null)
                {
                    RectangleF futureBounds = new RectangleF(position.x + resolvedMovement.x, position.y + resolvedMovement.y, scale.x, scale.y);
                    RectangleF otherBounds = new RectangleF(v.position.x, v.position.y, v.scale.x, v.scale.y);

                    if (futureBounds.IntersectsWith(otherBounds))
                    {
                        OnCollEnter?.Invoke();
                        collObject = v;

                        resolvedMovement = ResolveCollision(v, movement, out collPos);
                    } // Dont put break; if you love your life (Its buggy and I gave soo much time to fix that)
                }
            }

            OnCollExit?.Invoke();

            varObject.position += resolvedMovement;
        }
        void FixVelocity()
        {
            if (collPos.x == 1 && velocity.x > 0) velocity.x = 0;
            if (collPos.x == -1 && velocity.x < 0) velocity.x = 0;
            if (collPos.y == 1 && velocity.y < 0) velocity.y = 0;
            if (collPos.y == -1 && velocity.y > 0) velocity.y = 0;
        }
        Vector2 ResolveCollision(VarObject v, Vector2 velocity, out Vector2 pos)
        {
            pos = Vector2.zero;
            if (!hasCollider) return velocity;

            float xOverlap = Math.Min(position.x + scale.x, v.position.x + v.scale.x) - Math.Max(position.x, v.position.x);
            float yOverlap = Math.Min(position.y + scale.y, v.position.y + v.scale.y) - Math.Max(position.y, v.position.y);

            Vector2 answer = Vector2.zero;

            if (xOverlap < yOverlap)
            {
                pos = new Vector2(velocity.x > 0 ? -1 : 1, 0);
                answer = new Vector2(velocity.x > 0 ? -xOverlap : xOverlap, velocity.y);
            }
            else if (xOverlap > yOverlap)
            {
                pos = new Vector2(0, velocity.y > 0 ? -1 : 1);
                answer = new Vector2(velocity.x , velocity.y > 0 ? -yOverlap : yOverlap);
            }
            else
            {
                pos = new Vector2(velocity.x > 0 ? -1 : 1, velocity.y > 0 ? -1 : 1);
                answer = new Vector2(velocity.x > 0 ? -xOverlap : xOverlap, velocity.y > 0 ? -yOverlap : yOverlap);
            }

            return answer;
        }


        public void Move(Vector2 movement) => ApplyVelocity(movement);
        public void MoveTo(Vector2 pos, float speed = 1.0f, float stopDistance = 0)
        {
            if (pos == null) return;

            Vector2 direction = (pos - position).normalized;
            float distance = (pos - position).magnitude;

            if (distance > stopDistance)
            {
                float step = speed * Time.deltaTime;
                Vector2 movement = direction * Math.Min(step, distance - stopDistance);
                Move(movement);
            }
        }

        public void AddForce(Vector2 movement) => velocity += movement;
        public void RemoveForce(Vector2 movement) => velocity -= movement;


        void Push()
        {
            if (collObject != null && collObject.TryGetProp(out Physics p) && p.mass < mass)
            {
                Vector2 pushForce = (collPos / p.mass / mass) * -1;
                p.Move(pushForce);
            }
        }
        void Gravity()
        {
            if (gravity && collPos.y != -1)
            {
                velocity.y += 4.45f * mass * Time.deltaTime;
            } // GG GG GG GG GG GG GG GG GG GG GG GG GG GG GG GG (Lost Mentalite)
            else if (gravity) velocity.y = 0;
        }
        void PhysicMaterial()
        {
            if (physicMat == null || physicMat != null && physicMat.bounciness != 0) FixVelocity();

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
            }
        }

        public void IncreaseVelocity(Vector2 amount)
        {
            if (velocity.x >= 0) velocity.x += amount.x;
            else velocity.x -= amount.x;

            if (velocity.y >= 0) velocity.y += amount.y;
            else velocity.y -= amount.y;
        }
    }
}
