using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;
        //the game objects canvas group local variable
        public void Start ()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            //gets the canvas group component and assigns it as a local variable.
            StartCoroutine(FadeOutIn());
            //Calls the FadeOutIn() Function, fading the screen out and in
        }

        public IEnumerator FadeOutIn()
            //nested coroutines to fade out and in
        {
            yield return FadeOut(3f);
            //fades out, waits for a return call
            print("Faded out");
            yield return FadeIn(1.5f);
            //fades in after the fade out and print have completed
            print("Faded in");
        }

        public IEnumerator FadeOut(float time)
        {
            while (canvasGroup.alpha < 1)
                //while alpha less than 1
            {
                canvasGroup.alpha += Time.deltaTime / time;
                //sets the alpha to fade to 1 over the passed amount of time
                yield return null;
                //every IEnum needs a return
            }
        }

        public IEnumerator FadeIn(float time)
        {
            while (canvasGroup.alpha > 0)
            //while alpha more than 0
            {
                canvasGroup.alpha -= Time.deltaTime / time;
                //sets the alpha to fade to 0 over the passed amount of time
                yield return null;
                //every IEnum needs a return
            }
        }

    }
}
