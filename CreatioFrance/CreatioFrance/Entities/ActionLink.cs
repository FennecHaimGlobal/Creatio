using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace CreatioFrance.Entities
{
    public class ActionLink
    {
        /// <summary>
        /// Gets or sets the link text.
        /// </summary>
        /// <value>
        /// The link text.
        /// </value>
        public string linkText { get; set; }
        /// <summary>
        /// Gets or sets the name of the action.
        /// </summary>
        /// <value>
        /// The name of the action.
        /// </value>
        public string actionName { get; set; }
        /// <summary>
        /// Gets or sets the name of the controller.
        /// </summary>
        /// <value>
        /// The name of the controller.
        /// </value>
        public string controllerName { get; set; }

        public string area { get; set; }
        /// <summary>
        /// Gets or sets the route values.
        /// </summary>
        /// <value>
        /// The route values.
        /// </value>
        public RouteValueDictionary routeValues { get; set; }
        /// <summary>
        /// Gets or sets the HTML attributes.
        /// </summary>
        /// <value>
        /// The HTML attributes.
        /// </value>
        public IDictionary<string, Object> htmlAttributes { get; set; }

    }
}