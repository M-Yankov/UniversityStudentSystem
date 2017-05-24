using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;

namespace UniversityStudentSystem.Web.Helpers
{
    public static class CheckboxHelper
    {
        public static MvcHtmlString CheckboxCustom(this HtmlHelper htmlHelper, bool isChecked, string id, string value = null, string name = null, IDictionary<string, string> htmlAttributes = null)
        {
            TagBuilder checkboxTagBuilder = new TagBuilder("input");
            checkboxTagBuilder.MergeAttribute("type", "checkbox", true);

            if (htmlAttributes != null)
            {
                checkboxTagBuilder.MergeAttributes(htmlAttributes);
            }

            if (!string.IsNullOrEmpty(name))
            {
                checkboxTagBuilder.MergeAttribute("name", name, true);
            }

            if (!string.IsNullOrEmpty(value))
            {
                checkboxTagBuilder.MergeAttribute("value", value, true);
            }

            if (isChecked)
            {
                checkboxTagBuilder.MergeAttribute("checked", "checked", true);
            }

            checkboxTagBuilder.MergeAttribute("id", id, true);

            return new MvcHtmlString(checkboxTagBuilder.ToString(TagRenderMode.SelfClosing));
        }
    }
}