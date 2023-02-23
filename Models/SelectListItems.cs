﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SelectListUtilities
{
    public class SelectListItems<T>
    {
        public static SelectList Convert(IEnumerable<T> collection, string defaultText = "")
        {
            List<SelectListItem> items = new List<SelectListItem>();
            if (typeof(T).Name == "String")
            {
                int index = 0;
                foreach (T item in collection)
                {
                    items.Add(new SelectListItem() { Value = index.ToString(), Text = item.ToString() });
                    index++;
                }
            }
            else
            {
                foreach (T item in collection)
                {
                    items.Add(
                        new SelectListItem()
                        {
                            Value = typeof(T).GetProperty("Id").GetValue(item, null).ToString(),
                            Text = typeof(T).GetProperty("Name").GetValue(item, null).ToString()
                        });
                }
            }
            if (defaultText != "")
                items.Insert(0, new SelectListItem { Value = "0", Text = defaultText });
            return new SelectList(items, "Value", "Text", 0, new[] { 0 });
        }
    }

}