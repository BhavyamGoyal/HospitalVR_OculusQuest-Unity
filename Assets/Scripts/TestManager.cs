using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public Transform prefab_crab;
    public Transform prefab_player;
    int count_initial = 10;
    int min_distance = 2;
    public GameObject bloodCell, spawnCells1;
    // Start is called before the first frame update
    void Start()
    {
        //Spawn(10);
        StartCancerFlow();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public async void StartBloodFlow()
    {
        int count = 0;
        while (count < 10)
        {
            count++;
            await Task.Delay(Random.Range(400, 1500));
            Instantiate(bloodCell, spawnCells1.transform.position, Quaternion.identity, null).transform.position = Vector3.zero;
        }
    }
    public async void StartCancerFlow()
    {
        int count = 0;
        while (count < 10)
        {
            count++;
            await Task.Delay(Random.Range(400, 1500));
            GameObject x = Instantiate(bloodCell, spawnCells1.transform.position, Quaternion.identity, null);
            x.transform.position = Vector3.zero;
        }
    }
    void Spawn(int number)
    {
        Vector3 spawnPosition;
        int random_x;
        int random_y;
        int random_z;
        while (number > 0)
        {

            random_x = (Random.Range(-10, 10));
            if (Mathf.Abs(random_x) < min_distance)
                random_x = min_distance;

            random_z = (Random.Range(-10, 10));
            if (Mathf.Abs(random_z) < min_distance)
                random_z = min_distance;

            if (number % 3 == 0)
            {

                random_y = (Random.Range(-5, 5));

                if (Mathf.Abs(random_y) < min_distance)
                    random_y = min_distance;
                // spawnPosition = new Vector3(random_x, random_y, random_z);
                spawnPosition = new Vector3(random_x, 0.5f + random_y, random_z);
            }

            else
                spawnPosition = new Vector3(random_x, 0.5f, random_z);
            Instantiate(prefab_crab, prefab_player.transform.position + spawnPosition, Quaternion.identity);
            number--;
        }
    }
}

