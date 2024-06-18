using UnityEngine;
namespace Boids
{
    public class Boid : IHashable
    {
        public Vector3 position;
        public Vector3 velocity;
        public float size;
        public int id;

        // Initialize boid with position and velocity.
        public Boid(Vector3 position, Vector3 velocity, float size, int id)
        {
            this.position = position;
            this.velocity = velocity;
            this.size = size;
            this.id = id;
        }

        // Update boid's position.
        public void UpdatePosition(float deltaTime)
        {
            position += velocity * deltaTime;
            velocity = Vector3.Lerp(velocity, velocity, deltaTime * 4);
        }

        // Get boid's tranformation matrix.
        public Matrix4x4 GetMatrix()
        {
            //return Matrix4x4.TRS(position, Quaternion.LookRotation(Vector3.forward, Vector3.up), Vector3.one * size);
            return Matrix4x4.TRS(position, Quaternion.LookRotation(velocity, Vector3.up), Vector3.one * size);
        }
        public Vector3 Position()
        {
            return position;
        }
    }
}