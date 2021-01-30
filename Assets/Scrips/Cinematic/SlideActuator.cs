using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SlideActuator : MonoBehaviour
{
    public Sprite[] SlideSprites;

    private Image ImageComponent;
    private int CurrentSprite;

    private void Start()
    {
        ImageComponent = GetComponent<Image>();
        ImageComponent.sprite = SlideSprites[CurrentSprite];
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (++CurrentSprite >= SlideSprites.Length)
            {
                SceneManager.LoadScene("GameScene");
            }

            ImageComponent.sprite = SlideSprites[CurrentSprite];
        }
    }
}
