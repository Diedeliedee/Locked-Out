namespace Joeri.Tools.Movement
{
    public partial class Accel
    {
        /// <summary>
        /// Class handling the properties of velocity on a singular axis, only applicable in the air.
        /// </summary>
        public class Uncontrolled
        {
            public float acceleration   = 0f;
            public float velocity       = 0f;
            public float drag           = 0f;

            public Uncontrolled() { }

            public Uncontrolled(float initialAcceleration, float initialVelocity, float initialDrag)
            {
                acceleration = initialAcceleration;
                velocity = initialVelocity;
                drag = initialDrag;
            }

            /// <summary>
            /// Iterates the velocity based on the current set properties.
            /// </summary>
            public float CalculateVelocity(float deltaTime)
            {
                velocity += acceleration * deltaTime;
                velocity -= drag * deltaTime;
                return velocity;
            }

            public void AddVelocity(float velocity)
            {
                this.velocity += velocity;
            }
        }
    }
}