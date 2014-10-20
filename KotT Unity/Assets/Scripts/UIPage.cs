using UnityEngine;
using System.Collections;

public class UIPage : MonoBehaviour
{
	public UIMenu Menu = null;
	
	//**************************************************
	// INITIALIZATION / DESTROY
	//**************************************************
	public virtual void OpenPage() {}
	public virtual void ClosePage() {}
	public virtual void PageBack() {}

#region GENERAL UI ANIMATIONS
    public void PlayUIAnimationForward(GameObject animationObject, string animationName)
    {
        if (animationObject.animation[animationName].time != animationObject.animation[animationName].length)
        {
            animationObject.animation[animationName].speed = 1;
            animationObject.animation.Play(animationName);
        }
    }

    public void PlayUIAnimationBackward(GameObject animationObject, string animationName)
    {
        if (animationObject.animation[animationName].time != animationObject.animation[animationName].length)
        {
            animationObject.animation[animationName].speed = -2;
            animationObject.animation[animationName].time = animationObject.animation[animationName].length;
            animationObject.animation.Play(animationName);
        }
    }

    public IEnumerator WaitToAdvanceToPage(UIMainMenu MainMenu, string pageName, float time)
    {
        yield return new WaitForSeconds(time);
        if (pageName == "GoBack")
        {
            MainMenu.GoBack();
        }
        else
        {
            MainMenu.GotoPage(pageName);
        }
    }
#endregion
#region Button Animations
    public void PlayIntroAnimations(GameObject breadcrumb = null, GameObject button1 = null, GameObject button2 = null,
                                    GameObject button3 = null, GameObject button4 = null, GameObject button5 = null,
                                    GameObject rightSideButton = null)
    {
        if (breadcrumb != null)
        {
            PlayUIAnimationForward(breadcrumb, "breadcrumbSlideIn");
        }
        if (button1 != null)
        {
            PlayUIAnimationForward(button1, "button1SlideIn");
        }
        if (button2 != null)
        {
            PlayUIAnimationForward(button2, "button2SlideIn");
        }
        if (button3 != null)
        {
            PlayUIAnimationForward(button3, "button3SlideIn");
        }
        if (button4 != null)
        {
            PlayUIAnimationForward(button4, "button4SlideIn");
        }
        if (button5 != null)
        {
            PlayUIAnimationForward(button5, "button5SlideIn");
        }
        if (rightSideButton != null)
        {
            PlayUIAnimationForward(rightSideButton, "buttonRightSlideIn");
        }
    }

    public void PlayOutroAnimations(GameObject breadcrumb = null, GameObject button1 = null, GameObject button2 = null,
                                    GameObject button3 = null, GameObject button4 = null, GameObject button5 = null,
                                    GameObject rightSideButton = null)
    {
        if (breadcrumb != null)
        {
            PlayUIAnimationBackward(breadcrumb, "breadcrumbSlideIn");
        }
        if (button1 != null)
        {
            PlayUIAnimationBackward(button1, "button1SlideIn");
        }
        if (button2 != null)
        {
            PlayUIAnimationBackward(button2, "button2SlideIn");
        }
        if (button3 != null)
        {
            PlayUIAnimationBackward(button3, "button3SlideIn");
        }
        if (button4 != null)
        {
            PlayUIAnimationBackward(button4, "button4SlideIn");
        }
        if (button5 != null)
        {
            PlayUIAnimationBackward(button5, "button5SlideIn");
        }
        if (rightSideButton != null)
        {
            PlayUIAnimationBackward(rightSideButton, "buttonRightSlideIn");
        }
    }
#endregion
}
