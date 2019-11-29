using HCFramework;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
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
    float m_trigvalue;
    public OVRScreenFade BlankScreen;
    public GameObject PatientPos, explode,textCanvas;
    public float trigBegin = 0.55f;
    public float trigEnd = 0.35f;
    public TextMeshProUGUI textView1, textView2, timerText, headText, headText2;
    bool gameOn = true;
    public Animator anim;
    public Animator doorAnim;
    [SerializeField]
    protected OVRInput.Controller m_controller;
    public AudioSource soundSource, audioSourceHospital;
    public AudioClip gunShotClip, diyingBeep, beeping, gunPick, bulletpickup;
    public GameObject secondPos, firstPos, cameraObject, firstScene, flash, hospital, ThirdScenePos, thirdScene; //gunPosition
    int currentLa = 0;
    public float timeLeft = 60;
    BulletType hitBulletType;
    int pickWhat = 0, scene = 0;
    // Start is called before the first frame update
    public List<List<cellPos>> cancer;
    public Transform muzzle;
    bool canShoot = true;
    public GameObject cancerBody;
    public GameObject bloodCell, spawnCells;

    public GameObject cell, bulletPrefab;
    int iteration = 0;
    int cellCount = 0;
    public GameObject cancerModel;
    int score=0;
    GameObject dot;
    public BulletType bullets;
    void Start()
    {
        textCanvas.SetActive(false);
        cancerModel.SetActive(false);
       
        cameraObject.transform.position = firstPos.transform.position;
        EventManager.Instance.AddListner<ObjectClickedEvent>(ObjectClicked);
        EventManager.Instance.AddListner<BulletHitEvent>(BulletHit);
        EventManager.Instance.AddListner<GetDotEvent>(GetDot);
        cancer = new List<List<cellPos>>();
        anim.enabled = false;
        //StartBloodFlow();
        // doorAnim.SetBool("Play", true);
        //  gunPosition = FindObjectOfType<Camera>().transform.FindChildGameObject("gunPos", false);
        tut();
        Invoke("SetFunction", 30);
        StartCancerFlow();
        StartBloodFlow();
        timerText.text = "00:" + (int)timeLeft;

    }
    void SetFun()
    {
        Debug.Log("*******scene Change*******");
        cameraObject.transform.position = secondPos.transform.position;
    }
    public async void tut()
    {
        await Task.Delay(2000);
        Utils.EventAsync(new NextTutorial("Pick Your Weapon Against Cancer", textView1));
    }
    public void GetDot(GetDotEvent data)
    {
        dot = data.dot;
    }
    public async void goToHospital()
    {
        hospital.SetActive(true);
        await Task.Delay(1000);
        doorAnim.SetBool("Play", true);
        anim.enabled = true;
        cameraObject.transform.position = secondPos.transform.position;
        anim.SetBool("Play", true);
        Debug.Log("AnimationPlayed");
        StartBeeping();

    }
    public async void StartBeeping()
    {
        await Task.Delay(10000);
        anim.enabled = false;
        audioSourceHospital.clip = beeping;
        //firstScene.SetActive(false);
        audioSourceHospital.Play();
        //  Utils.EventAsync(new NextTutorial("Shoot The Patient To Inject The Medicine", headText));
        scene++;
        BlackOut();
    }
    public void ObjectClicked(ObjectClickedEvent data)
    {
        switch (data.objectType)
        {
            case ObjectType.Bullet:
                soundSource.clip = gunPick;
                soundSource.time = .5f;
                soundSource.Play();
                if (pickWhat >= 1)
                {
                    bullets = data.type;
                    if (bullets == BulletType.right)
                    {
                        int gamesPlayed = PlayerPrefs.GetInt("GamesPlayed", 0);
                        gamesPlayed++;
                        PlayerPrefs.SetInt("GamesPlayed", gamesPlayed);
                    }
                    goToHospital();
                }
                break;

            case ObjectType.Gun:
                muzzle = data.clickedObj.transform.FindChildGameObject("Muzzle", false).transform;
                muzzle.gameObject.AddComponent<RayCastPointer>();
                Utils.EventAsync(new NextTutorial("Pick Your Bullets", textView1));
                Utils.EventAsync(new CanGrabNowEvent());
                if (pickWhat == 0)
                {
                    pickWhat++;
                }
                Debug.Log("PickedUp Gun");
                soundSource.clip = gunPick;
                //soundSource.
                soundSource.time = .5f;
                soundSource.Play();
                break;
        }
    }

    public async void StartCancerFlow()
    {
        int count = 0;
        while (count < 150)
        {
            count++;
            await Task.Delay(Random.Range(500, 1500));
            GameObject x = Instantiate(cancerBody, spawnCells.transform.position, Quaternion.identity, null);
            x.transform.position = Vector3.zero;
        }
    }

    public async void StartBloodFlow()
    {
        int count = 0;
        while (count < 600)
        {
            count++;
            await Task.Delay(100);
            Instantiate(bloodCell, spawnCells.transform.position, Quaternion.identity, null).transform.position = Vector3.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (scene > 1 && timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerText.text = "00:" + (int)timeLeft;
            float prevFlex = m_trigvalue;
            m_trigvalue = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
            if (canShoot && OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0.5f)
            {
                Shoot();
            }
            else if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) < 0.5 && !canShoot)
            {
                canShoot = true;
            }
        }
        else if (timeLeft < 0 && gameOn)
        {
            gameOn = false;
            BackToHospital();
            if (bullets == BulletType.wrong)
            {
                Utils.EventAsync(new NextTutorial("Sorry! Your Patient Didn't Survive", textView2));
                audioSourceHospital.clip = diyingBeep;
                audioSourceHospital.Play();
            }
            else
            {
                Utils.EventAsync(new NextTutorial("Congrats! You Saved A Life", textView2));
            }
            timerText.text = score+" Cancers Killed";
        }
    }
    bool hit = false;
    public void Shoot()
    {
        soundSource.clip = gunShotClip;
        soundSource.Play();
        hit = false;
        canShoot = false;
        Debug.Log(bullets);
        HepticFeedback();
        GameObject cel = Instantiate(bulletPrefab, muzzle.transform.position, muzzle.transform.rotation, null);
        cell.transform.LookAt(dot.transform);
        Rigidbody rb = cel.GetComponent<Rigidbody>();
        rb.AddForce(cel.transform.forward * 1000);
        GameObject fla = Instantiate(flash, muzzle.transform.position, muzzle.transform.rotation, null);
        Destroy(fla, 2f);
    }
    public async void HepticFeedback()
    {
        OVRInput.SetControllerVibration(.8f, .8f, OVRInput.Controller.RTouch);
        await Task.Delay(400);
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
    }
    public void BulletHit(BulletHitEvent data)
    {
        Debug.Log("********Bullet Hit an Object With Name********* " + data.hit.name);
        if (data.hit.name == "crab1" || data.hit.name == "Cancer")
        {
            score++;
            data.hit.transform.parent.gameObject.GetComponentInChildren<Animator>().SetBool("destroy", true);
            Destroy(Instantiate(explode, data.bullet.transform.position, Quaternion.identity, null), 1.5f);
            Destroy(data.hit);
        }

        Destroy(data.bullet);

    }
    public async void BlackOut()
    {

        
        StartCancerFlow();
        textCanvas.SetActive(true);
        firstScene.SetActive(false);
        Utils.EventAsync(new NextTutorial("Entering Tumour Microenvironment.", headText));

        await Task.Delay(4000);

        cancerModel.SetActive(true);
        Utils.EventAsync(new NextTutorial("Destroy These Cancer Cells", headText));

        await Task.Delay(5000);

        Utils.EventAsync(new NextTutorial("Get Ready To Shoot", headText2));
        cancerModel.SetActive(false);

        await Task.Delay(3000);

        textCanvas.SetActive(false);
        headText.text = "";
        BlankScreen.fadeTime = 1f;
        BlankScreen.StartFade(0, 1);

        await Task.Delay(2000);

        scene++;
        timeLeft = 50;
        cameraObject.transform.position = ThirdScenePos.transform.position;
        cameraObject.transform.localScale = Vector3.one;
        BlankScreen.StartFade(1, 0);
        cancerModel.SetActive(false);

    }
    public async void BackToHospital()
    {
        BlankScreen.fadeTime = 1f;
        BlankScreen.StartFade(0, 1);
        await Task.Delay(1000);
        cameraObject.transform.position = secondPos.transform.position;
        BlankScreen.StartFade(1, 0);
    }
}
