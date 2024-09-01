using UnityEngine;

public class CatMentalHealthEffects : MentalHealthEffects
{
    
    [SerializeField] GameObject emptyIconPrefab;

    public override void TriggerFirstActMentalHealthEffects()
    {
        Roller.instance.AddImagePrefab(ImageType.Empty,emptyIconPrefab);
    }

    public override void TriggerSecondActMentalHealthEffects()
    {
       Roller.instance.AddImagePrefab(ImageType.Empty,emptyIconPrefab);
    }
    public override void TriggerThirdActMentalHealthEffects()
    {
        Roller.instance.AddImagePrefab(ImageType.Empty,emptyIconPrefab);
    }
}
