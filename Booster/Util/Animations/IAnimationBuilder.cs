namespace Booster.Util.Animations
{
    public interface IAnimationBuilder
    {
        void BuildTexture();
        void BuildFrames();
        void BuildDestinationRect();
        void BuildScale();
        void BuildLooping();
        void BuildLayerDepth();
        Animation GetProduct();
    }
}