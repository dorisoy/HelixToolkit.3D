﻿using HelixToolkit.SharpDX.Core2D;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.WIC;
using Bitmap = SharpDX.Direct2D1.Bitmap;

namespace HelixToolkit.SharpDX.Model.Scene2D;

public class ImageNode2D : SceneNode2D
{
    private Stream? imageStream;

    public Stream? ImageStream
    {
        set
        {
            if (SetAffectsMeasure(ref imageStream, value))
            {
                bitmapChanged = true;
            }
        }
        get
        {
            return imageStream;
        }
    }

    public float Opacity
    {
        set
        {
            if (RenderCore is ImageRenderCore2D core)
            {
                core.Opacity = value;
            }
        }
        get
        {
            if (RenderCore is ImageRenderCore2D core)
            {
                return core.Opacity;
            }

            return 0.0f;
        }
    }

    protected bool bitmapChanged { private set; get; } = true;

    protected override RenderCore2D CreateRenderCore()
    {
        return new ImageRenderCore2D();
    }

    protected override bool OnAttach(IRenderHost host)
    {
        if (base.OnAttach(host))
        {
            bitmapChanged = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void LoadBitmap(RenderContext2D context, Stream? stream)
    {
        if (RenderCore is ImageRenderCore2D core)
        {
            core.Bitmap = stream == null ? null : OnLoadImage(context, stream);
        }
    }

    protected virtual Bitmap? OnLoadImage(RenderContext2D context, Stream stream)
    {
        if (context.DeviceResources is null)
        {
            return null;
        }

        stream.Position = 0;
        using var decoder = new BitmapDecoder(context.DeviceResources.WICImgFactory, stream, DecodeOptions.CacheOnLoad);
        using var frame = decoder.GetFrame(0);
        using var converter = new FormatConverter(context.DeviceResources.WICImgFactory);
        converter.Initialize(frame, global::SharpDX.WIC.PixelFormat.Format32bppPBGRA);
        return Bitmap1.FromWicBitmap(context.DeviceContext, converter);
    }

    public override void Update(RenderContext2D context)
    {
        base.Update(context);
        if (bitmapChanged)
        {
            LoadBitmap(context, ImageStream);
            bitmapChanged = false;
        }
    }

    protected override Vector2 MeasureOverride(Vector2 availableSize)
    {
        if (ImageStream != null)
        {
            var imageSize = (RenderCore as ImageRenderCore2D)?.ImageSize ?? Vector2Helper.Zero;
            imageSize.X *= DpiScale;
            imageSize.Y *= DpiScale;
            if (Width == 0 && Height == 0)
            {
                return new Vector2(Math.Min(availableSize.X, imageSize.X), Math.Min(availableSize.Y, imageSize.Y));
            }
            else if (imageSize.X == 0 || imageSize.Y == 0)
            {
                return availableSize;
            }
            else
            {
                var aspectRatio = imageSize.X / imageSize.Y;
                if (Width == 0)
                {
                    var height = Math.Min(availableSize.Y, Height) * DpiScale;
                    return new Vector2(height / aspectRatio, height);
                }
                else
                {
                    var width = Math.Min(availableSize.Y, Width) * DpiScale;
                    return new Vector2(width, width * aspectRatio);
                }
            }
        }
        return new Vector2(Math.Max(0, Width * DpiScale), Math.Max(0, Height * DpiScale));
    }

    protected override bool OnHitTest(ref Vector2 mousePoint, out HitTest2DResult? hitResult)
    {
        hitResult = null;
        if (LayoutBoundWithTransform.Contains(mousePoint))
        {
            hitResult = new HitTest2DResult(this);
            return true;
        }
        else
        {
            return false;
        }
    }
}
