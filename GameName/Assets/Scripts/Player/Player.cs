public class Player : Entity
{
    protected override void Start(){
        base.Start();
    }

    protected override void Die(){
        //Debug.Log("Player died!");
        base.Die();
    }
}