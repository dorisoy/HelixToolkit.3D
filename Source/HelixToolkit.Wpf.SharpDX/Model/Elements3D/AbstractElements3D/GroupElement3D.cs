﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GroupElement3D.cs" company="Helix Toolkit">
//   Copyright (c) 2014 Helix Toolkit contributors
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace HelixToolkit.Wpf.SharpDX
{
    using System.Windows;
    using System.Windows.Markup;

    [ContentProperty("Children")]
    public abstract class GroupElement3D : Element3D //, IElement3DCollection
    {
        private Element3DCollection childrenInternal;
        public Element3DCollection Children
        {
            get { return (Element3DCollection)this.GetValue(ChildrenProperty); }
            set { this.SetValue(ChildrenProperty, value); }
        }

        public static readonly DependencyProperty ChildrenProperty =
            DependencyProperty.Register("Children", typeof(Element3DCollection), typeof(GroupElement3D),
                new FrameworkPropertyMetadata(new Element3DCollection(), FrameworkPropertyMetadataOptions.AffectsRender,
                    (d, e) => { (d as GroupElement3D).childrenInternal = e.NewValue as Element3DCollection; }));

        public GroupElement3D()
        {
            this.childrenInternal = this.Children;
        }

        protected override bool OnAttach(IRenderHost host)
        {
            foreach (var c in this.childrenInternal)
            {
                if (c.Parent == null)
                {
                    this.AddLogicalChild(c);                    
                }

                c.Attach(host);
            }
            return true;
        }

        protected override void OnDetach()
        {
            base.OnDetach();
            foreach (var c in this.childrenInternal)
            {
                c.Detach();
                if (c.Parent == this)
                {
                    this.RemoveLogicalChild(c);                    
                }
            }
        }

        protected override void OnRender(RenderContext context)
        {
            foreach (var c in this.childrenInternal)
            {
                c.Render(context);
            }
        }
    }
}