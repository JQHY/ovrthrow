using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerHealth : MonoBehaviour
{

    public bool Alive = true;

    public List<HealthEntity> HealthEs;

    public Sprite crossImg;
    public RectTransform HealthBar;
    public Gradient healthGrad;
    Image img;

    public float MaxHealth;
    public float Health;
    float maxWidth;

    bool Updating = true;
    

    // Start is called before the first frame update
    void Start()
    {
        maxWidth = HealthBar.rect.width;
        Debug.Log(maxWidth);
        if (HealthEs.Count > 0)
        {
            foreach (HealthEntity he in HealthEs)
            {
                MaxHealth += he.MaxHealth;
            }
        }

        Health = MaxHealth;
        img = HealthBar.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Health > 0.0f && Updating)
        {
            HealthBar.sizeDelta = new Vector2(maxWidth * (Health/MaxHealth), HealthBar.sizeDelta.y);
            img.color = healthGrad.Evaluate(Health / MaxHealth);
        }
        else if (Updating)
        {
            HealthBar.sizeDelta = new Vector2(HealthBar.sizeDelta.y, HealthBar.sizeDelta.y);
            img.sprite = crossImg;
            Updating = false;
            Alive = false;
        }

        if (Input.GetKeyDown(KeyCode.H)) Damage(10f);

    }

    public void Damage(float d)
    {
        Health -= d;
    }
}
