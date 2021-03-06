﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Skclusive.Core.Component
{
    public class CssUtil
    {
        public static IEnumerable<Tuple<string, object>> ToStyles(string style)
        {
            if (string.IsNullOrWhiteSpace(style))
            return Enumerable.Empty<Tuple<string, object>>();

            return style.Split(";").Select(single => single.Split(":"))
            .Select(pair => Tuple.Create<string, object>(pair[0], pair[1]));
        }

        public static string ToStyle(IEnumerable<Tuple<string, object>> styles, string style = "")
        {
            var _style = string.Join(";", styles
                .Select(p => $"{p.Item1}:{p.Item2}"));

            if (!string.IsNullOrWhiteSpace(style))
            {
                if (!string.IsNullOrWhiteSpace(_style))
                {
                    _style = $"{_style};{style}";
                }
                else
                {
                    _style = style;
                }
            }
            return _style;
        }

        public static string ToClass(string selector, IEnumerable<string> classes, string suffix = "")
        {
            return ToClass(selector, null, classes, suffix);
        }

        public static string ToClass(string selector, string extendor, IEnumerable<string> classes, string suffix = "")
        {
            var extending = !string.IsNullOrWhiteSpace(extendor);

            var _class = string.Join(" ", classes.Select(key =>
            {
                if (key.StartsWith("~", StringComparison.Ordinal))
                {
                    return key.Substring(1);
                }

                var currentKey = $"{(key.Length > 0 ? "-" : "")}{key}";

                var current = $"{selector}{currentKey}";

                if (extending)
                {
                    current = $"{current} {extendor}{currentKey}";
                }

                return current;
            }));

            if (!string.IsNullOrWhiteSpace(suffix))
            {
                if (suffix.StartsWith("~", StringComparison.Ordinal))
                {
                    suffix = suffix.Substring(1);
                }

                if (!string.IsNullOrWhiteSpace(_class))
                {
                    _class = $"{_class} {suffix}";
                }
                else
                {
                    _class = suffix;
                }
            }
            return _class;
        }
    }
}
