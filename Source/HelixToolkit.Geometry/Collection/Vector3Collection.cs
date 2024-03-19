﻿using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace HelixToolkit;

[Serializable]
[TypeConverter(typeof(Vector3CollectionConverter))]
public sealed class Vector3Collection : FastList<Vector3>
{
    public Vector3Collection()
    {
    }

    public Vector3Collection(int capacity)
        : base(capacity)
    {
    }

    public Vector3Collection(IEnumerable<Vector3> items)
        : base(items)
    {
    }

    public static Vector3Collection Parse(string source)
    {
        IFormatProvider formatProvider = CultureInfo.InvariantCulture;

        var th = new TokenizerHelper(source, formatProvider);
        var resource = new Vector3Collection();

        Vector3 value;

        while (th.NextToken())
        {
            value = new Vector3(
                Convert.ToSingle(th.GetCurrentToken(), formatProvider),
                Convert.ToSingle(th.NextTokenRequired(), formatProvider),
                Convert.ToSingle(th.NextTokenRequired(), formatProvider));

            resource.Add(value);
        }

        return resource;
    }

    public string ConvertToString(string? format, IFormatProvider? provider)
    {
        if (this.Count == 0)
        {
            return string.Empty;
        }

        var str = new StringBuilder();
        for (var i = 0; i < this.Count; i++)
        {
            //str.AppendFormat(provider, "{0:" + format + "}", this[i]);
            str.AppendFormat(provider, "{0},{1},{2}", this[i].X, this[i].Y, this[i].Z);
            if (i != this.Count - 1)
            {
                str.Append(' ');
            }
        }

        return str.ToString();
    }
}
