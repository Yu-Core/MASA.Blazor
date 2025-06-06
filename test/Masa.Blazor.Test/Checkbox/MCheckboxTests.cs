﻿using Bunit;

namespace Masa.Blazor.Test.Checkbox
{
    [TestClass]
    public class MCheckboxTests : TestBase
    {
        [TestMethod]
        public void RenderCheckboxWithDark()
        {
            //Act
            var cut = RenderComponent<MCheckbox<bool>>(props =>
            {
                props.Add(checkbox => checkbox.Dark, true);
            });
            var classes = cut.Instance.GetClass();
            var hasDarkClass = classes.Contains("theme--dark");

            // Assert
            Assert.IsTrue(hasDarkClass);
        }

        [TestMethod]
        public void RenderCheckboxWithDense()
        {
            //Act
            var cut = RenderComponent<MCheckbox<bool>>(props =>
            {
                props.Add(checkbox => checkbox.Dense, true);
            });
            var classes = cut.Instance.GetClass();
            var hasDenseClass = classes.Contains("m-input--dense");

            // Assert
            Assert.IsTrue(hasDenseClass);
        }

        [TestMethod]
        public void RenderCheckboxWithDisabled()
        {
            //Act
            var cut = RenderComponent<MCheckbox<bool>>(props =>
            {
                props.Add(breadcrumbs => breadcrumbs.Disabled, true);
            });
            var classes = cut.Instance.GetClass();
            var hasDisabledClass = classes.Contains("m-input--is-disabled");

            // Assert
            Assert.IsTrue(hasDisabledClass);
        }

        [TestMethod]
        public void RenderCheckboxWithError()
        {
            //Act
            var cut = RenderComponent<MCheckbox<bool>>(props =>
            {
                props.Add(checkbox => checkbox.Error, true);
            });
            var classes = cut.Instance.GetClass();
            var hasErrorClass = classes.Contains("m-input--checkbox");

            // Assert
            Assert.IsTrue(hasErrorClass);
        }

        [TestMethod]
        public void RenderCheckboxWithErrorCount()
        {
            //Act
            var cut = RenderComponent<MCheckbox<bool>>(props =>
            {
                props.Add(alert => alert.ErrorCount, 1);
            });
            var classes = cut.Instance.GetClass();
            var hasErrorCountClass = classes.Contains("m-input--checkbox");

            // Assert
            Assert.IsTrue(hasErrorCountClass);
        }

        [TestMethod]
        public void RenderCheckboxWithHideDetails()
        {
            //Act
            var cut = RenderComponent<MCheckbox<bool>>(props =>
            {
                props.Add(checkbox => checkbox.HideDetails, true);
            });
            var classes = cut.Instance.GetClass();
            var hasHideDetailsClass = classes.Contains("m-input--checkbox");

            // Assert
            Assert.IsTrue(hasHideDetailsClass);
        }

        [TestMethod]
        public void RenderCheckboxWithIndeterminate()
        {
            //Act
            var cut = RenderComponent<MCheckbox<bool>>(props =>
            {
                props.Add(checkbox => checkbox.Indeterminate, true);
            });
            var classes = cut.Instance.GetClass();
            var hasIndeterminateClass = classes.Contains("m-input--indeterminate");

            // Assert
            Assert.IsTrue(hasIndeterminateClass);
        }

        [TestMethod]
        public void RenderCheckboxWithPersistentHint()
        {
            //Act
            var cut = RenderComponent<MCheckbox<bool>>(props =>
            {
                props.Add(checkbox => checkbox.PersistentHint, true);
            });
            var classes = cut.Instance.GetClass();
            var hasIndeterminateClass = classes.Contains("m-input--checkbox");

            // Assert
            Assert.IsTrue(hasIndeterminateClass);
        }

        [TestMethod]
        public void RenderCheckboxWithReadonly()
        {
            //Act
            var cut = RenderComponent<MCheckbox<bool>>(props =>
            {
                props.Add(checkbox => checkbox.Readonly, true);
            });
            var classes = cut.Instance.GetClass();
            var hasReadonlyClass = classes.Contains("m-input--checkbox");

            // Assert
            Assert.IsTrue(hasReadonlyClass);
        }

        [TestMethod]
        public void RenderCheckboxWithRipple()
        {
            //Act
            var cut = RenderComponent<MCheckbox<bool>>(props =>
            {
                props.Add(checkbox => checkbox.Ripple, true);
            });
            var classes = cut.Instance.GetClass();
            var hasRippleClass = classes.Contains("m-input--checkbox");

            // Assert
            Assert.IsTrue(hasRippleClass);
        }

        [TestMethod]
        public void RenderCheckboxWithSuccess()
        {
            //Act
            var cut = RenderComponent<MCheckbox<bool>>(props =>
            {
                props.Add(checkbox => checkbox.Success, true);
            });
            var classes = cut.Instance.GetClass();
            var hasSuccessClass = classes.Contains("m-input--checkbox");

            // Assert
            Assert.IsTrue(hasSuccessClass);
        }

        [TestMethod]
        public void RenderCheckboxWithValidateOnBlur()
        {
            //Act
            var cut = RenderComponent<MCheckbox<bool>>(props =>
            {
                props.Add(checkbox => checkbox.ValidateOnBlur, true);
            });
            var classes = cut.Instance.GetClass();
            var hasValidateOnBlurClass = classes.Contains("m-input--checkbox");

            // Assert
            Assert.IsTrue(hasValidateOnBlurClass);
        }

        [TestMethod]
        public void RenderCheckboxWithValue()
        {
            //Act
            var cut = RenderComponent<MCheckbox<bool>>(props =>
            {
                props.Add(checkbox => checkbox.Value, true);
            });
            var classes = cut.Instance.GetClass();
            var hasValueClass = classes.Contains("m-input--checkbox");

            // Assert
            Assert.IsTrue(hasValueClass);
        }

        [TestMethod]
        public void RenderWithLabelContent()
        {
            // Arrange & Act
            var cut = RenderComponent<MCheckbox<bool>>(props =>
            {
                props.Add(checkbox => checkbox.LabelContent, "<span>Hello world</span>");
            });
            var contentDiv = cut.Find(".m-label");

            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }
    }
}
