﻿using Bunit;

namespace Masa.Blazor.Test.DataIterators
{
    [TestClass]
    public class MDataIteratorTests : TestBase
    {
        [TestMethod]
        public void RenderDataIteratorWithDisableFiltering()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MDataIterator<string>>(props =>
            {
                props.Add(dataiterator => dataiterator.DisableFiltering, true);
            });
            var classes = cut.Instance.GetClass();
            var hasDisableFilteringClass = classes.Contains("m-data-iterator");

            // Assert
            Assert.IsTrue(hasDisableFilteringClass);
        }

        [TestMethod]
        public void RenderDataIteratorWithDisablePagination()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MDataIterator<string>>(props =>
            {
                props.Add(dataiterator => dataiterator.DisablePagination, true);
            });
            var classes = cut.Instance.GetClass();
            var hasDisablePaginationClass = classes.Contains("m-data-iterator");

            // Assert
            Assert.IsTrue(hasDisablePaginationClass);
        }

        [TestMethod]
        public void RenderDataIteratorWithDisableSort()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MDataIterator<string>>(props =>
            {
                props.Add(dataiterator => dataiterator.DisableSort, true);
            });
            var classes = cut.Instance.GetClass();
            var hasDisableSortClass = classes.Contains("m-data-iterator");

            // Assert
            Assert.IsTrue(hasDisableSortClass);
        }

        [TestMethod]
        public void RenderDataIteratorWithHideDefaultFooter()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MDataIterator<string>>(props =>
            {
                props.Add(dataiterator => dataiterator.HideDefaultFooter, true);
            });
            var classes = cut.Instance.GetClass();
            var hasHideDefaultFooterClass = classes.Contains("m-data-iterator");

            // Assert
            Assert.IsTrue(hasHideDefaultFooterClass);
        }

        [TestMethod]
        public void RenderDataIteratorWithItemsPerPage()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MDataIterator<string>>(props =>
            {
                props.Add(dataiterator => dataiterator.ItemsPerPage, 10);
            });
            var classes = cut.Instance.GetClass();
            var hasItemsPerPageClass = classes.Contains("m-data-iterator");

            // Assert
            Assert.IsTrue(hasItemsPerPageClass);
        }

        [TestMethod]
        public void RenderDataIteratorWithLoading()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MDataIterator<string>>(props =>
            {
                props.Add(dataiterator => dataiterator.Loading, true);
            });
            var classes = cut.Instance.GetClass();
            var hasLoadingClass = classes.Contains("m-data-iterator");

            // Assert
            Assert.IsTrue(hasLoadingClass);
        }

        [TestMethod]
        public void RenderDataIteratorWithMultiSort()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MDataIterator<string>>(props =>
            {
                props.Add(dataiterator => dataiterator.MultiSort, true);
            });
            var classes = cut.Instance.GetClass();
            var hasMultiSortClass = classes.Contains("m-data-iterator");

            // Assert
            Assert.IsTrue(hasMultiSortClass);
        }

        [TestMethod]
        public void RenderDataIteratorWithMustSort()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MDataIterator<string>>(props =>
            {
                props.Add(dataiterator => dataiterator.MustSort, true);
            });
            var classes = cut.Instance.GetClass();
            var hasMustSortClass = classes.Contains("m-data-iterator");

            // Assert
            Assert.IsTrue(hasMustSortClass);
        }

        [TestMethod]
        public void RenderDataIteratorWithPage()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MDataIterator<string>>(props =>
            {
                props.Add(dataiterator => dataiterator.Page, 1);
            });
            var classes = cut.Instance.GetClass();
            var hasPageClass = classes.Contains("m-data-iterator");

            // Assert
            Assert.IsTrue(hasPageClass);
        }

        [TestMethod]
        public void RenderDataIteratorWithServerItemsLength()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MDataIterator<string>>(props =>
            {
                props.Add(dataiterator => dataiterator.ServerItemsLength, -1);
            });
            var classes = cut.Instance.GetClass();
            var hasServerItemsLengthClass = classes.Contains("m-data-iterator");

            // Assert
            Assert.IsTrue(hasServerItemsLengthClass);
        }

        [TestMethod]
        public void RenderDataIteratorWithSingleExpand()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MDataIterator<string>>(props =>
            {
                props.Add(dataiterator => dataiterator.SingleExpand, true);
            });
            var classes = cut.Instance.GetClass();
            var hasSingleExpandClass = classes.Contains("m-data-iterator");

            // Assert
            Assert.IsTrue(hasSingleExpandClass);
        }

        [TestMethod]
        public void RenderDataIteratorWithSingleSelect()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MDataIterator<string>>(props =>
            {
                props.Add(dataiterator => dataiterator.SingleSelect, true);
            });
            var classes = cut.Instance.GetClass();
            var hasSingleSelectClass = classes.Contains("m-data-iterator");

            // Assert
            Assert.IsTrue(hasSingleSelectClass);
        }

        [TestMethod]
        public void RenderDataIteratorWithSortDesc()
        {
            //Act
            JSInterop.Mode = JSRuntimeMode.Loose;
            var cut = RenderComponent<MDataIterator<string>>(props =>
            {
                props.Add(dataiterator => dataiterator.SortDesc, true);
            });
            var classes = cut.Instance.GetClass();
            var hasSortDescClass = classes.Contains("m-data-iterator");

            // Assert
            Assert.IsTrue(hasSortDescClass);
        }
    }
}
