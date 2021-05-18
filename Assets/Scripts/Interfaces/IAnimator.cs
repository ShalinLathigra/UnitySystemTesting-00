using Pixel;

public interface IAnimator
{
    SquashAnimator squash { get; }
    PixelBoxAnimator pixel { get; }
}

// IAnimator