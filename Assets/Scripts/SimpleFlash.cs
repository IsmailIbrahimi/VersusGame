using System.Collections;

using UnityEngine;

namespace SpriteFlash
{
    public class SimpleFlash : MonoBehaviour
    {
        #region Datamembers

        #region Editor Settings

        [Tooltip("Material to switch to during the flash.")]
        [SerializeField] private Material flashMaterial;

        [Tooltip("Duration of the flash.")]
        [SerializeField] private float duration;

        [Tooltip("Sprite to display during the flash.")]
        [SerializeField] private Sprite flashSprite;

        [Tooltip("Number of times to flash.")]
        [SerializeField] private int flashCount = 3;

        [Tooltip("Duration to keep the sprite after flashing.")]
        [SerializeField] private float spriteDisplayDuration = 1f;

        #endregion
        #region Private Fields

        private SpriteRenderer spriteRenderer;

        private Material originalMaterial;

        private Sprite originalSprite;

        private Coroutine flashRoutine;

        #endregion

        #endregion


        #region Methods

        #region Unity Callbacks

        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            originalMaterial = spriteRenderer.material;
            originalSprite = spriteRenderer.sprite;
        }

        #endregion

        public void Flash()
        {
            if (flashRoutine != null)
            {
                StopCoroutine(flashRoutine);
            }

            flashRoutine = StartCoroutine(FlashRoutine());
        }

        private IEnumerator FlashRoutine()
        {
            for (int i = 0; i < flashCount; i++)
            {
                spriteRenderer.material = flashMaterial;
                if (flashSprite != null)
                {
                    spriteRenderer.sprite = flashSprite;
                }

                yield return new WaitForSeconds(duration);

                spriteRenderer.material = originalMaterial;
                spriteRenderer.sprite = originalSprite;

                yield return new WaitForSeconds(duration);
            }

            if (flashSprite != null)
            {
                spriteRenderer.sprite = flashSprite;
            }

            yield return new WaitForSeconds(spriteDisplayDuration);

            spriteRenderer.material = originalMaterial;
            spriteRenderer.sprite = originalSprite;

            flashRoutine = null;
        }

        #endregion
    }
}