﻿using Bunit;

namespace Masa.Blazor.Test.Badge
{
    [TestClass]
    public class MBadgeTests : TestBase
    {
        [TestMethod]
        public void RenderBadgeWithAvatar()
        {
            //Act
            var cut = RenderComponent<MBadge>(props =>
            {
                props.Add(badge => badge.Avatar, true);
            });
            var classes = cut.Instance.GetClass();
            var hasAvatarClass = classes.Contains("m-badge--avatar");

            // Assert
            Assert.IsTrue(hasAvatarClass);
        }

        [TestMethod]
        public void RenderBadgeWithBordered()
        {
            //Act
            var cut = RenderComponent<MBadge>(props =>
            {
                props.Add(badge => badge.Bordered, true);
            });
            var classes = cut.Instance.GetClass();
            var hasBorderedClass = classes.Contains("m-badge--bordered");

            // Assert
            Assert.IsTrue(hasBorderedClass);
        }

        [TestMethod]
        public void RenderBadgeWithBottom()
        {
            //Act
            var cut = RenderComponent<MBadge>(props =>
            {
                props.Add(badge => badge.Bottom, true);
            });
            var classes = cut.Instance.GetClass();
            var hasBottomClass = classes.Contains("m-badge--bottom");

            // Assert
            Assert.IsTrue(hasBottomClass);
        }

        [TestMethod]
        public void RenderBadgeWithDot()
        {
            //Act
            var cut = RenderComponent<MBadge>(props =>
            {
                props.Add(badge => badge.Dot, true);
            });
            var classes = cut.Instance.GetClass();
            var hasDotClass = classes.Contains("m-badge--dot");

            // Assert
            Assert.IsTrue(hasDotClass);
        }

        [TestMethod]
        public void RenderBadgeWithDark()
        {
            //Act
            var cut = RenderComponent<MBadge>(props =>
            {
                props.Add(badge => badge.Dark, true);
            });
            var classes = cut.Instance.GetClass();
            var hasdarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasdarkClass);
        }

        [TestMethod]
        public void RenderBadgeWithInLine()
        {
            //Act
            var cut = RenderComponent<MBadge>(props =>
            {
                props.Add(badge => badge.Inline, true);
            });
            var classes = cut.Instance.GetClass();
            var hasInLineClass = classes.Contains("m-badge--inline");

            // Assert
            Assert.IsTrue(hasInLineClass);
        }

        [TestMethod]
        public void RenderBadgeWithLeft()
        {
            //Act
            var cut = RenderComponent<MBadge>(props =>
            {
                props.Add(badge => badge.Left, true);
            });
            var classes = cut.Instance.GetClass();
            var hasLeftClass = classes.Contains("m-badge--left");

            // Assert
            Assert.IsTrue(hasLeftClass);
        }

        [TestMethod]
        public void RenderBadgeWithLight()
        {
            //Act
            var cut = RenderComponent<MBadge>(props =>
            {
                props.Add(badge => badge.Light, true);
            });
            var classes = cut.Instance.GetClass();
            var hasLightClass = classes.Contains("theme--light");

            // Assert
            Assert.IsTrue(hasLightClass);
        }

        [TestMethod]
        public void RenderBadgeWithOverLap()
        {
            //Act
            var cut = RenderComponent<MBadge>(props =>
            {
                props.Add(badge => badge.OverLap, true);
            });
            var classes = cut.Instance.GetClass();
            var hasOverLapClass = classes.Contains("m-badge--overlap");

            // Assert
            Assert.IsTrue(hasOverLapClass);
        }

        [TestMethod]
        public void RenderBadgeWithTile()
        {
            //Act
            var cut = RenderComponent<MBadge>(props =>
            {
                props.Add(badge => badge.Tile, true);
            });
            var classes = cut.Instance.GetClass();
            var hasTileClass = classes.Contains("m-badge--tile");

            // Assert
            Assert.IsTrue(hasTileClass);
        }

        [TestMethod]
        public void RenderWithBadgeContent()
        {
            // Arrange & Act
            var cut = RenderComponent<MBadge>(props =>
            {
                props.Add(badge => badge.BadgeContent, "<span>Hello world</span>");
            });
            var contentDiv = cut.Find(".m-badge__badge");

            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }

        [TestMethod]
        public void RenderBadgeWithValue()
        {
            //Act
            var cut = RenderComponent<MBadge>(props =>
            {
                props.Add(badge => badge.Value, true);
            });
            var classes = cut.Instance.GetClass();
            var hasValueClass = !classes.Contains(".m-badge__badge");

            // Assert
            Assert.IsTrue(hasValueClass);
        }
    }
}
