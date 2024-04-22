using CoreGraphics;
using Foundation;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform.Compatibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Maui.Controls.Platform.Compatibility.ShellSectionRootHeader;
using UIKit;

namespace NestedTabbed.Platforms.iOS
{
    public class CustomShellRenderer : ShellRenderer
    {
        protected override IShellSectionRenderer CreateShellSectionRenderer(ShellSection shellSection)
        {
            var shellSectionRenderer = new CustomShellSectionRenderer(this);
            return shellSectionRenderer;
        }
    }
    public class CustomShellSectionRenderer : ShellSectionRenderer
    {

        public CustomShellSectionRenderer(IShellContext context) : base(context)
        { }

        protected override IShellSectionRootRenderer CreateShellSectionRootRenderer(ShellSection shellSection, IShellContext shellContext)
        {
            var renderer = new CustomShellSectionRootRenderer(shellSection, shellContext);


            return renderer;
        }
    }
    public class CustomShellSectionRootRenderer : ShellSectionRootRenderer
    {
        public CustomShellSectionRootRenderer(ShellSection section, IShellContext context) : base(section, context)
        {



        }

        protected override IShellSectionRootHeader CreateShellSectionRootHeader(IShellContext shellContext)
        {

            var renderer = new CustomShellSectionRootHeader(shellContext);
            return renderer;
        }
    }
    public class CustomShellSectionRootHeader : ShellSectionRootHeader
    {
        public CustomShellSectionRootHeader(IShellContext context) : base(context)
        {

        }



        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = base.GetCell(collectionView, indexPath) as ShellSectionHeaderCell;

            if (cell == null)
                return cell;

            // var headerCell = reusedCell as ShellSectionHeaderCell;


            var layout = new UICollectionViewFlowLayout();
            // var layout = collectionView.CollectionViewLayout as UICollectionViewFlowLayout;
            layout.SectionInset = new UIEdgeInsets(top: 0, left: 10, bottom: 0, right: 0);
            layout.ScrollDirection = UICollectionViewScrollDirection.Horizontal;

            var collectionViewHeight = collectionView.Bounds.Height;
            var sectionInsets = layout.SectionInset;
            var contentInsets = collectionView.ContentInset;
            var maximumItemHeight = collectionViewHeight - sectionInsets.Top - sectionInsets.Bottom - contentInsets.Top - contentInsets.Bottom;

            layout.ItemSize = new SizeF(120, (float)maximumItemHeight);
            collectionView.ContentInsetAdjustmentBehavior = UIScrollViewContentInsetAdjustmentBehavior.Never;


            //var layout = new UICollectionViewFlowLayout();
            //layout.MinimumInteritemSpacing = UIScreen.MainScreen.Bounds.Size.Width / 5;
            //layout.SectionInset = new UIEdgeInsets(top: 0, left: 10, bottom: 0, right: 0);
            //layout.ItemSize = new SizeF(120, 50);
            //layout.ScrollDirection = UICollectionViewScrollDirection.Horizontal;


            collectionView.CollectionViewLayout = layout;






            return cell;
        }
    }
    public class CustomUICollectionViewFlowLayout : UICollectionViewFlowLayout
    {
        public override UICollectionViewLayoutAttributes[] LayoutAttributesForElementsInRect(CGRect rect)
        {
            return base.LayoutAttributesForElementsInRect(rect);

        }

    }
}
