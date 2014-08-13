namespace Booster.Util.Animations
{
    public class AnimationCreator
    {
        public void Construct(IAnimationBuilder animationBuilder)
        {
            animationBuilder.BuildTexture();
            animationBuilder.BuildFrames();
            animationBuilder.BuildDestinationRect();
            animationBuilder.BuildScale();
            animationBuilder.BuildLooping();
            animationBuilder.BuildLayerDepth();
        }
    }
}