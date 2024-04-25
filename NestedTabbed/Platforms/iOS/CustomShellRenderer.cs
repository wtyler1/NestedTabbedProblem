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
    #region Old Renderer
    //public class CustomShellRenderer : ShellRenderer
    //{
    //    protected override IShellSectionRenderer CreateShellSectionRenderer(ShellSection shellSection)
    //    {
    //        var shellSectionRenderer = new CustomShellSectionRenderer(this);
    //        return shellSectionRenderer;
    //    }
    //}
    //public class CustomShellSectionRenderer : ShellSectionRenderer
    //{

    //    public CustomShellSectionRenderer(IShellContext context) : base(context)
    //    { }

    //    protected override IShellSectionRootRenderer CreateShellSectionRootRenderer(ShellSection shellSection, IShellContext shellContext)
    //    {
    //        var renderer = new CustomShellSectionRootRenderer(shellSection, shellContext);


    //        return renderer;
    //    }
    //}
    //public class CustomShellSectionRootRenderer : ShellSectionRootRenderer
    //{
    //    public CustomShellSectionRootRenderer(ShellSection section, IShellContext context) : base(section, context)
    //    {



    //    }

    //    protected override IShellSectionRootHeader CreateShellSectionRootHeader(IShellContext shellContext)
    //    {

    //        var renderer = new CustomShellSectionRootHeader(shellContext);
    //        return renderer;
    //    }
    //}
    //public class CustomShellSectionRootHeader : ShellSectionRootHeader
    //{
    //    public CustomShellSectionRootHeader(IShellContext context) : base(context)
    //    {

    //    }



    //    public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
    //    {
    //        var cell = base.GetCell(collectionView, indexPath) as ShellSectionHeaderCell;

    //        if (cell == null)
    //            return cell;

    //        // var headerCell = reusedCell as ShellSectionHeaderCell;


    //        var layout = new UICollectionViewFlowLayout();
    //        // var layout = collectionView.CollectionViewLayout as UICollectionViewFlowLayout;
    //        layout.SectionInset = new UIEdgeInsets(top: 0, left: 10, bottom: 0, right: 0);
    //        layout.ScrollDirection = UICollectionViewScrollDirection.Horizontal;

    //        var collectionViewHeight = collectionView.Bounds.Height;
    //        var sectionInsets = layout.SectionInset;
    //        var contentInsets = collectionView.ContentInset;
    //        var maximumItemHeight = collectionViewHeight - sectionInsets.Top - sectionInsets.Bottom - contentInsets.Top - contentInsets.Bottom;

    //        layout.ItemSize = new SizeF(120, (float)maximumItemHeight);
    //        collectionView.ContentInsetAdjustmentBehavior = UIScrollViewContentInsetAdjustmentBehavior.Never;


    //        //var layout = new UICollectionViewFlowLayout();
    //        //layout.MinimumInteritemSpacing = UIScreen.MainScreen.Bounds.Size.Width / 5;
    //        //layout.SectionInset = new UIEdgeInsets(top: 0, left: 10, bottom: 0, right: 0);
    //        //layout.ItemSize = new SizeF(120, 50);
    //        //layout.ScrollDirection = UICollectionViewScrollDirection.Horizontal;


    //        collectionView.CollectionViewLayout = layout;




    //        return cell;
    //    }
    //}
    //public class CustomUICollectionViewFlowLayout : UICollectionViewFlowLayout
    //{
    //    public override UICollectionViewLayoutAttributes[] LayoutAttributesForElementsInRect(CGRect rect)
    //    {
    //        return base.LayoutAttributesForElementsInRect(rect);

    //    }

    //}
    #endregion

    #region Fixed Renderer
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
        int count;
        public bool IsSelected { get; set; }
        List<string> imgs = new List<string>();
        public CustomShellSectionRootHeader(IShellContext context) : base(context)
        {
            imgs = new List<string>()
            {
                "dotnet_bot.png","dotnet_bot.png","dotnet_bot.png"
            };
            DeviceDisplay.Current.MainDisplayInfoChanged += Current_MainDisplayInfoChanged;
        }

        private void Current_MainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
        {
            this.CollectionView.ReloadData();
        }


        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            // var headerCell = reusedCell as ShellSectionHeaderCell;
            var layout = new UICollectionViewFlowLayout();
            // var layout = collectionView.CollectionViewLayout as UICollectionViewFlowLayout;
            layout.SectionInset = new UIEdgeInsets(top: 0, left: 0, bottom: 0, right: 0);
            layout.ScrollDirection = UICollectionViewScrollDirection.Horizontal;
            layout.MinimumInteritemSpacing = 0;
            layout.MinimumLineSpacing = 0;
            layout.ItemSize = new SizeF((float)UIScreen.MainScreen.Bounds.Width / count, (float)50);
            CollectionView.ContentInsetAdjustmentBehavior = UIScrollViewContentInsetAdjustmentBehavior.Never;
            CollectionView.CollectionViewLayout = layout;
            Console.WriteLine(CollectionView.Bounds);
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            CollectionView.Frame = new CGRect(0, 0, CollectionView.Frame.Size.Width, 50);
        }

        NSIndexPath selectedIndexPath;
        public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
        {
            base.ItemSelected(collectionView, indexPath);
            if (selectedIndexPath != null)
            {
                var previousCell = collectionView.CellForItem(selectedIndexPath) as ShellSectionHeaderCell;
                previousCell.BackgroundColor = UIColor.Green;
            }

            var selectedCell = collectionView.CellForItem(indexPath) as ShellSectionHeaderCell;

            selectedCell.BackgroundColor = UIColor.Yellow;

            selectedIndexPath = indexPath;
        }

        public override nint GetItemsCount(UICollectionView collectionView, nint section)
        {

            count = (int)base.GetItemsCount(collectionView, section);
            collectionView.AllowsMultipleSelection = false;
            return base.GetItemsCount(collectionView, section);
        }

        public override UICollectionReusableView GetViewForSupplementaryElement(UICollectionView collectionView, NSString elementKind, NSIndexPath indexPath)
        {
            if (elementKind == UICollectionElementKindSection.Footer.ToString())
            {
                return null;
            }
            return base.GetViewForSupplementaryElement(collectionView, elementKind, indexPath);
        }

        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = base.GetCell(collectionView, indexPath) as ShellSectionHeaderCell;

            if (cell == null)
                return cell;

            cell.Label.TextColor = UIColor.Black;
            foreach (var item in cell.Subviews)
            {
                Console.WriteLine(item);
            }
            cell.Frame = new CGRect(0, 0, (float)CollectionView.Bounds.Width, 50);

            cell.Label.TextColor = UIColor.Clear;
            cell.Label.Hidden = true;
            UILabel label = new UILabel(new CGRect((float)CollectionView.Bounds.Width / count / 2 - 60, 25, 120, 25));
            label.Text = cell.Label.Text;
            label.TextAlignment = UITextAlignment.Center;
            label.TextColor = UIColor.Black;
            label.Font = UIFont.SystemFontOfSize(13);
            cell.AddSubview(label);


            UIImageView imageView = new UIImageView(new CGRect((float)CollectionView.Bounds.Width / count / 2 - 12.5, 0, 25, 25));
            imageView.ContentMode = UIViewContentMode.ScaleAspectFit;
            imageView.Image = new UIImage(imgs[indexPath.Row]);
            cell.AddSubview(imageView);
            cell.BackgroundColor = UIColor.Green;

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

    #endregion
}
