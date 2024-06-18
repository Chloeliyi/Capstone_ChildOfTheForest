using System.Collections.Generic;
using UnityEngine;
namespace Boids
{
    [System.Serializable]
    public class BoidType
    {
        [Header("Boid")]
        public Mesh mesh;
        public Material material;
        public bool isPredator;
        public Vector2 randomSizeRange = new(1, 1.5f);
        [Min(0)] public int spawnCount = 100;

        [Header("Weights")]
        [Range(0f, 1f)] public float separationWeight = 0.05691103f;
        [Range(0f, 1f)] public float alignmentWeight = 0.5797515f;
        [Range(0f, 1f)] public float cohesionWeight = 0.3633375f;
        [Range(0f, 1f)] public float predatorWeight = 0.25f;
        [Range(0f, 1f)] public float pointTargetWeight = 0.139f;
        [Range(0f, 1f)] public float obstacleAvoidanceWeight = 0.446f;

        [Header("Parameters")]
        [Range(0, 180)] public float perceptionAngle = 120;
        public float perceptionDistance = 15;
        public float separationDistance = 5;
        public float collsionDistance = 5;
        public float maxSpeed = 25;
        public float minSpeed = 10;
        public float maxSteering = 20;

        [Header("Points")]
        public Target[] targets;

        [Header("Debug")]
        public bool drawPerception = false;
        public bool drawFlock = false;
        public bool drawForces = false;
        public bool drawPoints = false;
        public bool drawLineToPoint = false;
        public bool drawCollsions = false;
        [HideInInspector] public float perceptionCosValue;
        public void NormalizeWeights()
        {
            float weightsSum = separationWeight + cohesionWeight + alignmentWeight;
            if (weightsSum != 0)
            {
                separationWeight /= weightsSum;
                cohesionWeight /= weightsSum;
                alignmentWeight /= weightsSum;
            }
        }
    }

    [System.Serializable]
    public class BoidController : MonoBehaviour
    {
        [Header("Spatial Hash Grid")]
        [Tooltip("Best to be the same as Perception Distance of the type of boids that has the most members")]
        [SerializeField, Min(0)] float gridSize = 1.0f;
        [SerializeField, Min(1)] int memoryUsage = 1024;
        [SerializeField, Min(0)] int drawRange = 5;
        [SerializeField] bool drawGrid = false;

        public BoidType[] types;
        public List<Boid> boids;

        private SpatialHashGrid<Boid> hashGrid;
        private Matrix4x4[][] matrices;

        private void OnValidate()
        {
            if (types != null)
            {
                int totalBoidCount = 0;
                foreach (BoidType type in types)
                {
                    type.NormalizeWeights();
                    type.perceptionCosValue = Mathf.Cos(type.perceptionAngle * Mathf.Deg2Rad);
                    totalBoidCount += type.spawnCount;
                }

                if (boids != null && boids.Count != totalBoidCount)
                {
                    Start();
                }
            }
            if (hashGrid != null && (hashGrid.cellSize != gridSize || hashGrid.buckets.Length != memoryUsage))
            {
                hashGrid.Clear();
                hashGrid = new SpatialHashGrid<Boid>(gridSize, memoryUsage);
            }
            if (matrices != null && (matrices.Length != types.Length))
            {
                matrices = new Matrix4x4[types.Length][];
            }
            if (matrices != null)
            {
                for (int i = 0; i < types.Length; i++)
                {
                    if (matrices[i] != null && (matrices[i].Length != types[i].spawnCount))
                    {
                        matrices[i] = new Matrix4x4[types[i].spawnCount];
                    }
                }
            }
        }
        private void Start()
        {
            // Count how many boids are needed.
            int totalBoidCount = 0;
            foreach (BoidType type in types)
                totalBoidCount += type.spawnCount;

            // Initialize boids array and Hash Grid.
            boids = new List<Boid>(totalBoidCount);
            hashGrid = new SpatialHashGrid<Boid>(gridSize, memoryUsage);
            for (int i = 0; i < types.Length; i++)
            {
                for (int j = 0; j < types[i].spawnCount; j++)
                {
                    // Locations where you want to spawn the boids.
                    Vector3[] boidSpawningLocations = new Vector3[4];
                    Vector3 position = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));
                    position = position + boidSpawningLocations[Random.Range(0, boidSpawningLocations.Length)];

                    Vector3 velocity = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)) * 100;
                    float size = Random.Range(types[i].randomSizeRange.x, types[i].randomSizeRange.y);
                    boids.Add(new Boid(position, velocity, size, i));
                    hashGrid.Insert(boids[boids.Count - 1]);
                }
            }

            boids[0].position = Vector3.zero;
            boids[0].velocity = Vector3.down + Vector3.forward + Vector3.right;
            // Initialize matrix array.
            matrices = new Matrix4x4[types.Length][];
            for (int i = 0; i < types.Length; i++)
            {
                matrices[i] = new Matrix4x4[types[i].spawnCount];
            }
        }
        private void Update()
        {
            if (boids == null)
                Start();

            foreach (Boid boid in boids)
            {
                // Obstacle Avoidance.
                Vector3 acceleration = GetObstacleAvoidanceForce(boid);

                // Flocking forces.
                Vector3 separationForce, alignmentForce, cohesionForce, predatorForce;
                Vector3 pointTargetingForce = GetPointTargetingForce(boid);
                if (GetFlockingForces(boid, out separationForce, out alignmentForce, out cohesionForce, out predatorForce) || pointTargetingForce != Vector3.zero)
                {
                    Vector3 targetVelocity = (separationForce + alignmentForce + cohesionForce + pointTargetingForce + predatorForce) * types[boid.id].maxSpeed;
                    acceleration += Vector3.ClampMagnitude(targetVelocity - boid.velocity, types[boid.id].maxSteering);
                }

                // Euler Integration and velocity finalization.
                boid.velocity += acceleration * Time.deltaTime;
                float speed = boid.velocity.magnitude;
                boid.velocity *= Mathf.Clamp(speed, types[boid.id].minSpeed, types[boid.id].maxSpeed) / speed;

                // Update boid's position and it's position in Hash Grid.
                Vector3 prevPos = boid.position;
                boid.UpdatePosition(Time.deltaTime);
                hashGrid.Update(boid, prevPos);

                if (types[boid.id].drawForces)
                {
                    Debug.DrawLine(boid.position, boid.position + separationForce, Color.red);
                    Debug.DrawLine(boid.position, boid.position + alignmentForce, Color.green);
                    Debug.DrawLine(boid.position, boid.position + cohesionForce, Color.blue);
                    Debug.DrawLine(boid.position, boid.position + pointTargetingForce, Color.cyan);
                    Debug.DrawLine(boid.position, boid.position + predatorForce * 3, Color.yellow);
                }
            }

            // Instanced drawing of all boids, by their type.
            int sum = 0;
            for (int k = 0; k < types.Length; k++)
            {
                for (int j = 0; j < types[k].spawnCount; j++)
                {
                    matrices[k][j] = boids[sum + j].GetMatrix();
                }
                sum += types[k].spawnCount;
                Graphics.DrawMeshInstanced(types[k].mesh, 0, types[k].material, matrices[k]);
            }
        }
        private void OnDrawGizmos()
        {
            if (drawGrid && hashGrid != null)
            {
                Gizmos.color = new Color(1, 1, 1, 0.2f);
                Vector3Int cellPos = new Vector3Int();
                for (int z = -drawRange; z < drawRange; z++)
                {
                    for (int y = -drawRange; y < drawRange; y++)
                    {
                        for (int x = -drawRange; x < drawRange; x++)
                        {
                            cellPos.Set(x, y, z);
                            if (hashGrid.Quarry(cellPos).Count != 0)
                            {
                                Gizmos.DrawCube((cellPos + Vector3.one * 0.5f) * gridSize, Vector3.one * gridSize);
                            }
                        }
                    }
                }
            }
            if (types != null)
            {
                foreach (BoidType type in types)
                {
                    if (type.drawPoints)
                    {
                        foreach (Target target in type.targets)
                        {
                            switch (target.type)
                            {
                                case Target.Type.Attractor: Gizmos.color = Color.green; break;
                                case Target.Type.Repeller: Gizmos.color = Color.red; break;
                                default: break;
                            }
                            Gizmos.DrawSphere(target.position, 1);
                            target.DebugDraw();
                        }
                    }
                }
            }
            if (boids != null)
            {
                Gizmos.color = Color.yellow;
                foreach (Boid boid in boids)
                {
                    if (types[boid.id].drawPerception)
                        Gizmos.DrawWireSphere(boid.position, types[boid.id].perceptionDistance);
                }
                Gizmos.color = Color.blue;
                foreach (Boid boid in boids)
                {
                    if (types[boid.id].drawPerception)
                        Gizmos.DrawWireSphere(boid.position, types[boid.id].separationDistance);
                }
                Gizmos.color = Color.magenta;
                foreach (Boid boid in boids)
                {
                    if (types[boid.id].drawPerception)
                        Gizmos.DrawWireSphere(boid.position, types[boid.id].collsionDistance);
                }
            }
        }
        private Vector3 GetObstacleAvoidanceForce(Boid boid)
        {
            Vector3 obstacleAvoidanceForce = Vector3.zero;

            // Get all colliders inside sphere of radius collisionDistance.
            Collider[] colliders = Physics.OverlapSphere(boid.position, types[boid.id].collsionDistance);
            if (colliders.Length == 0) return obstacleAvoidanceForce;

            Vector3 boidDirection = boid.velocity.normalized;
            float minCollsionDistance = float.MaxValue;

            // Foreach Collider get the closest point to boid and use it as collision point.
            foreach (Collider collider in colliders)
            {
                Quaternion rot = Quaternion.LookRotation(boid.velocity, Vector3.up);
                Vector3 collisionPoint = Vector3.positiveInfinity;
                Vector3 normal = Vector3.zero;
                float distance = types[boid.id].collsionDistance;

                //If Collider is not convex then approximate collision point.
                if ((collider is MeshCollider collider1 && !collider1.convex) || collider is TerrainCollider)
                {
                    // Cast rays in 5 directions relative to boid's rotation and pick closest one.
                    RaycastHit hit = new();
                    Vector3[] rayDirections = { Vector3.forward, Vector3.down, Vector3.up, Vector3.right, Vector3.left };
                    foreach (Vector3 dir in rayDirections)
                    {
                        if (collider.Raycast(new Ray(boid.position, rot * dir), out hit, distance) && hit.distance < distance)
                        {
                            collisionPoint = hit.point;
                            normal = hit.normal;
                            distance = hit.distance;
                        }
                    }
                    if (collisionPoint.x == Mathf.Infinity) continue;
                }
                else
                {
                    collisionPoint = collider.ClosestPoint(boid.position);
                    normal = boid.position - collisionPoint;
                    distance = normal.magnitude;
                    normal /= distance;
                }

                if (distance == 0) continue;
                float distanceNormalized = distance / (types[boid.id].collsionDistance);

                // Calculate collision response.
                Vector3 tangent = -Vector3.Cross(Vector3.Cross(boidDirection + rot * Vector3.up, normal), normal).normalized;
                float normalVelocity = Mathf.Max(Vector3.Dot(boid.velocity, -normal), 0);

                Vector3 depenetration = normal / distanceNormalized / distanceNormalized * (1 + normalVelocity);
                Vector3 courseChange = tangent;

                obstacleAvoidanceForce += (depenetration + tangent) * distanceNormalized;

                // Update min collision distance.
                minCollsionDistance = Mathf.Min(minCollsionDistance, distanceNormalized);
                if (types[boid.id].drawCollsions) Debug.DrawLine(boid.position, collisionPoint, Color.red);
            }

            return 3f * types[boid.id].maxSpeed * types[boid.id].obstacleAvoidanceWeight * obstacleAvoidanceForce.normalized / minCollsionDistance;
        }
        private Vector3 GetPointTargetingForce(Boid boid)
        {
            Vector3 pointTargetingForce = Vector3.zero;

            // Get Point Targeting Forces.
            foreach (Target target in types[boid.id].targets)
            {
                // Discard targets that are not in range.
                Vector3 gradient = target.Gradient(boid.position);
                pointTargetingForce += gradient * target.strength * (target.type == Target.Type.Attractor ? 1 : -1);

                if (types[boid.id].drawLineToPoint) Debug.DrawLine(boid.position, target.position, target.type == Target.Type.Attractor ? Color.green : Color.red);
            }

            return pointTargetingForce * 0.1f * types[boid.id].pointTargetWeight;
        }
        private bool GetFlockingForces(Boid boid, out Vector3 separationForce, out Vector3 alignmentForce, out Vector3 cohesionForce, out Vector3 predatorForce)
        {
            bool anyNeighbours = false;
            Vector3 boidDirection = boid.velocity.normalized;
            separationForce = Vector3.zero;
            alignmentForce = Vector3.zero;
            cohesionForce = Vector3.zero;
            predatorForce = Vector3.zero;

            List<Boid> quarry = hashGrid.Querry(boid.position, types[boid.id].perceptionDistance);
            foreach (Boid other in quarry)
            {
                if (other == boid) continue;

                // Discard boids out of Perception View.
                Vector3 toOther = other.position - boid.position;
                float distance = Vector3.Magnitude(toOther);
                toOther /= distance;
                anyNeighbours = true;
                if (Vector3.Dot(boidDirection, toOther) < types[boid.id].perceptionCosValue) continue;

                // Predators don't flock with each other, others flock with same type.
                if (boid.id == other.id)
                {
                    alignmentForce += other.velocity;
                    cohesionForce += toOther / distance;
                }

                // As predator chase prey, as prey evade predators.
                if (types[boid.id].isPredator && !types[other.id].isPredator)
                {
                    predatorForce += toOther / distance;
                }
                else if (!types[boid.id].isPredator && types[other.id].isPredator)
                {
                    predatorForce -= toOther / distance;
                }

                if (types[boid.id].drawFlock && boid.id == other.id)
                    Debug.DrawLine(boid.position, boid.position + (other.position - boid.position) * 0.5f, Color.yellow);

                // Discard boids out of Separation Distance.
                if (distance > types[boid.id].separationDistance) continue;

                // Separation.
                separationForce -= toOther * (types[boid.id].separationDistance / distance);

                if (types[boid.id].drawFlock)
                    Debug.DrawLine(boid.position, boid.position + (other.position - boid.position) * 0.5f, Color.red);
            }

            // Finalize forces.
            separationForce = separationForce * types[boid.id].separationWeight;
            alignmentForce = alignmentForce.normalized * types[boid.id].alignmentWeight;
            cohesionForce = cohesionForce.normalized * types[boid.id].cohesionWeight;
            predatorForce = predatorForce.normalized * types[boid.id].predatorWeight * 4;
            return anyNeighbours;
        }
    }
}