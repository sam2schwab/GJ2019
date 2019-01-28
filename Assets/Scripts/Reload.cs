using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reload : MonoBehaviour
{
    float barDisplay = 0f;
    public Sprite noPowerUpSprite;
    Vector2 pos;
    Vector2 size;
    public Texture2D weaponIconEmpty;
    RectTransform rectTransform;
    public GameObject canvas;
    RectTransform menuCanvasRectTransform;
    //float lastUse = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        menuCanvasRectTransform = canvas.GetComponent<RectTransform>();
        size = new Vector2(rectTransform.rect.width, rectTransform.rect.height);
        pos = new Vector2(rectTransform.position.x- size.x/2, menuCanvasRectTransform.rect.height- rectTransform.position.y-size.y/2);
    }

    void OnGUI()
    {

        // draw the filled-in part:
        GUI.contentColor = new Color(0, 0, 0, 0.6f);
        GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y * barDisplay));
        GUI.Box(new Rect(0, 0, size.x, size.y), weaponIconEmpty);
        GUI.EndGroup();
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetMouseButtonDown(0))
        //{
        //    barDisplay += 0.2f;
        //    if (barDisplay>=1)
        //    {
        //        UpdateWeaponSprite(noPowerUpSprite);
        //    }
        //}
        //if (Input.GetMouseButtonDown(1))
        //{
        //    UpdateWeaponSprite(test);
        //}
        //if (barDisplay < 1)
        //{
        //    barDisplay = (Time.time - lastUse) * 0.3f;
        //}
        //else if (Input.GetMouseButtonDown(0))
        //{
        //    barDisplay = 0;
        //    lastUse = Time.time;
        //    //Shoot
        //    GameManager.Instance.homeHealth -= 1;
        //    GameManager.Instance.score += 100;
        //}
        //else
        //{
        //    barDisplay = 1;
        //}
    }
    public void UpdateAmmo(float ammoPercent)
    {
        if (ammoPercent <= 0)
        {
            UpdateWeaponSprite(noPowerUpSprite);
        }
        barDisplay = 1- ammoPercent;
    }

    public void UpdateWeaponSprite(Sprite weaponSprite)
    {
        GetComponent<Image>().sprite = weaponSprite;
        barDisplay = 0;
    }
}
