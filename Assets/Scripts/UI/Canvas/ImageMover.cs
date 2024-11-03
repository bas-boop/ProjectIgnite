using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using NPC;

namespace UI.Canvas
{
    public class ImageMover : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float moveDuration = 3f;
        [SerializeField] private float fadeDuration = 1f;
        [SerializeField] private float moveUpDistance = 1f;

        [Header("References")]
        [SerializeField] private Transform movingImage;
        [SerializeField] private GameObject fadeImage;
        [SerializeField] private GameObject movingGameObject;
        [SerializeField] private List<SpriteRenderer> spriteRenderers;
        [SerializeField] private Button activateButton;
        [SerializeField] private Bobbing bobbing;

        private Vector3 _startPosition;
        private Vector3 _endPosition;

        private void Start()
        {
            _startPosition = new (-8.75f, movingImage.localPosition.y, movingImage.localPosition.z);
            _endPosition = new (8.75f, movingImage.localPosition.y, movingImage.localPosition.z);
            movingImage.localPosition = _startPosition;
        
            fadeImage.SetActive(false);
        
            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
            {
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);
            }
        
            activateButton.gameObject.SetActive(false);
        
            StartCoroutine(Sequence());
        }

        private IEnumerator Sequence()
        {
            // 1. Move the image from left to right
            yield return StartCoroutine(MoveImage());

            // 2. Set fadeImage active with fade in after 1 second
            yield return new WaitForSeconds(1f);
            yield return StartCoroutine(FadeInImage(fadeImage));

            // 3. Move the GameObject up and fade in spriteRenderers at the same time
            StartCoroutine(MoveGameObjectUp());
            yield return StartCoroutine(FadeInSpriteRenderers());

            // 4. Activate the button at the end
            activateButton.gameObject.SetActive(true);
            bobbing.enabled = true;
        }

        private IEnumerator MoveImage()
        {
            float elapsed = 0f;
            while (elapsed < moveDuration)
            {
                movingImage.localPosition = Vector3.Lerp(_startPosition, _endPosition, elapsed / moveDuration);
                elapsed += Time.deltaTime;
                yield return null;
            }
            movingImage.localPosition = _endPosition;
        }

        private IEnumerator FadeInImage(GameObject image)
        {
            CanvasGroup canvasGroup = image.GetComponent<CanvasGroup>();
        
            if (canvasGroup == null)
            {
                canvasGroup = image.AddComponent<CanvasGroup>();
            }
        
            image.SetActive(true);

            float elapsed = 0f;
            while (elapsed < fadeDuration)
            {
                canvasGroup.alpha = Mathf.Lerp(0, 1, elapsed / fadeDuration);
                elapsed += Time.deltaTime;
                yield return null;
            }
            canvasGroup.alpha = 1;
        }

        private IEnumerator MoveGameObjectUp()
        {
            Vector3 startPos = movingGameObject.transform.position;
            Vector3 endPos = startPos + Vector3.up * moveUpDistance;
            float elapsed = 0f;

            while (elapsed < fadeDuration)
            {
                movingGameObject.transform.position = Vector3.Lerp(startPos, endPos, elapsed / fadeDuration);
                elapsed += Time.deltaTime;
                yield return null;
            }
            movingGameObject.transform.position = endPos;
        }

        private IEnumerator FadeInSpriteRenderers()
        {
            float elapsed = 0f;
        
            while (elapsed < fadeDuration)
            {
                foreach (SpriteRenderer spriteRenderer in spriteRenderers)
                {
                    Color color = spriteRenderer.color;
                    color.a = Mathf.Lerp(0, 1, elapsed / fadeDuration);
                    spriteRenderer.color = color;
                }
            
                elapsed += Time.deltaTime;
                yield return null;
            }
        
            // Ensure all are fully visible
            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
            {
                Color color = spriteRenderer.color;
                color.a = 1;
                spriteRenderer.color = color;
            }
        }
    }
}
