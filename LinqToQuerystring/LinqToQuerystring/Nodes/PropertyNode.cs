﻿using System.Linq;
using System.Linq.Expressions;
using LinqToQuerystring.Nodes.Base;

namespace LinqToQuerystring.Nodes
{
    internal class PropertyNode : SingleNode
    {
        private readonly ParameterExpression _parameter;

        public PropertyNode(string value, ParameterExpression parameter) : base(value)
        {
            _parameter = parameter;
        }

        public override Expression CreateExpression()
        {
            return Value.Split("/").Aggregate<string, MemberExpression>(null,
                (propertyChainExpression, propertyName) =>
                    propertyChainExpression == null
                        ? Expression.Property(_parameter, propertyName)
                        : Expression.Property(propertyChainExpression, propertyName));
        }
    }
}
