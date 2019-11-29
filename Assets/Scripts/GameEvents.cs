using HCFramework;
using TMPro;
using UnityEngine;

public class ObjectClickedEvent : GameEvent
{
    public BulletType type;
    public bool selected;
    public ObjectType objectType;
    public GunType gunType;
    public GameObject clickedObj;
    public ObjectClickedEvent(BulletType type, ObjectType objType, GunType gunType, GameObject clicked, bool selected)
    {
        this.type = type;
        this.selected = selected;
        objectType = objType;
        this.gunType = gunType;
        this.clickedObj = clicked;
    }
}

public class CanGrabNowEvent:GameEvent
{

}
public class GetDotEvent : GameEvent
{
    public GameObject dot;
    public GetDotEvent(GameObject dot)
    {
        this.dot=dot;
    }

}

public class BulletHitEvent : GameEvent
{
    public GameObject bullet, hit;
    public BulletHitEvent(GameObject bull, GameObject hi)
    {
        this.bullet = bull;
        this.hit = hi;
    }
}
public class ChangeSceneEvent : GameEvent
{

}
public class NextTutorial : GameEvent
{
    public string text;
    public TextMeshProUGUI textMesh;
    public NextTutorial(string text, TextMeshProUGUI textMesh)
    {
        this.textMesh=textMesh;
        this.text = text;
    }
}
