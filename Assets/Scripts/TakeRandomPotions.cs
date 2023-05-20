using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeRandomPotions : MonoBehaviour
{
    public List<Potion> potions;
    void Start()
    {
        StartCoroutine(PlacePotions());
    }

    IEnumerator PlacePotions()
    {
        while(true)
        {
            yield return new WaitForSeconds(5f);
            Potion choosedPotion = potions[Random.Range(0, 2)];
            choosedPotion.transform.position = new Vector3(
                Random.Range(-20, 20),
                choosedPotion.transform.position.y,
                Random.Range(-20, 20)
                );
            Instantiate(choosedPotion);

        }
    }
    
    void Update()
    {
        
    }
}
