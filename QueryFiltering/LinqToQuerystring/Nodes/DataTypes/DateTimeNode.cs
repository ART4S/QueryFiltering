using System;
using System.Linq.Expressions;
using LinqToQuerystring.Nodes.Base;

namespace LinqToQuerystring.Nodes.DataTypes
{
    internal class DateTimeNode : SingleNode
    {
        public DateTimeNode(string value) : base(value)
        {
        }

        public override Expression CreateExpression()
        {
            return Expression.Constant(DateTime.Parse(Value.Replace("datetime","").Trim('\'')));
        }
    }
}
