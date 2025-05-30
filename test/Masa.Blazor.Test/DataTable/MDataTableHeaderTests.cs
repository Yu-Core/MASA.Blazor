﻿using Masa.Blazor.Components.DataTable;

namespace Masa.Blazor.Test.DataTable
{
    [TestClass]
    public class MDataTableHeaderTests : TestBase
    {
        [TestMethod]
        public void RenderDataTableHeaderWithDisableSort()
        {
            //Act
            var cut = RenderComponent<MDataTableHeader>(props =>
            {
                props.Add(datatableheader => datatableheader.DisableSort, true);
            });
            var classes = cut.Instance.GetClass();
            var hasDisableSortClass = classes.Contains("m-data-table-header");

            // Assert
            Assert.IsTrue(hasDisableSortClass);
        }

        [TestMethod]
        public void RenderDataTableHeaderWithEveryItem()
        {
            //Act
            var cut = RenderComponent<MDataTableHeader>(props =>
            {
                props.Add(datatableheader => datatableheader.EveryItem, true);
            });
            var classes = cut.Instance.GetClass();
            var hasEveryItemClass = classes.Contains("m-data-table-header");

            // Assert
            Assert.IsTrue(hasEveryItemClass);
        }

        [TestMethod]
        public void RenderDataTableHeaderWithShowGroupBy()
        {
            //Act
            var cut = RenderComponent<MDataTableHeader>(props =>
            {
                props.Add(datatableheader => datatableheader.ShowGroupBy, true);
            });
            var classes = cut.Instance.GetClass();
            var hasShowGroupByClass = classes.Contains("m-data-table-header");

            // Assert
            Assert.IsTrue(hasShowGroupByClass);
        }

        [TestMethod]
        public void RenderDataTableHeaderWithSingleSelect()
        {
            //Act
            var cut = RenderComponent<MDataTableHeader>(props =>
            {
                props.Add(datatableheader => datatableheader.SingleSelect, true);
            });
            var classes = cut.Instance.GetClass();
            var hasSingleSelectClass = classes.Contains("m-data-table-header");

            // Assert
            Assert.IsTrue(hasSingleSelectClass);
        }

        [TestMethod]
        public void RenderDataTableHeaderWithSomeItems()
        {
            //Act
            var cut = RenderComponent<MDataTableHeader>(props =>
            {
                props.Add(datatableheader => datatableheader.SomeItems, true);
            });
            var classes = cut.Instance.GetClass();
            var hasSomeItemsClass = classes.Contains("m-data-table-header");

            // Assert
            Assert.IsTrue(hasSomeItemsClass);
        }
    }
}
