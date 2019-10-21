using HCFramework;


public class ObjectClickedEvent : GameEvent
{
    public BulletType type;
    public bool selected;
    public ObjectClickedEvent(BulletType type,bool selected)
    {
        this.type = type;
        this.selected = selected;
    }
}
public class BulletHitEvent:GameEvent
{

}
