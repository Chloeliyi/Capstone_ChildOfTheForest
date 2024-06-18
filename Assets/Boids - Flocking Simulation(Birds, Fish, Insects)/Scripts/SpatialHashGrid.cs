using System.Collections.Generic;
using UnityEngine;

namespace Boids
{
    public interface IHashable
    {
        public Vector3 Position();
    }
    // Spatial Hash Grid for optymized position quarrying.
    public class SpatialHashGrid<T> where T : IHashable
    {
        public List<T>[] buckets;
        public float cellSize;
        public SpatialHashGrid(float cellSize, int memoryUsage)
        {
            this.cellSize = cellSize;
            buckets = new List<T>[memoryUsage];
            for (int i = 0; i < buckets.Length; i++)
            {
                buckets[i] = new List<T>();
            }
        }
        public bool Contains(T obj)
        {
            return buckets[Hash(obj)].Contains(obj);
        }
        public void Insert(T obj)
        {
            buckets[Hash(obj)].Add(obj);
        }
        public void Remove(T obj)
        {
            buckets[Hash(obj)].Remove(obj);
        }
        public void Update(T obj, Vector3 previousPosition)
        {
            int prevHash = Hash(previousPosition);
            int newHash = Hash(obj);
            if (prevHash == newHash) return;

            buckets[prevHash].Remove(obj);
            buckets[newHash].Add(obj);
        }
        public void Clear()
        {
            foreach (List<T> bucket in buckets)
            {
                bucket.Clear();
            }
        }
        public List<T> Quarry(Vector3Int cellIndex)
        {
            return buckets[Hash(cellIndex)];
        }
        public List<T> Querry(Vector3 position, float range)
        {
            List<T> result = new List<T>();
            Vector3Int index = Vector3Int.FloorToInt(position / cellSize);
            int cellRange = (int)Mathf.Ceil(range / cellSize);

            Vector3Int cellPos = new Vector3Int();
            for (int z = -cellRange; z <= cellRange; z++)
            {
                if (z < 0 && Mathf.Abs((z + index.z + 1) * cellSize - position.z) >= range) continue;
                else if (z > 0 && Mathf.Abs((z + index.z) * cellSize - position.z) >= range) continue;
                for (int y = -cellRange; y <= cellRange; y++)
                {
                    if (y < 0 && Mathf.Abs((y + index.y + 1) * cellSize - position.y) >= range) continue;
                    else if (y > 0 && Mathf.Abs((y + index.y) * cellSize - position.y) >= range) continue;
                    for (int x = -cellRange; x <= cellRange; x++)
                    {
                        if (x < 0 && Mathf.Abs((x + index.x + 1) * cellSize - position.x) >= range) continue;
                        else if (x > 0 && Mathf.Abs((x + index.x) * cellSize - position.x) >= range) continue;

                        cellPos.Set(index.x + x, index.y + y, index.z + z);
                        List<T> bucket = buckets[Hash(cellPos)];
                        for (int i = 0; i < bucket.Count; i++)
                        {
                            if (Vector3.SqrMagnitude(position - bucket[i].Position()) < range * range)
                            {
                                result.Add(bucket[i]);
                            }
                        }
                    }
                }
            }
            return result;
        }
        private Vector3Int CellIndex(Vector3 pos)
        {
            return Vector3Int.FloorToInt(pos / cellSize);
        }
        private int Hash(T obj)
        {
            return Hash(obj.Position());
        }
        private int Hash(Vector3 pos)
        {
            return Hash(CellIndex(pos));
        }
        private int Hash(Vector3Int index)
        {
            // Example of custom hashing function, used in examples.
            // return Mathf.Abs((index.x + 8) + (index.y + 8) * 8 + (index.z + 8) * 64) % buckets.Length;

            return Mathf.Abs((index.x * 73856093) ^ (index.y * 19349663) ^ (index.z * 83492791) + 83492791) % buckets.Length;
        }
    }
}