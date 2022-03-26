using UnityEngine;

public class BombInstaller : MonoBehaviour
{
    [SerializeField] private Bomb _bomb;

    public void InstantiateBomb()
    {
        Instantiate(_bomb, transform.position, Quaternion.identity);
    }
}
