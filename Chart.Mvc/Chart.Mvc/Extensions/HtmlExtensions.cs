﻿using System.Text;
using System.Web.Mvc;
using Chart.Mvc.ComplexChart;
using Chart.Mvc.SimpleChart;

namespace Chart.Mvc.Extensions
{
    /// <summary>
    /// The html extensions.
    /// </summary>
    public static class HtmlExtensions
    {
        /// <summary>
        /// The create chart.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="canvasId">
        /// The canvas id.
        /// </param>
        /// <param name="complexChart">
        /// The complex chart.
        /// </param>
        /// <returns>
        /// The <see cref="MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString CreateChart<TComplexChartOptions>(this HtmlHelper htmlHelper, string canvasId, ComplexChartBase<TComplexChartOptions> complexChart) where TComplexChartOptions : ComplexChartOptions
        {
            return CreateChart(canvasId, complexChart.ChartType.ToString(), complexChart.ComplexData.ToJson(), complexChart.ChartConfiguration.ToJson());
        }

        private static MvcHtmlString CreateChart(string canvasId, string chartType, string jsonData, string jsonOptions)
        {
            var tag = new TagBuilder("script");
            tag.Attributes.Add("type", "text/javascript");
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("var ctx = document.getElementById(\"{0}\").getContext(\"2d\");", canvasId);
            stringBuilder.AppendFormat("var data = JSON.parse('{0}');", jsonData);
            stringBuilder.AppendFormat("var options = JSON.parse('{0}');", jsonOptions);
            stringBuilder.Append("var " + canvasId + "_chart = new Chart(ctx, { type:'" + chartType.ToCamelCase() + "', data: data, options: options });");
            tag.InnerHtml = stringBuilder.ToString();
            return new MvcHtmlString(tag.ToString());
        }
    }
}
