using UnityEngine;
using Random = System.Random;

namespace Controllers.Mobs
{
    public class MobSpawner : MonoBehaviour
    {
        public GameObject mobPrefab;

        public int desiredMobsCount = 10;
        private GameObject[] _mobs;

        // Start is called before the first frame update
        private void Start()
        {
            _mobs = new GameObject[desiredMobsCount];
            InvokeRepeating(nameof(Spawner), 1f, 1f);
        }

        private void Spawner()
        {
            var rand = new Random();
            for (var i = 0; i < _mobs.Length; i++)
            {
                var mob = _mobs[i];
                if (mob != null)
                {
                    if (mob.transform.position.y < -100)
                        Destroy(mob);
                }
                else
                {
                    var x = rand.Next(20, 150);
                    var y = rand.Next(20, 150);
                    _mobs[i] = Instantiate(mobPrefab, new Vector3(x, 5 + i, y), Quaternion.identity);
                }
            }
        }
    }
}