using HCFramework;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableObject : OVRGrabbable
{
    public BulletType type;
    //public GameObject SecondSpawn;
    public ObjectType objectType;
    public GunType gunType = GunType.none;
    bool selected = false;
    public Transform gunOffSet;
    Vector3 addition;



    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        if (objectType == ObjectType.Bullet)
        {
            type = BulletType.wrong; 
            gameObject.GetComponent<BoxCollider>().enabled = false;

            int gamesPlayed = PlayerPrefs.GetInt("GamesPlayed", 0);
            if (gamesPlayed < 45)
            {
                if (Random.Range(0, 100) < 50)
                {
                    type = BulletType.right;
                }
            }
        }
        EventManager.Instance.AddListner<CanGrabNowEvent>(CanGrab);
        // GetComponent<OVRGrabbable>().
        addition = gameObject.transform.localScale * 0.2f;
    }
    public void CanGrab(CanGrabNowEvent data)
    {
        if (objectType == ObjectType.Bullet)
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }
    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        base.GrabBegin(hand, grabPoint);

        if (objectType == ObjectType.Bullet)
        {
            // gotBullets();

        }
        else if (objectType == ObjectType.Gun)
        {
            transform.parent = null;
            Utils.EventAsync(new ObjectClickedEvent(type, objectType, gunType, this.gameObject, true));
            //transform.rotation = m_grabbedBy.transform.rotation;
            //transform.parent = gunOffSet;
            //transform.localPosition = Vector3.zero;
            Debug.Log("******************Set Rotation For Gun" + transform.position);

        }
    }
    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {

        base.GrabEnd(linearVelocity, angularVelocity);
        if (objectType == ObjectType.Bullet)
        {
            DestroyBullets();
          //  m_grabbedBy.m_grabbedObj = null;
        }
    }
    public async void DestroyBullets()
    {
        await Task.Delay(50);
        this.gameObject.SetActive(false);
        Utils.EventAsync(new ObjectClickedEvent(type, objectType, gunType, this.gameObject, true));


    }
    // Update is called once per frame
    void Update()
    {

    }

}
