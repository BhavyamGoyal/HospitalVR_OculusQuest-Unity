using HCFramework;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class cellPos
{
    public float x, y;
    public cellPos(float x, float y)
    {
        this.x = x;
        this.y = y;
    }
}
public class GameManager : MonoBehaviour
{
    int currentLayer=0;
    BulletType hitBulletType;
    // Start is called before the first frame update
    public List<List<cellPos>> cancer;
    public Transform muzzle;
    bool canShoot = true;
    public Transform cancerBody;
    public List<List<GameObject>> cellToDestroy = new List<List<GameObject>>();
    public GameObject cell,bulletPrefab;
    int iteration = 0;
    int cellCount = 0;
    public List<BulletType> bullets = new List<BulletType>();
    void Start()
    {
        EventManager.Instance.AddListner<ObjectClickedEvent>(ObjectClicked);
        EventManager.Instance.AddListner<BulletHitEvent>(BulletHit);
        cancer = new List<List<cellPos>>();
        cancer.Add(new List<cellPos>() { new cellPos(0f, 0f), new cellPos(0f, 0.05f), new cellPos(0.05f, 0.05f), new cellPos(0.05f, 0f) });
        cancer.Add(new List<cellPos>() { new cellPos(0f, -0.05f), new cellPos(-0.05f, 0f), new cellPos(-0.05f, -0.05f), new cellPos(0.05f, -0.05f), new cellPos(-0.05f, 0.05f) });
        cancer.Add(new List<cellPos>() { new cellPos(0f, 0.1f), new cellPos(0f, -0.1f), new cellPos(0.1f, 0f), new cellPos(-0.1f, 0f) });
        cancer.Add(new List<cellPos>() { new cellPos(0.1f, 0.05f), new cellPos(0.05f, 0.1f), new cellPos(0.1f, 0.1f), new cellPos(-0.1f, -0.05f), new cellPos(-0.05f, -0.1f), new cellPos(-0.1f, -0.1f) });
        cancer.Add(new List<cellPos>() { new cellPos(-0.1f, 0.05f), new cellPos(-0.05f, 0.1f), new cellPos(-0.1f, 0.1f), new cellPos(0.1f, -0.05f), new cellPos(0.05f, -0.1f), new cellPos(0.1f, -0.1f) });
        cancer.Add(new List<cellPos>() { new cellPos(-0.1f, 0.15f), new cellPos(0.1f, 0.15f), new cellPos(0.15f, 0.05f), new cellPos(0.1f, -0.15f), new cellPos(-0.05f, -0.15f) });
        InvokeRepeating("PopulateCancer", 3f, 5f);
    }

    public void ObjectClicked(ObjectClickedEvent data)
    {
        if (bullets.Count < 6 && data.selected)
        {
            bullets.Add(data.type);
        }
        else if (!data.selected)
        {
            bullets.Remove(data.type);
        }
    }

    public void PopulateCancer()
    {
        if (iteration < cancer.Count)
        {
            Debug.Log(currentLayer);    
            List<cellPos> iter = cancer[currentLayer];
            cellToDestroy.Add(new List<GameObject>());
            for (int i = 0; i < iter.Count; i++)
            {
                GameObject cel = Instantiate(cell, cancerBody);
                cel.transform.localPosition = new Vector3(0f, iter[i].y, iter[i].x);
                cellToDestroy[currentLayer].Add(cel);
            }
            iteration++;
            currentLayer++;
        }
       
    }
    public void DestroyCell()
    {
        List<GameObject> destroyingCells = cellToDestroy[currentLayer-1];
        for (int i = 0; i < destroyingCells.Count; i++)
        {
            Destroy(destroyingCells[i]);
        }
        cellToDestroy.RemoveAt(cellToDestroy.Count - 1);
        currentLayer--;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1) && bullets.Count>0)
        {
            Shoot();
          
        }
    }
    bool hit = false;
    public async void Shoot()
    {
        if (canShoot)
        {
            hit = false;
            canShoot = false;
            hitBulletType = bullets[0];
            bullets.RemoveAt(0);
            Debug.Log(hitBulletType);
            GameObject cel = Instantiate(bulletPrefab,muzzle.transform.position,transform.rotation, null);
            Rigidbody rb=cel.GetComponent<Rigidbody>();
            rb.AddForce(cel.transform.forward * 60);
            await Task.Delay(1000);
            canShoot = true;
        }
    }
    public void BulletHit(BulletHitEvent data)
    {
        if (!hit)
        {
            if (hitBulletType == BulletType.right)
            {
                hit = true;
                DestroyCell();
            }
        }
    }
}
