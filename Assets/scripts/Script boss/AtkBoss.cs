using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtkBoss : MonoBehaviour
{
    public Transform[] transforms;
    public GameObject Flame;
    public float TimeToShoot, Countdown;
    public float TimeToTP, CountdownToTP;

    public float BossHP, currentHP;
    public Image HPimage;

    private void Start()
    {
        var initialPosition = Random.Range(0, transforms.Length);
        transform.position = transforms[initialPosition].position;
        Countdown = TimeToShoot;
        CountdownToTP = TimeToTP;

    }
    private void Update()
    {
        countdowns();
        DamageBoss();
    }
    public void countdowns()
    {
        Countdown -= Time.deltaTime;
        CountdownToTP -= Time.deltaTime;
        if (Countdown > 0)
        {
            DisparoPlayer();
            Countdown = TimeToShoot;
            Teleport();
        }

        if (CountdownToTP <= 0)
        {
            CountdownToTP = TimeToTP;
            Teleport();
        }
    }
    public void DisparoPlayer()
    {
        GameObject spell = Instantiate(Flame, transform.position, Quaternion.identity);

    }

    public void Teleport()
    {
        var initialPosition = Random.Range(0, transforms.Length);
        transform.position = transforms[initialPosition].position;
    }
    public void DamageBoss()
    {
        currentHP = GetComponent<Esqueleto>().hp;
        HPimage.fillAmount = currentHP / BossHP;
    }

}
