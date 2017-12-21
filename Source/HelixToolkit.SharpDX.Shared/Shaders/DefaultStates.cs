﻿using SharpDX.Direct3D11;

#if !NETFX_CORE
namespace HelixToolkit.Wpf.SharpDX.Shaders
#else
namespace HelixToolkit.UWP.Shaders
#endif
{
    using Model;


    /// <summary>
    /// 
    /// </summary>
    public static class DefaultBlendStateDescriptions
    {
        public readonly static BlendStateDescription BSNormal;
        public readonly static BlendStateDescription NoBlend;
        public readonly static BlendStateDescription BSOverlayBlending;
        static DefaultBlendStateDescriptions()
        {
            BSNormal.RenderTarget[0] = new RenderTargetBlendDescription()
            {
                AlphaBlendOperation = BlendOperation.Add,
                BlendOperation = BlendOperation.Add,
                DestinationBlend = BlendOption.InverseSourceAlpha,
                SourceBlend = BlendOption.SourceAlpha,
                DestinationAlphaBlend = BlendOption.DestinationAlpha,
                SourceAlphaBlend = BlendOption.SourceAlpha,
                IsBlendEnabled = true,
                RenderTargetWriteMask = ColorWriteMaskFlags.All
            };

            NoBlend.RenderTarget[0] = new RenderTargetBlendDescription() { IsBlendEnabled = false };
            BSOverlayBlending.RenderTarget[0] = new RenderTargetBlendDescription()
            {
                IsBlendEnabled = true,
                SourceBlend = BlendOption.One,
                DestinationBlend = BlendOption.One,
                BlendOperation = BlendOperation.Add,
                SourceAlphaBlend = BlendOption.Zero,
                DestinationAlphaBlend = BlendOption.One,
                AlphaBlendOperation = BlendOperation.Add,
                RenderTargetWriteMask = ColorWriteMaskFlags.All
            };
        }
    }

    public static class DefaultDepthStencilDescriptions
    {
        public readonly static DepthStencilStateDescription DSSDepthLess = new DepthStencilStateDescription()
        {
            IsDepthEnabled = true,
            DepthWriteMask = DepthWriteMask.All,
            DepthComparison = Comparison.Less,
            IsStencilEnabled = false
        };

        public readonly static DepthStencilStateDescription DSSDepthLessEqual = new DepthStencilStateDescription()
        {
            IsDepthEnabled = true,
            DepthWriteMask = DepthWriteMask.All,
            DepthComparison = Comparison.LessEqual,
            IsStencilEnabled = false
        };

        public readonly static DepthStencilStateDescription DSSLessNoWrite = new DepthStencilStateDescription()
        {
            IsDepthEnabled = true,
            DepthWriteMask = DepthWriteMask.Zero,
            DepthComparison = Comparison.Less,
            IsStencilEnabled = false
        };

        public readonly static DepthStencilStateDescription DSSLessEqualNoWrite = new DepthStencilStateDescription()
        {
            IsDepthEnabled = true,
            DepthWriteMask = DepthWriteMask.Zero,
            DepthComparison = Comparison.LessEqual,
            IsStencilEnabled = false
        };

        public readonly static DepthStencilStateDescription DSSGreaterNoWrite = new DepthStencilStateDescription()
        {
            IsDepthEnabled = true,
            DepthWriteMask = DepthWriteMask.Zero,
            DepthComparison = Comparison.Greater
        };

        public readonly static DepthStencilStateDescription DSSClipPlaneBackface = new DepthStencilStateDescription()
        {
            IsDepthEnabled = true,
            IsStencilEnabled = true,
            DepthWriteMask = DepthWriteMask.Zero,
            DepthComparison = Comparison.Less,
            StencilWriteMask = 0xFF,
            StencilReadMask = 0,
            BackFace = new DepthStencilOperationDescription()
            {
                PassOperation = StencilOperation.Replace,
                Comparison = Comparison.Always,
                DepthFailOperation = StencilOperation.Keep,
                FailOperation = StencilOperation.Keep
            },
            FrontFace = new DepthStencilOperationDescription()
            {
                PassOperation = StencilOperation.Keep,
                Comparison = Comparison.Never,
                DepthFailOperation = StencilOperation.Keep,
                FailOperation = StencilOperation.Keep
            }
        };

        public readonly static DepthStencilStateDescription DSSClipPlaneFillQuad = new DepthStencilStateDescription()
        {
            IsDepthEnabled = false,
            IsStencilEnabled = true,
            DepthWriteMask = DepthWriteMask.Zero,
            DepthComparison = Comparison.Less,
            FrontFace = new DepthStencilOperationDescription()
            {
                FailOperation = StencilOperation.Keep,
                DepthFailOperation = StencilOperation.Keep,
                PassOperation = StencilOperation.Keep,
                Comparison = Comparison.Equal
            },
            BackFace = new DepthStencilOperationDescription()
            {
                Comparison = Comparison.Never,
                FailOperation = StencilOperation.Keep,
                DepthFailOperation = StencilOperation.Keep,
                PassOperation = StencilOperation.Keep
            },
            StencilReadMask = 0xFF,
            StencilWriteMask = 0
        };
    }

    public static class DefaultRasterDescriptions
    {

    }
}