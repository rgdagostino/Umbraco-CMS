﻿using Newtonsoft.Json;
using Umbraco.Core.PropertyEditors;

namespace Umbraco.Web.PropertyEditors
{
    /// <summary>
    /// Represents the configuration for the listview value editor.
    /// </summary>
    public class ListViewConfiguration
    {
        [ConfigurationField("pageSize", "Page Size", "number", Description = "Number of items per page")]
        public int PageSize { get; set; }
        
        [ConfigurationField("displayAtTabNumber", "Display At Tab Number", "number", Description = "Which tab position that the list of child items will be displayed")]
        public int DisplayAtTabNumber { get; set; }

        [ConfigurationField("orderBy", "Order By", "views/propertyeditors/listview/sortby.prevalues.html",
            Description = "The default sort order for the list")]
        public string OrderBy { get; set; }

        [ConfigurationField("orderDirection", "Order Direction", "views/propertyeditors/listview/orderdirection.prevalues.html")]
        public string OrderDirection { get; set; }

        [ConfigurationField("includeProperties", "Columns Displayed", "views/propertyeditors/listview/includeproperties.prevalues.html",
            Description = "The properties that will be displayed for each column")]
        public Property[] IncludeProperties { get; set; }

        [ConfigurationField("layouts", "Layouts", "views/propertyeditors/listview/layouts.prevalues.html")]
        public Layout[] Layouts { get; set; }

        [ConfigurationField("bulkActionPermissions", "Bulk Action Permissions", "views/propertyeditors/listview/bulkactionpermissions.prevalues.html",
            Description = "The bulk actions that are allowed from the list view")]
        public BulkActionPermissionSettings BulkActionPermissions { get; set; } = new BulkActionPermissionSettings(); // fixme managing defaults?

        [ConfigurationField("tabName", "Tab Name", "textstring", Description = "The name of the listview tab (default if empty: 'Child Items')")]
        public string TabName { get; set; }

        public class Property
        {
            [JsonProperty("alias")]
            public string Alias { get; set; }

            [JsonProperty("header")]
            public string Header { get; set; }

            [JsonProperty("isSystem")]
            public int IsSystem { get; set; } // fixme bool
        }

        public class Layout
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("path")]
            public string Path { get; set; }

            [JsonProperty("icon")]
            public string Icon { get; set; }

            [JsonProperty("isSystem")]
            public int IsSystem { get; set; } // fixme bool

            [JsonProperty("selected")]
            public bool Selected { get; set; }
        }

        public class BulkActionPermissionSettings
        {
            [JsonProperty("allowBulkPublish")]
            public bool AllowBulkPublish { get; set; } = true;

            [JsonProperty("allowBulkUnpublish")]
            public bool AllowBulkUnpublish { get; set; } = true;

            [JsonProperty("allowBulkCopy")]
            public bool AllowBulkCopy { get; set; } = true;

            [JsonProperty("allowBulkMove")]
            public bool AllowBulkMove { get; set; } = true;

            [JsonProperty("allowBulkDelete")]
            public bool AllowBulkDelete { get; set; } = true;
        }
    }
}
