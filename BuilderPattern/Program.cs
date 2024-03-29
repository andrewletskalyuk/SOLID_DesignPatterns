﻿using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace BuilderPattern
{
    internal class Program
    {
        public class HtmlElement
        {
            public string Name, Text;
            public List<HtmlElement> Elements = new List<HtmlElement>();
            private const int indexSize = 2;

            public HtmlElement()
            {
            }
            public HtmlElement(string name, string text)
            {
                Name = name ?? throw new ArgumentNullException("name");
                Text = text ?? throw new ArgumentNullException("text");
            }

            private string ToStringImpl(int indent)
            {
                var sb = new StringBuilder();
                var i = new string(' ', indexSize * indent);
                sb.AppendLine($"{i}<{Name}>");

                if (!string.IsNullOrWhiteSpace(Text))
                {
                    sb.Append(new string(' ', indexSize * indent + 1));
                    sb.AppendLine(Text);
                }

                foreach (var e in Elements)
                {
                    sb.Append(e.ToStringImpl(indent + 1));
                }

                sb.AppendLine($"{i}</{Name}>");
                return sb.ToString();
            }
            
            public override string ToString()
            {
                return ToStringImpl(0);
            }
        }

        public class HtmlBuilder
        {
            private readonly string rootName;
            private HtmlElement root = new HtmlElement();
            public HtmlBuilder(string rootName)
            {
                this.rootName = rootName;
                root.Name = rootName;
            }
            public void AddChild(string childName, string childText)
            {
                var e = new HtmlElement(childName, childText);
                root.Elements.Add(e);
            }
            public override string ToString()
            {
                return root.ToString();
            }

            public void Clear()
            {
                root = new HtmlElement { Name = rootName };
            }
        }
        static void Main(string[] args)
        {
            var builder = new HtmlBuilder("ul");
            builder.AddChild("li", "hello");
            builder.AddChild("li", "world");
            WriteLine(builder.ToString());
            //first commit
        }
    }
}
