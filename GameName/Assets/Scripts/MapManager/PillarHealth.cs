using UnityEngine;

public class StalpDestructibil : MonoBehaviour
{
    [Header("Setari Viata")]
    [Tooltip("Cate lovituri rezista stalpul inainte sa se sparga")]
    public int viata = 3;

    public void PrimesteLovitura(int damage)
    {
        viata -= damage;
        if (viata <= 0)
        {
            DistrugeStalpul();
        }
    }

    private void DistrugeStalpul()
    {
        Destroy(gameObject);
    }
}