using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class HealthBarScreenSpaceController : MonoBehaviour
{
    private Slider healthBarSlider;

    [Header("Health Properties")]
    [Range(0, 100)]
    public float maximumHealth = 100;
    [Range(1,100)]
    public float currentHealth = 100;
    

    // Start is called before the first frame update
    void Start()
    {
        healthBarSlider = GetComponent<Slider>();
        currentHealth = maximumHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(10);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
    }

    public void TakeDamage(float damage)
    {
        healthBarSlider.value -= damage;
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            healthBarSlider.value = 0;
            currentHealth = 0;
        }
    }

    public void Reset()
    {
        healthBarSlider.value = maximumHealth;
        currentHealth = maximumHealth;
    }
}
