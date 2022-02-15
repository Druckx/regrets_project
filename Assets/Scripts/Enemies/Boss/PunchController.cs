using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchController : MonoBehaviour
{
    [SerializeField] private BossController _bossController;
    [SerializeField] private LifeController _playerLife;
    public int damage;

    private void Awake()
    {
        _playerLife = FindObjectOfType<LifeController>();
        _bossController = GetComponent<BossController>();
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("LifeManager") && !_bossController.isFallen)
        {
            Vector3 hitDirection = transform.position;

            _playerLife.RecieveDamage(damage, hitDirection, true);
        }
    }
}
