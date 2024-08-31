using UnityEngine;

public class FrogMentalHealthEffects : MentalHealthEffects
{
    [SerializeField] GameObject randomIconPrefab;

    public override void TriggerFirstActMentalHealthEffects()
    {
        Roller.instance.AddImagePrefab(ImageType.Poison,randomIconPrefab);
    }

    public override void TriggerSecondActMentalHealthEffects()
    {
       Roller.instance.AddImagePrefab(ImageType.Poison,randomIconPrefab);
    }
    public override void TriggerThirdActMentalHealthEffects()
    {
        Roller.instance.AddImagePrefab(ImageType.Poison,randomIconPrefab);
    }

}
