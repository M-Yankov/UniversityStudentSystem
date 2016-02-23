namespace UniversityStudentSystem.Web.Areas.Admin.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web;
    using System.Web.Mvc;
    using Kendo.Mvc.UI;
    using Kendo.Mvc.UI.Fluent;

    public static class GridHelper
    {
        public static GridBuilder<T> FullFeaturedGrid<T>(
            this HtmlHelper helper,
            string controllerName, 
            Expression<Func<T, object>> modelIdExpression, 
            Action<GridColumnFactory<T>> columns = null) where T : class
        {
            if (columns == null)
            {
                columns = cols =>
                {
                    cols.AutoGenerate(true);
                    cols.Command(c => c.Destroy());
                };
            }

            return helper.Kendo()
                .Grid<T>()
                .Name("grid")
                .Columns(columns)
                .ColumnMenu()
                .Pageable(page => page.Refresh(true))
                .Sortable()
                .Groupable()
                .Filterable()
                .Editable(c => c.Mode(GridEditMode.PopUp))
                .DataSource(data =>
                    data
                        .Ajax()
                        .Model(m => m.Id(modelIdExpression))
                        .Read(read => read.Action("Read", controllerName))
                        .Update(update => update.Action("Update", controllerName))
                        .Destroy(destroy => destroy.Action("Destroy", controllerName))
                        );
        }
    }
}