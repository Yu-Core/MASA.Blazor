﻿using AngleSharp.Css.Dom;
using Bunit;

namespace Masa.Blazor.Test.NavigationDrawer
{
    [TestClass]
    public class MNavigationDrawerTests : TestBase
    {
        [TestMethod]
        public void RenderNavigationDrawerWithAbsolute()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(navigationdrawer => navigationdrawer.Absolute, true);
            });
            var classes = cut.Instance.GetClass();
            var hasAbsoluteClass = classes.Contains("m-navigation-drawer");

            // Assert
            Assert.IsTrue(hasAbsoluteClass);
        }

        [TestMethod]
        public void RenderNavigationDrawerWithApp()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(navigationdrawer => navigationdrawer.App, true);
            });
            var classes = cut.Instance.GetClass();
            var hasAppClass = classes.Contains("m-navigation-drawer");

            // Assert
            Assert.IsTrue(hasAppClass);
        }

        [TestMethod]
        public void RenderNavigationDrawerWithBottom()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(navigationdrawer => navigationdrawer.Bottom, true);
            });
            var classes = cut.Instance.GetClass();
            var hasBottomClass = classes.Contains("m-navigation-drawer");

            // Assert
            Assert.IsTrue(hasBottomClass);
        }

        [TestMethod]
        public void RenderNavigationDrawerWithClipped()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(navigationdrawer => navigationdrawer.Clipped, true);
            });
            var classes = cut.Instance.GetClass();
            var hasClippedClass = classes.Contains("m-navigation-drawer");

            // Assert
            Assert.IsTrue(hasClippedClass);
        }

        [TestMethod]
        public void RenderNavigationDrawerWithDark()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(navigationdrawer => navigationdrawer.Dark, true);
            });
            var classes = cut.Instance.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderNavigationDrawerWithDisableResizeWatcher()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(navigationdrawer => navigationdrawer.DisableResizeWatcher, true);
            });
            var classes = cut.Instance.GetClass();
            var hasDisableResizeWatcherClass = classes.Contains("m-navigation-drawer");

            // Assert
            Assert.IsTrue(hasDisableResizeWatcherClass);
        }

        [TestMethod]
        public void RenderNavigationDrawerWithDisableRouteWatcher()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(navigationdrawer => navigationdrawer.DisableRouteWatcher, true);
            });
            var classes = cut.Instance.GetClass();
            var hasDisableRouteWatcherClass = classes.Contains("m-navigation-drawer");

            // Assert
            Assert.IsTrue(hasDisableRouteWatcherClass);
        }

        [TestMethod]
        public void RenderNavigationDrawerWithExpandOnHover()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(navigationdrawer => navigationdrawer.ExpandOnHover, true);
            });
            var classes = cut.Instance.GetClass();
            var hasExpandOnHoverClass = classes.Contains("m-navigation-drawer");

            // Assert
            Assert.IsTrue(hasExpandOnHoverClass);
        }

        [TestMethod]
        public void RenderNavigationDrawerWithFixed()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(navigationdrawer => navigationdrawer.Fixed, true);
            });
            var classes = cut.Instance.GetClass();
            var hasFixedClass = classes.Contains("m-navigation-drawer");

            // Assert
            Assert.IsTrue(hasFixedClass);
        }

        [TestMethod]
        public void RenderNavigationDrawerWithFloating()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(navigationdrawer => navigationdrawer.Floating, true);
            });
            var classes = cut.Instance.GetClass();
            var hasFloatingClass = classes.Contains("m-navigation-drawer");

            // Assert
            Assert.IsTrue(hasFloatingClass);
        }

        [TestMethod]
        public void RenderWithHeight()
        {
            // Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(p => p.Height, 100);
            });
            var inputSlotDiv = cut.Find(".m-navigation-drawer");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.IsTrue(style.Contains("height: 100px;"));
        }

        [TestMethod]
        public void RenderNavigationDrawerWithLight()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(navigationdrawer => navigationdrawer.Light, true);
            });
            var classes = cut.Instance.GetClass();
            var hasLightClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderNavigationDrawerWithMiniVariant()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(navigationdrawer => navigationdrawer.MiniVariant, true);
            });
            var classes = cut.Instance.GetClass();
            var hasMiniVariantClass = classes.Contains("m-navigation-drawer");

            // Assert
            Assert.IsTrue(hasMiniVariantClass);
        }

        [TestMethod]
        public void RenderWithMiniVariantWidth()
        {
            // Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(p => p.MiniVariantWidth, 56);
                props.Add(p => p.MiniVariant, true);
            });
            var inputSlotDiv = cut.Find(".m-navigation-drawer");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.IsTrue(style.Contains("width: 56px;"));
        }

        [TestMethod]
        public void RenderNavigationDrawerWithPermanent()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(navigationdrawer => navigationdrawer.Permanent, true);
            });
            var classes = cut.Instance.GetClass();
            var hasPermanentClass = classes.Contains("m-navigation-drawer");

            // Assert
            Assert.IsTrue(hasPermanentClass);
        }

        [TestMethod]
        public void RenderNavigationDrawerWithRight()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(navigationdrawer => navigationdrawer.Right, true);
            });
            var classes = cut.Instance.GetClass();
            var hasRightClass = classes.Contains("m-navigation-drawer");

            // Assert
            Assert.IsTrue(hasRightClass);
        }

        [TestMethod]
        public void RenderNavigationDrawerWithTemporary()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(navigationdrawer => navigationdrawer.Temporary, true);
            });
            var classes = cut.Instance.GetClass();
            var hasTemporaryClass = classes.Contains("m-navigation-drawer");

            // Assert
            Assert.IsTrue(hasTemporaryClass);
        }

        [TestMethod]
        public void RenderNavigationDrawerWithTouchless()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(navigationdrawer => navigationdrawer.Touchless, true);
            });
            var classes = cut.Instance.GetClass();
            var hasTouchlessClass = classes.Contains("m-navigation-drawer");

            // Assert
            Assert.IsTrue(hasTouchlessClass);
        }

        [TestMethod]
        public void RenderWithWidth()
        {
            // Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MNavigationDrawer>(props =>
            {
                props.Add(p => p.Width, 100);
            });
            var inputSlotDiv = cut.Find(".m-navigation-drawer");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.IsTrue(style.Contains("width: 100px;"));
        }
    }
}
