using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class _DanceHandler : MonoBehaviour
{

    public _Avator avator;
    public Image bgImage;
    public Sprite defaultBGSprite;
    public DanceView danceView;

    List<AudioClip> audioClips;
    List<Sprite> sprites;
    List<AnimationClip> animationClips;

    AnimatorOverrideController overrideConttroller;

    bool isDance = false;

    void Awake()
    {

        audioClips = new List<AudioClip>();
        sprites = new List<Sprite>();
        animationClips = new List<AnimationClip>();

        audioClips.AddRange(Resources.LoadAll<AudioClip>("DanceMusic"));

        sprites.AddRange(Resources.LoadAll<Sprite>("DanceBackground"));

        animationClips.AddRange(Resources.LoadAll<AnimationClip>("DanceAnimationClip"));

        RuntimeAnimatorController runtimeController = GetComponent<Animator>().runtimeAnimatorController;
        overrideConttroller = new AnimatorOverrideController();
        overrideConttroller.name = "Zero Override Animator";
        overrideConttroller.runtimeAnimatorController = runtimeController;

        /*LogOverrideController();*/

        if (GetComponent<Animator>().enabled)
            GetComponent<Animator>().enabled = false;

    }

    public void DoPose()
    {
        isDance = false;

        //danceView.OnInit();

        GetComponent<AudioSource>().Stop();

        bgImage.sprite = defaultBGSprite;

        if (GetComponent<Animator>().enabled)
            GetComponent<Animator>().enabled = false;

        avator.ResetHandsToLastItem();
        /*HandsPosture.SetCurPosture();*/
    }

    void DoDance(int id)
    {
        isDance = true;

        avator.StoreHandsItemAndSetToDefault();

        if (!GetComponent<Animator>().enabled)
            GetComponent<Animator>().enabled = true;

        GetComponent<Animator>().runtimeAnimatorController = overrideConttroller;
        overrideConttroller["Take 001"] = animationClips[id];
        LogOverrideController();

        GetComponent<AudioSource>().clip = audioClips[id];
        GetComponent<AudioSource>().loop = true;
        GetComponent<AudioSource>().Play();

        bgImage.sprite = sprites[id];

    }

    private void HandleOnUpdateDance(_SourceItemData item, bool toSelect)
    {
        if (toSelect)
        {
            if (isDance)
                DoPose();

            DoDance(item.id);

            /*DoIdleAnimation();*/
        }
        else
        {
            DoPose();

        }




    }

    private void HandleOnExitDanceView()
    {
        DoPose();
    }

    private void HandleOnEnterDanceView()
    {
        /*DoIdleAnimation();*/
        /*DoHandsPosture();*/
        /*avator.OnDefaultHands();*/
    }

    void OnEnable()
    {
        _UIController.OnUpdateDance += HandleOnUpdateDance;
        _UIController.OnEnterDanceView += HandleOnEnterDanceView;
        _UIController.OnExitDanceView += HandleOnExitDanceView;
    }

    void OnDisable()
    {
        _UIController.OnUpdateDance -= HandleOnUpdateDance;
        _UIController.OnEnterDanceView -= HandleOnEnterDanceView;
        _UIController.OnExitDanceView -= HandleOnExitDanceView;
    }

    /// <summary>
    /// This is the event on the end of animation clip.
    /// </summary>
    public void OnDanceEnd()
    {
        /*isDancing = false;*/
        /*GetComponent<AudioSource>().Stop();*/
        DoPose();
        danceView.OnInit();
        /*DoIdleAnimation();*/
        Debug.Log("DANCE IS END!!!");
    }

    /// <summary>
    /// This is the event at the begin of animation clip.
    /// </summary>
    public void OnDanceBegin()
    {
        Debug.Log("DANCE IS Begin!!!");
        //         GetComponent<AudioSource>().clip = audioClips[DanceID];
        //         GetComponent<AudioSource>().Play();
    }

    void LogOverrideController()
    {
        AnimationClipPair[] clipPair = overrideConttroller.clips;

        Debug.Log("ClipPair's Length :" + clipPair.Length);

        int i = 0;
        while (i < clipPair.Length)
        {
            if (clipPair[i].originalClip != null)
                Debug.Log(string.Format("Original Clip {0} : {1}", i, clipPair[i].originalClip.name));
            if (clipPair[i].overrideClip != null)
                Debug.Log(string.Format("Override Clip {0} : {1}", i, clipPair[i].overrideClip.name));
            i++;
        }

    }
}